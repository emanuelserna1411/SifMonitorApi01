using Sif;
using Sif.Data;
using Sif.Services;

namespace SifMonitorApi01.Services
{
    public class TopNumTransations : DataService
    {
        

        public TopNumTransations(DataDict dataDictionary) : base(dataDictionary)
        {
        }

        protected override ServiceState Process()
        {
            ServiceState state;
            using (SifDBCommand NumTransactionsCommand = DBFactory.DefaultFactory.NewDBCommand(fTopNumTransactions, this.Connection))
            {
                state = ServiceState.Accepted;
            }
            return state;
        }
        private string fTopNumTransactions;

        public String GetScripInsert()
        {
            return @"WITH transacciones AS (
                        SELECT
                        J.TELLER_ID,
                        COUNT(*) AS N_TRANSACTIONS
                        FROM
                        SIF.JOURNAL J
                        WHERE
                        J.DATE_SETTLEMENT = " +/* WorkDate +*/
                    @"GROUP BY
                        J.TELLER_ID
                    ), usuarios_activos AS (
                        SELECT
                        UA.USER_ID,
                        UA.ID,
                        UA.SESSION_TIME,
                        UA.INACTIVITY_TIME,
                        T.N_TRANSACTIONS
                        FROM
                        SIF.USER_ACTIVITY UA
                        LEFT JOIN
                        transacciones T ON UA.USER_ID = TO_CHAR(T.TELLER_ID)
                        WHERE
                        T.N_TRANSACTIONS IS NOT NULL
                    )
                    SELECT
                    UA.*,
                    UE.EVENT,
                    UA.N_TRANSACTIONS
                    FROM
                    usuarios_activos UA
                    INNER JOIN
                    SIF.USER_EVENTS UE ON UA.ID = UE.ID
                    ORDER BY
                    UA.N_TRANSACTIONS DESC;";
        }
    }


}
