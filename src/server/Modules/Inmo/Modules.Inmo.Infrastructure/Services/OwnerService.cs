﻿// --------------------------------------------------------------------------------------------------
// <copyright file="OwnerService.cs" company="InmoIT">
// Copyright (c) InmoIT. All rights reserved.
// Developer: Vladimir P. CHibás (vladperchi).
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// --------------------------------------------------------------------------------------------------

using System;
using System.Threading.Tasks;
using InmoIT.Modules.Inmo.Core.Features.Owners.Commands;
using InmoIT.Modules.Inmo.Core.Features.Owners.Queries;
using InmoIT.Shared.Core.Integration.Inmo;
using InmoIT.Shared.Core.Wrapper;
using InmoIT.Shared.Dtos.Inmo.Owners;
using InmoIT.Shared.Infrastructure.Common;
using MediatR;

namespace InmoIT.Modules.Inmo.Infrastructure.Services
{
    public class OwnerService : IOwnerService
    {
        private readonly IMediator _mediator;

        public OwnerService(IMediator mediator)
        {
            _mediator = mediator;
        }

        public async Task<Result<GetOwnerByIdResponse>> GetDetailsOwnerAsync(Guid ownerId)
        {
            return await _mediator.Send(new GetOwnerByIdQuery(ownerId, false));
        }

        public async Task<Result<Guid>> RemoveOwnerAsync(Guid ownerId)
        {
            return await _mediator.Send(new RemoveOwnerCommand(ownerId));
        }

        public async Task<string> GenerateFileName(int length)
        {
            return await Utilities.GenerateCode("O", length);
        }
    }
}