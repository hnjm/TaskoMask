﻿using MediatR;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using TaskoMask.BuildingBlocks.Application.Bus;
using TaskoMask.BuildingBlocks.Application.Commands;
using TaskoMask.BuildingBlocks.Application.Exceptions;
using TaskoMask.BuildingBlocks.Contracts.Events;
using TaskoMask.BuildingBlocks.Contracts.Helpers;
using TaskoMask.BuildingBlocks.Contracts.Resources;
using TaskoMask.BuildingBlocks.Domain.Models;
using TaskoMask.BuildingBlocks.Domain.Resources;
using TaskoMask.Services.Owners.Write.Domain.Data;
using TaskoMask.Services.Owners.Write.Domain.Entities;
using TaskoMask.Services.Owners.Write.Domain.Events.Organizations;
using TaskoMask.Services.Owners.Write.Domain.Services;

namespace TaskoMask.Services.Owners.Write.Application.UseCases.Organizations.AddOrganization
{
    public class AddOrganizationUseCase : BaseCommandHandler, IRequestHandler<AddOrganizationRequest, CommandResult>

    {
        #region Fields

        private readonly IOwnerAggregateRepository _ownerAggregateRepository;
        private readonly IOwnerValidatorService _ownerValidatorService;

        #endregion

        #region Ctors


        public AddOrganizationUseCase(IOwnerAggregateRepository ownerAggregateRepository, IMessageBus messageBus, IOwnerValidatorService ownerValidatorService, IInMemoryBus inMemoryBus) : base(messageBus, inMemoryBus)
        {
            _ownerAggregateRepository = ownerAggregateRepository;
            _ownerValidatorService = ownerValidatorService;
        }

        #endregion

        #region Handlers



        /// <summary>
        /// 
        /// </summary>
        public async Task<CommandResult> Handle(AddOrganizationRequest request, CancellationToken cancellationToken)
        {
            var owner = await _ownerAggregateRepository.GetByIdAsync(request.OwnerId);
            if (owner == null)
                throw new ApplicationException(ContractsMessages.Data_Not_exist, DomainMetadata.Owner);


            var organization = Organization.CreateOrganization(request.Name, request.Description);

            owner.AddOrganization(organization);

            await _ownerAggregateRepository.UpdateAsync(owner);

            await PublishDomainEventsAsync(owner.DomainEvents);

            var organizationAdded = MapOrganizationAddedIntegrationEvent(owner.DomainEvents);

            await PublishIntegrationEventAsync(organizationAdded);

            return new CommandResult(ContractsMessages.Create_Success, organization.Id);
        }


        #endregion

        #region Private Methods


        private OrganizationAdded MapOrganizationAddedIntegrationEvent(IReadOnlyCollection<DomainEvent> domainEvents)
        {
            var OrganizationAddedDomainEvent = (OrganizationAddedEvent)domainEvents.FirstOrDefault(e => e.EventType == nameof(OrganizationAddedEvent));
            return new OrganizationAdded(OrganizationAddedDomainEvent.Id, OrganizationAddedDomainEvent.Name, OrganizationAddedDomainEvent.Description, OrganizationAddedDomainEvent.OwnerId);
        }


        #endregion

    }
}
