using System;
using System.Threading.Tasks;

namespace UserManegement.Models
{
    public class WorkTimeRecord
    {
        public int RecordId { get; set; }
        public int UserId { get; set; }
        public int TaskId { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime? EndTime { get; set; }
        public int BreakMinutes { get; set; }
        public DateTime WorkDate { get; set; }
        public string Comments { get; set; }

        public virtual Task Task { get; set; }
    }

}
