using System;
using System.Collections.Generic;
using System.Text;

namespace Reti.PortalePercorsi.BL.DTO
{
    public class DTOLesson
    {
        public int Id { get; set; }
        public int? IdCourse { get; set; }
        public int? IdTeacher { get; set; }
        public int? IdRoom { get; set; }
        public int? LessonNumber { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public int? IdCreator { get; set; }
        public string Creator { get; set; }
        public string CourseName { get; set; }
        public string Teacher { get; set; }
        public string RoomName { get; set; }
        public List<DTOResource> Students { get; set; }

        public DTOLesson()
        {
            Students = new List<DTOResource>();
        }
    }
}
