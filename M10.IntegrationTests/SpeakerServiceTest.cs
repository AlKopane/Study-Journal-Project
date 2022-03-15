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
    public class SpeakerServiceTest
    {
        [Test]
        public async Task GetAllSpeakers_SendRequest_ShouldReturnAllSpeakers()
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

            SpeakerService service = (SpeakerService)webHost.Services.GetService(typeof(ISpeakerService));

            // Act

            List<Speaker> result = service.GetAllSpeakers().ToList();

            //Assert
            int expectedCount = 3;

            Speaker expectedSpeaker = result.FirstOrDefault(s => s.SpeakerName == "Pavel");

            Assert.AreEqual(expectedCount, result.Count);
            Assert.AreEqual("Pavel", expectedSpeaker.SpeakerName);
        }

        [Test]
        public async Task GetSpeakerByID_Send1_ShouldReturnCorrectSpeaker()
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

            SpeakerService service = (SpeakerService)webHost.Services.GetService(typeof(ISpeakerService));

            // Act

            Speaker result = service.GetSpeakerByID(1);

            // Assert

            Assert.AreEqual("Pavel", result.SpeakerName);
            Assert.AreEqual("Petrov", result.SpeakerSurname);
        }

        [Test]
        public async Task InsertSpeaker_SendSpeaker_ShouldInsertCorrectly()
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
                    builder.ConfigureTestServices(services =>
                    {
                        var speakerRepository = services.SingleOrDefault(d => d.ServiceType == typeof(ISpeakerRepository));

                        services.Remove(speakerRepository);

                    });

                });
            });

            DataBaseContext dbTestContext = webHost.Services.CreateScope().ServiceProvider.GetService<DataBaseContext>();
            HttpClient httpClient = webHost.CreateClient();

            Speaker sp = new Speaker() { SpeakerName = "InsertName", SpeakerSurname = "InsertSurname", SpeakerEmail = "InsertEmail" };

            SpeakerService service = (SpeakerService)webHost.Services.GetService(typeof(ISpeakerService));

            // Act

            service.InsertSpeaker(sp);

            // Assert

            Speaker check = dbTestContext.Speakers.FirstOrDefault(s => s.SpeakerName == "InsertName");

            Assert.AreEqual("InsertName", check.SpeakerName);
        }

        [Test]
        public async Task UpdateSpeaker_SendSpeaker_ShouldInsertCorrectly()
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

            Speaker sp = dbTestContext.Speakers.FirstOrDefault(s => s.SpeakerID == 1);

            sp.SpeakerName = "TestUpdate";

            SpeakerService service = (SpeakerService)webHost.Services.GetService(typeof(ISpeakerService));

            // Act

            service.UpdateSpeaker(sp);

            // Assert

            Speaker check = dbTestContext.Speakers.FirstOrDefault(s => s.SpeakerName == "TestUpdate");
            Assert.AreEqual("TestUpdate", check.SpeakerName);
        }

        [Test]
        public async Task DeleteSpeaker_SendSpeaker_ShouldInsertCorrectly()
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

            SpeakerService service = (SpeakerService)webHost.Services.GetService(typeof(ISpeakerService));

            // Act

            service.DeleteSpeaker(1);

            // Assert

            Speaker check = dbTestContext.Speakers.FirstOrDefault(s => s.SpeakerID == 1);

            Assert.AreEqual(null, check);
        }

    }
}
