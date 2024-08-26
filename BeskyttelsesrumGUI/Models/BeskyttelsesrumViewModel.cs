using BeskyttelsesrumGUI.Services;

namespace BeskyttelsesrumGUI.Models;

public class BeskyttelsesrumViewModel
{
    public BbrBeskyttelsesrum BbrBeskyttelserum { get; set; }
    public DAWAAdress Adresse { get; set; }
    public double Distance { get
        {
            Location adresseLocation = new Location(Adresse.Y, Adresse.X);
            Location beskyttelsesRum = _beskyttelsesrumService.GetLocationFromGMLPoint(BbrBeskyttelserum.Geometri.Point);
            var distance = adresseLocation.DistanceTo(beskyttelsesRum);
            return distance;
        }
    }
    public string DistanceFormatted { get
        {
            var distance_km = this.Distance / 1000;
            return distance_km.ToString("F2");
        } }
    private readonly BeskyttelsesrumService _beskyttelsesrumService;

    public BeskyttelsesrumViewModel(DAWAAdress adresse, BbrBeskyttelsesrum bbrBeskyttelsesrum, BeskyttelsesrumService beskyttelsesrumService)
    {
        Adresse = adresse;
        BbrBeskyttelserum = bbrBeskyttelsesrum;
        _beskyttelsesrumService = beskyttelsesrumService;
        var adresseLoc = new Location(adresse.X, adresse.Y);
        var bbrLoc = _beskyttelsesrumService.GetLocationFromGMLPoint(bbrBeskyttelsesrum.Geometri.Point);
    }
}
