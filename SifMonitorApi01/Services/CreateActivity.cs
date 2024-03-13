using Sif;
using Sif.Data;
using Sif.Services;
using SifMonitorApi01.Entities;

using System.Data.Common;
using System.Runtime.CompilerServices;

namespace SifMonitorApi01.Services
{
    public class CreateActivity : DataService
    {

        private static UserActivity Activity;
        public CreateActivity(DataDict dataDictionary, UserActivity activity) : base(dataDictionary)
        {
            Activity = activity;
        }


        protected override ServiceState Process()
        {
            ServiceState state;
            fUserActivityData = GetScripInsert();
            using (SifDBCommand userActivityCommand = DBFactory.DefaultFactory.NewDBCommand(fUserActivityData, this.Connection))
            {
                userActivityCommand.ExecuteNonQuery(this.Message);
                state = ServiceState.Accepted;
            }
            return state;
        }

        private static String fUserActivityData;

        public static String GetScripInsert()
        {
            return "INSERT INTO SIF.USER_ACTIVITY (USER_ID, BRANCH_ID, FECHA, SESSION_TIME, INACTIVITY_TIME) VALUES ('" + Activity.UserId + "'," + Activity.BranchId + ", TO_DATE('" + Activity.Fecha + "', 'YYYY-MM-DD'), " + Activity.SessionTime + ", " + Activity.InactivityTime + ")";
        }
        

      
            
    }
}
