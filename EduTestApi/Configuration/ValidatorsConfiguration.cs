using EduTest.Application.Common.Validators;
using EduTest.Application.Dtos.Groups;
using EduTest.Application.Dtos.Students;
using EduTest.Application.Dtos.Teachers;
using EduTest.Application.Dtos.TestResults;
using EduTest.Application.Dtos.Tests;
using FluentValidation;

namespace EduTestApi.Configuration
{
    public static class ValidatorsConfiguration
    {
        public static void ConfigurationValidators(this WebApplicationBuilder builder)
        {
            builder.Services.AddScoped<IValidator<TeacherCreateDto>, TeacherValidator>();
            builder.Services.AddScoped<IValidator<TeacherLoginDto>, TeacherLoginValidator>();

            builder.Services.AddScoped<IValidator<StudentCreateDto>, StudentValidator>();
            builder.Services.AddScoped<IValidator<StudentLoginDto>, StudentLoginValidator>();

            builder.Services.AddScoped<IValidator<GroupCreateDto>, GroupValidator>();

            builder.Services.AddScoped<IValidator<TestCreateDto>, TestValidator>();
        }
    }
}
