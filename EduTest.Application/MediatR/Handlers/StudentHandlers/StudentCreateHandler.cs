using EduTest.Application.Dtos.Students;
using EduTest.Application.MediatR.Commands.StudentCommands;
using EduTest.DataAccess.Repositories.IRepository;
using EduTest.Domain.Models;
using FluentValidation;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace EduTest.Application.MediatR.Handlers.StudentHandlers
{
    public class StudentCreateHandler : IRequestHandler<StudentCreateCommand, bool>
    {
        private readonly IStudentRepository _studentRepository;
        private readonly IValidator<StudentCreateDto> _validator;
        public StudentCreateHandler(IStudentRepository studentRepository, IValidator<StudentCreateDto> validator)
        {
            _studentRepository = studentRepository;
            _validator = validator;
        }
        public async Task<bool> Handle(StudentCreateCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var ValidateRes= _validator.Validate(request.StudentCreateDto);
                if(!ValidateRes.IsValid)
                    throw new ValidationException(ValidateRes.Errors.First().ErrorMessage);

                var saltBytes = RandomNumberGenerator.GetBytes(32);

                var salt = Convert.ToBase64String(saltBytes);
                var passwordHashBytes = HashPassword(request.StudentCreateDto.Password, saltBytes);
                var passwordHash = Convert.ToBase64String(passwordHashBytes);

                var student = new Student()
                {
                    FirstName = request.StudentCreateDto.FirstName,
                    LastName = request.StudentCreateDto.LastName,
                    PhoneNumber = request.StudentCreateDto.PhoneNumber, 
                    Role = request.StudentCreateDto.Role,

                    GroupId = request.StudentCreateDto.GroupId,
                    TeacherId=request.StudentCreateDto.TeacherId,
                    UserName = request.StudentCreateDto.UserName,
                    PasswordHash = passwordHash,
                    PasswordSalt = salt,

                };

                await _studentRepository.AddAsync(student);
                return true;

            }
            catch(Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
        }

        private byte[] HashPassword(string password, byte[] salt)
        {
            using var hmac = new HMACSHA512(salt);
            return hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
        }
    }
}
