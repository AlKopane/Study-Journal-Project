using System;
using System.Collections.Generic;
using StudyJournal.DataBase.Entities;

namespace StudyJournal.DataBase
{
    public interface IJournalRepository
    {
        public void UpdateMark(Mark mark);
        public void CreateDefaultMarks(int lectureId, int studyGroupId);
        public double CheckAverageMark(int studentId);
        public bool ChekAttending(int studentId);
        public Mark GetMarkByID(int id); 
    }
}
