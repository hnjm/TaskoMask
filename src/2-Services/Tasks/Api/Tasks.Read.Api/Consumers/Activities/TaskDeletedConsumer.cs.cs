﻿using System.Threading.Tasks;
using MassTransit;
using TaskoMask.BuildingBlocks.Application.Bus;
using TaskoMask.BuildingBlocks.Contracts.Events;
using TaskoMask.BuildingBlocks.Web.MVC.Consumers;
using TaskoMask.Services.Tasks.Read.Api.Infrastructure.DbContext;

namespace TaskoMask.Services.Tasks.Read.Api.Consumers.Activities;

public class TaskDeletedConsumer : BaseConsumer<TaskDeleted>
{
    private readonly TaskReadDbContext _taskReadDbContext;

    public TaskDeletedConsumer(IInMemoryBus inMemoryBus, TaskReadDbContext taskReadDbContext)
        : base(inMemoryBus)
    {
        _taskReadDbContext = taskReadDbContext;
    }

    public override async Task ConsumeMessage(ConsumeContext<TaskDeleted> context)
    {
        var activity = new Domain.Activity() { TaskId = context.Message.Id, Description = "Task Deleted" };

        await _taskReadDbContext.Activities.InsertOneAsync(activity);
    }
}
