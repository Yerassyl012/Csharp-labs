using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace rx13.Messages
{
    public class AddMessage
    {
        public AddMessage(double term1, double term2)
        {
            Term1 = term1;
            Term2 = term2;
        }

        public double Term1;
        public double Term2;
    }
}
