using EduTest.Application.Dtos;
using EduTest.Application.JwtTokenSerives;
using EduTest.Application.MediatR.Commands.AuthCommands;
using EduTest.DataAccess.Repositories.IRepository;
using EduTest.DataAccess.Repositories.Repository;
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
    public class LoginHandler : IRequestHandler<LoginCommand, LoginResponse>
    {
        private readonly IStudentRepository _studentRepository;
        private readonly ITeacherRepository _teacherRepository;
        private readonly IJwtTokenService _jwtTokenService;
        private readonly IValidator<LoginDto> _validator;

        public LoginHandler(IStudentRepository studentRepository,ITeacherRepository teacherRepository, IJwtTokenService jwtTokenService, IValidator<LoginDto> validator)
        {
            _studentRepository = studentRepository;
            _teacherRepository= teacherRepository;
            _jwtTokenService = jwtTokenService;
            _validator = validator;
        }

        public async Task<LoginResponse> Handle(LoginCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var ValidateRes=_validator.Validate(request.LoginDto);
                if(!ValidateRes.IsValid) 
                    throw new ValidationException(ValidateRes.Errors.First().ErrorMessage);

                var teacher = await _teacherRepository.GetByUserNameAsync(request.LoginDto.UserName);
                if (teacher != null)
                {
                    var hash = GetHashedPassword(request.LoginDto.Password, teacher.PasswordSalt);
                    if (hash == teacher.PasswordHash)
                    {
                        return new LoginResponse
                        {
                            Id=teacher.Id,
                            Token = _jwtTokenService.GenerateToken(teacher.UserName),
                            Username = teacher.UserName,
                            Role = teacher.Role.ToString(),
                            Result = "Success"
                        };
                    }

                    return Fail("Noto'g'ri parol (teacher)");
                }


                var student = await _studentRepository.GetByUserNameAsync(request.LoginDto.UserName);
                if (student != null)
                {
                    var hash = GetHashedPassword(request.LoginDto.Password, student.PasswordSalt);
                    if (hash == student.PasswordHash)
                    {
                        return new LoginResponse
                        {
                            Id=student.Id,
                            Token = _jwtTokenService.GenerateToken(student.UserName),
                            Username = student.UserName,
                            Role = student.Role.ToString(),
                            Result = "Success"
                        };
                    }

                    return Fail("Noto'g'ri parol (student)");
                }

                
                return Fail("User topilmadi");
            }
            catch (Exception ex)
            {
                return Fail($"Login failed: {ex.Message}");
            }
        }

        private string GetHashedPassword(string password, string saltBase64)
        {
            var salt = Convert.FromBase64String(saltBase64);
            using var hmac = new HMACSHA512(salt);
            var hashBytes = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
            return Convert.ToBase64String(hashBytes);
        }
        private LoginResponse Fail(string reason) => new()
        {
            Id =0,
            Token = "null",
            Username = "null",
            Role = "null",
            Result = reason
        };
    }
}
