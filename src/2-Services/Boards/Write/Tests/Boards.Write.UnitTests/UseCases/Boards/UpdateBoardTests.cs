﻿using FluentAssertions;
using MongoDB.Bson;
using NSubstitute;
using TaskoMask.BuildingBlocks.Contracts.Events;
using TaskoMask.BuildingBlocks.Contracts.Resources;
using TaskoMask.BuildingBlocks.Domain.Resources;
using TaskoMask.Services.Boards.Write.Application.UseCases.Boards.UpdateBoard;
using TaskoMask.Services.Boards.Write.Domain.Events.Boards;
using TaskoMask.Services.Boards.Write.UnitTests.Fixtures;
using Xunit;

namespace TaskoMask.Services.Boards.Write.UnitTests.UseCases.Boards
{
    public class UpdateBoardTests : TestsBaseFixture
    {

        #region Fields

        private UpdateBoardUseCase _updateBoardUseCase;

        #endregion

        #region Ctor

        public UpdateBoardTests()
        {
        }

        #endregion

        #region Test Methods


        [Fact]
        public async Task Board_Is_Updated()
        {
            //Arrange
            var expectedBoard = Boards.FirstOrDefault();
            var updateBoardRequest = new UpdateBoardRequest(expectedBoard.Id, "Test_New_Name", "Test_New_Description");

            //Act
            var result = await _updateBoardUseCase.Handle(updateBoardRequest, CancellationToken.None);

            //Assert
            result.Message.Should().Be(ContractsMessages.Update_Success);
            result.EntityId.Should().Be(expectedBoard.Id);

            expectedBoard.Name.Value.Should().Be(updateBoardRequest.Name);

            await InMemoryBus.Received(1).PublishEvent(Arg.Any<BoardUpdatedEvent>());
            await MessageBus.Received(1).Publish(Arg.Any<BoardUpdated>());
        }




        #endregion

        #region Fixture

        protected override void TestClassFixtureSetup()
        {
            _updateBoardUseCase = new UpdateBoardUseCase(BoardAggregateRepository, MessageBus, InMemoryBus,BoardValidatorService);
        }

        #endregion
    }
}
