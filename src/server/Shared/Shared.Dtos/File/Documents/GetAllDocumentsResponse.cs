﻿// --------------------------------------------------------------------------------------------------
// <copyright file="GetAllDocumentsResponse.cs" company="InmoIT">
// Copyright (c) InmoIT. All rights reserved.
// Developer: Vladimir P. CHibás (vladperchi).
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// --------------------------------------------------------------------------------------------------

using System;

namespace InmoIT.Shared.Dtos.File.Documents
{
    public record GetAllDocumentsResponse(Guid Id, string Title, string Description, bool IsPublic, string CreatedBy, DateTime CreatedOn, string DocumentUrl, string DocumentTypeName, Guid DocumentTypeId, Guid PropertyId);
}