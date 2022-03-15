using System;
using System.Collections.Generic;
using StudyJournal.DataBase.Entities;

namespace StudyJournal.DataBase
{
    public interface IStudentRepository
    {
        IEnumerable<Student> GetAllStudents();
        Task<IEnumerable<Student>> GetAllStudentsAsync();
        Student GetStudentByID(int studentId);
        void InsertStudent(Student student);
        void DeleteStudent(int studentId);
        void UpdateStudent(Student student);
    }
}
