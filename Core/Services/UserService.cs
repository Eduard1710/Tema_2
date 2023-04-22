using DataLayer;
using DataLayer.Dtos;
using DataLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Services
{
    public class UserService
    {
        private readonly UnitOfWork unitOfWork;

        private AuthorizationService authService { get; set; }

        public UserService(UnitOfWork unitOfWork, AuthorizationService authService)
        {
            this.unitOfWork = unitOfWork;
            this.authService = authService;
        }

        public void Register(RegisterDto registerData)
        {
            if (registerData == null)
            {
                return;
            }

            var hashedPassword = authService.HashPassword(registerData.Password);
            var user = new User
            {
                Email = registerData.Email,
                PasswordHash = hashedPassword,
                RoleId = registerData.RoleId
            };
            unitOfWork.Users.Insert(user);
            unitOfWork.SaveChanges();

            switch (unitOfWork.Roles.GetById(registerData.RoleId).Name)
            {
                case "Student":
                    var student = new Student
                    {
                        FirstName = registerData.FirstName,
                        LastName = registerData.LastName,
                        ClassId = registerData.ClassId,
                        UserId = user.Id
                    };

                    unitOfWork.Students.Insert(student);
                    unitOfWork.SaveChanges();
                    break;
                default: break;
            }

        }

        public string Validate(LoginDto payload)
        {
            var user = unitOfWork.Users.GetByEmail(payload.Email);

            var passwordFine = authService.VerifyHashedPassword(user.PasswordHash, payload.Password);

            if (passwordFine)
            {
                var role = GetRole(user);

                return authService.GetToken(user, role);
            }
            else
            {
                return null;
            }

        }
        public string GetRole(User user)
        {
            return unitOfWork.Roles.GetById(user.RoleId).Name;
        }

        public int GetStudentIdByUserId(int userId)
        {
            return unitOfWork.Users.GetStudentIdByUserId(userId);
        }
    }
}
