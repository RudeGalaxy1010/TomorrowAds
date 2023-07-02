namespace Tomorrow.Services
{
    public struct RemoteServiceConfig
    {
        public string Url;
        public string ContentType;
        public IDeserializer Deserializer;
    }
}
