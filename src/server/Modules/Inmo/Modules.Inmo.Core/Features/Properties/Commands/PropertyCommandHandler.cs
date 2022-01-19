﻿// --------------------------------------------------------------------------------------------------
// <copyright file="PropertyCommandHandler.cs" company="InmoIT">
// Copyright (c) InmoIT. All rights reserved.
// Developer: Vladimir P. CHibás (vladperchi).
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// --------------------------------------------------------------------------------------------------

using System;
using System.Linq;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using InmoIT.Modules.Inmo.Core.Abstractions;
using InmoIT.Modules.Inmo.Core.Entities;
using InmoIT.Modules.Inmo.Core.Exceptions;
using InmoIT.Modules.Inmo.Core.Features.Properties.Events;
using InmoIT.Shared.Core.Common.Extensions;
using InmoIT.Shared.Core.Constants;
using InmoIT.Shared.Core.Interfaces.Services;
using InmoIT.Shared.Core.Wrapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Localization;

namespace InmoIT.Modules.Inmo.Core.Features.Properties.Commands
{
    internal class PropertyCommandHandler :
       IRequestHandler<RegisterPropertyCommand, Result<Guid>>,
        IRequestHandler<UpdatePropertyCommand, Result<Guid>>,
       IRequestHandler<RemovePropertyCommand, Result<Guid>>
    {
        private readonly IDistributedCache _cache;
        private readonly IInmoDbContext _context;
        private readonly IMapper _mapper;
        private readonly IUploadService _uploadService;
        private readonly IStringLocalizer<PropertyCommandHandler> _localizer;

        public PropertyCommandHandler(
            IInmoDbContext context,
            IMapper mapper,
            IUploadService uploadService,
            IStringLocalizer<PropertyCommandHandler> localizer,
            IDistributedCache cache)
        {
            _context = context;
            _mapper = mapper;
            _uploadService = uploadService;
            _localizer = localizer;
            _cache = cache;
        }

        public async Task<Result<Guid>> Handle(RegisterPropertyCommand command, CancellationToken cancellationToken)
        {
            command.CodeInternal = StringExtensions.GenerateCode(10);
            if (await _context.Properties
                .AnyAsync(p => p.CodeInternal == command.CodeInternal, cancellationToken))
            {
                throw new PropertyAlreadyExistsException(_localizer);
            }

            var property = _mapper.Map<Property>(command);
            var fileUploadRequest = command.FileUploadRequest;
            if (fileUploadRequest != null)
            {
                fileUploadRequest.FileName = $"P-{command.FileName}.{fileUploadRequest.Extension}";

                // Upload ImageUrl In PropertyImage
            }

            try
            {
                property.AddDomainEvent(new PropertyRegisteredEvent(property));
                await _context.Properties.AddAsync(property, cancellationToken);
                await _context.SaveChangesAsync(cancellationToken);
                return await Result<Guid>.SuccessAsync(property.Id, _localizer["Property Saved"]);
            }
            catch (Exception)
            {
                throw new PropertyCustomException(_localizer, null);
            }
        }

        public async Task<Result<Guid>> Handle(UpdatePropertyCommand command, CancellationToken cancellationToken)
        {
            if (!await _context.Properties
                .Where(p => p.Id == command.Id)
                .AnyAsync(cancellationToken))
            {
                throw new PropertyNotFoundException(_localizer);
            }

            var property = _mapper.Map<Property>(command);
            var fileUploadRequest = command.FileUploadRequest;
            if (fileUploadRequest != null)
            {
                fileUploadRequest.FileName = $"P-{command.FileName}.{fileUploadRequest.Extension}";

                // Upload ImageUrl In PropertyImage
            }

            try
            {
                property.AddDomainEvent(new PropertyUpdatedEvent(property));
                _context.Properties.Update(property);
                await _context.SaveChangesAsync(cancellationToken);
                await _cache.RemoveAsync(CacheKeys.Common.GetEntityByIdCacheKey<Guid, Property>(command.Id), cancellationToken);
                return await Result<Guid>.SuccessAsync(property.Id, _localizer["Property Updated"]);
            }
            catch (Exception)
            {
                throw new PropertyCustomException(_localizer, null);
            }
        }

        public async Task<Result<Guid>> Handle(RemovePropertyCommand command, CancellationToken cancellationToken)
        {
            var property = await _context.Properties
                .Where(x => x.Id == command.Id)
                .FirstOrDefaultAsync(cancellationToken);
            if (property == null)
            {
                throw new PropertyNotFoundException(_localizer);
            }

            try
            {
                property.AddDomainEvent(new PropertyRemovedEvent(property.Id));
                _context.Properties.Remove(property);
                await _context.SaveChangesAsync(cancellationToken);
                await _cache.RemoveAsync(CacheKeys.Common.GetEntityByIdCacheKey<Guid, Property>(command.Id), cancellationToken);
                return await Result<Guid>.SuccessAsync(property.Id, _localizer["Property Deleted"]);
            }
            catch (Exception)
            {
                throw new PropertyCustomException(_localizer, null);
            }
        }
    }
}