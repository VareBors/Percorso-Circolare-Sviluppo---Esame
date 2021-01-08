using System;
using System.Collections.Generic;

#nullable disable

namespace Reti.PortalePercorsi.DAL.Entity
{
    public partial class Room
    {
        public Room()
        {
            Lessons = new HashSet<Lesson>();
        }

        public int Id { get; set; }
        public int? RoomNumber { get; set; }
        public string Name { get; set; }
        public int? Places { get; set; }
        public bool? Bookable { get; set; }

        public virtual ICollection<Lesson> Lessons { get; set; }
    }
}
