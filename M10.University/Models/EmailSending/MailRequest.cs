using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudyJournal.University.Models
{
    public class MailRequest
    {
        public string NameTo { get; set; }
        public string EmailTo { get; set; }
        public string Subject { get; set; } = "Attandance warning";
        public string Message { get; set; }

    }
}
