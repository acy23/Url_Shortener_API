namespace Url_Shortener_API.Utils
{
    public class ServiceResult<T>
    {
        public bool HasValue { get; }
        public T Value { get; }

        public ServiceResult(T value)
        {
            Value = value;
            HasValue = true;
        }

        public ServiceResult()
        {
            Value = default;
            HasValue = false;
        }
    }
}
