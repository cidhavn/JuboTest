using Microsoft.Extensions.DependencyInjection;
using System.Text;

namespace JuboTest
{
    public static class ServiceCollectionExtension
    {
        /// <summary>
        /// 註冊 JuboTest.Base
        /// </summary>
        public static IServiceCollection AddBase(this IServiceCollection services)
        {
            // 額外擴充 .NET Core Encoding 編碼
            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);

            // 註冊 IHttpClientFactory
            services.AddHttpClient();

            return services;
        }
    }
}