using Microsoft.EntityFrameworkCore;
using StudyJournal.DataBase.Entities;
using Microsoft.EntityFrameworkCore.Design;

namespace StudyJournal.DataBase.Data
{
    public class DataBaseContext : DbContext
    {
        public DbSet<Mark> Marks { get; set; }
        public DbSet<Homework> Homeworks { get; set; }
        public DbSet<Lecture> Lectures { get; set; }
        public DbSet<Speaker> Speakers { get; set; }
        public DbSet<Student> Students { get; set; }
        public DbSet<StudyGroup> StudyGroups { get; set; }
        public DbSet<Subject> Subjects { get; set; }

        public DataBaseContext(DbContextOptions<DataBaseContext> options) : base(options)
        {

        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Homework>().ToTable("Homeworks");
            modelBuilder.Entity<Lecture>().ToTable("Lectures");
            modelBuilder.Entity<Mark>().ToTable("Marks");
            modelBuilder.Entity<Speaker>().ToTable("Speakers");
            modelBuilder.Entity<Student>().ToTable("Students");
            modelBuilder.Entity<StudyGroup>().ToTable("StudyGroups");
            modelBuilder.Entity<Subject>().ToTable("Subjects");

            //Homeworks
            modelBuilder.Entity<Homework>()
                .HasOne(h => h.Lecture)
                .WithOne(l => l.Homework)
                .HasForeignKey<Homework>(h => h.LectureID).HasForeignKey<Homework>(h=>h.LectureID);
            modelBuilder.Entity<Homework>()
                .Navigation(h => h.Lecture)
                .UsePropertyAccessMode(PropertyAccessMode.Property);

            //Lectures
            modelBuilder.Entity<Lecture>()
                .HasOne(l => l.Homework)
                .WithOne(h => h.Lecture)
                .HasForeignKey<Homework>(h => h.LectureID);
            modelBuilder.Entity<Lecture>()
                .HasOne(l => l.StudyGroup)
                .WithMany(s => s.Lectures)
                .HasForeignKey(l => l.StudyGroupID)
                .OnDelete(DeleteBehavior.SetNull);
            modelBuilder.Entity<Lecture>()
                .HasOne(l => l.Speaker)
                .WithMany(s => s.Lectures)
                .HasForeignKey(l => l.SpeakerID)
                .OnDelete(DeleteBehavior.SetNull);
            modelBuilder.Entity<Lecture>()
                .HasOne(l => l.Subject)
                .WithMany(s => s.Lectures)
                .HasForeignKey(l => l.SubjectID)
                .OnDelete(DeleteBehavior.SetNull);
            modelBuilder.Entity<Lecture>()
                .Navigation(l => l.StudyGroup).
                UsePropertyAccessMode(PropertyAccessMode.Property);
            modelBuilder.Entity<Lecture>()
                .Navigation(l => l.Speaker).
                UsePropertyAccessMode(PropertyAccessMode.Property);
            modelBuilder.Entity<Lecture>()
                .Navigation(l => l.Subject).
                UsePropertyAccessMode(PropertyAccessMode.Property);
            modelBuilder.Entity<Lecture>()
                .Navigation(l => l.Homework).
                UsePropertyAccessMode(PropertyAccessMode.Property);

            //Marks
            modelBuilder.Entity<Mark>()
                .HasOne(m => m.Lecture)
                .WithMany(h => h.Marks)
                .HasForeignKey(m => m.LectureID);
            modelBuilder.Entity<Mark>()
                .HasOne(m => m.Student)
                .WithMany(h => h.Marks)
                .HasForeignKey(m => m.StudentID);
            modelBuilder.Entity<Mark>()
                .Navigation(m => m.Lecture)
                .UsePropertyAccessMode(PropertyAccessMode.Property);
            modelBuilder.Entity<Mark>()
                .Navigation(m => m.Student)
                .UsePropertyAccessMode(PropertyAccessMode.Property);

            //Speakers
            modelBuilder.Entity<Speaker>()
                .HasMany(s => s.Lectures)
                .WithOne(l => l.Speaker);
            modelBuilder.Entity<Speaker>()
               .Navigation(s => s.Lectures)
               .UsePropertyAccessMode(PropertyAccessMode.Property);

            //Students
            modelBuilder.Entity<Student>()
                .HasOne(s => s.StudyGroup)
                .WithMany(g => g.Students)
                .HasForeignKey(s => s.StudyGroupID)
                .OnDelete(DeleteBehavior.SetNull);
            modelBuilder.Entity<Student>()
                .HasMany(s => s.Marks)
                .WithOne(m => m.Student);
            modelBuilder.Entity<Student>()
                .Navigation(s => s.StudyGroup)
                .UsePropertyAccessMode(PropertyAccessMode.Property);
            modelBuilder.Entity<Student>()
                .Navigation(s => s.Marks)
                .UsePropertyAccessMode(PropertyAccessMode.Property);

            //StudyGroups
            modelBuilder.Entity<StudyGroup>()
               .HasMany(s => s.Lectures)
               .WithOne(l => l.StudyGroup);
            modelBuilder.Entity<StudyGroup>()
                .HasMany(s => s.Students)
                .WithOne(l => l.StudyGroup);
            modelBuilder.Entity<StudyGroup>()
                .Navigation(s => s.Lectures)
                .UsePropertyAccessMode(PropertyAccessMode.Property);
            modelBuilder.Entity<StudyGroup>()
               .Navigation(s => s.Students)
               .UsePropertyAccessMode(PropertyAccessMode.Property);

            //Subjects
            modelBuilder.Entity<Subject>()
                .HasMany(s => s.Lectures)
                .WithOne(l => l.Subject);
            modelBuilder.Entity<Subject>()
                .Navigation(s => s.Lectures)
                .UsePropertyAccessMode(PropertyAccessMode.Property);
        }
    }
}
