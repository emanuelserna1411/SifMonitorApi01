using Sif;
using Sif.Data;
using Sif.Services;
using SifMonitorApi01.Helpers;

namespace SifMonitorApi01.Services
{
    public class TopInactivity : DataService
    {
        public static String Response;
        private static TopUsersInactivity TopUsersInactivity;
        public TopInactivity(DataDict dataDictionary, TopUsersInactivity topInativity) : base(dataDictionary)
        {
            TopUsersInactivity = topInativity;
        }

        protected override ServiceState Process()
        {
            ServiceState state;
            fTopInactivityUsers = GetScriptTopInactivity();
            using (SifDBCommand topInactivityCommand = DBFactory.DefaultFactory.NewDBCommand(fTopInactivityUsers, this.Connection))
            {
                var result = topInactivityCommand.ExecuteReader(this.Message);
                Response = InactivityHelper.TopInactivityResponse(result);
                state = ServiceState.Accepted;
            }
            return state;
        }

        private static String fTopInactivityUsers;

        public static String GetScriptTopInactivity()
        {
            return "SELECT * FROM SIF.USER_ACTIVITY WHERE FECHA = TO_DATE('"+TopUsersInactivity.Fecha+"','YYYY-MM-DD') ORDER BY INACTIVITY_TIME DESC FETCH FIRST "+ TopUsersInactivity.Top +" ROWS ONLY";
        }
    }
}
