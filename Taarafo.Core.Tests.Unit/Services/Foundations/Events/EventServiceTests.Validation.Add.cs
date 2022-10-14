// ---------------------------------------------------------------
// Copyright (c) Coalition of the Good-Hearted Engineers
// FREE TO USE TO CONNECT THE WORLD
// ---------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Moq;
using Taarafo.Core.Models.Events.Exceptions;
using Taarafo.Core.Models.Events;
using Xunit;
using FluentAssertions;

namespace Taarafo.Core.Tests.Unit.Services.Foundations.Events
{
    public partial class EventServiceTests
    {
        [Fact]
        public async Task ShouldThrowValidationExceptionOnAddIfEventIsNullAndLogItAsync()
        {
            // given
            Event nullEvent = null;

            var nullEventException =
                new NullEventException();

            var expectedEventValidationException =
                new EventValidationException(nullEventException);

            // when
            ValueTask<Event> addEventTask =
                this.eventService.AddEventAsync(nullEvent);

            EventValidationException actualEventValidationException =
               await Assert.ThrowsAsync<EventValidationException>(
                   addEventTask.AsTask);

            // then
            actualEventValidationException.Should().BeEquivalentTo(
                expectedEventValidationException);

            this.loggingBrokerMock.Verify(broker =>
                broker.LogError(It.Is(SameExceptionAs(
                    expectedEventValidationException))),
                        Times.Once);

            this.loggingBrokerMock.VerifyNoOtherCalls();
            this.storageBrokerMock.VerifyNoOtherCalls();
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData("   ")]
        public async Task ShouldThrowValidationExceptionOnAddIfEventIsInvalidAndLogItAsync(
            string invalidString)
        {
            // given
            var invalidEvent = new Event
            {
                Name = invalidString,
                Description = invalidString,
                Location = invalidString,
                Image = invalidString
            };

            var invalidEventException =
                new InvalidEventException();

            invalidEventException.AddData(
                key: nameof(Event.Id),
                "Id is required");
            
            invalidEventException.AddData(
                key: nameof(Event.Name),
                "Text is required");

            invalidEventException.AddData(
                key: nameof(Event.Description),
                "Text is required");

            invalidEventException.AddData(
                key: nameof(Event.Image),
                "Image is required");

            invalidEventException.AddData(
                key: nameof(Event.Date),
                "Date is required");

            invalidEventException.AddData(
                key: nameof(Event.CreatedDate),
                "Date is required");

            invalidEventException.AddData(
                key: nameof(Event.UpdatedDate),
                "Date is required");

            invalidEventException.AddData(
                key: nameof(Event.CreatedBy),
                "Id is required");

            invalidEventException.AddData(
                key: nameof(Event.UpdatedBy),
                "Id is required");

            var expectedEventValidationException =
                new EventValidationException(
                    invalidEventException);

            // when
            ValueTask<Event> addEventTask =
                this.eventService.AddEventAsync(invalidEvent);

            EventValidationException actualEventValidationException =
               await Assert.ThrowsAsync<EventValidationException>(
                   addEventTask.AsTask);

            // then
            actualEventValidationException.Should().BeEquivalentTo(
                expectedEventValidationException);

            this.loggingBrokerMock.Verify(broker =>
                broker.LogError(It.Is(SameExceptionAs(
                    expectedEventValidationException))),
                        Times.Once);

            this.loggingBrokerMock.VerifyNoOtherCalls();
            this.storageBrokerMock.VerifyNoOtherCalls();
        }
    }
}
