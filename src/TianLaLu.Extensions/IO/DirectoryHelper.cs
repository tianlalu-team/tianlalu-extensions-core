namespace System.IO
{
    public static class DirectoryHelper
    {
        /// <summary>
        /// 确保目录是存在的，并返回这个目录
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public static string EnsureExists(string path)
        {
            if (Directory.Exists(path))
                return path;

            Directory.CreateDirectory(path);
            return path;
        }

        /// <summary>
        /// 确保使用 Path.Combine 合并的目录是存在的，并返回这个目录
        /// </summary>
        /// <param name="path1"></param>
        /// <param name="path2"></param>
        /// <returns></returns>
        public static string EnsureExists(string path1, string path2)
        {
            return EnsureExists(Path.Combine(path1, path2));
        }

        /// <summary>
        /// 确保使用 Path.Combine 合并的目录是存在的，并返回这个目录
        /// </summary>
        /// <param name="path1"></param>
        /// <param name="path2"></param>
        /// <param name="path3"></param>
        /// <returns></returns>
        public static string EnsureExists(string path1, string path2, string path3)
        {
            return EnsureExists(Path.Combine(path1, path2, path3));
        }

        /// <summary>
        /// 确保使用 Path.Combine 合并的目录是存在的，并返回这个目录
        /// </summary>
        /// <param name="path1"></param>
        /// <param name="path2"></param>
        /// <param name="path3"></param>
        /// <param name="path4"></param>
        /// <returns></returns>
        public static string EnsureExists(string path1, string path2, string path3, string path4)
        {
            return EnsureExists(Path.Combine(path1, path2, path3, path4));
        }


        /// <summary>
        /// 向上搜寻文件，并返回包含此文件的文件夹地址
        /// </summary>
        /// <param name="path"></param>
        /// <param name="fileName"></param>
        /// <returns></returns>
        public static string Search(string path, string fileName)
        {
            while (true)
            {
                if (path.IsNullOrEmpty())
                {
                    return path;
                }

                if (File.Exists(path))
                {
                    path = Path.GetDirectoryName(path);
                    continue;
                }

                if (Directory.Exists(path) && File.Exists(Path.Combine(path, fileName)))
                {
                    return path;
                }

                path = Path.GetDirectoryName(path);
            }
        }
    }
}