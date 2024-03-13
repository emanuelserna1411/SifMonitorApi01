using Newtonsoft.Json;
using SifMonitorApi01.Entities;
using System.Data;
using System.Data.Common;

namespace SifMonitorApi01.Helpers
{
    public class InactivityHelper
    {
        private static List<UserActivity> InactivityList = new List<UserActivity>();

        public static String TopInactivityResponse(DbDataReader table) 
        {
            while(table.Read())
            {
                UserActivity activity = new UserActivity(table.GetString(1), table.GetInt32(2), table.GetString(3), table.GetDouble(4), table.GetDouble(5));
                InactivityList.Add(activity);
            }
            String result = JsonConvert.SerializeObject(InactivityList);
            return result;
        }

    }
}
