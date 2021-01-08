using System;
using System.Collections.Generic;

#nullable disable

namespace Reti.PortalePercorsi.DAL.Entity
{
    public partial class LessonsResource
    {
        public int IdLesson { get; set; }
        public int IdStudent { get; set; }

        public virtual Lesson IdLessonNavigation { get; set; }
        public virtual Resource IdStudentNavigation { get; set; }
    }
}
