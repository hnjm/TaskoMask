﻿using Microsoft.Extensions.DependencyInjection;
using TaskoMask.BuildingBlocks.Application.Services;
using TaskoMask.BuildingBlocks.Infrastructure.Extensions;
using Microsoft.Extensions.Configuration;
using TaskoMask.Services.Identity.Infrastructure.CrossCutting.Mapper;

namespace TaskoMask.Services.Identity.Infrastructure.CrossCutting.IoC
{

    /// <summary>
    /// 
    /// </summary>
    public static class IoCConfiguration
    {

  
        /// <summary>
        /// 
        /// </summary>
        public static void AddProjectServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddBuildingBlocksApplicationServices();

            services.AddBuildingBlocksInfrastructureServices();

            services.AddApplicationServices();

            services.AddInfrastructureServices(configuration);

            services.AddAutoMapperSetup();
        }


    }
}