using Microsoft.EntityFrameworkCore;
using Reti.PortalePercorsi.DAL.Entity;
using Reti.PortalePercorsi.DAL.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Reti.PortalePercorsi.DAL.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork, IDisposable
    {
        private PortalePercorsiContext dbContext;
        private IRepository<Course> courseRepository;
        private IRepository<Lesson> lessonRepository;
        private IRepository<LessonsResource> lessonsResourceRepository;
        private IRepository<Resource> resourceRepository;
        private IRepository<Room> roomrepository;

        public IRepository<Course> CourseRepository => courseRepository = courseRepository ?? new Repository<Course>(dbContext);
        public IRepository<Lesson> LessonRepository => lessonRepository = lessonRepository ?? new Repository<Lesson>(dbContext);
        public IRepository<LessonsResource> LessonsResourceRepository => lessonsResourceRepository = lessonsResourceRepository ?? new Repository<LessonsResource>(dbContext);
        public IRepository<Resource> ResourceRepository => resourceRepository = resourceRepository ?? new Repository<Resource>(dbContext);
        public IRepository<Room> RoomRepository => roomrepository = roomrepository ?? new Repository<Room>(dbContext);

        public UnitOfWork(PortalePercorsiContext context)
        {
            this.dbContext = context;
        }

        public void Commit()
        {
            dbContext.SaveChanges();
        }

        public void Dispose()
        {
            dbContext.Dispose();   
        }

        public void Rollback()
        {
            dbContext.Dispose();
        }

        public int GetResourceId()
        {
            //Codice per l'utilizzo della funzion dbo.GetResourceNumber per ottenere il numero matricola randomico ed univoco (nella fuction viene eseguito il controllo per l'unicità)
            int Id = dbContext.Resources.FromSqlRaw("SELECT TOP (1) dbo.GetResourceId() as Id,'' as Name,'' as LastName,'' as Username,'' as Email FROM dbo.Resources")
            .ToList()[0].Id;
            dbContext.Resources.Local.ToList().ForEach(p => dbContext.Entry(p).State = EntityState.Detached);
            return Id;
        }
    }
}
