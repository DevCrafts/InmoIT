﻿// --------------------------------------------------------------------------------------------------
// <copyright file="ImageNotFoundException.cs" company="InmoIT">
// Copyright (c) InmoIT. All rights reserved.
// Developer: Vladimir P. CHibás (vladperchi).
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// --------------------------------------------------------------------------------------------------

using System.Net;
using InmoIT.Shared.Core.Exceptions;
using Microsoft.Extensions.Localization;

namespace InmoIT.Modules.Inmo.Core.Exceptions
{
    public class ImageNotFoundException : CustomException
    {
        public ImageNotFoundException(IStringLocalizer localizer)
            : base(localizer["Image was not found..."], null, HttpStatusCode.NotFound)
        {
        }
    }
}