using System.Collections;

namespace Common.DotNetCore.Utilities
{
    //This class is for validating input parameters.
    
    public static class Assert
    {
        public static void NotNull<T>(T obj, string name, string message = null)
            where T : class
        {
            if (obj is null)
                throw new ArgumentNullException($"{name} : {typeof(T)}" , message);
        }
    }
}
