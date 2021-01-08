using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace Reti.PortalePercorsi.DAL.Entity
{
    public partial class PortalePercorsiContext : DbContext
    {
        public PortalePercorsiContext()
        {
        }

        public PortalePercorsiContext(DbContextOptions<PortalePercorsiContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Course> Courses { get; set; }
        public virtual DbSet<Lesson> Lessons { get; set; }
        public virtual DbSet<LessonsResource> LessonsResources { get; set; }
        public virtual DbSet<Resource> Resources { get; set; }
        public virtual DbSet<Room> Rooms { get; set; }
        public virtual DbSet<VwRandomValue> VwRandomValues { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "Latin1_General_CI_AS");

            modelBuilder.Entity<Course>(entity =>
            {
                entity.Property(e => e.Description)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(e => e.EndDate).HasColumnType("date");

                entity.Property(e => e.IdReferent).HasColumnName("Id_Referent");

                entity.Property(e => e.StartDate).HasColumnType("date");

                entity.HasOne(d => d.IdReferentNavigation)
                    .WithMany(p => p.Courses)
                    .HasForeignKey(d => d.IdReferent)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Courses_Resources");
            });

            modelBuilder.Entity<Lesson>(entity =>
            {
                entity.Property(e => e.EndDate).HasColumnType("datetime");

                entity.Property(e => e.IdCourse).HasColumnName("Id_Course");

                entity.Property(e => e.IdCreator).HasColumnName("Id_Creator");

                entity.Property(e => e.IdRoom).HasColumnName("Id_Room");

                entity.Property(e => e.IdTeacher).HasColumnName("Id_Teacher");

                entity.Property(e => e.StartDate).HasColumnType("datetime");

                entity.HasOne(d => d.IdCourseNavigation)
                    .WithMany(p => p.Lessons)
                    .HasForeignKey(d => d.IdCourse)
                    .HasConstraintName("FK_Lessons_Courses");

                entity.HasOne(d => d.IdCreatorNavigation)
                    .WithMany(p => p.LessonIdCreatorNavigations)
                    .HasForeignKey(d => d.IdCreator)
                    .HasConstraintName("FK_Lessons_Resources_Creator");

                entity.HasOne(d => d.IdRoomNavigation)
                    .WithMany(p => p.Lessons)
                    .HasForeignKey(d => d.IdRoom)
                    .HasConstraintName("FK_Lessons_Rooms");

                entity.HasOne(d => d.IdTeacherNavigation)
                    .WithMany(p => p.LessonIdTeacherNavigations)
                    .HasForeignKey(d => d.IdTeacher)
                    .HasConstraintName("FK_Lessons_Resources_Teacher");
            });

            modelBuilder.Entity<LessonsResource>(entity =>
            {
                entity.HasKey(e => new { e.IdLesson, e.IdStudent });

                entity.Property(e => e.IdLesson).HasColumnName("Id_Lesson");

                entity.Property(e => e.IdStudent).HasColumnName("Id_Student");

                entity.HasOne(d => d.IdLessonNavigation)
                    .WithMany(p => p.LessonsResources)
                    .HasForeignKey(d => d.IdLesson)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_LessonsResources_Lessons");

                entity.HasOne(d => d.IdStudentNavigation)
                    .WithMany(p => p.LessonsResources)
                    .HasForeignKey(d => d.IdStudent)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_LessonsResources_Resources");
            });

            modelBuilder.Entity<Resource>(entity =>
            {
                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.Email).HasMaxLength(100);

                entity.Property(e => e.LastName).HasMaxLength(50);

                entity.Property(e => e.Name).HasMaxLength(50);

                entity.Property(e => e.Username).HasMaxLength(10);
            });

            modelBuilder.Entity<Room>(entity =>
            {
                entity.Property(e => e.Name).HasMaxLength(50);
            });

            modelBuilder.Entity<VwRandomValue>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("vw_RandomValue");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
