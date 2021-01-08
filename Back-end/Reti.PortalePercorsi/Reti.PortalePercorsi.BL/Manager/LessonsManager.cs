using Reti.PortalePercorsi.BL.DTO;
using Reti.PortalePercorsi.BL.Mapper;
using Reti.PortalePercorsi.DAL.Entity;
using Reti.PortalePercorsi.DAL.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Reti.PortalePercorsi.BL.Manager
{
    public class LessonsManager
    {
        private readonly IUnitOfWork UnitOfWork;

        public LessonsManager(IUnitOfWork unitOfWork)
        {
            this.UnitOfWork = unitOfWork;
        }

        public List<DTOLesson> GetAll()
        {
            List<DTOLesson> LessonsList = new List<DTOLesson>();

            foreach (Lesson item in UnitOfWork.LessonRepository.GetAll())
            {
                LessonsList.Add(LessonMapper.GetDTOLesson(item));
            }

            return LessonsList;
        }

        public DTOLesson GetById(int Id)
        {
            return LessonMapper.GetDTOLesson(UnitOfWork.LessonRepository.GetByID(Id));
        }

        public int Add(DTOLesson dtoLesson, out string ErrorText)
        {
            ErrorText = "";
            Lesson entityLesson = LessonMapper.GetEntityLesson(dtoLesson);


            //Controlli da effettuare prima di salvare la lezione
            //Controllo se ci sono già lezioni dello stesso percorso in quel giorno
            if(UnitOfWork.LessonRepository.GetAll().Where(lesson => lesson.IdCourse == entityLesson.IdCourse && (lesson.StartDate.GetValueOrDefault().Date == entityLesson.StartDate.GetValueOrDefault().Date)).Count() > 0)
            {
                ErrorText = "Impossibile salvare la lezione. Un'altra lezione per questo percorso è organizzata per quella giornata.";
                return -1;
            }

            //Controllo se la stanza  è già stata prenotata quel giorno
            if(UnitOfWork.LessonRepository.GetAll().Where(lesson => lesson.IdRoom == dtoLesson.IdRoom && (lesson.StartDate.GetValueOrDefault().Date == entityLesson.StartDate.GetValueOrDefault().Date)).Count() > 0)
            {
                ErrorText = "Impossibile salvare la lezione. La sala scelta è già occupata da un'altra lezione in quella data.";
                return -1;
            }

            UnitOfWork.LessonRepository.Add(entityLesson);
            UnitOfWork.Commit();

            foreach (DTOResource item in dtoLesson.Students)
            {
                UnitOfWork.LessonsResourceRepository.Add(new LessonsResource
                {
                    IdLesson = entityLesson.Id,
                    IdStudent = item.Id
                });
            }

            UnitOfWork.Commit();
            return entityLesson.Id;
        }
        public int Update(DTOLesson dtoLesson, out string ErrorText)
        {
            Lesson entityLesson = LessonMapper.GetEntityLesson(dtoLesson);
            ErrorText = "";

            //Controlli da effettuare prima di salvare la lezione
            //Controllo se ci sono già lezioni dello stesso percorso in quel giorno escludendo se stessa
            if (UnitOfWork.LessonRepository.GetAll().Where(lesson => lesson.Id != entityLesson.Id && lesson.IdCourse == entityLesson.IdCourse && (lesson.StartDate.GetValueOrDefault().Date == entityLesson.StartDate.GetValueOrDefault().Date)).Count() > 0)
            {
                ErrorText = "Impossibile salvare la lezione. Un'altra lezione per questo percorso è organizzata per quella giornata.";
                return -1;
            }

            //Controllo se la stanza  è già stata prenotata quel giorno escludendo se stessa
            if (UnitOfWork.LessonRepository.GetAll().Where(lesson => lesson.Id != entityLesson.Id && lesson.IdRoom == dtoLesson.IdRoom && (lesson.StartDate.GetValueOrDefault().Date == entityLesson.StartDate.GetValueOrDefault().Date)).Count() > 0)
            {
                ErrorText = "Impossibile salvare la lezione. La sala scelta è già occupata da un'altra lezione in quella data.";
                return -1;
            }

            //Prima elimino tutte le relazioni tra lezioni e studenti
            foreach (LessonsResource item in UnitOfWork.LessonsResourceRepository.GetAll().Where(link => link.IdLesson == dtoLesson.Id))
            {
                UnitOfWork.LessonsResourceRepository.Delete(item);
            }

            foreach (DTOResource item in dtoLesson.Students)
            {
                UnitOfWork.LessonsResourceRepository.Add(new LessonsResource
                {
                    IdLesson = entityLesson.Id,
                    IdStudent = item.Id
                });
            }

            UnitOfWork.LessonRepository.Update(entityLesson);
            UnitOfWork.Commit();

            return entityLesson.Id;
        }

        public void Remove(int Id)
        {
            Lesson entityLesson = UnitOfWork.LessonRepository.GetByID(Id);

            foreach (LessonsResource item in UnitOfWork.LessonsResourceRepository.GetAll().Where(link => link.IdLesson == Id))
            {
                UnitOfWork.LessonsResourceRepository.Delete(item);
            }

            UnitOfWork.LessonRepository.Delete(entityLesson);
            UnitOfWork.Commit();
        }

        public void Dispose()
        {
            UnitOfWork.Rollback();
        }
    }
}
