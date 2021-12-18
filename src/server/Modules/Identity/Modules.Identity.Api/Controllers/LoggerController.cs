﻿// --------------------------------------------------------------------------------------------------
// <copyright file="LoggerController.cs" company="InmoIT">
// Copyright (c) InmoIT. All rights reserved.
// Developer: Vladimir P. CHibás (vladperchi).
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// --------------------------------------------------------------------------------------------------

using System.Threading.Tasks;

using InmoIT.Modules.Identity.Infrastructure.Permissions;
using InmoIT.Shared.Core.Constants;
using InmoIT.Shared.Core.Interfaces.Services;
using InmoIT.Shared.Dtos.Identity.Logging;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace InmoIT.Modules.Identity.Api.Controllers
{
    [ApiVersion("1")]
    internal sealed class LoggerController : BaseController
    {
        private readonly ILoggerService _eventLog;

        public LoggerController(ILoggerService eventLog)
        {
            _eventLog = eventLog;
        }

        [HttpGet]
        [HavePermission(PermissionsConstant.Logs.ViewAll)]
        public async Task<IActionResult> GetAllAsync([FromQuery] PaginatedLogFilter filter)
        {
            var request = Mapper.Map<GetAllLogsRequest>(filter);
            var eventLogs = await _eventLog.GetAllAsync(request);
            return Ok(eventLogs);
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> LogCustomEventAsync(LogRequest request)
        {
            var result = await _eventLog.LogCustomEventAsync(request);
            return Ok(result);
        }
    }
}