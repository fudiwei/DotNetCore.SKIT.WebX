using System.IO;
using Microsoft.AspNetCore.Hosting;

namespace Microsoft.AspNetCore.Hosting
{
    /// <summary>
    /// 
    /// </summary>
    public static class HostingEnvironmentStaticExtensions
    {
        /// <summary>
        /// Maps the specified relative or virtual path to the corresponding physical directory on the server.
        /// </summary>
        /// <param name="env"></param>
        /// <param name="path"></param>
        /// <returns></returns>
#if NETCORE_2_X
        public static string MapPath(this IHostingEnvironment env, string path)
#else
        public static string MapPath(this IWebHostEnvironment env, string path)
#endif
        {
            // Root Path
            if (string.IsNullOrWhiteSpace(path))
                return env.WebRootPath;

            // Absolute Path
            if (Path.VolumeSeparatorChar == ':' ? path.IndexOf(Path.VolumeSeparatorChar) > 0 : path.IndexOf('\\') > 0)
                return path;

            // Relative Path
            return Path.Combine(env.WebRootPath, path.TrimStart('~', '/').Replace("/", Path.DirectorySeparatorChar.ToString()));
        }
    }
}
