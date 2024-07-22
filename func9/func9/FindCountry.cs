using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace func9
{
    internal class FindCountry
    {
        public FindCountry(string country) => _country = country;

        private readonly string _country;

        public bool FindCountryPredicate(Racer racer) => racer?.Country == _country;

    }
}
