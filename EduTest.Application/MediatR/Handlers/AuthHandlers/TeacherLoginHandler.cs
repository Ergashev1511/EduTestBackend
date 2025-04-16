using EduTest.Application.Common.Validators;
using EduTest.Application.Dtos;
using EduTest.Application.Dtos.Teachers;
using EduTest.Application.JwtTokenSerives;
using EduTest.Application.MediatR.Commands.AuthCommands;
using EduTest.DataAccess.Repositories.IRepository;
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
    public class TeacherLoginHandler : IRequestHandler<TeacherLoginCommand, LoginRequst>
    {
        private readonly ITeacherRepository _teacherRepository;
        private readonly IJwtTokenService _jwtTokenService;
        private readonly IValidator<TeacherLoginDto> _validator;
        public TeacherLoginHandler(ITeacherRepository teacherRepository, IJwtTokenService jwtTokenService, IValidator<TeacherLoginDto> validator)
        {
            _teacherRepository = teacherRepository;
            _jwtTokenService = jwtTokenService;
            _validator = validator;
        }

        public async Task<LoginRequst> Handle(TeacherLoginCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var validationResult = _validator.Validate(request.TeacherLoginDto);
                if(!validationResult.IsValid)
                {
                    throw new ValidationException(validationResult.Errors.First().ErrorMessage);
                }
                
                var teacher=await _teacherRepository.GetByUserNameAsync(request.TeacherLoginDto.UserName);
                if (teacher == null)
                {
                    return new LoginRequst()
                    {
                        Token = "null",
                        Username = "null",
                        Role = "null",
                        Result = "User name topilmadi"
                    };
                }


                var saltBytes = Convert.FromBase64String(teacher.PasswordSalt);
                var hashBytes = HashPassword(request.TeacherLoginDto.Password, saltBytes);
                var hash = Convert.ToBase64String(hashBytes);

                if (hash != teacher.PasswordHash)
                {
                    return new LoginRequst()
                    {
                        Token = "null",
                        Username = "null",
                        Role = "null",
                        Result = "No'to'g'ri parol"
                    };
                }

                var res=new LoginRequst()
                {
                    Token= _jwtTokenService.GenerateToken(teacher.UserName),
                    Username=teacher.UserName,
                    Role=teacher.Role.ToString(),
                    Result="Success"
                };

                return res;

            }
            catch (Exception ex)
            {
                return new LoginRequst()
                {
                    Token="null",
                    Username="null",
                    Role="null",
                    Result= $"Login failed: {ex.Message}"
                };   
            }
        }
        private byte[] HashPassword(string password, byte[] salt)
        {
            using var hmac = new HMACSHA512(salt);
            return hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
        }
    }
}
