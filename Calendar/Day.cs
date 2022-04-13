using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;


namespace Calendar
{
    public class Day
    {
        
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public DateTime Id { get; set; }
        public int Mood { get; set; }

        //public virtual ICollection<Event> Events { get; set; }
    }
}
