using Core.Dtos;
using DataLayer;
using DataLayer.Entities;
using DataLayer.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Services
{
    public class GradeService
    {
        private readonly UnitOfWork unitOfWork;
        public GradeService(UnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        public List<Grade> GetStudentGrades(int studentId)
        {
            return unitOfWork.Grades.GetStudentGrades(studentId);
        }

        public GradeAddDto AddGrade(GradeAddDto payload)
        {
            if (payload == null) return null;

            var existingStudent = unitOfWork.Students.GetById(payload.StudentId);
            if (existingStudent == null) return null;

            var newGrade = new Grade
            {
                StudentId = payload.StudentId,
                Value = payload.Value,
                Course = payload.Course,
                DateCreated = payload.DateCreated
            };

            unitOfWork.Grades.Insert(newGrade);
            unitOfWork.SaveChanges();

            return payload;
        }

        public List<Grade> GetAllGrades()
        {
            return unitOfWork.Grades.GetAll();
        }
    }
}
