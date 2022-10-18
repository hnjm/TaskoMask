﻿using TaskoMask.BuildingBlocks.Application.Queries;
using TaskoMask.BuildingBlocks.Contracts.Dtos.Owners;

namespace TaskoMask.Services.Owners.Read.Api.Features.GetOwnerById
{
    public class GetOwnerByIdRequest : BaseQuery<OwnerBasicInfoDto>
    {
        public GetOwnerByIdRequest(string id)
        {
            Id = id;
        }


        public string Id { get; }


    }
}
