using Should;
using Swagger.ObjectModel;
using Swagger.ObjectModel;

namespace Nancy.Swagger.Tests
{
    public static class Extensions
    {
        public static void ShouldEqual(this DataType actual, DataType expected, string userMessage = null)
        {
            actual.DefaultValue.ShouldEqual(expected.DefaultValue, userMessage.Append("DefaultValue"));
            actual.Enum.ShouldEqual(expected.Enum, userMessage.Append("Enum"));
            actual.Format.ShouldEqual(expected.Format, userMessage.Append("Format"));
            actual.Items.ShouldEqual(expected.Items, userMessage.Append("Items"));
            actual.Maximum.ShouldEqual(expected.Maximum, userMessage.Append("Maximum"));
            actual.Minimum.ShouldEqual(expected.Minimum, userMessage.Append("Minimum"));
            actual.Ref.ShouldEqual(expected.Ref, userMessage.Append("Ref"));
            actual.Type.ShouldEqual(expected.Type, userMessage.Append("Type"));
            actual.UniqueItems.ShouldEqual(expected.UniqueItems, userMessage.Append("UniqueItems"));
        }

        public static void ShouldEqual(this Parameter actual, Parameter expected, string userMessage = null)
        {
            (actual as DataType).ShouldEqual(expected, userMessage);

            actual.Description.ShouldEqual(expected.Description, userMessage.Append("DefaultValue"));
            actual.Name.ShouldEqual(expected.Name, userMessage.Append("Name"));
            actual.In.ShouldEqual(expected.In, userMessage.Append("ParamType"));
            actual.Required.ShouldEqual(expected.Required, userMessage.Append("Required"));
        }

        public static void ShouldEqual(this Operation actual, Operation expected, string userMessage = null)
        {
            (actual as DataType).ShouldEqual(expected, userMessage);

            actual.Authorizations.ShouldEqual(expected.Authorizations, userMessage.Append("Authorizations"));
            actual.Consumes.ShouldEqual(expected.Consumes, userMessage.Append("Consumes"));
            actual.Deprecated.ShouldEqual(expected.Deprecated, userMessage.Append("Deprecated"));
            actual.Method.ShouldEqual(expected.Method, userMessage.Append("Method"));
            actual.Description.ShouldEqual(expected.Description, userMessage.Append("Notes"));
            actual.OperationId.ShouldEqual(expected.OperationId, userMessage.Append("Nickname"));
            actual.Parameters.ShouldEqual(expected.Parameters, userMessage.Append("Parameters"));
            actual.Produces.ShouldEqual(expected.Produces, userMessage.Append("Produces"));
            actual.Summary.ShouldEqual(expected.Summary, userMessage.Append("Summary"));
        }

        public static void ShouldEqual(this ModelProperty actual, ModelProperty expected, string userMessage = null)
        {
            (actual as DataType).ShouldEqual(expected, userMessage);

            actual.Description.ShouldEqual(expected.Description, userMessage.Append("Description"));
        }

        public static void ShouldEqual(this Item actual, Item expected, string userMessage = null)
        {
            if (actual == null)
            {
                expected.ShouldEqual<object>(null, userMessage.Append("Items"));
            }
            else
            {
                actual.Format.ShouldEqual(expected.Format, userMessage.Append("Items.Format"));
                actual.Ref.ShouldEqual(expected.Ref, userMessage.Append("Items.Ref"));
                actual.Type.ShouldEqual(expected.Type, userMessage.Append("Items.Type"));
            }
        }

        private static string Append(this string userMessage, string other)
        {
            return string.IsNullOrEmpty(userMessage) ? other : string.Format("{0}: {1}", userMessage, other);
        }
    }
}