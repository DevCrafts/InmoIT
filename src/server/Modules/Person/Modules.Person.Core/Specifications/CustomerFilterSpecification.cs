﻿// --------------------------------------------------------------------------------------------------
// <copyright file="CustomerFilterSpecification.cs" company="InmoIT">
// Copyright (c) InmoIT. All rights reserved.
// Developer: Vladimir P. CHibás (vladperchi).
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// --------------------------------------------------------------------------------------------------

using InmoIT.Modules.Person.Core.Entities;
using InmoIT.Shared.Core.Interfaces.Specifications;

namespace InmoIT.Modules.Person.Core.Specifications
{
    public class CustomerFilterSpecification : Specification<Customer>
    {
        public CustomerFilterSpecification(string searchString)
        {
            if (!string.IsNullOrEmpty(searchString))
            {
                Criteria = p => !string.IsNullOrWhiteSpace(p.Email) && (p.Name.Contains(searchString) || p.SurName.Contains(searchString) || p.PhoneNumber.Contains(searchString) || p.Gender.Contains(searchString) || p.Group.Contains(searchString));
            }
            else
            {
                Criteria = p => !string.IsNullOrWhiteSpace(p.Email);
            }
        }
    }
}