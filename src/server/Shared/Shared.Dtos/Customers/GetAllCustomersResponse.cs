﻿// --------------------------------------------------------------------------------------------------
// <copyright file="GetAllCustomersResponse.cs" company="InmoIT">
// Copyright (c) InmoIT. All rights reserved.
// Developer: Vladimir P. CHibás (vladperchi).
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// --------------------------------------------------------------------------------------------------

using System;

namespace InmoIT.Shared.Dtos.Customers
{
    public record GetCustomersResponse(Guid Id, string Name, string PhoneNumber, string Email, string ImageUrl);
}