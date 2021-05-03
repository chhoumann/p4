namespace Interpreter
{
    static class ObjectExtensions
    {
        // https://stackoverflow.com/questions/7814591/access-property-by-index
        public static TResult Get<TResult>(this object @this, string propertyName)
        {
            return (TResult) @this.GetType().GetProperty(propertyName)?.GetValue(@this, null);
        }
    }
}