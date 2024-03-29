global using FluentValidation.AspNetCore;
global using FluentValidation;

global using Grpc.Core;

global using MediatR;

global using Microsoft.AspNetCore.Builder;
global using Microsoft.AspNetCore.Diagnostics;
global using Microsoft.AspNetCore.Diagnostics.HealthChecks;
global using Microsoft.AspNetCore.Hosting;
global using Microsoft.AspNetCore.Http;
global using Microsoft.AspNetCore.Mvc;
global using Microsoft.AspNetCore.Server.Kestrel.Core;
global using Microsoft.EntityFrameworkCore;
global using Microsoft.EntityFrameworkCore.Metadata.Builders;
global using Microsoft.Extensions.Caching.Distributed;
global using Microsoft.Extensions.Configuration;
global using Microsoft.Extensions.DependencyInjection;
global using Microsoft.Extensions.Hosting;
global using Microsoft.Extensions.Logging;
global using Microsoft.Extensions.Options;

global using NewLake.Api.GrpcProto.Services;
global using NewLake.Api.Infrastructure.Extensions;
global using NewLake.Api.Infrastructure.Services.Bulk;
global using NewLake.Api.Infrastructure.Services;
global using NewLake.Api.Infrastructure.Settings;
global using NewLake.Api.Model;

global using Serilog.Events;
global using Serilog;

global using StackExchange.Redis;

global using System.Collections.Generic;
global using System.IO;
global using System.Linq;
global using System.Net.Http;
global using System.Net;
global using System.Reflection;
global using System.Text;
global using System.Threading.Tasks;
global using System.Threading;
global using System;
global using static System.Net.Mime.MediaTypeNames;