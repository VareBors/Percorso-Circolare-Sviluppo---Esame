using Reti.PortalePercorsi.BL.DTO;
using Reti.PortalePercorsi.BL.Mapper;
using Reti.PortalePercorsi.DAL.Entity;
using Reti.PortalePercorsi.DAL.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Text;

namespace Reti.PortalePercorsi.BL.Manager
{
    public class RoomsManager
    {
        private readonly IUnitOfWork UnitOfWork;

        public RoomsManager(IUnitOfWork unitOfWork)
        {
            this.UnitOfWork = unitOfWork;
        }

        public List<DTORoom> GetAll()
        {
            List<DTORoom> dtoRoomsList = new List<DTORoom>();

            foreach (var item in UnitOfWork.RoomRepository.GetAll())
            {
                dtoRoomsList.Add(RoomMapper.GetDTORoom(item));
            }
            return dtoRoomsList;
        }

        public DTORoom GetById(int Id)
        {
            return RoomMapper.GetDTORoom(UnitOfWork.RoomRepository.GetByID(Id));
        }

        public int Add(DTORoom dtoRoom)
        {
            Room entityRoom = RoomMapper.GetEntityRoom(dtoRoom);

            UnitOfWork.RoomRepository.Add(entityRoom);
            UnitOfWork.Commit();

            return entityRoom.Id;
        }

        public void Remove(int Id)
        {
            Room entityRoom = UnitOfWork.RoomRepository.GetByID(Id);

            UnitOfWork.RoomRepository.Delete(entityRoom);
            UnitOfWork.Commit();
        }

        public bool CanDelete(int Id)
        {
            if(UnitOfWork.RoomRepository.GetByID(Id).Lessons.Count == 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        
        public void Dispose()
        {
            UnitOfWork.Rollback();
        }
    }
}
