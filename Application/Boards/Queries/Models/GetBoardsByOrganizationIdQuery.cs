﻿using System.Collections.Generic;
using TaskoMask.Application.Core.Dtos.Boards;
using TaskoMask.Application.Core.Queries;


namespace TaskoMask.Application.Boards.Queries.Models
{
   
    public class GetBoardsByOrganizationIdQuery : BaseQuery<IEnumerable<BoardBasicInfoDto>>
    {
        public GetBoardsByOrganizationIdQuery(string organizationId)
        {
            OrganizationId = organizationId;
        }

        public string OrganizationId { get; }
    }
}