namespace Url_Shortener_API.Data
{
    public class MongoDatabaseSettings
    {
        public string ConnectionString { get; set; } = null!;
        public string DatabaseName { get; set; } = null!;
        public string Collection { get; set; } = null!;
    }
}
