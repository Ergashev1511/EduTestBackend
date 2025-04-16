using EduTest.Application.MediatR.Commands.StudentCommands;
using EduTest.DataAccess.Repositories.IRepository;
using EduTest.Domain.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace EduTest.Application.MediatR.Handlers.StudentHandlers
{
    public class StudentUpdateHandler : IRequestHandler<StudentUpdateCommand, bool>
    {
        private readonly IStudentRepository _studentRepository;
        public StudentUpdateHandler(IStudentRepository studentRepository)
        {
            _studentRepository = studentRepository; 
        }
        public async Task<bool> Handle(StudentUpdateCommand request, CancellationToken cancellationToken)
        {
            try
            {
                if(request==null || request.Id<=0)
                    return false;


                var saltBytes = RandomNumberGenerator.GetBytes(32);

                var salt = Convert.ToBase64String(saltBytes);
                var passwordHashBytes = HashPassword(request.StudentUpdateDto.Password, saltBytes);
                var passwordHash = Convert.ToBase64String(passwordHashBytes);
                var student = new Student()
                {
                    FirstName=request.StudentUpdateDto.FirstName,
                    LastName=request.StudentUpdateDto.LastName, 
                    PhoneNumber=request.StudentUpdateDto.PhoneNumber,
                    
                    UserName=request.StudentUpdateDto.UserName,
                    PasswordHash=passwordHash,
                    PasswordSalt=salt,
                };
                await _studentRepository.UpdateAsync(request.Id, student);
                return true;
            }
            catch
            {
                return false;
            }
        }

        private byte[] HashPassword(string password, byte[] salt)
        {
            using var hmac = new HMACSHA512(salt);
            return hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
        }
    }
}
