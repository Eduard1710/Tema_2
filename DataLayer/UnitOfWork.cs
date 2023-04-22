using DataLayer.Repositories;

namespace DataLayer
{
    public class UnitOfWork
    {
        public StudentsRepository Students { get; }
        public ClassRepository Classes { get; }
        public UserRepository Users { get; }
        public RoleRepository Roles { get; }
        public GradeRepository Grades { get; }

        private readonly AppDbContext _dbContext;

        public UnitOfWork
        (
            AppDbContext dbContext,
            StudentsRepository studentsRepository,
            ClassRepository classes,
            UserRepository users,
            RoleRepository roles,
            GradeRepository grades
        )
        {
            _dbContext = dbContext;
            Students = studentsRepository;
            Classes = classes;
            Users = users;
            Roles = roles;
            Grades = grades;
        }

        public void SaveChanges()
        {
            try
            {
                _dbContext.SaveChanges();
            }
            catch (Exception exception)
            {
                var errorMessage = "Error when saving to the database: "
                    + $"{exception.Message}\n\n"
                    + $"{exception.InnerException}\n\n"
                    + $"{exception.StackTrace}\n\n";

                Console.WriteLine(errorMessage);
            }
        }
    }
}
