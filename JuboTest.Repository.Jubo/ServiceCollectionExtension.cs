using Microsoft.Extensions.DependencyInjection;

namespace JuboTest.Repository.Jubo
{
    public static class ServiceCollectionExtension
    {
        public static IServiceCollection AddJuboRepository(this IServiceCollection services, Func<JuboRepositorySetting> funcSetting)
        {
            services.AddTransient<JuboRepositorySetting>(provider => funcSetting.Invoke());

            string pattern = @"^JuboTest\.Repository\.Jubo.+Repository$";
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