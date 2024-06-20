using System.Collections;
using System.Runtime.Serialization;

namespace Authentication.Services.Foundations
{
    public class ConnException : Exception
    {
        protected ConnException(SerializationInfo serializationInfo, StreamingContext streamingContext)
        : base(serializationInfo, streamingContext)
        {
        }

        public ConnException() { }

        public ConnException(string message) : base(message) { }

        public ConnException(string message, Exception exception) : base(message, exception) { }

        public ConnException(Exception exception, IDictionary data) : base(exception.Message, exception)
        {
            AddData(data);
        }

        public ConnException(string message, Exception exception, IDictionary data) : base(message, exception)
        {
            AddData(data);
        }

        public void UpsertDataList(string key, string value)
        {
            if (Data.Contains(key)) {
                (Data[key] as List<string>)?.Add(value);
                return;
            }

            Data.Add(key, new List<string> { value });
        }

        public void ThrowIfContainsErrors()
        {
            if (Data.Count > 0) {
                throw this;
            }
        }

        protected void AddData(IDictionary dictionary)
        {
            if (dictionary == null) return;

            foreach (DictionaryEntry item in dictionary) {
                Data.Add(item.Key, item.Value);
            }
        }
    }
}
