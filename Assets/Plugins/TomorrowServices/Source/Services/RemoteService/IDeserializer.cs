namespace Tomorrow.Services
{
    public interface IDeserializer
    {
        public T Deserialize<T>(string data);
    }
}
