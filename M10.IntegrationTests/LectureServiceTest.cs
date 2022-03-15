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
using System;

namespace StudyJournal.IntegrationTests
{
    [TestFixture]
    public class LectureServiceTest
    {
        [Test]
        public async Task GetAllLectures_SendRequest_ShouldReturnAllLectures()
        {
            // Arrange
            WebApplicationFactory<Program> webHost = new WebApplicationFactory<Program>().WithWebHostBuilder(builder =>
            {
                builder.ConfigureTestServices(services =>
                {
                    var dbContextDescriptor = services.SingleOrDefault(d =>
                    d.ServiceType == typeof(DbContextOptions<DataBaseContext>));
                    var dbContextS = services.SingleOrDefault(d =>
                    d.ServiceType == typeof(DbInitializer));

                    services.Remove(dbContextDescriptor);
                    services.Remove(dbContextS);

                    services.AddDbContextFactory<DataBaseContext>(options =>
                    {
                        options.UseInMemoryDatabase("test_database");
                    });
                });
            });

            DataBaseContext dbTestContext = webHost.Services.CreateScope().ServiceProvider.GetService<DataBaseContext>();
            HttpClient httpClient = webHost.CreateClient();

            LectureService service = (LectureService)webHost.Services.GetService(typeof(ILectureService));

            // Act

            List<Lecture> result = service.GetAllLectures().ToList();


            //Assert
            int expectedCount = 22;

            Lecture check = result.FirstOrDefault(s => s.LectureID == 1);

            Assert.AreEqual(expectedCount, result.Count);
            Assert.AreEqual("Math. Lecture N1", check.LectureName);
        }

        [Test]
        public async Task GetLecture_Send1_ShouldReturnLecture()
        {
            // Arrange
            WebApplicationFactory<Program> webHost = new WebApplicationFactory<Program>().WithWebHostBuilder(builder =>
            {
                builder.ConfigureTestServices(services =>
                {
                    var dbContextDescriptor = services.SingleOrDefault(d =>
                    d.ServiceType == typeof(DbContextOptions<DataBaseContext>));
                    var dbContextS = services.SingleOrDefault(d =>
                    d.ServiceType == typeof(DbInitializer));

                    services.Remove(dbContextDescriptor);
                    services.Remove(dbContextS);

                    services.AddDbContextFactory<DataBaseContext>(options =>
                    {
                        options.UseInMemoryDatabase("test_database");
                    });
                });
            });

            DataBaseContext dbTestContext = webHost.Services.CreateScope().ServiceProvider.GetService<DataBaseContext>();
            HttpClient httpClient = webHost.CreateClient();

            LectureService service = (LectureService)webHost.Services.GetService(typeof(ILectureService));

            // Act

            Lecture result = service.GetLectureByID(1);

            //Assert

            Assert.AreEqual("Math. Lecture N1", result.LectureName);
        }

        [Test]
        public async Task InsertLecture_SendLecture_ShouldInsertLecture()
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
            
            LectureService service = (LectureService)webHost.Services.GetService(typeof(ILectureService));
           
            Lecture lect = new Lecture() { LectureName = "InsertLect", LectureDescription = "Description Lect1", LectureDate = new DateTime(2020, 10, 10, 10, 0, 0), StudyGroupID = 1, SpeakerID = 1, SubjectID = 1 };
            
            Lecture lecture = dbTestContext.Lectures.FirstOrDefault(s => s.LectureID == 1);


            // Act
            service.InsertLecture(lect);
            //Assert
            Lecture check = dbTestContext.Lectures.FirstOrDefault(s => s.LectureName == "InsertLect");

            //Assert.AreEqual("InsertLect", check.LectureName);
            Assert.AreEqual(22, dbTestContext.Lectures.Count());
        }

        [Test]
        public async Task UpdateLecture_SendLecture_ShouldUpdateLecture()
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

            LectureService service = (LectureService)webHost.Services.GetService(typeof(ILectureService));

            Lecture lecture = dbTestContext.Lectures.FirstOrDefault(s => s.LectureID == 1);

            // Act
            lecture.LectureName = "UpdateTest";
            service.UpdateLecture(lecture);

            //Assert
            Lecture check = dbTestContext.Lectures.FirstOrDefault(s => s.LectureID == 1);

            Assert.AreEqual("UpdateTest", check.LectureName);
        }

        [Test]
        public async Task DeleteLecture_SendLecture_ShouldDeleteLecture()
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

            LectureService service = (LectureService)webHost.Services.GetService(typeof(ILectureService));

            // Act
            service.DeleteLecture(1);

            //Assert
            Lecture check = dbTestContext.Lectures.FirstOrDefault(s => s.LectureID == 1);

            Assert.AreEqual(null, check);
        }

    }
}
