// ---------------------------------------------------------------
// Copyright (c) Coalition of the Good-Hearted Engineers
// FREE TO USE TO CONNECT THE WORLD
// ---------------------------------------------------------------

using System.Threading.Tasks;
using Taarafo.Core.Brokers.Storages;
using Taarafo.Core.Models.Events;

namespace Taarafo.Core.Services.Foundations.Events
{
    public class EventService : IEventService
    {
        private readonly IStorageBroker storageBroker;

        public EventService(IStorageBroker storageBroker) =>
            this.storageBroker = storageBroker;

        public async ValueTask<Event> AddEventAsync(Event @event)
        {
            return await this.storageBroker.InsertEventAsync(@event);
        }
    }
}
