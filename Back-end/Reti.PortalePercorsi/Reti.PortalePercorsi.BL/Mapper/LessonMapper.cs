using Reti.PortalePercorsi.BL.DTO;
using Reti.PortalePercorsi.DAL.Entity;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace Reti.PortalePercorsi.BL.Mapper
{
    public static class LessonMapper
    {
        public static DTOLesson GetDTOLesson (Lesson entityLesson)
        {
            DTOLesson dtoLesson = new DTOLesson()
            {
                EndDate = entityLesson.EndDate,
                Id = entityLesson.Id,
                IdCourse = entityLesson.IdCourse,
                IdRoom = entityLesson.IdRoom,
                IdTeacher = entityLesson.IdTeacher,
                LessonNumber = entityLesson.LessonNumber,
                StartDate = entityLesson.StartDate,
                Teacher = entityLesson.IdTeacherNavigation == null ? string.Empty : $"{entityLesson.IdTeacherNavigation.Name} {entityLesson.IdTeacherNavigation.LastName}",
                CourseName = entityLesson.IdCourseNavigation == null ? string.Empty  : entityLesson.IdCourseNavigation.Description,
                RoomName = entityLesson.IdRoomNavigation == null ? string.Empty : entityLesson.IdRoomNavigation.Name,
                Students = (from r in entityLesson.LessonsResources select ResourceMapper.GetDTOResources(r.IdStudentNavigation)).ToList(),
                IdCreator = entityLesson.IdCreator,
                Creator = entityLesson.IdCreatorNavigation == null ? string.Empty: $"{entityLesson.IdCreatorNavigation.Name} {entityLesson.IdCreatorNavigation.LastName}",
            };

            return dtoLesson;
        }
        public static Lesson GetEntityLesson(DTOLesson dtoLesson)
        {
            Lesson entityLesson = new Lesson()
            {
                EndDate = dtoLesson.EndDate,
                Id = dtoLesson.Id,
                IdCourse = dtoLesson.IdCourse,
                IdRoom = dtoLesson.IdRoom,
                IdTeacher = dtoLesson.IdTeacher,
                LessonNumber = dtoLesson.LessonNumber,
                StartDate = dtoLesson.StartDate,
                IdCreator = dtoLesson.IdCreator
            };
            return entityLesson;
        }
    }
}
