using EduTest.Application.MediatR.Commands.AuthCommands;

namespace EduTestApi.DependencyExtension
{
    public static class MediatRHandlers
    {
        public static IServiceCollection AddMediatRHandlers(this IServiceCollection services)
        {
            // MediatR handlerlarini ro'yxatdan o'tkazish
            services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(TeacherLoginCommand).Assembly));
            services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(TeacherRegisterCommand).Assembly));
            services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(StudentLoginCommand).Assembly));

           

            return services;
        }
    }
}
