using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Airport.Domain.Models
{
    class Airport
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Country { get; set; }
        public string City { get; set; }
        public string Street { get; set; }
        public string CodeIATA { get; set; }

        public virtual IEnumerable<Airplane> Airplanes { get; set; }
        public virtual IEnumerable<Flight> Origins { get; set; }
        public virtual IEnumerable<Flight> Destinations { get; set; }
    }
}
