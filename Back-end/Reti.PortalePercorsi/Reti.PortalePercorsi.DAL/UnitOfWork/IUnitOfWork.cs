using Reti.PortalePercorsi.DAL.Entity;
using Reti.PortalePercorsi.DAL.Repository;
using System;
using System.Collections.Generic;
using System.Text;

namespace Reti.PortalePercorsi.DAL.UnitOfWork
{
    public interface IUnitOfWork
    {
        IRepository<Course> CourseRepository { get; }
        IRepository<Lesson> LessonRepository { get; }
        IRepository<LessonsResource> LessonsResourceRepository { get; }
        IRepository<Resource> ResourceRepository { get; }
        IRepository<Room> RoomRepository { get; }

        void Commit();
        void Rollback();
        int GetResourceId();
    }
}
