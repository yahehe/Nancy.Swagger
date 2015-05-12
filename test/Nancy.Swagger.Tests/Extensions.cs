using Should;
using Swagger.ObjectModel;

namespace Nancy.Swagger.Tests
{
    public static class Extensions
    {
        public static void ShouldEqual(this DataType actual, DataType expected, string userMessage = null)
        {
            actual.Default.ShouldEqual(expected.Default, userMessage.Append("Default"));
            actual.Enum.ShouldEqual(expected.Enum, userMessage.Append("Enum"));
            actual.Format.ShouldEqual(expected.Format, userMessage.Append("Format"));
            actual.Items.ShouldEqual(expected.Items, userMessage.Append("Items"));
            actual.Maximum.ShouldEqual(expected.Maximum, userMessage.Append("Maximum"));
            actual.Minimum.ShouldEqual(expected.Minimum, userMessage.Append("Minimum"));
            actual.Ref.ShouldEqual(expected.Ref, userMessage.Append("Ref"));
            actual.Type.ShouldEqual(expected.Type, userMessage.Append("Type"));
            actual.UniqueItems.ShouldEqual(expected.UniqueItems, userMessage.Append("UniqueItems"));
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