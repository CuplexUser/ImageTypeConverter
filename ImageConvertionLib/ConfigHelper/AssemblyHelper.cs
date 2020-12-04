using System.Reflection;

namespace ImageConverterLib.Configuration
{
    public static class AssemblyHelper
    {
        public static string GetNameOfThisAssembly()
        {
            return Assembly.GetExecutingAssembly().GetName().Name;
        }

        public static Assembly GetAssembly()
        {
            return Assembly.GetExecutingAssembly();
        }
    }
}