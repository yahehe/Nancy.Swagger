namespace Swagger.ObjectModel
{
    /// <summary>
    /// This object is used to describe the value types used inside an array.
    /// </summary>
    /// <example>
    /// For a primitive type:
    /// <code>
    /// {
    ///   "type": "string"
    /// }
    /// </code>
    /// For a complex type:
    /// <code>
    /// {
    ///   "$ref": "Pet"
    /// }
    /// </code>
    /// </example>
    public class Item : DataType
    {
    }
}