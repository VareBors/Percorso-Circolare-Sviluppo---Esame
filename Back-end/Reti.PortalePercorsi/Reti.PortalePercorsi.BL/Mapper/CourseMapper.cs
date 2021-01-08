using Reti.PortalePercorsi.BL.DTO;
using Reti.PortalePercorsi.DAL.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Reti.PortalePercorsi.BL.Mapper
{
    public static class CourseMapper
    {
        public static DTOCourse GetDTOCourse (Course entityCourse)
        {
            DTOCourse dtoCourse = new DTOCourse()
            {
                Description = entityCourse.Description,
                EndDate = entityCourse.EndDate,
                Id = entityCourse.Id,
                IdReferent = entityCourse.IdReferent,
                Lessons = (from lesson in entityCourse.Lessons select LessonMapper.GetDTOLesson(lesson)).ToList(),
                StartDate = entityCourse.StartDate,
                Referent = $"{entityCourse.IdReferentNavigation.Name} {entityCourse.IdReferentNavigation.LastName}",
                ReferenceYear = entityCourse.ReferenceYear
            };

            return dtoCourse;
        }

        public static Course GetEntityCourse (DTOCourse dtoCourse)
        {
            Course entityCourse = new Course()
            {
                Description = dtoCourse.Description,
                EndDate = dtoCourse.EndDate,
                Id = dtoCourse.Id,
                IdReferent = dtoCourse.IdReferent,
                StartDate = dtoCourse.StartDate,
                ReferenceYear = dtoCourse.ReferenceYear
            };

            return entityCourse;
        }
    }
}