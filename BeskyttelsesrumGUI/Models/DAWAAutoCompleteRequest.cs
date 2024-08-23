namespace BeskyttelsesrumGUI.Models;

public class DAWAAutoCompleteRequest
{
    //Dokumentation findes her: https://dawadocs.dataforsyningen.dk/dok/api/autocomplete#autocomplete-svar
    public DAWAType? Type { get; set; } // Angiver typen: "vejnavn", "adgangsadresse" eller "adresse"
    public DAWAType? Startfra { get; set; } // Angiver startpunktet: "vejnavn" (default), "adgangsadresse", "adresse"
    public string Q { get; set; } // Søgetekst indtastet af brugeren
    public int Caretpos { get; set; } // Position af careten (cursoren) i den indtastede tekst
    public List<string>? Postnr { get; set; } // Postnumre til at begrænse søgningen (flerværdi mulig)
    public List<string>? Kommunekode { get; set; } // Kommunekoder til at begrænse søgningen (flerværdi mulig)
    public string? Adgangsadresseid { get; set; } // Adgangsadresse ID til at begrænse søgningen
    public bool? Multilinje { get; set; } // Angiver, om forslag formateres med linjeskift (default false)
    public bool? Supplerendebynavn { get; set; } // Angiver, om adresser formateres med supplerende bynavn (default true)
    public bool? Stormodtagerpostnumre { get; set; } // Angiver, om forslag med stormodtagerpostnumre returneres (default false)
    public bool? Fuzzy { get; set; } // Aktiverer fuzzy søgning
    public string? Id { get; set; } // Returnerer adresse eller adgangsadresse med den angivne ID
    public bool? Gaeldende { get; set; } // Returnerer kun gældende adresser, ikke foreløbige
    public List<List<List<double>>>? Polygon { get; set; } // Som koordinatsystem kan anvendes (ETRS89/UTM32 eller) WGS84/geografisk. Dette angives vha. srid parameteren, Eksempel: polygon=[[[10.3, 55.3],[10.4, 55.3],[10.4, 55.31],[10.4, 55.31],[10.3, 55.3]]].
    public string? Geometri { get; set; } // Angiver geometri for geometriske søgninger: "adgangspunkt" eller "vejpunkt"
    public Circle? Cirkel { get; set; } //Som koordinatsystem kan anvendes (ETRS89/UTM32 eller) WGS84/geografisk. Radius angives i meter. cirkel={x},{y},{r}.
    public int? Srid { get; set; } // Angiver SRID for koordinatsystemet (default 4326)
    public string? Callback { get; set; } // Output i JSONP format
    public string? Format { get; set; } // Output i andet format end JSON
    public bool? Noformat { get; set; } // Angiver, at whitespaceformatering skal udelades
    public bool? Ndjson { get; set; } // Output i Newline Delimited JSON
    public int Side { get; set; } // Angiver hvilken side der skal leveres (paginering)
    public int PerSide { get; set; } // Antal resultater per side (paginering)

    public DAWAAutoCompleteRequest(string q, int caretpos, int perSide = 24, int side=1)
    {
        Q = q;
        Caretpos = caretpos;
        PerSide = perSide;
        Side = side;
    }
}

public class Circle
{
    public double X { get; set; } 
    public double Y { get; set; }
    public double Radius { get; set; } // Radius i meter
}