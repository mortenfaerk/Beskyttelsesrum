﻿using RestSharp;
using BeskyttelsesrumGUI.Models;
using System.Xml.Serialization;
using ProjNet.CoordinateSystems.Transformations;
using ProjNet.CoordinateSystems;
using System.Globalization;
namespace BeskyttelsesrumGUI.Services;

public class BeskyttelsesrumService
{
    public async Task<List<BbrBeskyttelsesrum>> GetBeskyttelsesrumsAsync(DAWAAdress adresse, CancellationToken cancellationToken, int range = 1000)
    {
        List<BbrBeskyttelsesrum> beskyttelsesrums = new List<BbrBeskyttelsesrum>();

        var wgs84 = GeographicCoordinateSystem.WGS84;
        var utm32N = ProjectedCoordinateSystem.WGS84_UTM(32, true);
        var transformationFactory = new CoordinateTransformationFactory();
        var transform = transformationFactory.CreateFromCoordinateSystems(wgs84, utm32N);
        double[] utmCoordinates = transform.MathTransform.Transform(new[] { adresse.X, adresse.Y });

        var client = new RestClient("https://webkort.herning.dk");
        var request = new RestRequest("wfs/spatialsuite/ows", Method.Get);

        request.AddParameter("SERVICE", "WFS");
        request.AddParameter("REQUEST", "GetFeature");
        request.AddParameter("VERSION", "2.0.0");
        request.AddParameter("TYPENAME", "spatialsuite:bbr_beskyttelsesrum");
        request.AddParameter("OUTPUTFORMAT", "GML3");
        request.AddParameter("SRSNAME", "EPSG:25832");

        string cqlFilter = string.Format(CultureInfo.InvariantCulture,
            "DWithin(geometri,POINT({0} {1}),{2},meters)",
            utmCoordinates[0], utmCoordinates[1], range);
        request.AddParameter("cql_filter", cqlFilter);

        try
        {
            var response = await client.ExecuteAsync(request, cancellationToken);

            if (response.IsSuccessful)
            {
                var serializer = new XmlSerializer(typeof(FeatureCollection));
                using (var reader = new StringReader(response.Content))
                {
                    var featureCollection = (FeatureCollection)serializer.Deserialize(reader);
                    foreach (BbrBeskyttelsesrum beskyttelsesrum in featureCollection.FeatureMembers.BbrBeskyttelsesrumList)
                    {
                        beskyttelsesrums.Add(beskyttelsesrum);
                    }
                }
            }
            else
            {
                var errorMessage = $"Request failed with status code {response.StatusCode} and message: {response.ErrorMessage}.";
                if (response.Content != null)
                {
                    errorMessage += $"\nResponse Content: {response.Content}";
                }
                throw new Exception(errorMessage);
            }
        }
        catch (OperationCanceledException)
        {
            // Handle cancellation gracefully and avoid logging it as an error
            // Log or handle the cancellation if needed, but do not throw an error
        }
        catch (Exception ex)
        {

        }

        return beskyttelsesrums;
    }
}
