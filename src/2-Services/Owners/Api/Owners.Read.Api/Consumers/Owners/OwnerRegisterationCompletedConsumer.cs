﻿using System.Threading.Tasks;
using MassTransit;
using TaskoMask.BuildingBlocks.Application.Bus;
using TaskoMask.BuildingBlocks.Contracts.Events;
using TaskoMask.BuildingBlocks.Web.MVC.Consumers;
using TaskoMask.Services.Owners.Read.Api.Domain;
using TaskoMask.Services.Owners.Read.Api.Infrastructure.DbContext;

namespace TaskoMask.Services.Owners.Read.Api.Consumers.Owners;

public class OwnerRegisterationCompletedConsumer : BaseConsumer<OwnerRegisterationCompleted>
{
    private readonly OwnerReadDbContext _ownerReadDbContext;

    public OwnerRegisterationCompletedConsumer(IInMemoryBus inMemoryBus, OwnerReadDbContext ownerReadDbContext)
        : base(inMemoryBus)
    {
        _ownerReadDbContext = ownerReadDbContext;
    }

    public override async Task ConsumeMessage(ConsumeContext<OwnerRegisterationCompleted> context)
    {
        var owner = new Owner(context.Message.Id) { DisplayName = context.Message.DisplayName, Email = context.Message.Email };
        await _ownerReadDbContext.Owners.InsertOneAsync(owner);
    }
}
