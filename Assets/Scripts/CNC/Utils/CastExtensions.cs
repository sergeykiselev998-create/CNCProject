using UnityEngine;

namespace CNC.Utils
{
    public static class CastExtensions
    {
        public static bool TryCast<TConcrete, TSource>(this TSource source, out TConcrete concrete) 
            where TConcrete : class, TSource
            where TSource : class
        {
            concrete = source as TConcrete;
    
            if (concrete == null)
            {
                var stackTrace = new System.Diagnostics.StackTrace();
                var callerFrame = stackTrace.GetFrame(1);
                var callerMethod = callerFrame.GetMethod();
                var callerClass = callerMethod.DeclaringType?.Name;
        
                Debug.LogError($"[CastExtensions] {callerClass} - Failed to cast {typeof(TSource).Name} to {typeof(TConcrete).Name}");
            }
    
            return concrete != null;
        }
    }
}