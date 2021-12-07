﻿// --------------------------------------------------------------------------------------------------
// <copyright file="RoleClaimResponse.cs" company="InmoIT">
// Copyright (c) InmoIT. All rights reserved.
// Developer: Vladimir P. CHibás (vladperchi).
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// --------------------------------------------------------------------------------------------------

namespace InmoIT.Shared.Dtos.Identity.Roles
{
    public class RoleClaimResponse
    {
        public int Id { get; set; }

        public string RoleId { get; set; }

        public string Type { get; set; }

        public string Value { get; set; }

        public string Description { get; set; }

        public string Group { get; set; }
    }
}