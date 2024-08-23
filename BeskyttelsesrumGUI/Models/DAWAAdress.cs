namespace BeskyttelsesrumGUI.Models;

public class DAWAAdress
{
    public Guid Id { get; set; }
    public  int Status { get; set; }
    public int Darstatus { get; set; }
    public int Vejkode { get; set; }
    public string Vejnavn { get; set; }
    public string Adresseringsvejnavn { get; set; }
    public string HusNr { get; set; }
    public string? Etage { get; set; }
    public string? Doer { get; set; }
    public string? SupplerendeBynavn { get; set; }
    public int PostNr { get; set; }
    public string Postnrnavn { get; set; }
    public string Stormodtagerpostnr { get; set; }
    public string Stormodtagerpostnrnavn { get; set; }
    public string Kommunekode { get; set; }
    public Guid Adgangsadresseid { get; set; }
    public double X { get; set; }
    public double Y { get; set; }
    public string Href { get; set; }
}
