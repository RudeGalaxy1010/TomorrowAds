using System;
using UnityEngine;

namespace Tomorrow.Services
{
    public class JsonDeserializer : IDeserializer
    {
        public string ContentType => "application/json";

        public T Deserialize<T>(string text)
        {
            try
            {
                var result = JsonUtility.FromJson<T>(text);
                return result;
            }
            catch (Exception ex)
            {
                Debug.LogError($"Could not parse response {text}. {ex.Message}");
                return default;
            }
        }
    }
}
