using StudyJournal.DataBase;
using StudyJournal.DataBase.Data;
using StudyJournal.DataBase.Entities;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;

namespace StudyJournal.University
{
    public static class UniversityInitializer
    {
        public static void RegisterDbContext(IServiceCollection services, string connectionString)
        {
            services.AddDbContextFactory<DataBaseContext>(options => options.UseSqlServer(connectionString));
        }

        public static void RegisterServices(IServiceCollection services)
        {
            services.AddSingleton<IHomeworkRepository, HomeworkRepository>();
            services.AddSingleton<ILectureRepository, LectureRepository>();
            services.AddSingleton<ISpeakerRepository, SpeakerRepository>();
            services.AddSingleton<IStudentRepository, StudentRepository>();
            services.AddSingleton<IStudyGroupRepository, StudyGroupRepository>();
            services.AddSingleton<IJournalRepository, JournalRepository>();

            services.AddSingleton<IHomeworkService, HomeworkService>();
            services.AddSingleton<ILectureService, LectureService>();
            services.AddSingleton<ISpeakerService, SpeakerService>();
            services.AddSingleton<IStudentService, StudentService>();
            services.AddSingleton<IStudyGroupService, StudyGroupService>();
            services.AddSingleton<IJournalService, JournalService>();

        }
    }

}
