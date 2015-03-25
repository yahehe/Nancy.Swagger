using System;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;

namespace Nancy.Swagger
{
    [SwaggerApi]
    public class SwaggerModelDataBuilder<T>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SwaggerModelDataBuilder{T}"/> class.
        /// </summary>
        public SwaggerModelDataBuilder()
        {
            Data = new SwaggerModelData(typeof(T));
        }

        /// <summary>
        /// Gets the <see cref="SwaggerModelData"/> instance.
        /// </summary>
        public SwaggerModelData Data { get; private set; }

        /// <summary>
        /// Specify the description for the model.
        /// </summary>
        /// <param name="description">The description.</param>
        /// <returns>The <see cref="SwaggerModelDataBuilder{T}"/> instance.</returns>
        public SwaggerModelDataBuilder<T> Description(string description)
        {
            Data.Description = description;

            return this;
        }

        /// <summary>
        /// Access a <see cref="SwaggerModelPropertyDataBuilder{TProperty}"/> for a given property of the model.
        /// </summary>
        /// <param name="expression">An <see cref="Expression{Func{T, TProperty}}"/> for accessing the property.</param>
        /// <returns>The <see cref="SwaggerModelPropertyDataBuilder{TProperty}"/> instance.</returns>
        public SwaggerModelPropertyDataBuilder<TProperty> Property<TProperty>(Expression<Func<T, TProperty>> expression)
        {
            var memberInfo = GetMemberInfo(expression);
            var propertyData = Data.Properties.First(d => d.Name == memberInfo.Name);

            return new SwaggerModelPropertyDataBuilder<TProperty>(propertyData);
        }

        private static MemberInfo GetMemberInfo<TProperty>(Expression<Func<T, TProperty>> expression)
        {
            var member = expression.Body as MemberExpression;

            if (member != null)
            {
                return member.Member;
            }

            throw new ArgumentException("Expression is not a member access", "expression");
        }
    }
}