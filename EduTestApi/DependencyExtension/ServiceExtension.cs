
using EduTest.Application.JwtTokenSerives;
using EduTest.DataAccess.DBContext;
using EduTest.DataAccess.Repositories.IRepository;
using EduTest.DataAccess.Repositories.Repository;
using Microsoft.EntityFrameworkCore;

namespace EduTestApi.DependencyExtension
{
    public static class ServiceExtension
    {
      
        public static void AddDbContextes(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<AppDbContext>(options =>
            options.UseNpgsql(configuration.GetConnectionString("DefaultConnection")));
        }
        public static void AddRepositories(this IServiceCollection services)
        {
            services.AddScoped<ITeacherRepository, TeacherRepository>();
            services.AddScoped<IStudentRepository, StudentRepository>();
            services.AddScoped<IGroupRepository, GroupRepository>();
            services.AddScoped<ITestRepository, TestRepository>();
            services.AddScoped<ITestResultRepository, TestResultRepository>();



            services.AddScoped<IJwtTokenService, JwtTokenService>();

        }


    }
}
