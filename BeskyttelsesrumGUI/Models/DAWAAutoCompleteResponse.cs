namespace BeskyttelsesrumGUI.Models;

public class DAWAAutoCompleteResponse
{
    public DAWAAdress Data { get; set; }
    public bool Stormodtagerpostnr { get; set; }
    public DAWAType Type { get; set; }
    public string Tekst { get; set; }
    public string Forslagstekst { get; set; }
    public int CaretPos { get; set; }

    public override string ToString()
    {
        return Forslagstekst;
    }

}
    