// ---------------------------------------------------------------
// Copyright (c) Coalition of the Good-Hearted Engineers
// FREE TO USE TO CONNECT THE WORLD
// ---------------------------------------------------------------

using System.Linq;
using System.Threading.Tasks;
using Taarafo.Core.Models.Events;
using Taarafo.Core.Models.Events.Exceptions;
using Xeptions;

namespace Taarafo.Core.Services.Foundations.Events
{
    public partial class EventService
    {
        private delegate IQueryable<Event> ReturningEventsFunction();
        private delegate ValueTask<Event> ReturningEventFunction();

        private async ValueTask<Event> TryCatch(ReturningEventFunction returningEventFunction)
        {
            try
            {
                return await returningEventFunction();
            }
            catch (NullEventException nullEventException)
            {
                throw CreateAndLogValidationException(nullEventException);
            }
        }

        private EventValidationException CreateAndLogValidationException(
            Xeption exception)
        {
            var eventValidationException =
                new EventValidationException(exception);

            this.loggingBroker.LogError(eventValidationException);

            return eventValidationException;
        }
    }
}
