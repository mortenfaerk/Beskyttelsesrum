using BeskyttelsesrumGUI.Models;
using Newtonsoft.Json;
using RestSharp;

namespace BeskyttelsesrumGUI.Services;

public class AdressValidationService
{
    public RestClientOptions Options { get; set; }
    public RestClient Client { get; set; }

    public AdressValidationService()
    {
        Options = new RestClientOptions("https://api.dataforsyningen.dk");
        Client = new RestClient(Options);
    }

    public async Task<List<DAWAAutoCompleteResponse>>? GetAutoCompleteResponsesAsync(DAWAAutoCompleteRequest request, CancellationToken cancellationToken)
    {
        try
        {
            var restRequest = new RestRequest("/autocomplete", Method.Get);
            restRequest.AddQueryParameter("q", request.Q);
            restRequest.AddQueryParameter("per_side", request.PerSide);
            restRequest.AddQueryParameter("side", request.Side);

            var response = await Client.ExecuteAsync(restRequest, cancellationToken);

            if (!response.IsSuccessful)
                throw new Exception(response.ErrorMessage);
            var autoCompleteResponse = JsonConvert.DeserializeObject<List<DAWAAutoCompleteResponse>>(response.Content);
            return autoCompleteResponse;
        }
        catch (OperationCanceledException)
        {
            return new List<DAWAAutoCompleteResponse>();
        }
        catch (Exception ex)
        {
          if(cancellationToken.IsCancellationRequested)
                return new List<DAWAAutoCompleteResponse>();
            else
            {
                throw new Exception("Error fetching autocomplete data", ex);
            }
        }
    }
}
