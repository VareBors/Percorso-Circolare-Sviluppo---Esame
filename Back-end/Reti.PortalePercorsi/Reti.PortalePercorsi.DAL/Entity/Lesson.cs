using System;
using System.Collections.Generic;

#nullable disable

namespace Reti.PortalePercorsi.DAL.Entity
{
    public partial class Lesson
    {
        public Lesson()
        {
            LessonsResources = new HashSet<LessonsResource>();
        }

        public int Id { get; set; }
        public int? IdCourse { get; set; }
        public int? IdTeacher { get; set; }
        public int? IdRoom { get; set; }
        public int? LessonNumber { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public int? IdCreator { get; set; }

        public virtual Course IdCourseNavigation { get; set; }
        public virtual Resource IdCreatorNavigation { get; set; }
        public virtual Room IdRoomNavigation { get; set; }
        public virtual Resource IdTeacherNavigation { get; set; }
        public virtual ICollection<LessonsResource> LessonsResources { get; set; }
    }
}
