using System.Diagnostics;
using System.Net;
using System.Text.Json;
using RestSharp;

namespace ClientServicing.Main.Resources.Helper
{

    public sealed class ApiAuthRequestAndResponseHelper
    {
        private static readonly JsonSerializerOptions CaseInsensitive = new()
        {
            PropertyNameCaseInsensitive = true
        };

        public sealed class AuthResult
        {
            public bool IsSuccess { get; init; }
            public string? Token { get; init; }
            public RestResponse? Response { get; init; }
            public string? Error { get; init; }
        }

        private sealed class TokenEnvelope
        {
            public string? Token { get; init; }
            public string? Access_Token { get; init; }
            public string? AccessToken { get; init; }
            public string? Id_Token { get; init; }
            public string? IdToken { get; init; }
            public DataNode? Data { get; init; }

            public sealed class DataNode
            {
                public string? Token { get; init; }
            }

            public string? GetAnyToken() =>
                Token
                ?? Access_Token
                ?? AccessToken
                ?? Id_Token
                ?? IdToken
                ?? Data?.Token;
        }


        /// <summary>
        /// Name: AuthenticateCredentialsAsync
        /// Description:
        /// Executes a REST API request asynchronously while measuring execution time.
        /// This method centralizes request execution, logging, timing, and exception handling
        /// to ensure consistent API communication behavior across the automation framework.
        ///
        /// Benefits:
        /// • Automatically logs both the request and response using HttpLoggerHelpers.
        /// • Ensures timing consistency by stopping the stopwatch only once, even on exceptions.
        /// • Provides unified error handling through DocumentTemplate for reporting.
        /// • Wraps execution in a safe try/catch/finally block, returning a structured AuthResult.
        /// • Improves reusability and reduces code duplication across tests and API clients.
        ///
        /// Parameters:
        /// <param name="client">The RestClient instance used to send the request.</param>
        /// <param name="request">The REST request object containing URL, method, headers, and payload.</param>
        /// <param name="stopwatch">The stopwatch used to measure total API call duration.</param>
        /// <param name="cancellationToken">Optional token for cancelling the request.</param>
        ///
        /// Returns:
        /// <returns>
        /// An AuthResult that includes the RestResponse (when available), a token if present, and success/failure info.
        /// </returns>
        /// </summary>

        public static async Task<AuthResult> AuthenticateCredentialsAsync(
            RestClient client,
            RestRequest request,
            Stopwatch stopwatch,
            CancellationToken cancellationToken = default)
        {
            ArgumentNullException.ThrowIfNull(client);
            ArgumentNullException.ThrowIfNull(request);
            ArgumentNullException.ThrowIfNull(stopwatch);

            try
            {
                var response = await client.PostAsync(request, cancellationToken).ConfigureAwait(false);

                if (response is null)
                {
                    DocumentTemplate.DisplayRequestAndResponseExceptionLogging(
                        new InvalidOperationException("Null response from RestClient.PostAsync."));
                    return new AuthResult
                    {
                        IsSuccess = false,
                        Error = "Null response received"
                    };
                }

                if (!response.IsSuccessful)
                {
                    DocumentTemplate.DisplayRequestAndResponseExceptionLogging(
                        new HttpRequestException($"Auth request failed with {response.StatusCode}: {response.ErrorMessage}"));
                    return new AuthResult
                    {
                        IsSuccess = false,
                        Response = response,
                        Error = $"HTTP {(int)response.StatusCode} {response.StatusCode}: {response.ErrorMessage}"
                    };
                }

                string? token = null;

                if (!string.IsNullOrWhiteSpace(response.Content))
                {
                    try
                    {
                        var envelope = JsonSerializer.Deserialize<TokenEnvelope>(response.Content!, CaseInsensitive);
                        token = envelope?.GetAnyToken();
                    }
                    catch (JsonException jx)
                    {
                        DocumentTemplate.DisplayRequestAndResponseExceptionLogging(jx);
                        // You can choose to treat this as failure or success-without-token.
                    }
                }

                HttpLoggerHelpers.RequestAndResponseLogging(request, response, null, stopwatch);

                return new AuthResult
                {
                    IsSuccess = true,
                    Token = token,
                    Response = response
                };
            }
            catch (Exception ex)
            {
                DocumentTemplate.DisplayRequestAndResponseExceptionLogging(ex);
                return new AuthResult
                {
                    IsSuccess = false,
                    Error = $"Exception occurred: {ex.Message}"
                };
            }

            finally
            {
                if (stopwatch.IsRunning)
                    stopwatch.Stop();
            }

        }
    }
}