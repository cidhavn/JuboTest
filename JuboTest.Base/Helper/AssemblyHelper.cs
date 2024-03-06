using System.Text.RegularExpressions;

namespace JuboTest
{
    public static class AssemblyHelper
    {
        /// <summary>
        /// 取得 DI 對應
        /// </summary>
        /// <param name="fullNameRegexPattern">FullName 比對樣本</param>
        public static Dictionary<Type, Type> GetInjectionMapping(string fullNameRegexPattern)
        {
            var result = new Dictionary<Type, Type>();
            var reg = new Regex(fullNameRegexPattern);

            List<Type> services = AppDomain.CurrentDomain
                                           .GetAssemblies()
                                           .SelectMany(x => x.GetTypes())
                                           .Where(x => reg.IsMatch(x.FullName))
                                           .ToList();

            List<Type> interfaces = services.Where(x => x.IsInterface).ToList();
            List<Type> instances = services.Where(x => x.IsInterface == false && x.IsClass).ToList();

            foreach (Type item in interfaces)
            {
                Type instance = instances.Where(x => ("I" + x.Name) == item.Name).FirstOrDefault();

                if (instance != null)
                {
                    result.Add(item, instance);
                }
            }

            return result;
        }
    }
}