using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudyJournal.University.Exceptions
{
    public class JournalException : CustomException
    {
        public JournalException(string message)
        : base(message) { }
    }
}
