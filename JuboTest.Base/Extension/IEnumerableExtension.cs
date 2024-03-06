namespace JuboTest
{
    public static class IEnumerableExtension
    {
        /// <summary>
        /// 是否為空集合
        /// </summary>
        public static bool IsNullOrEmpty<T>(this IEnumerable<T> items)
        {
            return items == null || items.Any() == false;
        }

        /// <summary>
        /// 取得第一筆資料並指定預設值
        /// </summary>
        public static T FirstOrDefault<T>(this IEnumerable<T> items, T defaultValue)
        {
            return items != null && items.Any() ? items.First() : defaultValue;
        }
    }
}