using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reactive.Concurrency;
using System.Text;
using System.Threading.Tasks;
using RestSharp;

namespace ClientServicing.Main.Resources.Helper
{
    public static class ApiRequestAndResponseHelper
    {

        /// <summary>
        /// Description:
        /// Builds a RestRequest for the specified HTTP method, attaches headers, query parameters,
        /// and a JSON body when applicable. This method also initializes and starts a Stopwatch
        /// to measure the API call duration, enabling consistent performance timing across requests.
        ///
        /// Benefits:
        /// • Provides a centralized, reusable way to construct RestRequests for various HTTP methods.
        /// • Supports optional headers and query parameters for flexible API interactions.
        /// • Automatically attaches JSON payloads for methods that support request bodies (POST, PUT, PATCH, DELETE).
        /// • Starts and returns a Stopwatch, ensuring consistent timing for request execution.
        /// • Reduces duplicated setup code across your automation framework and improves maintainability.
        ///
        /// Parameters:
        /// <param name="url">The target API endpoint URL where the request will be sent.</param>
        /// <param name="method">The HTTP method to use (e.g., GET, POST, PUT, PATCH, DELETE).</param>
        /// <param name="payload">The JSON-serializable object to include as the request body (if applicable).</param>
        /// <param name="stopwatch">Outputs a Stopwatch instance started at the moment the request is created.</param>
        /// <param name="headers">Optional dictionary of headers to include in the request.</param>
        /// <param name="queryParams">Optional dictionary of query string parameters to append to the URL.</param>
        ///
        /// Returns:
        /// <returns>
        /// A fully constructed RestRequest instance with the specified method, headers, parameters,
        /// and JSON body (when applicable), ready for execution.
        /// </returns>
        /// </summary>
        public static RestRequest GetRequestDetails<T>(
        string url,
        Method method,
        T? payload,
        out Stopwatch stopwatch,
        IDictionary<string, string>? headers = null,
        IDictionary<string, string>? queryParams = null,
        IDictionary<string, int>? urlSegment = null)
        where T : class
        {
            var request = new RestRequest(url, method);

            // Add headers
            if (headers != null)
            {
                foreach (var kv in headers)
                    request.AddOrUpdateHeader(kv.Key, kv.Value);
            }
            // Add query params
            if (queryParams != null)
            {
                foreach (var kv in queryParams)
                    request.AddQueryParameter(kv.Key, kv.Value);
            }
            // Add URL segments
            if (urlSegment != null)
            {
                foreach (var kv in urlSegment)
                    request.AddUrlSegment(kv.Key, kv.Value);
            }
            // Only attach body for methods that support it
            if (method is Method.Post or Method.Put or Method.Patch or Method.Delete)
            {
                request.AddJsonBody(payload);
            }
            stopwatch = Stopwatch.StartNew();
            return request;
        }


        /// <summary>
        /// Description:
        /// Executes a REST API request asynchronously while measuring execution time.
        /// This method centralizes request execution, logging, timing, and exception handling
        /// to ensure consistent API communication behavior across the automation framework.
        ///
        /// Benefits:
        /// • Automatically logs both the request and response using HttpLoggerHelpers.
        /// • Ensures timing consistency by stopping the stopwatch only once, even on exceptions.
        /// • Provides unified error handling through DocumentTemplate for reporting.
        /// • Wraps execution in a safe try/catch block, returning a structured RestResponse on failure.
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
        /// A RestResponse object containing the response details, status code, and content.
        /// On failure, returns a RestResponse with InternalServerError status and an error message.
        /// </returns>
        /// </summary>
        public static async Task<RestResponse> ExecuteAsync(
        RestClient client,
        RestRequest request,
        Stopwatch stopwatch,
        CancellationToken cancellationToken = default)
        {
            try
            {
                var response = await client.ExecuteAsync(request, cancellationToken).ConfigureAwait(false);
                HttpLoggerHelpers.RequestaAndResponseLogging(request, response, null, stopwatch);
                stopwatch.Stop();
                DocumentTemplate.DisplayResponseEnsureSuccess(response);
                return response;
            }
            catch (Exception ex)
            {
                stopwatch.Stop();
                DocumentTemplate.DisplayRequestAndResponseExceptionLogging(ex);
                return new RestResponse
                {
                    StatusCode = System.Net.HttpStatusCode.InternalServerError,
                    ErrorMessage = $"Exception occurred: {ex.Message}"
                };
            }
        }
    }
}
