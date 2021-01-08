using System;
using System.Collections.Generic;
using System.Text;

namespace Reti.PortalePercorsi.BL.DTO
{
    public class DTOCourse
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public int IdReferent { get; set; }
        public string Referent { get; set; }
        public int ReferenceYear { get; set; }
        public List<DTOLesson> Lessons { get; set; }
    }
}
