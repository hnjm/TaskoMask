﻿using AutoMapper;
using TaskoMask.Application.Core.Helpers;
using System.Collections.Generic;
using System.Threading.Tasks;
using TaskoMask.Application.Team.Organizations.Commands.Models;
using TaskoMask.Application.Team.Organizations.Queries.Models;
using TaskoMask.Application.Core.Dtos.Organizations;
using TaskoMask.Application.Core.Commands;
using TaskoMask.Application.Core.Notifications;
using TaskoMask.Application.Core.ViewModels;
using TaskoMask.Application.Team.Projects.Queries.Models;
using System.Linq;
using TaskoMask.Application.Core.Bus;
using TaskoMask.Application.Common.BaseEntities.Services;
using TaskoMask.Application.TaskManagement.Boards.Queries.Models;
using TaskoMask.Application.TaskManagement.Tasks.Queries.Models;
using TaskoMask.Domain.Team.Entities;

namespace TaskoMask.Application.Team.Organizations.Services
{
    public class OrganizationService : BaseEntityService<Organization>, IOrganizationService
    {
        #region Fields

        #endregion

        #region Ctors

        public OrganizationService(IInMemoryBus inMemoryBus, IMapper mapper, IDomainNotificationHandler notifications) : base(inMemoryBus, mapper, notifications)
        { }

        #endregion

        #region Public Methods




        /// <summary>
        /// 
        /// </summary>
        public async Task<Result<CommandResult>> CreateAsync(OrganizationInputDto input)
        {
            var cmd = new CreateOrganizationCommand(userId: input.UserId, name: input.Name, description: input.Description);
            return await SendCommandAsync(cmd);
        }



        /// <summary>
        /// 
        /// </summary>
        public async Task<Result<CommandResult>> UpdateAsync(OrganizationInputDto input)
        {
            var cmd = new UpdateOrganizationCommand(id: input.Id, name: input.Name, description: input.Description);
            return await SendCommandAsync(cmd);
        }



        /// <summary>
        /// 
        /// </summary>
        public async Task<Result<OrganizationDetailsViewModel>> GetDetailsAsync(string id)
        {
            var organizationQueryResult = await SendQueryAsync(new GetOrganizationByIdQuery(id));
            if (!organizationQueryResult.IsSuccess)
                return Result.Failure<OrganizationDetailsViewModel>(organizationQueryResult.Errors);


            var projectQueryResult = await SendQueryAsync(new GetProjectsByOrganizationIdQuery(id));
            if (!projectQueryResult.IsSuccess)
                return Result.Failure<OrganizationDetailsViewModel>(projectQueryResult.Errors);


            var organizationReportQueryResult = await SendQueryAsync(new GetOrganizationReportQuery(id));
            if (!organizationReportQueryResult.IsSuccess)
                return Result.Failure<OrganizationDetailsViewModel>(organizationReportQueryResult.Errors);


            var boardQueryResult = await SendQueryAsync(new GetBoardsByOrganizationIdQuery(id));
            if (!boardQueryResult.IsSuccess)
                return Result.Failure<OrganizationDetailsViewModel>(boardQueryResult.Errors);


            var taskQueryResult = await SendQueryAsync(new GetTasksByOrganizationIdQuery(id,takeCount:20));
            if (!taskQueryResult.IsSuccess)
                return Result.Failure<OrganizationDetailsViewModel>(taskQueryResult.Errors);

            var organizationDetail = new OrganizationDetailsViewModel
            {
                Organization = organizationQueryResult.Value,
                Projects = projectQueryResult.Value,
                Boards = boardQueryResult.Value,
                LastTasks = taskQueryResult.Value,
                Reports = organizationReportQueryResult.Value,
            };

            return Result.Success(organizationDetail);

        }




        /// <summary>
        /// 
        /// </summary>
        public async Task<Result<IEnumerable<OrganizationDetailsViewModel>>> GetUserOrganizationsDetailAsync(string userId)
        {
            var organizationQueryResult = await SendQueryAsync(new GetOrganizationsByUserIdQuery(userId));
            if (!organizationQueryResult.IsSuccess)
                return Result.Failure<IEnumerable<OrganizationDetailsViewModel>> (organizationQueryResult.Errors);

            var organizationsDetail = new List<OrganizationDetailsViewModel>();

            foreach (var organization in organizationQueryResult.Value)
            {
                var organizationDetailResult = await GetDetailsAsync(organization.Id);
                if (!organizationDetailResult.IsSuccess)
                    return Result.Failure<IEnumerable<OrganizationDetailsViewModel>>(organizationDetailResult.Errors);

                organizationsDetail.Add(organizationDetailResult.Value);
            }

            return Result.Success(organizationsDetail.AsEnumerable());
        }



        /// <summary>
        /// 
        /// </summary>
        public async Task<Result<OrganizationBasicInfoDto>> GetByIdAsync(string id)
        {
            return await SendQueryAsync(new GetOrganizationByIdQuery(id));
        }




        /// <summary>
        /// 
        /// </summary>
        public async Task<Result<IEnumerable<OrganizationBasicInfoDto>>> GetListByUserIdAsync(string userId)
        {
            return await SendQueryAsync(new GetOrganizationsByUserIdQuery(userId));
        }



        /// <summary>
        /// 
        /// </summary>
        public async Task<Result<OrganizationReportDto>> GetReportAsync(string id)
        {
            return await SendQueryAsync(new GetOrganizationReportQuery(id));
        }


        #endregion

        #region Private Methods


        #endregion
    }
}