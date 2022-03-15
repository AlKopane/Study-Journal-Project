using System;
using System.Linq;
using StudyJournal.DataBase.Entities;

namespace StudyJournal.DataBase.Data
{
    public class DbInitializer
    {
        public static void Initialize(DataBaseContext context)
        {
            context.Database.EnsureDeleted();
            context.Database.EnsureCreated();

            if (context.Students.Any())
            {
                return;
            }


            var subjects = new Subject[]
            {
                    new Subject { SubjectName ="Maths"},
                    new Subject { SubjectName ="Geometry"},
                    new Subject { SubjectName ="Phisics"},
            };
            foreach (Subject s in subjects)
            {
                context.Subjects.Add(s);
            }
            context.SaveChanges();

            var studyGroups = new StudyGroup[]
           {
                new StudyGroup { StudyGroupName = "A-101"},
                new StudyGroup { StudyGroupName = "B-102"},
                new StudyGroup { StudyGroupName = "DF-432"},
           };
            foreach (StudyGroup s in studyGroups)
            {
                context.StudyGroups.Add(s);
            }
            context.SaveChanges();

            var speakers = new Speaker[]
{
                new Speaker{ SpeakerName="Pavel", SpeakerSurname="Petrov", SpeakerEmail = "p.petrov@lecturer.com"},
                new Speaker{ SpeakerName="Anton", SpeakerSurname="Dolin", SpeakerEmail = "a.dolin@lecturer.com"},
                new Speaker{ SpeakerName="Arsen", SpeakerSurname="Mesropyan", SpeakerEmail = "a.mesropyan@lecturer.com"}
};
            foreach (Speaker s in speakers)
            {
                context.Speakers.Add(s);
            }
            context.SaveChanges();

            var lectures = new Lecture[]
            {
                //1
                new Lecture { LectureName = "Math. Lecture N1", LectureDescription = "Description of the lecture N1 (maths).", LectureDate = new DateTime(2020, 10, 10, 10, 0, 0), StudyGroupID = 1, SpeakerID = 1, SubjectID = 1},
                new Lecture { LectureName = "Phisics. Lecture N1", LectureDescription = "Description of the lecture N1 (phisics).", LectureDate = new DateTime(2020, 10, 10, 11, 0, 0), StudyGroupID = 3, SpeakerID = 3, SubjectID = 3},
                new Lecture { LectureName = "Geometry. Lecture N1", LectureDescription = "Description of the lecture N1 (geometry).", LectureDate = new DateTime(2020, 10, 11, 12, 0, 0), StudyGroupID = 2, SpeakerID = 2, SubjectID = 2},
                   
                //2
                new Lecture { LectureName = "Math. Lecture N2", LectureDescription = "Description of the lecture N2 (maths).", LectureDate = new DateTime(2020, 10, 15, 10, 0, 0), StudyGroupID = 1, SpeakerID = 1, SubjectID = 1},
                new Lecture { LectureName = "Phisics. Lecture N2", LectureDescription = "Description of the lecture N2 (phisics).", LectureDate = new DateTime(2020, 10, 15, 11, 0, 0), StudyGroupID = 3, SpeakerID = 3, SubjectID = 3},
                new Lecture { LectureName = "Geometry. Lecture N2", LectureDescription = "Description of the lecture N2 (geometry).", LectureDate = new DateTime(2020, 10, 16, 12, 0, 0), StudyGroupID = 2, SpeakerID = 2, SubjectID = 2},

                //3
                new Lecture { LectureName = "Math. Lecture N3", LectureDescription = "Description of the lecture N3 (maths).", LectureDate = new DateTime(2020, 10, 20, 10, 0, 0), StudyGroupID = 1, SpeakerID = 1, SubjectID = 1},
                new Lecture { LectureName = "Phisics. Lecture N3", LectureDescription = "Description of the lecture N3 (phisics).", LectureDate = new DateTime(2020, 10, 20, 11, 0, 0), StudyGroupID = 3, SpeakerID = 3, SubjectID = 3},
                new Lecture { LectureName = "Geometry. Lecture N3", LectureDescription = "Description of the lecture N3 (geometry).", LectureDate = new DateTime(2020, 10, 21, 12, 0, 0), StudyGroupID = 2, SpeakerID = 2, SubjectID = 2},

                //4
                new Lecture { LectureName = "Math. Lecture N4", LectureDescription = "Description of the lecture N4 (maths).", LectureDate = new DateTime(2020, 10, 30, 10, 0, 0), StudyGroupID = 1, SpeakerID = 1, SubjectID = 1},
                new Lecture { LectureName = "Phisics. Lecture N4", LectureDescription = "Description of the lecture N4 (phisics).", LectureDate = new DateTime(2020, 10, 30, 11, 0, 0), StudyGroupID = 3, SpeakerID = 3, SubjectID = 3},
                new Lecture { LectureName = "Geometry. Lecture N4", LectureDescription = "Description of the lecture N4 (geometry).", LectureDate = new DateTime(2020, 11, 10, 12, 0, 0), StudyGroupID = 2, SpeakerID = 2, SubjectID = 2},

                //5
                new Lecture { LectureName = "Math. Lecture N5", LectureDescription = "Description of the lecture N5 (maths).", LectureDate = new DateTime(2020, 11, 10, 10, 0, 0), StudyGroupID = 1, SpeakerID = 1, SubjectID = 1},
                new Lecture { LectureName = "Phisics. Lecture N5", LectureDescription = "Description of the lecture N5 (phisics).", LectureDate = new DateTime(2020, 11, 10, 11, 0, 0), StudyGroupID = 3, SpeakerID = 3, SubjectID = 3},
                new Lecture { LectureName = "Geometry. Lecture N5", LectureDescription = "Description of the lecture N5 (geometry).", LectureDate = new DateTime(2020, 11, 30, 12, 0, 0), StudyGroupID = 2, SpeakerID = 2, SubjectID = 2},

                //6
                new Lecture { LectureName = "Math. Lecture N6", LectureDescription = "Description of the lecture N6 (maths).", LectureDate = new DateTime(2020, 11, 10, 10, 0, 0), StudyGroupID = 1, SpeakerID = 1, SubjectID = 1},
                new Lecture { LectureName = "Phisics. Lecture N6", LectureDescription = "Description of the lecture N6 (phisics).", LectureDate = new DateTime(2020, 11, 14, 11, 0, 0), StudyGroupID = 3, SpeakerID = 3, SubjectID = 3},
                new Lecture { LectureName = "Geometry. Lecture N6", LectureDescription = "Description of the lecture N6 (geometry).", LectureDate = new DateTime(2020, 12, 15, 12, 0, 0), StudyGroupID = 2, SpeakerID = 2, SubjectID = 2},

                //7
                new Lecture { LectureName = "Math. Lecture N7", LectureDescription = "Description of the lecture N7 (maths).", LectureDate = new DateTime(2020, 11, 11, 10, 0, 0), StudyGroupID = 1, SpeakerID = 1, SubjectID = 1},
                new Lecture { LectureName = "Phisics. Lecture N7", LectureDescription = "Description of the lecture N7 (phisics).", LectureDate = new DateTime(2020, 11, 18, 11, 0, 0), StudyGroupID = 3, SpeakerID = 3, SubjectID = 3},
                new Lecture { LectureName = "Geometry. Lecture N7", LectureDescription = "Description of the lecture N7 (geometry).", LectureDate = new DateTime(2020, 12, 20, 12, 0, 0), StudyGroupID = 2, SpeakerID = 2, SubjectID = 2},

                //8
                new Lecture { LectureName = "Math. Lecture N8", LectureDescription = "Description of the lecture N8 (maths).", LectureDate = new DateTime(2020, 11, 13, 10, 0, 0), StudyGroupID = 1, SpeakerID = 1, SubjectID = 1},

            };
            foreach (Lecture s in lectures)
            {
                context.Lectures.Add(s);
            }
            context.SaveChanges();



            var homeworks = new Homework[]
            {
                new Homework { LectureID = 1, HomeworkName = "Homework: Maths, lecture 1.", HomeworkDescription = "Do something (maths, homework 1)."},
                new Homework { LectureID = 2, HomeworkName = "Homework: Phisics, lecture 1.", HomeworkDescription = "Do something (phisics, homework 1)."},
                new Homework { LectureID = 3, HomeworkName = "Homework: Geometry, lecture 1.", HomeworkDescription = "Do something (geometry, homework 1)."},

                new Homework { LectureID = 4, HomeworkName = "Homework: Maths, lecture 2.", HomeworkDescription = "Do something (maths, homework 2)."},
                new Homework { LectureID = 5, HomeworkName = "Homework: Phisics, lecture 2.", HomeworkDescription = "Do something (phisics, homework 2)."},
                new Homework { LectureID = 6, HomeworkName = "Homework: Geometry, lecture 2.", HomeworkDescription = "Do something (geometry, homework 2)."},

                new Homework { LectureID = 7, HomeworkName = "Homework: Maths, lecture 3.", HomeworkDescription = "Do something (maths, homework 3)."},
                new Homework { LectureID = 8, HomeworkName = "Homework: Phisics, lecture 3.", HomeworkDescription = "Do something (phisics, homework 3)."},
                new Homework { LectureID = 9, HomeworkName = "Homework: Geometry, lecture 3.", HomeworkDescription = "Do something (geometry, homework 3)."},

                new Homework { LectureID = 10, HomeworkName = "Homework: Maths, lecture 4.", HomeworkDescription = "Do something (maths, homework 4)."},
                new Homework { LectureID = 11, HomeworkName = "Homework: Phisics, lecture 4.", HomeworkDescription = "Do something (phisics, homework 4)."},
                new Homework { LectureID = 12, HomeworkName = "Homework: Geometry, lecture 4.", HomeworkDescription = "Do something (geometry, homework 4)."},

                new Homework { LectureID = 13, HomeworkName = "Homework: Maths, lecture 5.", HomeworkDescription = "Do something (maths, homework 5)."},
                new Homework { LectureID = 14, HomeworkName = "Homework: Phisics, lecture 5.", HomeworkDescription = "Do something (phisics, homework 5)."},
                new Homework { LectureID = 15, HomeworkName = "Homework: Geometry, lecture 5.", HomeworkDescription = "Do something (geometry, homework 5)."},

                new Homework { LectureID = 16, HomeworkName = "Homework: Maths, lecture 6.", HomeworkDescription = "Do something (maths, homework 6)."},
                new Homework { LectureID = 17, HomeworkName = "Homework: Phisics, lecture 6.", HomeworkDescription = "Do something (phisics, homework 6)."},
                new Homework { LectureID = 18, HomeworkName = "Homework: Geometry, lecture 6.", HomeworkDescription = "Do something (geometry, homework 6)."},

                new Homework { LectureID = 19, HomeworkName = "Homework: Maths, lecture 7.", HomeworkDescription = "Do something (maths, homework 7)."},
                new Homework { LectureID = 20, HomeworkName = "Homework: Phisics, lecture 7.", HomeworkDescription = "Do something (phisics, homework 7)."},
                new Homework { LectureID = 21, HomeworkName = "Homework: Geometry, lecture 7.", HomeworkDescription = "Do something (geometry, homework 7)."},

            };
            foreach (Homework s in homeworks)
            {
                context.Homeworks.Add(s);
            }
            context.SaveChanges();


            var students = new Student[]
{
                 new Student { StudentName="Andrey", StudentSurname = "Mashkov",
                     StudentEmail = "a.mashkov@test.com", StudentPhone = "8(100)200-30-40", StudyGroupID = 1},
                new Student { StudentName="Maxim", StudentSurname = "Volkov",
                    StudentEmail = "m.volkov@test.com", StudentPhone = "8(100)300-30-31", StudyGroupID = 1},
                new Student { StudentName="Arthur", StudentSurname = "Nazarov",
                    StudentEmail = "a.nazarov@test.com", StudentPhone = "8(100)600-30-04", StudyGroupID = 1},
                new Student { StudentName="Marina", StudentSurname = "Kuznetzova",
                    StudentEmail = "a.kuznetzova@test.com", StudentPhone = "8(100)700-30-95", StudyGroupID = 1},
                new Student { StudentName="Vladislav", StudentSurname = "Leonov",
                    StudentEmail = "v.leonov@test.com", StudentPhone = "8(100)900-30-77", StudyGroupID = 1},
                new Student { StudentName="Sergey", StudentSurname = "Mashkov",
                    StudentEmail = "s.mashkov@test.com", StudentPhone = "8(100)000-30-68", StudyGroupID = 1},
                new Student { StudentName="Marina", StudentSurname = "Komarova",
                    StudentEmail = "m.komarova@test.com", StudentPhone = "8(200)300-50-18", StudyGroupID = 2},
                new Student { StudentName="Elena", StudentSurname = "Sankina",
                    StudentEmail = "e.sankina@test.com", StudentPhone = "8(300)500-40-45", StudyGroupID = 2},
                new Student { StudentName="Maxim", StudentSurname = "Tarusov",
                    StudentEmail = "m.tarusov@test.com", StudentPhone = "8(400)600-20-62", StudyGroupID = 2},
                new Student { StudentName="Dmitiy", StudentSurname = "Usov",
                    StudentEmail = "d.usov@test.com", StudentPhone = "8(400)600-20-63", StudyGroupID = 2},
                new Student { StudentName="Anton", StudentSurname = "Mamikin",
                    StudentEmail = "a.mamikin@test.com", StudentPhone = "8(400)600-20-64", StudyGroupID = 2},
                new Student { StudentName="Aleksandr", StudentSurname = "Kopanev",
                    StudentEmail = "a.kopanev@test.com", StudentPhone = "8(400)600-20-65", StudyGroupID = 2},
                new Student { StudentName="Ksenia", StudentSurname = "Gubareva",
                    StudentEmail = "k.gubareva@test.com", StudentPhone = "8(400)600-20-66", StudyGroupID = 3},
                new Student { StudentName="Pavel", StudentSurname = "Yagunin",
                    StudentEmail = "p.yagunin@test.com", StudentPhone = "8(400)600-20-67", StudyGroupID = 3},
                new Student { StudentName="Elena", StudentSurname = "Denisova",
                    StudentEmail = "e.denisova@test.com", StudentPhone = "8(400)600-20-68", StudyGroupID = 3},
                new Student { StudentName="Galiya", StudentSurname = "Muhametdinova",
                    StudentEmail = "g.muhametdinova@test.com", StudentPhone = "8(400)600-20-69", StudyGroupID = 3},
                new Student { StudentName="Rinat", StudentSurname = "Nazarov",
                    StudentEmail = "r.nazarov@test.com", StudentPhone = "8(400)600-20-70", StudyGroupID = 3},
                new Student { StudentName="Nikita", StudentSurname = "Kontorshikov",
                    StudentEmail = "n.kontorshikiv@test.com", StudentPhone = "8(400)600-20-62", StudyGroupID = 3},

};
            foreach (Student s in students)
            {
                context.Students.Add(s);
            }
            context.SaveChanges();

            var marks = new Mark[]
             {
                //У студент c id=5 много пропусков. У id=4 - плохие оценки.

                //1 лекция
                new Mark { StudentID = 1, LectureID = 1, WasAtLecture = false, MarkValue = 5 },
                new Mark { StudentID = 2, LectureID = 1, WasAtLecture = true, MarkValue = 4 },
                new Mark { StudentID = 3, LectureID = 1, WasAtLecture = true, MarkValue = 5 },
                new Mark { StudentID = 4, LectureID = 1, WasAtLecture = true, MarkValue = 2 },
                new Mark { StudentID = 5, LectureID = 1, WasAtLecture = false, MarkValue = 0 },
                new Mark { StudentID = 6, LectureID = 1, WasAtLecture = true, MarkValue = 5 },

                new Mark { StudentID = 7,  LectureID = 2, WasAtLecture = true, MarkValue = 3 },
                new Mark { StudentID = 8,  LectureID = 2, WasAtLecture = true, MarkValue = 4 },
                new Mark { StudentID = 9,  LectureID = 2, WasAtLecture = true, MarkValue = 3 },
                new Mark { StudentID = 10, LectureID = 2, WasAtLecture = true, MarkValue = 5 },
                new Mark { StudentID = 11, LectureID = 2, WasAtLecture = true, MarkValue = 2 },
                new Mark { StudentID = 12, LectureID = 2, WasAtLecture = true, MarkValue = 5 },
                                           
                new Mark { StudentID = 13, LectureID = 3, WasAtLecture = true, MarkValue = 2 },
                new Mark { StudentID = 14, LectureID = 3, WasAtLecture = true, MarkValue = 4 },
                new Mark { StudentID = 15, LectureID = 3, WasAtLecture = false, MarkValue = 5 },
                new Mark { StudentID = 16, LectureID = 3, WasAtLecture = true, MarkValue = 4 },
                new Mark { StudentID = 17, LectureID = 3, WasAtLecture = true, MarkValue = 4 },
                new Mark { StudentID = 18, LectureID = 3, WasAtLecture = true, MarkValue = 3 },

                //2 лекция
                new Mark { StudentID = 1, LectureID = 4, WasAtLecture = true, MarkValue = 4 },
                new Mark { StudentID = 2, LectureID = 4, WasAtLecture = true, MarkValue = 5 },
                new Mark { StudentID = 3, LectureID = 4, WasAtLecture = false, MarkValue = 4 },
                new Mark { StudentID = 4, LectureID = 4, WasAtLecture = true, MarkValue = 0 },
                new Mark { StudentID = 5, LectureID = 4, WasAtLecture = true, MarkValue = 5 },
                new Mark { StudentID = 6, LectureID = 4, WasAtLecture = true, MarkValue = 2 },

                new Mark { StudentID = 7,  LectureID = 5, WasAtLecture = true, MarkValue = 4 },
                new Mark { StudentID = 8,  LectureID = 5, WasAtLecture = true, MarkValue = 4 },
                new Mark { StudentID = 9,  LectureID = 5, WasAtLecture = true, MarkValue = 4 },
                new Mark { StudentID = 10, LectureID = 5, WasAtLecture = true, MarkValue = 2 },
                new Mark { StudentID = 11, LectureID = 5, WasAtLecture = false, MarkValue = 3 },
                new Mark { StudentID = 12, LectureID = 5, WasAtLecture = true, MarkValue = 5 },
                                           
                new Mark { StudentID = 13, LectureID = 6, WasAtLecture = true, MarkValue = 5 },
                new Mark { StudentID = 14, LectureID = 6, WasAtLecture = true, MarkValue = 4 },
                new Mark { StudentID = 15, LectureID = 6, WasAtLecture = true, MarkValue = 0 },
                new Mark { StudentID = 16, LectureID = 6, WasAtLecture = true, MarkValue = 4 },
                new Mark { StudentID = 17, LectureID = 6, WasAtLecture = true, MarkValue = 5 },
                new Mark { StudentID = 18, LectureID = 6, WasAtLecture = true, MarkValue = 4 },

                //3 лекция
                new Mark { StudentID = 1, LectureID = 7, WasAtLecture = true, MarkValue = 5 },
                new Mark { StudentID = 2, LectureID = 7, WasAtLecture = true, MarkValue = 0 },
                new Mark { StudentID = 3, LectureID = 7, WasAtLecture = true, MarkValue = 2 },
                new Mark { StudentID = 4, LectureID = 7, WasAtLecture = true, MarkValue = 4 },
                new Mark { StudentID = 5, LectureID = 7, WasAtLecture = false, MarkValue = 3 },
                new Mark { StudentID = 6, LectureID = 7, WasAtLecture = true, MarkValue = 5 },

                new Mark { StudentID = 7,  LectureID = 8, WasAtLecture = true, MarkValue = 3 },
                new Mark { StudentID = 8,  LectureID = 8, WasAtLecture = true, MarkValue = 4 },
                new Mark { StudentID = 9,  LectureID = 8, WasAtLecture = true, MarkValue = 4 },
                new Mark { StudentID = 10, LectureID = 8, WasAtLecture = true, MarkValue = 0 },
                new Mark { StudentID = 11, LectureID = 8, WasAtLecture = true, MarkValue = 4 },
                new Mark { StudentID = 12, LectureID = 8, WasAtLecture = true, MarkValue = 5 },
                                           
                new Mark { StudentID = 13, LectureID = 9, WasAtLecture = true, MarkValue = 0 },
                new Mark { StudentID = 14, LectureID = 9, WasAtLecture = true, MarkValue = 4 },
                new Mark { StudentID = 15, LectureID = 9, WasAtLecture = true, MarkValue = 5 },
                new Mark { StudentID = 16, LectureID = 9, WasAtLecture = true, MarkValue = 4 },
                new Mark { StudentID = 17, LectureID = 9, WasAtLecture = true, MarkValue = 4 },
                new Mark { StudentID = 18, LectureID = 9, WasAtLecture = true, MarkValue = 5 },

                //4 лекция
                new Mark { StudentID = 1, LectureID = 10, WasAtLecture = true, MarkValue = 2 },
                new Mark { StudentID = 2, LectureID = 10, WasAtLecture = true, MarkValue = 4 },
                new Mark { StudentID = 3, LectureID = 10, WasAtLecture = true, MarkValue = 5 },
                new Mark { StudentID = 4, LectureID = 10, WasAtLecture = true, MarkValue = 3 },
                new Mark { StudentID = 5, LectureID = 10, WasAtLecture = false, MarkValue = 3 },
                new Mark { StudentID = 6, LectureID = 10, WasAtLecture = true, MarkValue = 3 },

                new Mark { StudentID = 7,  LectureID = 11, WasAtLecture = true, MarkValue = 4 },
                new Mark { StudentID = 8,  LectureID = 11, WasAtLecture = true, MarkValue = 4 },
                new Mark { StudentID = 9,  LectureID = 11, WasAtLecture = true, MarkValue = 5 },
                new Mark { StudentID = 10, LectureID = 11, WasAtLecture = true, MarkValue = 5 },
                new Mark { StudentID = 11, LectureID = 11, WasAtLecture = true, MarkValue = 3 },
                new Mark { StudentID = 12, LectureID = 11, WasAtLecture = true, MarkValue = 5 },
                                           
                new Mark { StudentID = 13, LectureID = 12, WasAtLecture = true, MarkValue = 5 },
                new Mark { StudentID = 14, LectureID = 12, WasAtLecture = true, MarkValue = 4 },
                new Mark { StudentID = 15, LectureID = 12, WasAtLecture = true, MarkValue = 5 },
                new Mark { StudentID = 16, LectureID = 12, WasAtLecture = true, MarkValue = 5 },
                new Mark { StudentID = 17, LectureID = 12, WasAtLecture = true, MarkValue = 3 },
                new Mark { StudentID = 18, LectureID = 12, WasAtLecture = true, MarkValue = 5 },

                //5 лекция
                new Mark { StudentID = 1, LectureID = 13, WasAtLecture = true, MarkValue = 5 },
                new Mark { StudentID = 2, LectureID = 13, WasAtLecture = true, MarkValue = 4 },
                new Mark { StudentID = 3, LectureID = 13, WasAtLecture = true, MarkValue = 0 },
                new Mark { StudentID = 4, LectureID = 13, WasAtLecture = true, MarkValue = 5 },
                new Mark { StudentID = 5, LectureID = 13, WasAtLecture = false, MarkValue = 4 },
                new Mark { StudentID = 6, LectureID = 13, WasAtLecture = true, MarkValue = 5 },

                new Mark { StudentID = 7,  LectureID = 14, WasAtLecture = true, MarkValue = 5 },
                new Mark { StudentID = 8,  LectureID = 14, WasAtLecture = true, MarkValue = 4 },
                new Mark { StudentID = 9,  LectureID = 14, WasAtLecture = true, MarkValue = 5 },
                new Mark { StudentID = 10, LectureID = 14, WasAtLecture = true, MarkValue = 4 },
                new Mark { StudentID = 11, LectureID = 14, WasAtLecture = true, MarkValue = 3 },
                new Mark { StudentID = 12, LectureID = 14, WasAtLecture = true, MarkValue = 5 },
                                           
                new Mark { StudentID = 13, LectureID = 15, WasAtLecture = true, MarkValue = 5 },
                new Mark { StudentID = 14, LectureID = 15, WasAtLecture = true, MarkValue = 5 },
                new Mark { StudentID = 15, LectureID = 15, WasAtLecture = true, MarkValue = 5 },
                new Mark { StudentID = 16, LectureID = 15, WasAtLecture = true, MarkValue = 5 },
                new Mark { StudentID = 17, LectureID = 15, WasAtLecture = true, MarkValue = 5 },
                new Mark { StudentID = 18, LectureID = 15, WasAtLecture = true, MarkValue = 5 },

                //6 лекция
                new Mark { StudentID = 1, LectureID = 16, WasAtLecture = true, MarkValue = 4 },
                new Mark { StudentID = 2, LectureID = 16, WasAtLecture = true, MarkValue = 3 },
                new Mark { StudentID = 3, LectureID = 16, WasAtLecture = true, MarkValue = 5 },
                new Mark { StudentID = 4, LectureID = 16, WasAtLecture = true, MarkValue = 4 },
                new Mark { StudentID = 5, LectureID = 16, WasAtLecture = true, MarkValue = 3 },
                new Mark { StudentID = 6, LectureID = 16, WasAtLecture = true, MarkValue = 4 },

                new Mark { StudentID = 7,  LectureID = 17, WasAtLecture = true, MarkValue = 5 },
                new Mark { StudentID = 8,  LectureID = 17, WasAtLecture = true, MarkValue = 4 },
                new Mark { StudentID = 9,  LectureID = 17, WasAtLecture = true, MarkValue = 5 },
                new Mark { StudentID = 10, LectureID = 17, WasAtLecture = true, MarkValue = 4 },
                new Mark { StudentID = 11, LectureID = 17, WasAtLecture = false, MarkValue = 5 },
                new Mark { StudentID = 12, LectureID = 17, WasAtLecture = true, MarkValue = 5 },
                                           
                new Mark { StudentID = 13, LectureID = 18, WasAtLecture = true, MarkValue = 5 },
                new Mark { StudentID = 14, LectureID = 18, WasAtLecture = true, MarkValue = 4 },
                new Mark { StudentID = 15, LectureID = 18, WasAtLecture = true, MarkValue = 5 },
                new Mark { StudentID = 16, LectureID = 18, WasAtLecture = true, MarkValue = 4 },
                new Mark { StudentID = 17, LectureID = 18, WasAtLecture = true, MarkValue = 5 },
                new Mark { StudentID = 18, LectureID = 18, WasAtLecture = true, MarkValue = 5 },

                //7 лекция
                new Mark { StudentID = 1, LectureID = 19, WasAtLecture = true, MarkValue = 3 },
                new Mark { StudentID = 2, LectureID = 19, WasAtLecture = true, MarkValue = 4 },
                new Mark { StudentID = 3, LectureID = 19, WasAtLecture = false, MarkValue = 3 },
                new Mark { StudentID = 4, LectureID = 19, WasAtLecture = true, MarkValue = 4 },
                new Mark { StudentID = 5, LectureID = 19, WasAtLecture = false, MarkValue = 5 },
                new Mark { StudentID = 6, LectureID = 19, WasAtLecture = true, MarkValue = 4 },

                new Mark { StudentID = 7,  LectureID = 20, WasAtLecture = true, MarkValue = 0 },
                new Mark { StudentID = 8,  LectureID = 20, WasAtLecture = true, MarkValue = 4 },
                new Mark { StudentID = 9,  LectureID = 20, WasAtLecture = true, MarkValue = 5 },
                new Mark { StudentID = 10, LectureID = 20, WasAtLecture = true, MarkValue = 3 },
                new Mark { StudentID = 11, LectureID = 20, WasAtLecture = true, MarkValue = 4 },
                new Mark { StudentID = 12, LectureID = 20, WasAtLecture = false, MarkValue = 5 },
                                           
                new Mark { StudentID = 13, LectureID = 21, WasAtLecture = true, MarkValue = 4 },
                new Mark { StudentID = 14, LectureID = 21, WasAtLecture = false, MarkValue = 5 },
                new Mark { StudentID = 15, LectureID = 21, WasAtLecture = true, MarkValue = 4 },
                new Mark { StudentID = 16, LectureID = 21, WasAtLecture = true, MarkValue = 3 },
                new Mark { StudentID = 17, LectureID = 21, WasAtLecture = true, MarkValue = 4},
                new Mark { StudentID = 18, LectureID = 21, WasAtLecture = true, MarkValue = 5 },

             };
            foreach (Mark s in marks)
            {
                context.Marks.Add(s);
            }
            context.SaveChanges();







        }
    }
}
