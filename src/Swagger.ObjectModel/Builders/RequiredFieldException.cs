// --------------------------------------------------------------------------------------------------------------------
// <copyright file="RequiredFieldException.cs" company="Premise Health">
//   Copyright (c) 2015 Premise Health. All rights reserved.
// </copyright>
// <summary>
//   The required field exception.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Swagger.ObjectModel.Builders
{
    using System;

    /// <summary>
    /// The required field exception.
    /// </summary>
    public class RequiredFieldException : InvalidOperationException
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="RequiredFieldException"/> class. 
        /// Initializes a new instance of the <see cref="T:System.InvalidOperationException"/> class.
        /// </summary>
        public RequiredFieldException(string field)
            : base(string.Format("'{0}' is required.", field))
        {
        }
    }
}