using System;
using System.Collections.Generic;
using StudyJournal.DataBase.Entities;

namespace StudyJournal.DataBase
{
    public interface ILectureRepository
    {
        IEnumerable<Lecture> GetAllLectures();
        Lecture GetLectureByID(int lectureId);
        void InsertLecture(Lecture lecture);
        void DeleteLecture(int lectureId);
        void UpdateLecture(Lecture lecture);
    }
}
