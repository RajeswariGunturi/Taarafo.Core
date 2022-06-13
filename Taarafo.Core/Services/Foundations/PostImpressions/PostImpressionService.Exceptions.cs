// ---------------------------------------------------------------
// Copyright (c) Coalition of the Good-Hearted Engineers
// FREE TO USE TO CONNECT THE WORLD
// ---------------------------------------------------------------

using System.Threading.Tasks;
using Microsoft.Data.SqlClient;
using Taarafo.Core.Models.PostImpressions;
using Taarafo.Core.Models.PostImpressions.Exceptions;
using Xeptions;

namespace Taarafo.Core.Services.Foundations.PostImpressions
{
    public partial class PostImpressionService
    {
        private delegate ValueTask<PostImpression> ReturningPostImpressionFunction();

        private async ValueTask<PostImpression> TryCatch(ReturningPostImpressionFunction returningPostImpressionFunction)
        {
            try
            {
                return await returningPostImpressionFunction();
            }
            catch (NullPostImpressionException nullPostImpressionException)
            {
                throw CreateAndLogValidationException(nullPostImpressionException);
            }
            catch (InvalidPostImpressionException invalidPostImpressionException)
            {
                throw CreateAndLogValidationException(invalidPostImpressionException);
            }
            catch (SqlException sqlException)
            {
                var failedPostImpressionStorageException =
                    new FailedPostImpressionStorageException(sqlException);

                throw CreateAndLogCriticalDependencyException(failedPostImpressionStorageException);
            }
        }

        private PostImpressionValidationException CreateAndLogValidationException(Xeption exception)
        {
            var postImpressionValidationException =
                new PostImpressionValidationException(exception);

            this.loggingBroker.LogError(postImpressionValidationException);

            return postImpressionValidationException;
        }

        private PostImpressionDependencyException CreateAndLogCriticalDependencyException (Xeption exception)
        {
            var postImpressionDependencyException = new PostImpressionDependencyException(exception);
            this.loggingBroker.LogCritical(postImpressionDependencyException);

            return postImpressionDependencyException;
        }
    }
}