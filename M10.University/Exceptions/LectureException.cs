using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudyJournal.University.Exceptions
{
    public class LectureException : CustomException
    {
        public LectureException(string message)
        : base(message) { }
    }
}
