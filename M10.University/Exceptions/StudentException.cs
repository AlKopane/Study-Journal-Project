using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudyJournal.University.Exceptions
{
    public class StudentException : CustomException
    {
        public StudentException(string message)
        : base(message) { }
    }
}
