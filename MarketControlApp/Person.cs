using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketControlApp
{
    public class Person
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public DateTime ArrivalDate { get; set; }
        public DateTime? ExitDate { get; set; }
        public string DocumentNumber { get; set; }
        public string VisitPurpose { get; set; }

        public Person()
        {
            Id = Guid.NewGuid();
        }
    }
}
