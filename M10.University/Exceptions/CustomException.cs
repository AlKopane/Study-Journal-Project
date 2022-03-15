using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudyJournal.University.Exceptions
{
    public abstract class CustomException : Exception
    {
        public CustomException(string message)
       : base(message) { }
    }
}
