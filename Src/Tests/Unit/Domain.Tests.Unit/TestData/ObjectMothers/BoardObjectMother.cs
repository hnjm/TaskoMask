﻿using MongoDB.Bson;
using TaskoMask.Domain.Tests.Unit.TestData.DataBuilders;
using TaskoMask.Domain.WriteModel.Workspace.Boards.Entities;
using TaskoMask.Domain.WriteModel.Workspace.Boards.Services;

namespace TaskoMask.Domain.Tests.Unit.TestData.ObjectMothers
{
    internal static class BoardObjectMother
    {
        private const string _name = "Test Name";
        private const string _description = "Test Description";
        private static string _projectId = ObjectId.GenerateNewId().ToString();

        public static Board CreateNewBoard(IBoardValidatorService boardValidatorService)
        {
            return BoardBuilder.Init(boardValidatorService)
                  .WithProjectId(_projectId)
                  .WithName(_name)
                  .WithDescription(_description)
                  .Build();
        }



        public static Board CreateNewBoard(string name, string description, IBoardValidatorService boardValidatorService)
        {
            return BoardBuilder.Init(boardValidatorService)
                  .WithProjectId(_projectId)
                  .WithName(name)
                  .WithDescription(description)
                  .Build();
        }



        public static Board CreateNewBoardWithProjectId(string projectId, IBoardValidatorService boardValidatorService)
        {
            return BoardBuilder.Init(boardValidatorService)
                  .WithProjectId(projectId)
                  .WithName(_name)
                  .WithDescription(_description)
                  .Build();
        }



        public static Board CreateNewBoardWithName(string name, IBoardValidatorService boardValidatorService)
        {
            return BoardBuilder.Init(boardValidatorService)
                  .WithProjectId(_projectId)
                  .WithName(name)
                  .WithDescription(_description)
                  .Build();
        }

    }
}