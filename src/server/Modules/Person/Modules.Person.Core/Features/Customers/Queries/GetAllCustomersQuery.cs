﻿// --------------------------------------------------------------------------------------------------
// <copyright file="GetAllCustomersQuery.cs" company="InmoIT">
// Copyright (c) InmoIT. All rights reserved.
// Developer: Vladimir P. CHibás (vladperchi).
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// --------------------------------------------------------------------------------------------------

using InmoIT.Shared.Core.Wrapper;
using InmoIT.Shared.Dtos.Person.Customers;
using MediatR;

namespace InmoIT.Modules.Person.Core.Features.Customers.Queries
{
    public class GetAllCustomersQuery : IRequest<PaginatedResult<GetAllCustomersResponse>>
    {
        public string SearchString { get; private set; }

        public int PageNumber { get; private set; }

        public int PageSize { get; private set; }

        public string[] OrderBy { get; private set; }
    }
}