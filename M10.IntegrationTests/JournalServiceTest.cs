using Microsoft.AspNetCore.Mvc.Testing;
using NUnit.Framework;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.TestHost;
using System.Linq;
using StudyJournal.University;
using StudyJournal.DataBase.Entities;
using System.Collections.Generic;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using StudyJournal.DataBase.Data;
using StudyJournal.DataBase;
using System;

namespace StudyJournal.IntegrationTests
{
    [TestFixture]
    public class JournalServiceTest
    {
        [Test]
        public async Task UpdateMark_SendMark_ShouldApdateMark()
        {
            // Arrange
            WebApplicationFactory<Program> webHost = new WebApplicationFactory<Program>().WithWebHostBuilder(builder =>
            {
                builder.ConfigureTestServices(services =>
                {
                    var dbContextDescriptor = services.SingleOrDefault(d =>
                    d.ServiceType == typeof(DbContextOptions<DataBaseContext>));

                    services.Remove(dbContextDescriptor);

                    services.AddDbContextFactory<DataBaseContext>(options =>
                    {
                        options.UseInMemoryDatabase("test_database");
                    });
                });
            });
            DataBaseContext dbTestContext = webHost.Services.CreateScope().ServiceProvider.GetService<DataBaseContext>();
            HttpClient httpClient = webHost.CreateClient();

            JournalService service = (JournalService)webHost.Services.GetService(typeof(IJournalService));

            Mark mark = dbTestContext.Marks.FirstOrDefault(s => s.MarkID == 1);

            mark.MarkValue = 1;
            // Act

            service.UpdateMark(mark);

            //Assert


            Mark check = dbTestContext.Marks.FirstOrDefault(s => s.MarkID == 1);
            Assert.AreEqual(1, check.MarkID);
        }

        [Test]
        public async Task CheckAverageMark_Send1_ShouldReturn4()
        {
            // Arrange
            WebApplicationFactory<Program> webHost = new WebApplicationFactory<Program>().WithWebHostBuilder(builder =>
            {
                builder.ConfigureTestServices(services =>
                {
                    var dbContextDescriptor = services.SingleOrDefault(d =>
                    d.ServiceType == typeof(DbContextOptions<DataBaseContext>));

                    services.Remove(dbContextDescriptor);

                    services.AddDbContextFactory<DataBaseContext>(options =>
                    {
                        options.UseInMemoryDatabase("test_database");
                    });
                });
            });
            DataBaseContext dbTestContext = webHost.Services.CreateScope().ServiceProvider.GetService<DataBaseContext>();
            HttpClient httpClient = webHost.CreateClient();

            JournalService service = (JournalService)webHost.Services.GetService(typeof(IJournalService));

            double averageMark;
            // Act

            averageMark = service.CheckAverageMark(1);

            //Assert

            Assert.AreEqual(4, averageMark);
        }

        [Test]
        public async Task CheckAttending_Send5_ShouldReturnTrue()
        {
            // Arrange
            WebApplicationFactory<Program> webHost = new WebApplicationFactory<Program>().WithWebHostBuilder(builder =>
            {
                builder.ConfigureTestServices(services =>
                {
                    var dbContextDescriptor = services.SingleOrDefault(d =>
                    d.ServiceType == typeof(DbContextOptions<DataBaseContext>));

                    services.Remove(dbContextDescriptor);

                    services.AddDbContextFactory<DataBaseContext>(options =>
                    {
                        options.UseInMemoryDatabase("test_database");
                    });
                });
            });
            DataBaseContext dbTestContext = webHost.Services.CreateScope().ServiceProvider.GetService<DataBaseContext>();
            HttpClient httpClient = webHost.CreateClient();

            JournalService service = (JournalService)webHost.Services.GetService(typeof(IJournalService));

            bool attending;
            // Act

            attending = service.ChekAttending(5);

            //Assert

            Assert.AreEqual(true, attending);
        }

        [Test]
        public async Task GetMarkByID_Send1_ShouldReturn5()
        {
            // Arrange
            WebApplicationFactory<Program> webHost = new WebApplicationFactory<Program>().WithWebHostBuilder(builder =>
            {
                builder.ConfigureTestServices(services =>
                {
                    var dbContextDescriptor = services.SingleOrDefault(d =>
                    d.ServiceType == typeof(DbContextOptions<DataBaseContext>));

                    services.Remove(dbContextDescriptor);

                    services.AddDbContextFactory<DataBaseContext>(options =>
                    {
                        options.UseInMemoryDatabase("test_database");
                    });
                });
            });
            DataBaseContext dbTestContext = webHost.Services.CreateScope().ServiceProvider.GetService<DataBaseContext>();
            HttpClient httpClient = webHost.CreateClient();

            JournalService service = (JournalService)webHost.Services.GetService(typeof(IJournalService));


            // Act

            Mark mark = service.GetMarkById(1);

            //Assert

            Assert.AreEqual(5, mark.MarkValue);
        }

        [Test]
        public async Task GenerateDefaultMarks_Send22and1_ShouldCreateMarks()
        {
            // Arrange
            WebApplicationFactory<Program> webHost = new WebApplicationFactory<Program>().WithWebHostBuilder(builder =>
            {
                builder.ConfigureTestServices(services =>
                {
                    var dbContextDescriptor = services.SingleOrDefault(d =>
                    d.ServiceType == typeof(DbContextOptions<DataBaseContext>));

                    services.Remove(dbContextDescriptor);

                    services.AddDbContextFactory<DataBaseContext>(options =>
                    {
                        options.UseInMemoryDatabase("test_database");
                    });
                });
            });
            DataBaseContext dbTestContext = webHost.Services.CreateScope().ServiceProvider.GetService<DataBaseContext>();
            HttpClient httpClient = webHost.CreateClient();

            JournalService service = (JournalService)webHost.Services.GetService(typeof(IJournalService));
            LectureService lectService = (LectureService)webHost.Services.GetService(typeof(ILectureService));

            Lecture lect = new Lecture();
            lect = dbTestContext.Lectures.FirstOrDefault(x => x.LectureID == 1);
            lectService.InsertLecture(lect);
            // Act

            service.CreateDefaultMarks(22, 1);

            //Assert

            Mark mark = dbTestContext.Marks.FirstOrDefault(s => s.LectureID == 22);
            Assert.AreNotEqual(null, mark);

        }
        [Test]
        public async Task GetLectureAttendingReportXML_Send1_ShouldReturnString()
        {
            // Arrange
            WebApplicationFactory<Program> webHost = new WebApplicationFactory<Program>().WithWebHostBuilder(builder =>
            {
                builder.ConfigureTestServices(services =>
                {
                    var dbContextDescriptor = services.SingleOrDefault(d =>
                    d.ServiceType == typeof(DbContextOptions<DataBaseContext>));

                    services.Remove(dbContextDescriptor);

                    services.AddDbContextFactory<DataBaseContext>(options =>
                    {
                        options.UseInMemoryDatabase("test_database");
                    });
                });
            });
            DataBaseContext dbTestContext = webHost.Services.CreateScope().ServiceProvider.GetService<DataBaseContext>();
            HttpClient httpClient = webHost.CreateClient();

            JournalService service = (JournalService)webHost.Services.GetService(typeof(IJournalService));


            // Act

            string s = service.GetLectureAttendingReportXml(1);

            //Assert

            Assert.AreNotEqual(null, s);
        }

        [Test]
        public async Task GetLectureAttendingReportJSON_Send1_ShouldReturnString()
        {
            // Arrange
            WebApplicationFactory<Program> webHost = new WebApplicationFactory<Program>().WithWebHostBuilder(builder =>
            {
                builder.ConfigureTestServices(services =>
                {
                    var dbContextDescriptor = services.SingleOrDefault(d =>
                    d.ServiceType == typeof(DbContextOptions<DataBaseContext>));

                    services.Remove(dbContextDescriptor);

                    services.AddDbContextFactory<DataBaseContext>(options =>
                    {
                        options.UseInMemoryDatabase("test_database");
                    });
                });
            });
            DataBaseContext dbTestContext = webHost.Services.CreateScope().ServiceProvider.GetService<DataBaseContext>();
            HttpClient httpClient = webHost.CreateClient();

            JournalService service = (JournalService)webHost.Services.GetService(typeof(IJournalService));


            // Act

            string s = service.GetLectureAttendingReportJson(1);

            //Assert

            Assert.AreNotEqual(null, s);
        }

        [Test]
        public async Task GetStudentAttendingReportXML_Send1_ShouldReturnString()
        {
            // Arrange
            WebApplicationFactory<Program> webHost = new WebApplicationFactory<Program>().WithWebHostBuilder(builder =>
            {
                builder.ConfigureTestServices(services =>
                {
                    var dbContextDescriptor = services.SingleOrDefault(d =>
                    d.ServiceType == typeof(DbContextOptions<DataBaseContext>));

                    services.Remove(dbContextDescriptor);

                    services.AddDbContextFactory<DataBaseContext>(options =>
                    {
                        options.UseInMemoryDatabase("test_database");
                    });
                });
            });
            DataBaseContext dbTestContext = webHost.Services.CreateScope().ServiceProvider.GetService<DataBaseContext>();
            HttpClient httpClient = webHost.CreateClient();

            JournalService service = (JournalService)webHost.Services.GetService(typeof(IJournalService));


            // Act

            string s = service.GetStudentAttendingReportXml(1);

            //Assert

            Assert.AreNotEqual(null, s);
        }

        [Test]
        public async Task GetStudentAttendingReportJSON_Send1_ShouldReturnString()
        {
            // Arrange
            WebApplicationFactory<Program> webHost = new WebApplicationFactory<Program>().WithWebHostBuilder(builder =>
            {
                builder.ConfigureTestServices(services =>
                {
                    var dbContextDescriptor = services.SingleOrDefault(d =>
                    d.ServiceType == typeof(DbContextOptions<DataBaseContext>));

                    services.Remove(dbContextDescriptor);

                    services.AddDbContextFactory<DataBaseContext>(options =>
                    {
                        options.UseInMemoryDatabase("test_database");
                    });
                });
            });
            DataBaseContext dbTestContext = webHost.Services.CreateScope().ServiceProvider.GetService<DataBaseContext>();
            HttpClient httpClient = webHost.CreateClient();

            JournalService service = (JournalService)webHost.Services.GetService(typeof(IJournalService));


            // Act

            string s = service.GetStudentAttendingReportJson(1);

            //Assert

            Assert.AreNotEqual(null, s);
        }

    }
}
