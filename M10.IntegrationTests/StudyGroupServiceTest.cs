using Microsoft.AspNetCore.Mvc.Testing;
using NUnit.Framework;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.TestHost;
using System.Linq;
using StudyJournal.University;
using Moq;
using StudyJournal.DataBase.Entities;
using System.Collections.Generic;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using StudyJournal.DataBase.Data;
using StudyJournal.DataBase;
using Microsoft.Extensions.Logging;

namespace StudyJournal.IntegrationTests
{
    [TestFixture]
    public class StudyGroupServiceTest
    {
        [Test]
        public async Task GetAllStudyGroups_SendRequest_ShouldReturnAllGroup()
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

            StudyGroupService service = (StudyGroupService)webHost.Services.GetService(typeof(IStudyGroupService));

            // Act

            List<StudyGroup> result = service.GetAllStudyGroups().ToList();

            //Assert
            int expectedCount = 3;

            StudyGroup resultStudyGroup = result.FirstOrDefault(s => s.StudyGroupName == "A-101");

            Assert.AreEqual(expectedCount, result.Count);
            Assert.AreEqual("A-101", resultStudyGroup.StudyGroupName);
        }

        [Test]
        public async Task GetStudyGroupByID_Send1_ShouldReturnCorrectGroup()
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

            StudyGroupService service = (StudyGroupService)webHost.Services.GetService(typeof(IStudyGroupService));

            // Act

            StudyGroup result = service.GetStudyGroupByID(1);

            // Assert

            Assert.AreEqual("A-101", result.StudyGroupName);
        }

        [Test]
        public async Task InsertGroup_SendGroup_ShouldInsertCorrectly()
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
            StudyGroup st = new StudyGroup() { StudyGroupName = "TestInsertName" };

            StudyGroupService service = (StudyGroupService)webHost.Services.GetService(typeof(IStudyGroupService));

            // Act

            service.InsertStudyGroup(st);

            // Assert
            StudyGroup check = dbTestContext.StudyGroups.FirstOrDefault(s => s.StudyGroupName == "TestInsertName");

            Assert.AreEqual("TestInsertName", check.StudyGroupName);
        }

        [Test]
        public async Task UpdateGroup_SendGroup_ShouldUpdateCorrectrly()
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

            StudyGroup st = dbTestContext.StudyGroups.FirstOrDefault(s => s.StudyGroupID == 1);

            StudyGroupService service = (StudyGroupService)webHost.Services.GetService(typeof(IStudyGroupService));

            st.StudyGroupName = "TestUpdate";

            // Act

            service.UpdateStudyGroup(st);

            // Assert
            StudyGroup check = dbTestContext.StudyGroups.FirstOrDefault(s => s.StudyGroupID == 1);

            Assert.AreEqual("TestUpdate", check.StudyGroupName);
        }

        [Test]
        public async Task DeleteGroup_SendGroup_ShouldUpdateCorrectrly()
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

            StudyGroupService service = (StudyGroupService)webHost.Services.GetService(typeof(IStudyGroupService));

            // Act

            service.DeleteStudyGroup(1);

            // Assert
            StudyGroup check = dbTestContext.StudyGroups.FirstOrDefault(s => s.StudyGroupID == 1);

            Assert.AreEqual(null, check);
        }


        // Arrange


        // Act


        // Assert


        private List<StudyGroup> GetTestGroups()
        {
            List<StudyGroup> testList = new List<StudyGroup>()

                {
                    new StudyGroup { StudyGroupName = "A-101"},
                    new StudyGroup { StudyGroupName = "B-102"},
                    new StudyGroup { StudyGroupName = "DF-432"},
                };
            return testList;
        }

        private StudyGroup GetOneTestGroup()
        {
            var testgroup = new StudyGroup();
            testgroup.StudyGroupName = "A-101";

            return testgroup;
        }
    }

}