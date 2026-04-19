using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UserManegement.Models
{
    public class TimeEntry
    {
        public int EntryId { get; set; }
        public DateTime Date { get; set; }
        public string Project { get; set; }
        public double Hours { get; set; }
        public string Description { get; set; }
    }
}
