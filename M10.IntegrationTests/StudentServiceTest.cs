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

namespace StudyJournal.IntegrationTests
{
    [TestFixture]
    public class StudentServiceTest
    {
        [Test]
        public async Task GetAllStudents_SendRequest_ShouldReturnAllStudents()
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

            StudentService service = (StudentService)webHost.Services.GetService(typeof(IStudentService));

            // Act

            List<Student> result = service.GetAllStudents().ToList();

            //Assert
            int expectedCount = 18;
            Student expectedStudent = result.FirstOrDefault(s => s.StudentName == "Andrey" && s.StudentSurname == "Mashkov");

            Assert.AreEqual(expectedCount, result.Count);
            Assert.AreEqual("Andrey", expectedStudent.StudentName);
        }

        [Test]
        public async Task GetStudentByID_Send1_ShouldReturnStudent()
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

            StudentService service = (StudentService)webHost.Services.GetService(typeof(IStudentService));

            // Act

            Student result = service.GetStudentByID(1);

            //Assert
            Assert.AreEqual("Andrey", result.StudentName);
        }

        [Test]
        public async Task InsertStudent_SendStudent_ShouldInsertStudent()
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

            Student st = new Student() { StudentID = 19, StudentName = "InsertName", StudentSurname = "InsertSurname", StudentEmail = "InsertEmail", StudentPhone = "InsertPhone", StudyGroupID = 1 };

            StudentService service = (StudentService)webHost.Services.GetService(typeof(IStudentService));

            // Act

            service.InsertStudent(st);

            //Assert

            Student check = dbTestContext.Students.FirstOrDefault(s => s.StudentName == "InsertName");
            Assert.AreEqual("InsertName", st.StudentName);
        }

        [Test]
        public async Task UpdateStudent_SendStudent_ShouldUpdateStudent()
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

            Student st = dbTestContext.Students.FirstOrDefault(s => s.StudyGroupID == 1);
            st.StudentName = "TestUpdate";
            StudentService service = (StudentService)webHost.Services.GetService(typeof(IStudentService));

            // Act
            service.UpdateStudent(st);

            //Assert
            Student check = dbTestContext.Students.FirstOrDefault(s => s.StudentName == "TestUpdate");
            Assert.AreEqual("TestUpdate", check.StudentName);
        }

        [Test]
        public async Task DeleteStudent_SendStudent_ShouldDeleteStudent()
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

            StudentService service = (StudentService)webHost.Services.GetService(typeof(IStudentService));

            // Act

            service.DeleteStudent(1);

            //Assert
            Student check = dbTestContext.Students.FirstOrDefault(s => s.StudentID == 1);
            Assert.AreEqual(null, check);
        }
    }
}
