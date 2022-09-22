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
using Taarafo.Core.Brokers.Storages;
using Taarafo.Core.Models.Events;
using Taarafo.Core.Services.Foundations.Events;
using Tynamix.ObjectFiller;

namespace Taarafo.Core.Tests.Unit.Services.Foundations.Events
{
    public partial class EventServiceTests
    {
        private readonly Mock<IStorageBroker> storageBrokerMock;
        private readonly IEventService eventService;

        public EventServiceTests()
        {
            this.storageBrokerMock = new Mock<IStorageBroker>();

            this.eventService = new EventService(
                this.storageBrokerMock.Object);
        }

        private static Event CreateRandomEvent() =>
            CreateEventFiller().Create();

        private static Filler<Event> CreateEventFiller()
        {
            var filler = new Filler<Event>();

            return filler;
        }
    }
}
