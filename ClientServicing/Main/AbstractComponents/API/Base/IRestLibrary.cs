using RestSharp;

namespace ClientServicing.Main.AbstractComponents.API.Base
{
    public interface IRestLibrary
    {
        RestClient RestClient { get; }
    }
}
