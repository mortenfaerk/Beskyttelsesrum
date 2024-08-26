using GeoCoordinatePortable;

namespace BeskyttelsesrumGUI.Models;

public class Location
{
    public double Latitude { get; set; }
    public double Longitude { get; set; }
    public Location(double latitude, double longitude)
    {
        Latitude = latitude;
        Longitude = longitude;
    }
    public GeoCoordinate ToGeoCoordinate()
    {
        return new GeoCoordinate(Latitude, Longitude);
    }
    public double DistanceTo(Location loc2)
    {
        GeoCoordinate pinThis = this.ToGeoCoordinate();
        GeoCoordinate pin2 = loc2.ToGeoCoordinate();
        return pinThis.GetDistanceTo(pin2);
    }
}
