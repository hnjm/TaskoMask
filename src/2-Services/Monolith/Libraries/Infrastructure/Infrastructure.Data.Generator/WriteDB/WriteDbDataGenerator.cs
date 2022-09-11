﻿using TaskoMask.BuildingBlocks.Contracts.Enums;
using TaskoMask.Services.Monolith.Domain.DomainModel.Membership.Entities;
using TaskoMask.Services.Monolith.Domain.DomainModel.Workspace.Boards.Entities;
using TaskoMask.Services.Monolith.Domain.DomainModel.Workspace.Boards.Services;
using TaskoMask.Services.Monolith.Domain.DomainModel.Workspace.Owners.Entities;
using TaskoMask.Services.Monolith.Domain.DomainModel.Workspace.Tasks.Services;
using TaskoMask.Services.Monolith.Infrastructure.Data.Generator.ReadDB;

namespace TaskoMask.Services.Monolith.Infrastructure.Data.Generator.WriteDB
{
    internal static class WriteDbDataGenerator
    {

        /// <summary>
        /// 
        /// </summary>
        public static IEnumerable<Permission> GeneratePermission()
        {
            var items = new List<Permission>();

            for (int i = 1; i <= 10; i++)
            {
                var groupNumber = i > 5 ? 1 : 0;
                items.Add(new Permission
                {
                    DisplayName = $"Permission_{i}",
                    SystemName = $"SystemName_{i}",
                    GroupName = $"Group_{groupNumber}",
                });
            }

            return items;
        }




        /// <summary>
        /// 
        /// </summary>
        public static IEnumerable<Role> GenerateRole(IEnumerable<Permission> permissions)
        {
            var items = new List<Role>();

            for (int i = 1; i <= 5; i++)
            {
                var permissionsId = permissions.Select(p => p.Id).ToArray();
                var permissionsCount = permissions.Count();
                var permissionTakeCount = i < permissionsCount ? i : permissionsCount;

                items.Add(new Role
                {
                    Name = $"Role Name {i}",
                    Description = $"Test Description {i}",
                    PermissionsId = permissionsId.Take(permissionTakeCount).ToArray(),
                });
            }

            return items;
        }



        /// <summary>
        /// 
        /// </summary>
        public static IEnumerable<Board> GenerateBoard(IEnumerable<Owner> owners, IBoardValidatorService boardValidatorService)
        {
            var items = new List<Board>();

            foreach (var owner in owners)
                foreach (var organization in owner.Organizations)
                    foreach (var project in organization.Projects)
                    {
                        var boards = ReadDbDataGenerator.GenerateBoard();
                        foreach (var board in boards)
                        {
                            var boardAggregate = Board.AddBoard(board.Name, board.Description, project.Id, boardValidatorService);

                            var cards = ReadDbDataGenerator.GenerateCard();
                            foreach (var card in cards)
                                boardAggregate.AddCard(Card.Create(card.Name, card.Type));

                            items.Add(boardAggregate);
                        }


                    }

            return items;
        }




        /// <summary>
        /// 
        /// </summary>
        public static IEnumerable<Domain.DomainModel.Workspace.Tasks.Entities.Task> GenerateTasks(IEnumerable<Board> boards, ITaskValidatorService taskValidatorService)
        {
            var items = new List<Domain.DomainModel.Workspace.Tasks.Entities.Task>();

            foreach (var board in boards)
                foreach (var card in board.Cards)
                {
                    var tasks = ReadDbDataGenerator.GenerateTasks();
                    foreach (var task in tasks)
                    {
                        var taskAggregate = Domain.DomainModel.Workspace.Tasks.Entities.Task.AddTask(task.Title, task.Description, card.Id, board.Id, taskValidatorService);
                        items.Add(taskAggregate);
                    }
                }

            return items;
        }

    }
}