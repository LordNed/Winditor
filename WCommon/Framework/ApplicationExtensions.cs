using System.IO;

namespace WindEditor
{
    public static class ApplicationExtensions
    {
        public static string GetBasePath()
        {
            return Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location);
        }
    }
}
