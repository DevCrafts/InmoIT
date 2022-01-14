﻿// --------------------------------------------------------------------------------------------------
// <copyright file="StringExtensions.cs" company="InmoIT">
// Copyright (c) InmoIT. All rights reserved.
// Developer: Vladimir P. CHibás (vladperchi).
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// --------------------------------------------------------------------------------------------------

#nullable enable

using System;
using System.Linq;

namespace InmoIT.Shared.Core.Common
{
    public static class StringExtensions
    {
        private static readonly Random Random = new Random();

        public static string NullToString(this object? Value) => Value?.ToString() ?? string.Empty;

        public static string GenerateCode(int length)
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            return new string(Enumerable.Repeat("INMO" + chars, length)
              .Select(s => s[Random.Next(s.Length)]).ToArray());
        }
    }
}