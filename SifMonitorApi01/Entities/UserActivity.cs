namespace SifMonitorApi01.Entities
{
    public class UserActivity
    {
        public String UserId { get; set; }
        public Int32 BranchId { get; set; }
        public String Fecha { get; set; }
        public Double SessionTime { get; set; }
        public Double InactivityTime { get; set; }
        public String Events{ get; set; }

        public UserActivity()
        {
            
        }

        public UserActivity(string userId, int branchId, string fecha, double sessionTime, double inactivityTime)
        {
            UserId = userId;
            BranchId = branchId;
            Fecha = fecha;
            SessionTime = sessionTime;
            InactivityTime = inactivityTime;
        }
    }
}
