using EduTest.Application.MediatR.Commands.AuthCommands;
using EduTest.Application.MediatR.Commands.GroupCommands;
using EduTest.Application.MediatR.Commands.StudentCommands;
using EduTest.Application.MediatR.Commands.TeacherCommands;
using EduTest.Application.MediatR.Commands.TestCommands;

namespace EduTestApi.DependencyExtension
{
    public static class MediatRHandlers
    {
        public static IServiceCollection AddMediatRHandlers(this IServiceCollection services)
        {
            // MediatR handlerlarini ro'yxatdan o'tkazish
            services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(TeacherLoginCommand).Assembly));
            services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(TeacherRegisterCommand).Assembly));
            services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(TeacherDeleteCommand).Assembly));
            services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(TeacherUpdateCommand).Assembly));


            services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(StudentCreateCommand).Assembly));
            services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(StudentDeleteCommand).Assembly));
            services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(StudentUpdateCommand).Assembly));
            services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(StudentGetAllQuery).Assembly));
            services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(StudentLoginCommand).Assembly));

            services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(TestCreateCommand).Assembly));

            services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(GroupCreateCommand).Assembly));
            services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(GroupDeleteCommand).Assembly));
            services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(GroupGetAllQuery).Assembly));
            services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(GroupUpdateCommand).Assembly));



            return services;
        }
    }
}
