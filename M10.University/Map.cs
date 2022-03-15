using StudyJournal.DataBase.Entities;
using StudyJournal.University.Models;


namespace StudyJournal.University

{
    public static class Map
    {
        public static StudyJournal.University.Models.Student StudentEntityToModel(StudyJournal.DataBase.Entities.Student inputStudent)
        {
            StudyJournal.University.Models.Student student = new StudyJournal.University.Models.Student();
            student.Id = inputStudent.StudentID;
            student.Name = inputStudent.StudentName;
            student.Surname = inputStudent.StudentSurname;
            student.Email = inputStudent.StudentEmail;
            student.Phone = inputStudent.StudentPhone;
            student.GroupId = inputStudent.StudyGroupID;

            return student;
        }

        public static StudyJournal.DataBase.Entities.Student StudentModelToEntity(StudyJournal.University.Models.Student inputStudent)
        {
            StudyJournal.DataBase.Entities.Student student = new StudyJournal.DataBase.Entities.Student();
            student.StudentID = inputStudent.Id;
            student.StudentName = inputStudent.Name;
            student.StudentSurname = inputStudent.Surname;
            student.StudentEmail = inputStudent.Email;
            student.StudentPhone = inputStudent.Phone;
            student.StudyGroupID = inputStudent.GroupId;

            return student;
        }

        public static StudyJournal.University.Models.Speaker SpeakerEntityToModel(StudyJournal.DataBase.Entities.Speaker inputSpeaker)
        {
            StudyJournal.University.Models.Speaker speaker = new StudyJournal.University.Models.Speaker();
            speaker.Id = inputSpeaker.SpeakerID;
            speaker.Name = inputSpeaker.SpeakerName;
            speaker.Surname = inputSpeaker.SpeakerSurname;
            speaker.Email = inputSpeaker.SpeakerEmail;

            return speaker;
        }

        public static StudyJournal.DataBase.Entities.Speaker SpeakerModelToEntity(StudyJournal.University.Models.Speaker inputSpeaker)
        {
            var speaker = new StudyJournal.DataBase.Entities.Speaker();
            speaker.SpeakerID = inputSpeaker.Id;
            speaker.SpeakerName = inputSpeaker.Name;
            speaker.SpeakerSurname = inputSpeaker.Surname;
            speaker.SpeakerEmail = inputSpeaker.Email;

            return speaker;
        }

        public static StudyJournal.University.Models.StudyGroup StudyGroupEntityToModel(StudyJournal.DataBase.Entities.StudyGroup inputStudyGroup)
        {
            StudyJournal.University.Models.StudyGroup studyGroup = new StudyJournal.University.Models.StudyGroup();
            studyGroup.Id = inputStudyGroup.StudyGroupID;
            studyGroup.Name = inputStudyGroup.StudyGroupName;

            return studyGroup;
        }

        public static StudyJournal.DataBase.Entities.StudyGroup StudyGroupModelToEntity(StudyJournal.University.Models.StudyGroup inputStudyGroup)
        {
            var studyGroup = new StudyJournal.DataBase.Entities.StudyGroup();
            studyGroup.StudyGroupID = inputStudyGroup.Id;
            studyGroup.StudyGroupName = inputStudyGroup.Name;

            return studyGroup;
        }


        public static StudyJournal.University.Models.Lecture LectureEntityToModel(StudyJournal.DataBase.Entities.Lecture inputLecture)
        {
            StudyJournal.University.Models.Lecture lecture = new StudyJournal.University.Models.Lecture();

            lecture.Id = inputLecture.LectureID;
            lecture.Name = inputLecture.LectureName;
            lecture.Description = inputLecture.LectureDescription;
            lecture.Date = inputLecture.LectureDate;
            lecture.GroupId = inputLecture.StudyGroupID;
            lecture.SpeakerId = inputLecture.SpeakerID;
            lecture.SubjectId = inputLecture.SubjectID;

            return lecture;
        }

        public static StudyJournal.DataBase.Entities.Lecture LectureModelToEntity(StudyJournal.University.Models.Lecture inputLecture)
        {
            var lecture = new StudyJournal.DataBase.Entities.Lecture();

            lecture.LectureID = inputLecture.Id;
            lecture.LectureName = inputLecture.Name;
            lecture.LectureDescription = inputLecture.Description;
            lecture.LectureDate = inputLecture.Date;
            lecture.StudyGroupID = inputLecture.GroupId;
            lecture.SpeakerID = inputLecture.SpeakerId;
            lecture.SubjectID = inputLecture.SubjectId;

            return lecture;
        }

        public static StudyJournal.University.Models.Homework HomeworkEntityToModel(StudyJournal.DataBase.Entities.Homework inputHomework)
        {
            StudyJournal.University.Models.Homework homework = new StudyJournal.University.Models.Homework();

            homework.Id = inputHomework.HomeworkID;
            homework.Name = inputHomework.HomeworkName;
            homework.Description = inputHomework.HomeworkDescription;
            homework.LectureID = inputHomework.LectureID;

            return homework;
        }

        public static StudyJournal.DataBase.Entities.Homework HomeworkModelToEntity(StudyJournal.University.Models.Homework inputHomework)
        {
            var homework = new StudyJournal.DataBase.Entities.Homework();

            homework.HomeworkID = inputHomework.Id;
            homework.HomeworkName = inputHomework.Name;
            homework.HomeworkDescription = inputHomework.Description;
            homework.LectureID = inputHomework.LectureID;

            return homework;
        }
        public static StudyJournal.University.Models.Mark MarkEntityToModel(StudyJournal.DataBase.Entities.Mark inputMark)
        {
            StudyJournal.University.Models.Mark mark = new StudyJournal.University.Models.Mark();

            mark.Id = inputMark.MarkID;
            mark.StudentId = inputMark.StudentID;
            mark.LectureId = inputMark.LectureID;
            mark.WasAtLecture = inputMark.WasAtLecture;
            mark.MarkValue = inputMark.MarkValue;

            return mark;
        }

        public static StudyJournal.DataBase.Entities.Mark MarkModelToEntity(StudyJournal.University.Models.Mark inputMark)
        {
            StudyJournal.DataBase.Entities.Mark mark = new StudyJournal.DataBase.Entities.Mark();

            mark.MarkID = inputMark.Id;
            mark.StudentID = inputMark.StudentId;
            mark.LectureID = inputMark.LectureId;
            mark.WasAtLecture = inputMark.WasAtLecture;
            mark.MarkValue = inputMark.MarkValue;

            return mark;
        }

    }
}
