using Swagger.Model.Attributes;

namespace Swagger.Model
{
    [SwaggerData]
    public class SwaggerModel
    {
        private static readonly IJsonSerializerStrategy SerializerStrategy = new SwaggerSerializerStrategy();

        public string ToJson()
        {
            return SimpleJson.SerializeObject(this, SerializerStrategy);
        }

        private class SwaggerSerializerStrategy : PocoJsonSerializerStrategy
        {
            // TODO: Customize serialization.
        }
    }
}