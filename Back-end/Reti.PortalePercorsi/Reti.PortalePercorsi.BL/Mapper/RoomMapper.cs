using Reti.PortalePercorsi.BL.DTO;
using Reti.PortalePercorsi.DAL.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace Reti.PortalePercorsi.BL.Mapper
{
    public static class RoomMapper
    {
        public static DTORoom GetDTORoom(Room entityRoom)
        {
            DTORoom dtoRoom = new DTORoom()
            {
                Bookable = entityRoom.Bookable,
                Id = entityRoom.Id,
                Name = entityRoom.Name,
                Places = entityRoom.Places,
                RoomNumber = entityRoom.RoomNumber
            };
            return dtoRoom;
        }
        public static Room GetEntityRoom(DTORoom dtoRoom)
        {
            Room entityRoom = new Room()
            {
                Bookable = dtoRoom.Bookable,
                Id = dtoRoom.Id,
                Name = dtoRoom.Name,
                Places = dtoRoom.Places,
                RoomNumber = dtoRoom.RoomNumber
            };
            return entityRoom;
        }
    }
}
