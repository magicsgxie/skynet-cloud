// <copyright file="ManyToOneAttribute.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace UWay.Skynet.Cloud.Data
{
    using System;

    /// <summary>
    /// 多对一.
    /// </summary>
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field, AllowMultiple = false)]
    public class ManyToOneAttribute : AbstractAssociationAttribute
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ManyToOneAttribute"/> class.
        /// </summary>
        public ManyToOneAttribute() => this.IsForeignKey = true;
    }
}
