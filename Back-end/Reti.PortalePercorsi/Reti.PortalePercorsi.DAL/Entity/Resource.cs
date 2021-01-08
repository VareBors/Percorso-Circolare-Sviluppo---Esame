using System;
using System.Collections.Generic;

#nullable disable

namespace Reti.PortalePercorsi.DAL.Entity
{
    public partial class Resource
    {
        public Resource()
        {
            Courses = new HashSet<Course>();
            LessonIdCreatorNavigations = new HashSet<Lesson>();
            LessonIdTeacherNavigations = new HashSet<Lesson>();
            LessonsResources = new HashSet<LessonsResource>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Username { get; set; }

        public virtual ICollection<Course> Courses { get; set; }
        public virtual ICollection<Lesson> LessonIdCreatorNavigations { get; set; }
        public virtual ICollection<Lesson> LessonIdTeacherNavigations { get; set; }
        public virtual ICollection<LessonsResource> LessonsResources { get; set; }
    }
}
