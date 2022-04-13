using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;


namespace Calendar
{
    public class Event
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public TimeSpan Beggining { get; set; }
        public TimeSpan End { get; set; }
        public bool Done { get; set; }
        [ForeignKey("Day")]
        public DateTime DayId { get; set; }

        public virtual Day Day { get; set; }
    }
}
