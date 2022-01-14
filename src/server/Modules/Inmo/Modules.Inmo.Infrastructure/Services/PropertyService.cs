﻿// --------------------------------------------------------------------------------------------------
// <copyright file="PropertyService.cs" company="InmoIT">
// Copyright (c) InmoIT. All rights reserved.
// Developer: Vladimir P. CHibás (vladperchi).
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// --------------------------------------------------------------------------------------------------

using System;
using System.Threading.Tasks;
using InmoIT.Modules.Inmo.Core.Features.Properties.Commands;
using InmoIT.Modules.Inmo.Core.Features.Properties.Queries;
using InmoIT.Shared.Core.Integration.Inmo;
using InmoIT.Shared.Core.Wrapper;
using InmoIT.Shared.Dtos.Inmo.Properties;
using MediatR;

namespace InmoIT.Modules.Inmo.Infrastructure.Services
{
    public class PropertyService : IPropertyService
    {
        private readonly IMediator _mediator;

        public PropertyService(IMediator mediator)
        {
            _mediator = mediator;
        }

        public async Task<Result<GetPropertyByIdResponse>> GetDetailsPropertyAsync(Guid propertyId)
        {
            return await _mediator.Send(new GetPropertyByIdQuery(propertyId, false));
        }

        public async Task<Result<Guid>> RemovePropertyAsync(Guid propertyId)
        {
            return await _mediator.Send(new RemovePropertyCommand(propertyId));
        }
    }
}