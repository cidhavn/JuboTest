using Microsoft.Extensions.DependencyInjection;

namespace JuboTest.Service.Management
{
    public static class ServiceCollectionExtension
    {
        public static IServiceCollection AddManagementService(this IServiceCollection services)
        {
            string pattern = @"^JuboTest\.Service\.Management.+Service";
            Dictionary<Type, Type> injectionDic = AssemblyHelper.GetInjectionMapping(pattern);

            if (injectionDic.IsNullOrEmpty() == false)
            {
                foreach (var item in injectionDic)
                {
                    services.AddTransient(item.Key, item.Value);
                }
            }

            return services;
        }
    }
}