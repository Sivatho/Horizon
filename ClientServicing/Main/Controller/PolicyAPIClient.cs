using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Security;
using System.Text;
using System.Threading.Tasks;
using ClientServicing.Main.IController;
using ClientServicing.Main.Resources.EndPoints.Bank;
using ClientServicing.Main.Resources.EndPoints.GSD;
using ClientServicing.Main.Resources.EndPoints.Policy;
using ClientServicing.Main.Resources.Helper;
using RestSharp;
using static ClientServicing.Main.Resources.EndPoints.Policy.PolicyAPIEndPoints;

namespace ClientServicing.Main.Controller
{
    public class PolicyAPIClient : IPolicy
    {
        readonly RestClient restClient;
        readonly UtilitiesHelper utilitiesHelper = new UtilitiesHelper();

        public PolicyAPIClient()
        {
            var options = new RestClientOptions()
            {
                BaseUrl = new Uri(utilitiesHelper.GetApiBaseUrl()),
                RemoteCertificateValidationCallback = (sender, certificate, chain, sslPolicyErrors) => true
            };
            restClient = new RestClient(options);
        }

        public async Task<RestResponse> AdvancedPersonSearchAsync<T>(T payload) where T : class
        {
            try
            {
                //Arrange
                var request = new RestRequest(PolicyAPIEndPoints.GetEndPoint(EndPoints.AdvancedPersonSearch), Method.Post);
                request.AddJsonBody(payload);
                var stopWatch = Stopwatch.StartNew();

                //Act
                var response = await restClient.ExecuteAsync(request);
                HttpLoggerHelpers.RequestaAndResponseLogging(request, response, null, stopWatch);

                //Assert
                if (!response.IsSuccessful)
                {
                    TestContext.Out.WriteLine($"API call failed with status code: {response.StatusCode} and message: {response.Content}");
                }
                return response;
            }
            catch (Exception ex)
            {
                TestContext.Out.WriteLine($"Exception occurred: {ex.Message}");
                TestContext.Out.WriteLine($"Stack Trace: {ex.StackTrace}");

                return new RestResponse
                {
                    StatusCode = System.Net.HttpStatusCode.InternalServerError,
                    ErrorMessage = $"Exception occurred: {ex.Message}"
                };
            }
        }

        public async Task<RestResponse> ChangeMainMemberUpsertAsync<T>(T payload) where T : class
        {
            try
            {
                //Arrange
                var request = new RestRequest(PolicyAPIEndPoints.GetEndPoint(EndPoints.ChangeMainMemberUpsert), Method.Post);
                request.AddJsonBody(payload);
                var stopWatch = Stopwatch.StartNew();

                //Act
                var response = await restClient.ExecuteAsync(request);
                HttpLoggerHelpers.RequestaAndResponseLogging(request, response, null, stopWatch);

                //Assert
                if (!response.IsSuccessful)
                {
                    TestContext.Out.WriteLine($"API call failed with status code: {response.StatusCode} and message: {response.Content}");
                }
                return response;
            }
            catch (Exception ex)
            {
                TestContext.Out.WriteLine($"Exception occurred: {ex.Message}");
                TestContext.Out.WriteLine($"Stack Trace: {ex.StackTrace}");

                return new RestResponse
                {
                    StatusCode = System.Net.HttpStatusCode.InternalServerError,
                    ErrorMessage = $"Exception occurred: {ex.Message}"
                };
            }
        }

        public async Task<RestResponse> ChangePolicyDOCAsync<T>(T payload) where T : class
        {
            try
            {
                //Arrange
                var request = new RestRequest(PolicyAPIEndPoints.GetEndPoint(EndPoints.ChangePolicyDOC), Method.Post);
                request.AddJsonBody(payload);
                var stopWatch = Stopwatch.StartNew();

                //Act
                var response = await restClient.ExecuteAsync(request);
                HttpLoggerHelpers.RequestaAndResponseLogging(request, response, null, stopWatch);

                //Assert
                if (!response.IsSuccessful)
                {
                    TestContext.Out.WriteLine($"API call failed with status code: {response.StatusCode} and message: {response.Content}");
                }
                return response;
            }
            catch (Exception ex)
            {
                TestContext.Out.WriteLine($"Exception occurred: {ex.Message}");
                TestContext.Out.WriteLine($"Stack Trace: {ex.StackTrace}");

                return new RestResponse
                {
                    StatusCode = System.Net.HttpStatusCode.InternalServerError,
                    ErrorMessage = $"Exception occurred: {ex.Message}"
                };
            }
        }

        public async Task<RestResponse> CheckHasProductAsync<T>(T payload) where T : class
        {
            try
            {
                //Arrange
                var request = new RestRequest(PolicyAPIEndPoints.GetEndPoint(EndPoints.CheckHasProduct), Method.Post);
                request.AddJsonBody(payload);
                var stopWatch = Stopwatch.StartNew();

                //Act
                var response = await restClient.ExecuteAsync(request);
                HttpLoggerHelpers.RequestaAndResponseLogging(request, response, null, stopWatch);

                //Assert
                if (!response.IsSuccessful)
                {
                    TestContext.Out.WriteLine($"API call failed with status code: {response.StatusCode} and message: {response.Content}");
                }
                return response;
            }
            catch (Exception ex)
            {
                TestContext.Out.WriteLine($"Exception occurred: {ex.Message}");
                TestContext.Out.WriteLine($"Stack Trace: {ex.StackTrace}");

                return new RestResponse
                {
                    StatusCode = System.Net.HttpStatusCode.InternalServerError,
                    ErrorMessage = $"Exception occurred: {ex.Message}"
                };
            }
        }

        public async Task<RestResponse> CheckPolicyIfMainMemberOnlyAsync<T>(T payload) where T : class
        {
            try
            {
                //Arrange
                var request = new RestRequest(PolicyAPIEndPoints.GetEndPoint(EndPoints.CheckPolicyIfMainMemberOnly), Method.Post);
                request.AddJsonBody(payload);
                var stopWatch = Stopwatch.StartNew();

                //Act
                var response = await restClient.ExecuteAsync(request);
                HttpLoggerHelpers.RequestaAndResponseLogging(request, response, null, stopWatch);

                //Assert
                if (!response.IsSuccessful)
                {
                    TestContext.Out.WriteLine($"API call failed with status code: {response.StatusCode} and message: {response.Content}");
                }
                return response;
            }
            catch (Exception ex)
            {
                TestContext.Out.WriteLine($"Exception occurred: {ex.Message}");
                TestContext.Out.WriteLine($"Stack Trace: {ex.StackTrace}");

                return new RestResponse
                {
                    StatusCode = System.Net.HttpStatusCode.InternalServerError,
                    ErrorMessage = $"Exception occurred: {ex.Message}"
                };
            }
        }

        public async Task<RestResponse> CheckRefundAvailabilityAsync<T>(T payload) where T : class
        {
            try
            {
                //Arrange
                var request = new RestRequest(PolicyAPIEndPoints.GetEndPoint(EndPoints.CheckPolicyIfMainMemberOnly), Method.Post);
                request.AddJsonBody(payload);
                var stopWatch = Stopwatch.StartNew();

                //Act
                var response = await restClient.ExecuteAsync(request);
                HttpLoggerHelpers.RequestaAndResponseLogging(request, response, null, stopWatch);

                //Assert
                if (!response.IsSuccessful)
                {
                    TestContext.Out.WriteLine($"API call failed with status code: {response.StatusCode} and message: {response.Content}");
                }
                return response;
            }
            catch (Exception ex)
            {
                TestContext.Out.WriteLine($"Exception occurred: {ex.Message}");
                TestContext.Out.WriteLine($"Stack Trace: {ex.StackTrace}");

                return new RestResponse
                {
                    StatusCode = System.Net.HttpStatusCode.InternalServerError,
                    ErrorMessage = $"Exception occurred: {ex.Message}"
                };
            }
        }

        public async Task<RestResponse> CheckRestartEligibilityAsync<T>(T payload) where T : class
        {
            try
            {
                //Arrange
                var request = new RestRequest(PolicyAPIEndPoints.GetEndPoint(EndPoints.CheckRestartEligibility), Method.Post);
                request.AddJsonBody(payload);
                var stopWatch = Stopwatch.StartNew();

                //Act
                var response = await restClient.ExecuteAsync(request);
                HttpLoggerHelpers.RequestaAndResponseLogging(request, response, null, stopWatch);

                //Assert
                if (!response.IsSuccessful)
                {
                    TestContext.Out.WriteLine($"API call failed with status code: {response.StatusCode} and message: {response.Content}");
                }
                return response;
            }
            catch (Exception ex)
            {
                TestContext.Out.WriteLine($"Exception occurred: {ex.Message}");
                TestContext.Out.WriteLine($"Stack Trace: {ex.StackTrace}");

                return new RestResponse
                {
                    StatusCode = System.Net.HttpStatusCode.InternalServerError,
                    ErrorMessage = $"Exception occurred: {ex.Message}"
                };
            }
        }

        public async Task<RestResponse> CheckWaitingPeriodAsync<T>(T payload) where T : class
        {
            try
            {
                //Arrange
                var request = new RestRequest(PolicyAPIEndPoints.GetEndPoint(EndPoints.CheckWaitingPeriod), Method.Post);
                request.AddJsonBody(payload);
                var stopWatch = Stopwatch.StartNew();

                //Act
                var response = await restClient.ExecuteAsync(request);
                HttpLoggerHelpers.RequestaAndResponseLogging(request, response, null, stopWatch);

                //Assert
                if (!response.IsSuccessful)
                {
                    TestContext.Out.WriteLine($"API call failed with status code: {response.StatusCode} and message: {response.Content}");
                }
                return response;
            }
            catch (Exception ex)
            {
                TestContext.Out.WriteLine($"Exception occurred: {ex.Message}");
                TestContext.Out.WriteLine($"Stack Trace: {ex.StackTrace}");

                return new RestResponse
                {
                    StatusCode = System.Net.HttpStatusCode.InternalServerError,
                    ErrorMessage = $"Exception occurred: {ex.Message}"
                };
            }
        }

        public async Task<RestResponse> EntityInfoUpsertAsync<T>(T payload) where T : class
        {
            try
            {
                //Arrange
                var request = new RestRequest(PolicyAPIEndPoints.GetEndPoint(EndPoints.EntityInfoUpsert), Method.Post);
                request.AddJsonBody(payload);
                var stopWatch = Stopwatch.StartNew();

                //Act
                var response = await restClient.ExecuteAsync(request);
                HttpLoggerHelpers.RequestaAndResponseLogging(request, response, null, stopWatch);

                //Assert
                if (!response.IsSuccessful)
                {
                    TestContext.Out.WriteLine($"API call failed with status code: {response.StatusCode} and message: {response.Content}");
                }
                return response;
            }
            catch (Exception ex)
            {
                TestContext.Out.WriteLine($"Exception occurred: {ex.Message}");
                TestContext.Out.WriteLine($"Stack Trace: {ex.StackTrace}");

                return new RestResponse
                {
                    StatusCode = System.Net.HttpStatusCode.InternalServerError,
                    ErrorMessage = $"Exception occurred: {ex.Message}"
                };
            }
        }

        public async Task<RestResponse> FetchPolicyStatusAsync<T>(T payload) where T : class
        {
            try
            {
                //Arrange
                var request = new RestRequest(PolicyAPIEndPoints.GetEndPoint(EndPoints.FetchPolicyStatus), Method.Post);
                request.AddJsonBody(payload);
                var stopWatch = Stopwatch.StartNew();

                //Act
                var response = await restClient.ExecuteAsync(request);
                HttpLoggerHelpers.RequestaAndResponseLogging(request, response, null, stopWatch);

                //Assert
                if (!response.IsSuccessful)
                {
                    TestContext.Out.WriteLine($"API call failed with status code: {response.StatusCode} and message: {response.Content}");
                }
                return response;
            }
            catch (Exception ex)
            {
                TestContext.Out.WriteLine($"Exception occurred: {ex.Message}");
                TestContext.Out.WriteLine($"Stack Trace: {ex.StackTrace}");

                return new RestResponse
                {
                    StatusCode = System.Net.HttpStatusCode.InternalServerError,
                    ErrorMessage = $"Exception occurred: {ex.Message}"
                };
            }
        }

        public async Task<RestResponse> GetBenefitCoverScreenHospitalAsync<T>(T payload) where T : class
        {
            try
            {
                //Arrange
                var request = new RestRequest(PolicyAPIEndPoints.GetEndPoint(EndPoints.GetBenefitCoverScreenHospital), Method.Post);
                request.AddJsonBody(payload);
                var stopWatch = Stopwatch.StartNew();

                //Act
                var response = await restClient.ExecuteAsync(request);
                HttpLoggerHelpers.RequestaAndResponseLogging(request, response, null, stopWatch);

                //Assert
                if (!response.IsSuccessful)
                {
                    TestContext.Out.WriteLine($"API call failed with status code: {response.StatusCode} and message: {response.Content}");
                }
                return response;
            }
            catch (Exception ex)
            {
                TestContext.Out.WriteLine($"Exception occurred: {ex.Message}");
                TestContext.Out.WriteLine($"Stack Trace: {ex.StackTrace}");

                return new RestResponse
                {
                    StatusCode = System.Net.HttpStatusCode.InternalServerError,
                    ErrorMessage = $"Exception occurred: {ex.Message}"
                };
            }
        }

        public async Task<RestResponse> GetBenefitCoverScreenWealthAsync<T>(T payload) where T : class
        {
            try
            {
                //Arrange
                var request = new RestRequest(PolicyAPIEndPoints.GetEndPoint(EndPoints.GetBenefitCoverScreenWealth), Method.Post);
                request.AddJsonBody(payload);
                var stopWatch = Stopwatch.StartNew();

                //Act
                var response = await restClient.ExecuteAsync(request);
                HttpLoggerHelpers.RequestaAndResponseLogging(request, response, null, stopWatch);

                //Assert
                if (!response.IsSuccessful)
                {
                    TestContext.Out.WriteLine($"API call failed with status code: {response.StatusCode} and message: {response.Content}");
                }
                return response;
            }
            catch (Exception ex)
            {
                TestContext.Out.WriteLine($"Exception occurred: {ex.Message}");
                TestContext.Out.WriteLine($"Stack Trace: {ex.StackTrace}");

                return new RestResponse
                {
                    StatusCode = System.Net.HttpStatusCode.InternalServerError,
                    ErrorMessage = $"Exception occurred: {ex.Message}"
                };
            }
        }

        public async Task<RestResponse> GetCustomerPolicyInfoByEntityNoAsync<T>(T payload) where T : class
        {
            try
            {
                //Arrange
                var request = new RestRequest(PolicyAPIEndPoints.GetEndPoint(EndPoints.GetCustomerPolicyInfoByEntityNo), Method.Post);
                request.AddJsonBody(payload);
                var stopWatch = Stopwatch.StartNew();

                //Act
                var response = await restClient.ExecuteAsync(request);
                HttpLoggerHelpers.RequestaAndResponseLogging(request, response, null, stopWatch);

                //Assert
                if (!response.IsSuccessful)
                {
                    TestContext.Out.WriteLine($"API call failed with status code: {response.StatusCode} and message: {response.Content}");
                }
                return response;
            }
            catch (Exception ex)
            {
                TestContext.Out.WriteLine($"Exception occurred: {ex.Message}");
                TestContext.Out.WriteLine($"Stack Trace: {ex.StackTrace}");

                return new RestResponse
                {
                    StatusCode = System.Net.HttpStatusCode.InternalServerError,
                    ErrorMessage = $"Exception occurred: {ex.Message}"
                };
            }
        }

        public async Task<RestResponse> GetMainMemChangeHistoryAsync<T>(T payload) where T : class
        {
            try
            {
                //Arrange
                var request = new RestRequest(PolicyAPIEndPoints.GetEndPoint(EndPoints.GetMainMemChangeHistory), Method.Post);
                request.AddJsonBody(payload);
                var stopWatch = Stopwatch.StartNew();

                //Act
                var response = await restClient.ExecuteAsync(request);
                HttpLoggerHelpers.RequestaAndResponseLogging(request, response, null, stopWatch);

                //Assert
                if (!response.IsSuccessful)
                {
                    TestContext.Out.WriteLine($"API call failed with status code: {response.StatusCode} and message: {response.Content}");
                }
                return response;
            }
            catch (Exception ex)
            {
                TestContext.Out.WriteLine($"Exception occurred: {ex.Message}");
                TestContext.Out.WriteLine($"Stack Trace: {ex.StackTrace}");

                return new RestResponse
                {
                    StatusCode = System.Net.HttpStatusCode.InternalServerError,
                    ErrorMessage = $"Exception occurred: {ex.Message}"
                };
            }
        }

        public async Task<RestResponse> GetPolicyAndMainMemberDetailsByPolicyNumberAsync<T>(T payload) where T : class
        {
            try
            {
                //Arrange
                var request = new RestRequest(PolicyAPIEndPoints.GetEndPoint(EndPoints.GetPolicyAndMainMemberDetailsByPolicyNumber), Method.Post);
                request.AddJsonBody(payload);
                var stopWatch = Stopwatch.StartNew();

                //Act
                var response = await restClient.ExecuteAsync(request);
                HttpLoggerHelpers.RequestaAndResponseLogging(request, response, null, stopWatch);

                //Assert
                if (!response.IsSuccessful)
                {
                    TestContext.Out.WriteLine($"API call failed with status code: {response.StatusCode} and message: {response.Content}");
                }
                return response;
            }
            catch (Exception ex)
            {
                TestContext.Out.WriteLine($"Exception occurred: {ex.Message}");
                TestContext.Out.WriteLine($"Stack Trace: {ex.StackTrace}");

                return new RestResponse
                {
                    StatusCode = System.Net.HttpStatusCode.InternalServerError,
                    ErrorMessage = $"Exception occurred: {ex.Message}"
                };
            }
        }

        public async Task<RestResponse> GetPolicyProductLineAsync<T>(T payload) where T : class
        {
            try
            {
                //Arrange
                var request = new RestRequest(PolicyAPIEndPoints.GetEndPoint(EndPoints.GetPolicyProductLine), Method.Post);
                request.AddJsonBody(payload);
                var stopWatch = Stopwatch.StartNew();

                //Act
                var response = await restClient.ExecuteAsync(request);
                HttpLoggerHelpers.RequestaAndResponseLogging(request, response, null, stopWatch);

                //Assert
                if (!response.IsSuccessful)
                {
                    TestContext.Out.WriteLine($"API call failed with status code: {response.StatusCode} and message: {response.Content}");
                }
                return response;
            }
            catch (Exception ex)
            {
                TestContext.Out.WriteLine($"Exception occurred: {ex.Message}");
                TestContext.Out.WriteLine($"Stack Trace: {ex.StackTrace}");

                return new RestResponse
                {
                    StatusCode = System.Net.HttpStatusCode.InternalServerError,
                    ErrorMessage = $"Exception occurred: {ex.Message}"
                };
            }
        }

        public async Task<RestResponse> GetPossibleMainMembersAsync<T>(T payload) where T : class
        {
            try
            {
                //Arrange
                var request = new RestRequest(PolicyAPIEndPoints.GetEndPoint(EndPoints.GetPossibleMainMembers), Method.Post);
                request.AddJsonBody(payload);
                var stopWatch = Stopwatch.StartNew();

                //Act
                var response = await restClient.ExecuteAsync(request);
                HttpLoggerHelpers.RequestaAndResponseLogging(request, response, null, stopWatch);

                //Assert
                if (!response.IsSuccessful)
                {
                    TestContext.Out.WriteLine($"API call failed with status code: {response.StatusCode} and message: {response.Content}");
                }
                return response;
            }
            catch (Exception ex)
            {
                TestContext.Out.WriteLine($"Exception occurred: {ex.Message}");
                TestContext.Out.WriteLine($"Stack Trace: {ex.StackTrace}");

                return new RestResponse
                {
                    StatusCode = System.Net.HttpStatusCode.InternalServerError,
                    ErrorMessage = $"Exception occurred: {ex.Message}"
                };
            }
        }

        public async Task<RestResponse> GetUnmentPremiumAsync(int policyNo)
        {
            try
            {
                //Arrange
                var request = new RestRequest(PolicyAPIEndPoints.GetEndPoint(EndPoints.GetUnmentPremium), Method.Post);
                request.AddUrlSegment("policyNo", policyNo);
                var stopWatch = Stopwatch.StartNew();

                //Act
                var response = await restClient.ExecuteAsync(request);
                HttpLoggerHelpers.RequestaAndResponseLogging(request, response, null, stopWatch);

                //Assert
                if (!response.IsSuccessful)
                {
                    TestContext.Out.WriteLine($"API call failed with status code: {response.StatusCode} and message: {response.Content}");
                }
                return response;
            }
            catch (Exception ex)
            {
                TestContext.Out.WriteLine($"Exception occurred: {ex.Message}");
                TestContext.Out.WriteLine($"Stack Trace: {ex.StackTrace}");

                return new RestResponse
                {
                    StatusCode = System.Net.HttpStatusCode.InternalServerError,
                    ErrorMessage = $"Exception occurred: {ex.Message}"
                };
            }
        }

        public async Task<RestResponse> InsertPolicyNoteAsync<T>(T payload) where T : class
        {
            try
            {
                //Arrange
                var request = new RestRequest(PolicyAPIEndPoints.GetEndPoint(EndPoints.InsertPolicyNote), Method.Post);
                request.AddJsonBody(payload);
                var stopWatch = Stopwatch.StartNew();

                //Act
                var response = await restClient.ExecuteAsync(request);
                HttpLoggerHelpers.RequestaAndResponseLogging(request, response, null, stopWatch);

                //Assert
                if (!response.IsSuccessful)
                {
                    TestContext.Out.WriteLine($"API call failed with status code: {response.StatusCode} and message: {response.Content}");
                }
                return response;
            }
            catch (Exception ex)
            {
                TestContext.Out.WriteLine($"Exception occurred: {ex.Message}");
                TestContext.Out.WriteLine($"Stack Trace: {ex.StackTrace}");

                return new RestResponse
                {
                    StatusCode = System.Net.HttpStatusCode.InternalServerError,
                    ErrorMessage = $"Exception occurred: {ex.Message}"
                };
            }
        }

        public async Task<RestResponse> PersonSearchAsync<T>(T payload) where T : class
        {
            try
            {
                //Arrange
                var request = new RestRequest(PolicyAPIEndPoints.GetEndPoint(EndPoints.PersonSearch), Method.Post);
                request.AddJsonBody(payload);
                var stopWatch = Stopwatch.StartNew();

                //Act
                var response = await restClient.ExecuteAsync(request);
                HttpLoggerHelpers.RequestaAndResponseLogging(request, response, null, stopWatch);

                //Assert
                if (!response.IsSuccessful)
                {
                    TestContext.Out.WriteLine($"API call failed with status code: {response.StatusCode} and message: {response.Content}");
                }
                return response;
            }
            catch (Exception ex)
            {
                TestContext.Out.WriteLine($"Exception occurred: {ex.Message}");
                TestContext.Out.WriteLine($"Stack Trace: {ex.StackTrace}");

                return new RestResponse
                {
                    StatusCode = System.Net.HttpStatusCode.InternalServerError,
                    ErrorMessage = $"Exception occurred: {ex.Message}"
                };
            }
        }

        public async Task<RestResponse> PingAsync()
        {
            try
            {
                //Arrange
                var request = new RestRequest(PolicyAPIEndPoints.GetEndPoint(EndPoints.Ping), Method.Get);
                var stopWatch = Stopwatch.StartNew();

                //Act
                var response = await restClient.ExecuteAsync(request);
                HttpLoggerHelpers.RequestaAndResponseLogging(request, response, null, stopWatch);

                //Assert
                if (!response.IsSuccessful)
                {
                    TestContext.Out.WriteLine($"API call failed with status code: {response.StatusCode} and message: {response.Content}");
                }
                return response;
            }
            catch (Exception ex)
            {
                TestContext.Out.WriteLine($"Exception occurred: {ex.Message}");
                TestContext.Out.WriteLine($"Stack Trace: {ex.StackTrace}");

                return new RestResponse
                {
                    StatusCode = System.Net.HttpStatusCode.InternalServerError,
                    ErrorMessage = $"Exception occurred: {ex.Message}"
                };
            }
        }

        public async Task<RestResponse> PrePopulateEntityInfoByIDAsync<T>(T payload) where T : class
        {
            try
            {
                //Arrange
                var request = new RestRequest(PolicyAPIEndPoints.GetEndPoint(EndPoints.PrePopulateEntityInfoByID), Method.Post);
                request.AddJsonBody(payload);
                var stopWatch = Stopwatch.StartNew();

                //Act
                var response = await restClient.ExecuteAsync(request);
                HttpLoggerHelpers.RequestaAndResponseLogging(request, response, null, stopWatch);

                //Assert
                if (!response.IsSuccessful)
                {
                    TestContext.Out.WriteLine($"API call failed with status code: {response.StatusCode} and message: {response.Content}");
                }
                return response;
            }
            catch (Exception ex)
            {
                TestContext.Out.WriteLine($"Exception occurred: {ex.Message}");
                TestContext.Out.WriteLine($"Stack Trace: {ex.StackTrace}");

                return new RestResponse
                {
                    StatusCode = System.Net.HttpStatusCode.InternalServerError,
                    ErrorMessage = $"Exception occurred: {ex.Message}"
                };
            }
        }

        public async Task<RestResponse> ProcessRefundAndBilcoCancellationAsync<T>(T payload) where T : class
        {
            try
            {
                //Arrange
                var request = new RestRequest(PolicyAPIEndPoints.GetEndPoint(EndPoints.ProcessRefundAndBilcoCancellation), Method.Post);
                request.AddJsonBody(payload);
                var stopWatch = Stopwatch.StartNew();

                //Act
                var response = await restClient.ExecuteAsync(request);
                HttpLoggerHelpers.RequestaAndResponseLogging(request, response, null, stopWatch);

                //Assert
                if (!response.IsSuccessful)
                {
                    TestContext.Out.WriteLine($"API call failed with status code: {response.StatusCode} and message: {response.Content}");
                }
                return response;
            }
            catch (Exception ex)
            {
                TestContext.Out.WriteLine($"Exception occurred: {ex.Message}");
                TestContext.Out.WriteLine($"Stack Trace: {ex.StackTrace}");

                return new RestResponse
                {
                    StatusCode = System.Net.HttpStatusCode.InternalServerError,
                    ErrorMessage = $"Exception occurred: {ex.Message}"
                };
            }
        }

        public async Task<RestResponse> SendInternalEmailsAsync<T>(T payload) where T : class
        {
            try
            {
                //Arrange
                var request = new RestRequest(PolicyAPIEndPoints.GetEndPoint(EndPoints.SendInternalEmails), Method.Post);
                request.AddJsonBody(payload);
                var stopWatch = Stopwatch.StartNew();

                //Act
                var response = await restClient.ExecuteAsync(request);
                HttpLoggerHelpers.RequestaAndResponseLogging(request, response, null, stopWatch);

                //Assert
                if (!response.IsSuccessful)
                {
                    TestContext.Out.WriteLine($"API call failed with status code: {response.StatusCode} and message: {response.Content}");
                }
                return response;
            }
            catch (Exception ex)
            {
                TestContext.Out.WriteLine($"Exception occurred: {ex.Message}");
                TestContext.Out.WriteLine($"Stack Trace: {ex.StackTrace}");

                return new RestResponse
                {
                    StatusCode = System.Net.HttpStatusCode.InternalServerError,
                    ErrorMessage = $"Exception occurred: {ex.Message}"
                };
            }
        }

        public async Task<RestResponse> StoreOTPAsync<T>(T payload) where T : class
        {
            try
            {
                //Arrange
                var request = new RestRequest(PolicyAPIEndPoints.GetEndPoint(EndPoints.StoreOTP), Method.Post);
                request.AddJsonBody(payload);
                var stopWatch = Stopwatch.StartNew();

                //Act
                var response = await restClient.ExecuteAsync(request);
                HttpLoggerHelpers.RequestaAndResponseLogging(request, response, null, stopWatch);

                //Assert
                if (!response.IsSuccessful)
                {
                    DocumentTemplate.DisplayTitle("API Call Failed");
                    DocumentTemplate.DisplayFieldAndValue("Status Code:", response.StatusCode.ToString());
                    DocumentTemplate.DisplayFieldAndValue("Message:", response.Content);
                    DocumentTemplate.DisplayRuler();
                }
                return response;
                
            }
            catch (Exception ex)
            {
                DocumentTemplate.DisplayTitle("Exception Occurred");
                DocumentTemplate.DisplayFieldAndValue("Message:", ex.Message);
                DocumentTemplate.DisplayFieldAndValue("Stack Trace:", ex.StackTrace);
                DocumentTemplate.DisplayRuler();

                return new RestResponse
                {
                    StatusCode = System.Net.HttpStatusCode.InternalServerError,
                    ErrorMessage = $"Exception occurred: {ex.Message}"
                };
            }
        }

        public async Task<RestResponse> VerifyAndUpdateOTPAsync<T>(T payload) where T : class
        {
            try
            {
                //Arrange
                var request = new RestRequest(PolicyAPIEndPoints.GetEndPoint(EndPoints.VerifyAndUpdateOTP), Method.Post);
                request.AddJsonBody(payload);
                var stopWatch = Stopwatch.StartNew();

                //Act
                var response = await restClient.ExecuteAsync(request);
                HttpLoggerHelpers.RequestaAndResponseLogging(request, response, null, stopWatch);

                //Assert
                if (!response.IsSuccessful)
                {
                    TestContext.Out.WriteLine($"API call failed with status code: {response.StatusCode} and message: {response.Content}");
                }
                return response;
            }
            catch (Exception ex)
            {
                TestContext.Out.WriteLine($"Exception occurred: {ex.Message}");
                TestContext.Out.WriteLine($"Stack Trace: {ex.StackTrace}");

                return new RestResponse
                {
                    StatusCode = System.Net.HttpStatusCode.InternalServerError,
                    ErrorMessage = $"Exception occurred: {ex.Message}"
                };
            }
        }
    }
}
