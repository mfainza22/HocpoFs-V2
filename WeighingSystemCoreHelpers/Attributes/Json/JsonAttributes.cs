using Newtonsoft.Json.Converters;

namespace WeighingSystemCoreHelpers.Attributes.Json
{
    public class JsonDateTimeFormat : IsoDateTimeConverter
    {
        public JsonDateTimeFormat(string format)
        {
            DateTimeFormat = format;
        }
    }
}