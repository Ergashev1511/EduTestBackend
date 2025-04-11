using EduTest.Application.Dtos.Students;
using EduTest.Application.JwtTokenSerives;
using EduTest.Application.MediatR.Commands.AuthCommands;
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

namespace EduTest.Application.MediatR.Handlers.AuthHandlers
{
    public class StudentLoginHandler : IRequestHandler<StudentLoginCommand, string>
    {
        private readonly IStudentRepository _studentRepository;
        private readonly IJwtTokenService _jwtTokenService;
        private readonly IValidator<StudentLoginDto> _validator;

        public StudentLoginHandler(IStudentRepository studentRepository,IJwtTokenService jwtTokenService, IValidator<StudentLoginDto> validator)
        {
            _studentRepository = studentRepository; 
            _jwtTokenService = jwtTokenService;
            _validator = validator;
        }


        public async Task<string> Handle(StudentLoginCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var validationResult=_validator.Validate(request.StudentLoginDto);
                if(!validationResult.IsValid)
                {
                    throw new ValidationException(validationResult.Errors.First().ErrorMessage);
                }

                var student=await _studentRepository.GetByUserNameAsync(request.StudentLoginDto.UserName);
                if (student == null)
                    throw new Exception("This is User not found!");

                var saltBytes = Convert.FromBase64String(student.PasswordSalt);
                var hashBytes = HashPassword(request.StudentLoginDto.Password, saltBytes);
                var hash = Convert.ToBase64String(hashBytes);

                if (hash != student.PasswordHash)
                    throw new Exception("Noto'g'ri parol");

                return _jwtTokenService.GenerateToken(student.UserName);

            }
            catch (Exception ex)
            {
                return $"Registration failed: {ex.Message}";
            }
        }
        private byte[] HashPassword(string password, byte[] salt)
        {
            using var hmac = new HMACSHA512(salt);
            return hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
        }
    }
}
