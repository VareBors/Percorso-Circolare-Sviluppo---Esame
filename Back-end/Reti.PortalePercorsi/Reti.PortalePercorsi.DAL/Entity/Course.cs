using System;
using System.Collections.Generic;

#nullable disable

namespace Reti.PortalePercorsi.DAL.Entity
{
    public partial class Course
    {
        public Course()
        {
            Lessons = new HashSet<Lesson>();
        }

        public int Id { get; set; }
        public string Description { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public int IdReferent { get; set; }
        public int ReferenceYear { get; set; }

        public virtual Resource IdReferentNavigation { get; set; }
        public virtual ICollection<Lesson> Lessons { get; set; }
    }
}
