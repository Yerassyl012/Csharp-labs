using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace rx13.Messages
{
    public class AnswerMessage
    {
        public AnswerMessage(double value)
        {
            Value = value;
        }

        public double Value;
    }
}
