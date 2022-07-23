﻿using TaskoMask.Application.Share.Dtos.Workspace.Comments;
using TaskoMask.Application.Share.Helpers;

namespace TaskoMask.Presentation.Framework.Share.Contracts
{
    public interface ICommentClientService
    {
        Task<Result<CommentBasicInfoDto>> Get(string id);
        Task<Result<CommandResult>> Create(CommentUpsertDto input);
        Task<Result<CommandResult>> Update(string id,CommentUpsertDto input);
        Task<Result<CommandResult>> Delete(string id);
    }
}
