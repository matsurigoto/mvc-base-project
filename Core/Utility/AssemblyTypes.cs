using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;

namespace Core.Utility
{
    /// <summary>
    /// 和Assembly相關的Static method
    /// </summary>
    public static class AssemblyTypes
    {
        /// <summary>
        /// 從目錄讀取符合的Assembly到目前Application Domain並且返回被讀取的Assembly清單
        /// </summary>
        /// <param name="assemblyfilter">過濾Assembly的邏輯</param>
        /// <param name="path">要讀取的資料夾路徑</param>
        /// <param name="fileFilter">要讀取的檔案Filter，預設是全部</param>
        /// <returns>返回被讀取的Assembly List</returns>
        public static IEnumerable<Assembly>
            GetAssemblyFromDirectory(Predicate<Assembly> assemblyfilter, string path = "", string fileFilter = "*")
        {
            if (string.IsNullOrEmpty(path))
            {
                path = Path.GetDirectoryName(AppDomain.CurrentDomain.RelativeSearchPath + @"\");
            }
            var filenames = System.IO.Directory.GetFiles(path, fileFilter).Where(IsAssamblyFile);
            var assemblyNames = FindAssemblies(filenames, assemblyfilter);
            var assemblies = assemblyNames.Select(name => Assembly.Load(name));
            return assemblies;
        }

        /// <summary>
        /// 建立一個暫時的Application Domain。用作於暫時讀取搜索到的Assembly
        /// </summary>
        /// <returns>暫時的Application Domain</returns>
        private static AppDomain CreateTempDomain()
        {
            return AppDomain.CreateDomain("Autofac.ScanningDomain",
                                          AppDomain.CurrentDomain.Evidence,
                                          AppDomain.CurrentDomain.SetupInformation);
        }

        /// <summary>
        /// 從檔案名稱List中，取得符合條件的AssemblyName List。
        /// </summary>
        /// <param name="filenames">檔案名稱</param>
        /// <param name="filter">Assembly過濾條件</param>
        /// <returns>符合條件的AssemblyName List</returns>
        private static IEnumerable<AssemblyName> FindAssemblies(IEnumerable<string> filenames, Predicate<Assembly> filter)
        {
            AppDomain tempDomain = CreateTempDomain();

            foreach (string file in filenames)
            {
                Assembly assembly;
                try
                {
                    var name = new AssemblyName { CodeBase = file };
                    assembly = tempDomain.Load(name);
                }
                catch (BadImageFormatException)
                {
                    continue;
                }

                if (filter(assembly))
                {
                    yield return assembly.GetName();
                }
            }

            AppDomain.Unload(tempDomain);
        }

        /// <summary>
        /// 設定判斷Assembly要以不區分英文大小寫方式去比對
        /// </summary>
        public const StringComparison AssemblyFileComparison = StringComparison.OrdinalIgnoreCase;

        /// <summary>
        /// 判斷某個檔案名稱是否符合Assembly
        /// </summary>
        /// <param name="file">檔案名稱</param>
        /// <returns>是否符合Assembly的檔案名稱邏輯</returns>
        public static bool IsAssamblyFile(string file)
        {
            string extension = System.IO.Path.GetExtension(file);
            return string.Equals(extension, ".dll", AssemblyFileComparison) || string.Equals(extension, ".exe", AssemblyFileComparison);
        }
    }
}
