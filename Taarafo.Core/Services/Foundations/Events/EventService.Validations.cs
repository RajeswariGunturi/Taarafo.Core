// ---------------------------------------------------------------
// Copyright (c) Coalition of the Good-Hearted Engineers
// FREE TO USE TO CONNECT THE WORLD
// ---------------------------------------------------------------

using Taarafo.Core.Models.Comments.Exceptions;
using Taarafo.Core.Models.Comments;
using Taarafo.Core.Models.Events;
using Taarafo.Core.Models.Posts;
using Taarafo.Core.Models.Events.Exceptions;
using System;

namespace Taarafo.Core.Services.Foundations.Events
{
    public partial class EventService
    {

        private void ValidateEventOnAdd(Event @event)
        {
            ValidateEventIsNotNull(@event);

            Validate(
               (Rule: IsInvalid(@event.Id), Parameter: nameof(Event.Id)),
               (Rule: IsInvalid(@event.Name), Parameter: nameof(Event.Name)),
               (Rule: IsInvalid(@event.Description), Parameter: nameof(Event.Description)),
               (Rule: IsInvalid(@event.Date), Parameter: nameof(Event.Date)),
               (Rule: IsInvalid(@event.Location), Parameter: nameof(Event.Location)),
               (Rule: IsInvalidImage(@event.Image), Parameter: nameof(Event.Image)),
               (Rule: IsInvalid(@event.CreatedDate), Parameter: nameof(Event.CreatedDate)),
               (Rule: IsInvalid(@event.UpdatedDate), Parameter: nameof(Event.UpdatedDate)),
               (Rule: IsInvalid(@event.CreatedBy), Parameter: nameof(Event.CreatedBy)),
               (Rule: IsInvalid(@event.UpdatedBy), Parameter: nameof(Event.UpdatedBy)));
        }

        private static void ValidateEventIsNotNull(Event @event)
        {
            if (@event is null)
            {
                throw new NullEventException();
            }
        }

        private static dynamic IsInvalid(Guid id) => new
        {
            Condition = id == Guid.Empty,
            Message = "Id is required"
        };

        private static dynamic IsInvalid(String name) => new
        {
            Condition = String.IsNullOrWhiteSpace(name),
            Message = "Text is required"
        };

        private static dynamic IsInvalidImage(String image) => new
        {
            Condition = String.IsNullOrWhiteSpace(image),
            Message = "Image is required"
        };

        private static dynamic IsInvalid(DateTime dateTime) => new
        {
            Condition = dateTime == default,
            Message = "Date is required"
        };

        private static void Validate(params (dynamic Rule, string Parameter)[] validations)
        {
            var invalidEventException = new InvalidEventException();

            foreach ((dynamic rule, string parameter) in validations)
            {
                if (rule.Condition)
                {
                    invalidEventException.UpsertDataList(
                        key: parameter,
                        value: rule.Message);
                }
            }

            invalidEventException.ThrowIfContainsErrors();
        }

    }
}
