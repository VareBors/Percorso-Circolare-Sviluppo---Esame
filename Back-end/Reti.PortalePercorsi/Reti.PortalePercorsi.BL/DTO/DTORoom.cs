using System;
using System.Collections.Generic;
using System.Text;

namespace Reti.PortalePercorsi.BL.DTO
{
    public class DTORoom
    {
        public int Id { get; set; }
        public int? RoomNumber { get; set; }
        public string Name { get; set; }
        public int? Places { get; set; }
        public bool? Bookable { get; set; }
    }
}
