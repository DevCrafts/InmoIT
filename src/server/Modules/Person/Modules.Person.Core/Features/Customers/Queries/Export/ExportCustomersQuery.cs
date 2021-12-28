﻿// --------------------------------------------------------------------------------------------------
// <copyright file="ExportCustomersQuery.cs" company="InmoIT">
// Copyright (c) InmoIT. All rights reserved.
// Developer: Vladimir P. CHibás (vladperchi).
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// --------------------------------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using InmoIT.Modules.Person.Core.Abstractions;
using InmoIT.Modules.Person.Core.Entities;
using InmoIT.Modules.Person.Core.Specifications;
using InmoIT.Shared.Core.Extensions;
using InmoIT.Shared.Core.Interfaces.Services;
using InmoIT.Shared.Core.Wrapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Localization;

namespace InmoIT.Modules.Person.Core.Features.Customers.Queries.Export
{
    public class ExportCustomersQuery : IRequest<Result<string>>
    {
        public string SearchString { get; set; }

        public ExportCustomersQuery(string searchString = "")
        {
            SearchString = searchString;
        }
    }

    internal class ExportCustomersQueryHandler : IRequestHandler<ExportCustomersQuery, Result<string>>
    {
        private readonly ICustomerDbContext _context;
        private readonly IExcelService _excelService;
        private readonly IStringLocalizer<ExportCustomersQueryHandler> _localizer;

        public ExportCustomersQueryHandler(
            IExcelService excelService,
            ICustomerDbContext context,
            IStringLocalizer<ExportCustomersQueryHandler> localizer)
        {
            _context = context;
            _excelService = excelService;
            _localizer = localizer;
        }

        public async Task<Result<string>> Handle(ExportCustomersQuery request, CancellationToken cancellationToken)
        {
            var customerFilterSpec = new CustomerFilterSpecification(request.SearchString);
            var customers = await _context.Customers.AsNoTracking().AsQueryable()
                .Specify(customerFilterSpec)
                .ToListAsync(cancellationToken);
            string data = await _excelService.ExportAsync(customers, mappers: new Dictionary<string, Func<Customer, object>>
            {
                { _localizer["Id"], item => item.Id },
                { _localizer["Name"], item => item.Name },
                { _localizer["SurName"], item => item.SurName },
                { _localizer["PhoneNumber"], item => item.PhoneNumber },
                { _localizer["Gender"], item => item.Gender },
                { _localizer["Group"], item => item.Group },
                { _localizer["Email"], item => item.Email }
            }, sheetName: _localizer["Customers"]);

            return await Result<string>.SuccessAsync(data: data);
        }
    }
}