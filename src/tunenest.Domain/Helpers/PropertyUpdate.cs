using System.Reflection;

namespace tunenest.Domain.Helpers
{
    public static class PropertyUpdate
    {
        public static void UpdateProperties<TTarget, TSource>(TTarget target, TSource source)
        {
            var sourceType = typeof(TSource);
            var targetType = typeof(TTarget);

            var sourceTypeProp = sourceType.GetProperties(BindingFlags.Public | BindingFlags.Instance)
                .Where(p => p is { CanRead: true, CanWrite: true });

            foreach (var property in sourceTypeProp)
            {
                var value = property.GetValue(source);
                if (value is null) continue;

                var propName = property.Name;

                var targetProp = targetType.GetProperty(propName, BindingFlags.IgnoreCase | BindingFlags.Public | BindingFlags.Instance);

                if (targetProp is null || !targetProp.CanWrite) continue;

                targetProp.SetValue(target, value);
            }
        }
    }
}
