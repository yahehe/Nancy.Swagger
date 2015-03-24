// --------------------------------------------------------------------------------------------------------------------
// <copyright file="DataTypeBuilder.cs" company="Premise Health">
//   Copyright (c) 2015 Premise Health. All rights reserved.
// </copyright>
// <summary>
//   The data type builder.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Swagger.ObjectModel.Builders
{
    using System.Collections.Generic;

    /// <summary>
    /// The data type builder.
    /// </summary>
    public abstract class DataTypeBuilder<TBuilder, T>
        where TBuilder : DataTypeBuilder<TBuilder, T> where T : DataType, new()
    {
        /// <summary>
        /// The maximum.
        /// </summary>
        private long? maximum;

        /// <summary>
        /// The minimum.
        /// </summary>
        private long? minimum;

        /// <summary>
        /// The type.
        /// </summary>
        private string type;

        /// <summary>
        /// The collection format.
        /// </summary>
        private CollectionFormats? collectionFormat;

        /// <summary>
        /// The default value.
        /// </summary>
        private object defaultValue;

        /// <summary>
        /// The enum.
        /// </summary>
        private List<string> @enum;

        /// <summary>
        /// The exclusive max.
        /// </summary>
        private bool? exclusiveMax;

        /// <summary>
        /// The exclusive min.
        /// </summary>
        private bool? exclusiveMin;

        /// <summary>
        /// The format.
        /// </summary>
        private string format;

        /// <summary>
        /// The items.
        /// </summary>
        private Item items;

        /// <summary>
        /// The max items.
        /// </summary>
        private int? maxItems;

        /// <summary>
        /// The max length.
        /// </summary>
        private long? maxLength;

        /// <summary>
        /// The min items.
        /// </summary>
        private int? minItems;

        /// <summary>
        /// The min length.
        /// </summary>
        private long? minLength;

        /// <summary>
        /// The multiple of.
        /// </summary>
        private int? multipleOf;

        /// <summary>
        /// The pattern.
        /// </summary>
        private string pattern;

        /// <summary>
        /// The reference.
        /// </summary>
        private string reference;

        /// <summary>
        /// The unique items.
        /// </summary>
        private bool? uniqueItems;

        /// <summary>
        /// The build.
        /// </summary>
        /// <returns>
        /// An instance of type T
        /// </returns>
        public abstract T Build();

        /// <summary>
        /// The build.
        /// </summary>
        /// <returns>
        /// The <see cref="DataType"/>.
        /// </returns>
        protected T BuildBase()
        {
            var dataType = new T
                               {
                                   Maximum = this.maximum,
                                   Minimum = this.minimum,
                                   Type = this.type,
                                   CollectionFormat = this.collectionFormat,
                                   Default = this.defaultValue,
                                   Enum = this.@enum,
                                   ExclusiveMaximum = this.exclusiveMax,
                                   ExclusiveMinimum = this.exclusiveMin,
                                   Format = this.format,
                                   Items = this.items,
                                   MaxItems = this.maxItems,
                                   MaxLength = this.maxLength,
                                   MinItems = this.minItems,
                                   MinLength = this.minLength,
                                   MultipleOf = this.multipleOf,
                                   Pattern = this.pattern,
                                   Ref = this.reference,
                                   UniqueItems = this.uniqueItems,
                               };

            return dataType;
        }

        /// <summary>
        /// The maximum.
        /// </summary>
        /// <param name="maximum">
        /// The maximum.
        /// </param>
        /// <returns>
        /// The builder
        /// </returns>
        public TBuilder Maximum(long maximum)
        {
            this.maximum = maximum;
            return (TBuilder)this;
        }

        /// <summary>
        /// The minimum.
        /// </summary>
        /// <param name="minimum">
        /// The minimum.
        /// </param>
        /// <returns>
        /// The builder
        /// </returns>
        public TBuilder Minimum(long minimum)
        {
            this.minimum = minimum;
            return (TBuilder)this;
        }

        /// <summary>
        /// The type.
        /// </summary>
        /// <param name="type">
        /// The type.
        /// </param>
        /// <returns>
        /// The builder
        /// </returns>
        public TBuilder Type(string type)
        {
            this.type = type;
            return (TBuilder)this;
        }

        /// <summary>
        /// The collection format.
        /// </summary>
        /// <param name="collectionFormat">
        /// The collection format.
        /// </param>
        /// <returns>
        /// The builder
        /// </returns>
        public TBuilder CollectionFormat(CollectionFormats collectionFormat)
        {
            this.collectionFormat = collectionFormat;
            return (TBuilder)this;
        }

        /// <summary>
        /// The default.
        /// </summary>
        /// <param name="defaultValue">
        /// The default value.
        /// </param>
        /// <returns>
        /// The builder
        /// </returns>
        public TBuilder Default(object defaultValue)
        {
            this.defaultValue = defaultValue;
            return (TBuilder)this;
        }

        /// <summary>
        /// The enum.
        /// </summary>
        /// <param name="enum">
        /// The enum.
        /// </param>
        /// <returns>
        /// The builder
        /// </returns>
        public TBuilder Enum(string @enum)
        {
            if (this.@enum == null)
            {
                this.@enum = new List<string>();
            }

            this.@enum.Add(@enum);
            return (TBuilder)this;
        }

        /// <summary>
        /// The enums.
        /// </summary>
        /// <param name="enums">
        /// The enums.
        /// </param>
        /// <returns>
        /// The builder
        /// </returns>
        public TBuilder Enums(IEnumerable<string> @enums)
        {
            if (this.@enum == null)
            {
                this.@enum = new List<string>();
            }
            foreach (var @enum in @enums)
            {
                this.@enum.Add(@enum);
            }
            return (TBuilder)this;
        }

        /// <summary>
        /// The is exclusive maximum.
        /// </summary>
        /// <returns>
        /// The builder
        /// </returns>
        public TBuilder IsExclusiveMaximum()
        {
            this.exclusiveMax = true;
            return (TBuilder)this;
        }

        /// <summary>
        /// The is exclusive minimum.
        /// </summary>
        /// <returns>
        /// The builder
        /// </returns>
        public TBuilder IsExclusiveMinimum()
        {
            this.exclusiveMin = true;
            return (TBuilder)this;
        }

        /// <summary>
        /// The format.
        /// </summary>
        /// <param name="format">
        /// The format.
        /// </param>
        /// <returns>
        /// The builder
        /// </returns>
        public TBuilder Format(string format)
        {
            this.format = format;
            return (TBuilder)this;
        }

        /// <summary>
        /// The items.
        /// </summary>
        /// <param name="items">
        /// The items.
        /// </param>
        /// <returns>
        /// The builder
        /// </returns>
        public TBuilder Items(Item items)
        {
            this.items = items;
            return (TBuilder)this;
        }

        /// <summary>
        /// The max items.
        /// </summary>
        /// <param name="maxItems">
        /// The max items.
        /// </param>
        /// <returns>
        /// The builder
        /// </returns>
        public TBuilder MaxItems(int maxItems)
        {
            this.maxItems = maxItems;
            return (TBuilder)this;
        }

        /// <summary>
        /// The max length.
        /// </summary>
        /// <param name="maxLength">
        /// The max length.
        /// </param>
        /// <returns>
        /// The builder
        /// </returns>
        public TBuilder MaxLength(long maxLength)
        {
            this.maxLength = maxLength;
            return (TBuilder)this;
        }

        /// <summary>
        /// The min items.
        /// </summary>
        /// <param name="minItems">
        /// The min items.
        /// </param>
        /// <returns>
        /// The builder
        /// </returns>
        public TBuilder MinItems(int minItems)
        {
            this.minItems = minItems;
            return (TBuilder)this;
        }

        /// <summary>
        /// The min length.
        /// </summary>
        /// <param name="minLength">
        /// The min length.
        /// </param>
        /// <returns>
        /// The builder
        /// </returns>
        public TBuilder MinLength(long minLength)
        {
            this.minLength = minLength;
            return (TBuilder)this;
        }

        /// <summary>
        /// The multiple of.
        /// </summary>
        /// <param name="multipleOf">
        /// The multiple of.
        /// </param>
        /// <returns>
        /// The builder
        /// </returns>
        public TBuilder MultipleOf(int multipleOf)
        {
            this.multipleOf = multipleOf;
            return (TBuilder)this;
        }

        /// <summary>
        /// The pattern.
        /// </summary>
        /// <param name="pattern">
        /// The pattern.
        /// </param>
        /// <returns>
        /// The builder
        /// </returns>
        public TBuilder Pattern(string pattern)
        {
            this.pattern = pattern;
            return (TBuilder)this;
        }

        /// <summary>
        /// The is unique items.
        /// </summary>
        /// <returns>
        /// The builder
        /// </returns>
        public TBuilder IsUniqueItems()
        {
            this.uniqueItems = true;
            return (TBuilder)this;
        }
    }
}