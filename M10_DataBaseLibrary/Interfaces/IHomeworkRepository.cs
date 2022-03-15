using System;
using System.Collections.Generic;
using StudyJournal.DataBase.Entities;

namespace StudyJournal.DataBase
{
    public interface IHomeworkRepository
    {
        IEnumerable<Homework> GetAllHomeworks();
        Homework GetHomeworkByID(int homeworkId);
        void InsertHomework(Homework homework);
        void DeleteHomework(int homeworkId);
        void UpdateHomework(Homework homework);
    }
}
