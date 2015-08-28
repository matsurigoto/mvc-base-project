using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using Core.Common.Mapper;
using Core.Common.Task;
using Core.Utility;


namespace Core.Mapper
{
    /// <summary>
    /// 註冊有設定AutoMapper的viewmodel
    /// </summary>
    public class AutoMapperConfig : IRunAtStartup
    {
        /// <summary>
        /// 要執行的邏輯
        /// </summary>
        [SuppressMessage("StyleCop.CSharp.MaintainabilityRules", "SA1119:StatementMustNotUseUnnecessaryParenthesis", Justification = "Reviewed.")]
        public void Execute()
        {
            var typeOfIHaveCustomMapping = typeof(IHaveCustomMapping);
            var typeOfIMapFrom = typeof(IMapFrom<>);

            // Type 符合 IHaveCustomMapping 和 IMapFrom 的 predicate方法
            // 這個predicate 的條件和下面個別mapping的第一個條件是一致的。
            Func<Type, bool> predicate = (t => typeOfIHaveCustomMapping.IsAssignableFrom(t) // 找到符合IHaveCustomMapping
                || t.GetInterfaces().Where(i => i.IsGenericType && i.GetGenericTypeDefinition() == typeOfIMapFrom).Any()); // 找到符合IMapFrom<>

            var types = AssemblyTypes.GetAssemblyFromDirectory(assembly => assembly.GetExportedTypes().Where(predicate).Any())     // 選擇要讀進來的Assembly - 只有符合IHaveCustomMapping 和 IMapFrom才讀
                // 把讀進來的Assembly取出裡面符合兩個interface的Type
                    .SelectMany(x => x.GetExportedTypes()
                    .Where(predicate)).ToList();

            LoadStandardMappings(types);

            LoadCustomMappings(types);
        }

        /// <summary>
        /// 註冊如果使用是自定義邏輯的Mapping
        /// </summary>
        /// <param name="types">可能符合的Type</param>
        private static void LoadCustomMappings(IEnumerable<Type> types)
        {
            var maps = (from t in types
                        from i in t.GetInterfaces()
                        where typeof(IHaveCustomMapping).IsAssignableFrom(t) &&
                              !t.IsAbstract &&
                              !t.IsInterface
                        select (IHaveCustomMapping)Activator.CreateInstance(t)).ToArray();

            foreach (var map in maps)
            {
                map.CreateMappings(AutoMapper.Mapper.Configuration);
            }
        }

        /// <summary>
        /// Loads the standard mappings.
        /// </summary>
        /// <param name="types">The types.</param>
        private static void LoadStandardMappings(IEnumerable<Type> types)
        {
            var maps = (from t in types
                        from i in t.GetInterfaces()
                        where i.IsGenericType && i.GetGenericTypeDefinition() == typeof(IMapFrom<>) &&
                              !t.IsAbstract &&
                              !t.IsInterface
                        select new
                        {
                            Source = i.GetGenericArguments()[0],
                            Destination = t
                        }).ToArray();

            foreach (var map in maps)
            {
                AutoMapper.Mapper.CreateMap(map.Source, map.Destination);
            }
        }
    }
}
