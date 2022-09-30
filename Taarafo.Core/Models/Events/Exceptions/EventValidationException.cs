using Xeptions;

namespace Taarafo.Core.Models.Events.Exceptions
{
    public class EventValidationException : Xeption
    {
        public EventValidationException(Xeption innerException)
            : base(message: "Event validation errors occurred, please try again.",
                  innerException)
        { }
    }
}