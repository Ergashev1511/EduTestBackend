using EduTest.Application.MediatR.Commands.TeacherCommands;
using EduTest.DataAccess.Repositories.IRepository;
using EduTest.Domain.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace EduTest.Application.MediatR.Handlers.TeacherHandlers
{
    public class TeacherUpdateHandler : IRequestHandler<TeacherUpdateCommand, bool>
    {
        private readonly ITeacherRepository _repository;
        public TeacherUpdateHandler(ITeacherRepository repository)
        {
            _repository = repository;   
        }
        public async Task<bool> Handle(TeacherUpdateCommand request, CancellationToken cancellationToken)
        {
            try
            {
                if (request == null || request.Id <= 0)
                    return false;

                var saltBytes = RandomNumberGenerator.GetBytes(32);

                var salt = Convert.ToBase64String(saltBytes);
                var passwordHashBytes = HashPassword(request.TeacherUpdateDto.Password, saltBytes);
                var passwordHash = Convert.ToBase64String(passwordHashBytes);

                var teacher = new Teacher()
                {
                    FirstName=request.TeacherUpdateDto.FirstName,
                    LastName=request.TeacherUpdateDto.LastName,
                    PhoneNumber=request.TeacherUpdateDto.PhoneNumber,

                    UserName=request.TeacherUpdateDto.UserName,
                    PasswordHash=passwordHash,
                    PasswordSalt=salt,

                };

                await _repository.UpdateAsync(request.Id, teacher);
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
