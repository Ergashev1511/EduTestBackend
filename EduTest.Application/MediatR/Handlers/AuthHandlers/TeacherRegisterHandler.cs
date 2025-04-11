using EduTest.Application.Dtos.Teachers;
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
    public class TeacherRegisterHandler : IRequestHandler<TeacherRegisterCommand, string>
    {
        private readonly ITeacherRepository _teacherRepository;
        private readonly IValidator<TeacherCreateDto> _validator;
        private readonly IJwtTokenService _jwtTokenService;
        public TeacherRegisterHandler(ITeacherRepository teacherRepository,IValidator<TeacherCreateDto> validator,IJwtTokenService jwtTokenService)
        {
            _teacherRepository = teacherRepository; 
            _validator = validator;
            _jwtTokenService = jwtTokenService;
        }

        public async Task<string> Handle(TeacherRegisterCommand request, CancellationToken cancellationToken)
        {

            try
            {

                var validationResult = _validator.Validate(request.TeacherCreateDto);
                if (!validationResult.IsValid)
                {
                    throw new ValidationException(validationResult.Errors.First().ErrorMessage);
                }

                var saltBytes = RandomNumberGenerator.GetBytes(32);

                var salt = Convert.ToBase64String(saltBytes);
                var passwordHashBytes = HashPassword(request.TeacherCreateDto.Password, saltBytes);
                var passwordHash = Convert.ToBase64String(passwordHashBytes);

                Teacher teacher = new Teacher()
                {
                    FirstName = request.TeacherCreateDto.FirstName,
                    LastName = request.TeacherCreateDto.LastName,
                    PhoneNumber = request.TeacherCreateDto.PhoneNumber,
                    UserName = request.TeacherCreateDto.UserName,
                    PasswordHash = passwordHash,
                    PasswordSalt = salt,
                    Role = request.TeacherCreateDto.Role,
                };

                await _teacherRepository.AddAsync(teacher);

                return _jwtTokenService.GenerateToken(request.TeacherCreateDto.UserName); ;

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
