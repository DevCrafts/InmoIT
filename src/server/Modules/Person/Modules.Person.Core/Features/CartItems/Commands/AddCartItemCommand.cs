﻿// --------------------------------------------------------------------------------------------------
// <copyright file="AddCartItemCommand.cs" company="InmoIT">
// Copyright (c) InmoIT. All rights reserved.
// Developer: Vladimir P. CHibás (vladperchi).
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// --------------------------------------------------------------------------------------------------

using System;
using InmoIT.Shared.Core.Wrapper;
using MediatR;

namespace InmoIT.Modules.Person.Core.Features.CartItems.Commands
{
    public class AddCartItemCommand : IRequest<Result<Guid>>
    {
        public Guid CartId { get; set; }

        public Guid PropertyId { get; set; }
    }
}