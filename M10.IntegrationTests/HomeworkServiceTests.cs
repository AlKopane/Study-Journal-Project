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

namespace StudyJournal.IntegrationTests
{
    [TestFixture]
    public class HomeworkServiceTests
    {
        [Test]
        public async Task GetAllHomeworks_SendRequest_ShouldReturnAllHomeworks()
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

            HomeworkService service = (HomeworkService)webHost.Services.GetService(typeof(IHomeworkService));

            // Act

            List<Homework> result = service.GetAllHomeworks().ToList();

            //Assert
            int expectedCount = 21;

            Homework expectedHomework = result.FirstOrDefault(s => s.HomeworkID == 1);

            Assert.AreEqual(expectedCount, result.Count);
            Assert.AreEqual("Homework: Maths, lecture 1.", expectedHomework.HomeworkName);
        }


        [Test]
        public async Task GetHomework_SendRequest_ShouldReturnHomework()
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

            HomeworkService service = (HomeworkService)webHost.Services.GetService(typeof(IHomeworkService));

            // Act

            Homework result = service.GetHomeworkByID(1);

            //Assert
            Assert.AreEqual("Homework: Maths, lecture 1.", result.HomeworkName);
        }

        [Test]
        public async Task InsertHomework_SendHomework_ShouldInsertHomework()
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

            Homework hw = new Homework() { LectureID = 22, HomeworkName = "InsertTest", HomeworkDescription = "sdfdsf." };

            HomeworkService service = (HomeworkService)webHost.Services.GetService(typeof(IHomeworkService));

            // Act

            service.InsertHomework(hw);

            //Assert
            Homework check = dbTestContext.Homeworks.FirstOrDefault(s => s.HomeworkName == "InsertTest");

            Assert.AreEqual("InsertTest", check.HomeworkName);
        }

        [Test]
        public async Task UpdateHomework_SendHomework_ShouldInsertHomework()
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

            //Homework hw = dbTestContext.Homeworks.FirstOrDefault(s=>s.HomeworkID == 1);
            

            HomeworkService service = (HomeworkService)webHost.Services.GetService(typeof(IHomeworkService));
            Homework hw = dbTestContext.Homeworks.FirstOrDefault(s => s.HomeworkID == 1);
            hw.LectureID = 22;
            // Act

            service.UpdateHomework(hw);

            //Assert
            Homework check = dbTestContext.Homeworks.FirstOrDefault(s=>s.HomeworkID==1);

            Assert.AreEqual(22, check.LectureID);
        }
        [Test]
        public async Task DeleteHomework_SendHomework_ShouldDeleteHomework()
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
                        
            HomeworkService service = (HomeworkService)webHost.Services.GetService(typeof(IHomeworkService));

            // Act

            service.DeleteHomework(1);

            //Assert
            Homework check = dbTestContext.Homeworks.FirstOrDefault(s => s.HomeworkID == 1);

            Assert.AreEqual(null, check);
        }

    }
}
