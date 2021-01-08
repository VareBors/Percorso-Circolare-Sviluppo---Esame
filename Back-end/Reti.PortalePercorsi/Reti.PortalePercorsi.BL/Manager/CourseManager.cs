using Reti.PortalePercorsi.BL.DTO;
using Reti.PortalePercorsi.BL.Mapper;
using Reti.PortalePercorsi.DAL.Entity;
using Reti.PortalePercorsi.DAL.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Text;

namespace Reti.PortalePercorsi.BL.Manager
{
    public class CourseManager
    {
        private readonly IUnitOfWork UnitOfWork;

        public CourseManager(IUnitOfWork unitOfWork)
        {
            this.UnitOfWork = unitOfWork;
        }

        public List<DTOCourse> GetAll()
        {
            List<DTOCourse> Courses = new List<DTOCourse>();

            foreach (Course item in UnitOfWork.CourseRepository.GetAll())
            {
                Courses.Add(CourseMapper.GetDTOCourse(item));
            }

            return Courses;
        }

        public DTOCourse GetById(int Id)
        {
           return CourseMapper.GetDTOCourse(UnitOfWork.CourseRepository.GetByID(Id));
        }
        public int Add(DTOCourse dtoCourse)
        {
            Course entytiCourse = CourseMapper.GetEntityCourse(dtoCourse);
            UnitOfWork.CourseRepository.Add(entytiCourse);
            UnitOfWork.Commit();

            return entytiCourse.Id;
            
        }
        public int Edit(DTOCourse dtoCourse)
        {
            Course entityCourse = CourseMapper.GetEntityCourse(dtoCourse);
            UnitOfWork.CourseRepository.Update(entityCourse);
            UnitOfWork.Commit();

            return entityCourse.Id;
        }

        public void Remove(int Id)
        {
            Course entityCourse = UnitOfWork.CourseRepository.GetByID(Id);

            foreach (Lesson lesson in entityCourse.Lessons)
            {
                foreach(LessonsResource lessonLink in lesson.LessonsResources)
                {
                    UnitOfWork.LessonsResourceRepository.Delete(lessonLink);
                }

                UnitOfWork.LessonRepository.Delete(lesson);
            }

            UnitOfWork.CourseRepository.Delete(entityCourse);
            UnitOfWork.Commit();
        }

        public void Dispose()
        {
            UnitOfWork.Rollback();
        }
    }
}
