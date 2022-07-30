global using System;
global using System.Threading.Tasks;
global using System.Linq;
global using NewLake.Core.Infrastructure;

global using Microsoft.AspNetCore.Builder;
global using Microsoft.AspNetCore.Hosting;
global using Microsoft.AspNetCore.Mvc;
global using Microsoft.Extensions.Configuration;
global using Microsoft.Extensions.DependencyInjection;
global using Microsoft.Extensions.Hosting;
global using Microsoft.Extensions.Logging;

global using FluentValidation;
global using FluentValidation.AspNetCore;

global using NewLake.Api.Infrastructure.Extensions;
global using NewLake.Core;
global using NewLake.Core.Domain.Model;
global using NewLake.Core.Services.Bulk;
global using NewLake.Core.Services.Messaging;

global using StackExchange.Redis;