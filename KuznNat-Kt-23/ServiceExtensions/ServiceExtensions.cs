

using static IStudentService;

namespace KuznNat_Kt_23.ServiceExtensions
{
    public static class ServiceExtensions

    {
        public static IServiceCollection AddServices(this IServiceCollection services)
        {
            services.AddScoped<IStudentService, StudentService>();
            return services;
        }
    }
    
}
