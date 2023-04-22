using DataLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.Repositories
{
    public class GradeRepository : RepositoryBase<Grade>
    {
        public GradeRepository(AppDbContext dbContext) : base(dbContext)
        {
        }

        public List<Grade> GetStudentGrades(int studentId)
        {
            return _dbContext.Grades.Where(g => g.StudentId == studentId).ToList();
        }

    }
}
