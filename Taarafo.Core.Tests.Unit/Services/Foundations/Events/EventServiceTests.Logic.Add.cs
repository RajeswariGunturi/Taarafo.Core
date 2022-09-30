// ---------------------------------------------------------------
// Copyright (c) Coalition of the Good-Hearted Engineers
// FREE TO USE TO CONNECT THE WORLD
// ---------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentAssertions;
using Force.DeepCloner;
using Moq;
using Taarafo.Core.Brokers.Storages;
using Taarafo.Core.Models.Events;
using Taarafo.Core.Models.Events.Exceptions;
using Taarafo.Core.Services.Foundations.Events;
using Tynamix.ObjectFiller;
using Xunit;

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

        [Fact]
        public async Task ShouldAddEventAsync()
        {
            // given
            Event randomEvent = CreateRandomEvent();
            Event inputEvent = randomEvent;
            Event insertedEvent = inputEvent;
            Event expectedEvent = insertedEvent.DeepClone();

            this.storageBrokerMock.Setup(broker =>
                broker.InsertEventAsync(inputEvent))
                    .ReturnsAsync(insertedEvent);
            // when
            Event actualEvent =
                await this.eventService.AddEventAsync(
                    inputEvent);

            // then
            actualEvent.Should().BeEquivalentTo(expectedEvent);

            this.storageBrokerMock.Verify(broker =>
                broker.InsertEventAsync(inputEvent),
                    Times.Once);

            this.storageBrokerMock.VerifyNoOtherCalls();
        }

        [Fact]
        public void ShouldDoStuff()
        {
            List<string> strings = new List<string>();

            strings.Where(ValueIsRajee);

        }
            static Func<string, bool> ValueIsRajee =
                s => s is "Rajee";
    }
}
