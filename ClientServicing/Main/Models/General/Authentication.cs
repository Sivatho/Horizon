using System.Text.Json;

namespace ClientServicing.Main.Models.Auth
{
    public static class AuthSerialization
    {
        public static readonly JsonSerializerOptions CaseInsensitive = new()
        {
            PropertyNameCaseInsensitive = true
        };
    }

    public sealed class AuthResult
    {
        public bool IsSuccess { get; init; }
        public string? Token { get; init; }
        public RestSharp.RestResponse? Response { get; init; }
        public string? Error { get; init; }
    }

    public sealed class TokenEnvelope
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
}