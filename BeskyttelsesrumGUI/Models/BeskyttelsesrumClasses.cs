namespace BeskyttelsesrumGUI.Models;

using System;
using System.Collections.Generic;
using System.Globalization;
using System.Xml.Serialization;

[XmlRoot(ElementName = "FeatureCollection", Namespace = "http://www.opengis.net/wfs")]
public class FeatureCollection
{
    [XmlElement(ElementName = "featureMembers", Namespace = "http://www.opengis.net/gml")]
    public FeatureMembers FeatureMembers { get; set; }
}

public class FeatureMembers
{
    [XmlElement(ElementName = "bbr_beskyttelsesrum", Namespace = "http://wfs.spatialserver.spatialsuite.com/")]
    public List<BbrBeskyttelsesrum> BbrBeskyttelsesrumList { get; set; }
}

public class BbrBeskyttelsesrum
{
    [XmlElement(ElementName = "ejd_nr", Namespace = "http://wfs.spatialserver.spatialsuite.com/")]
    public string Ejdnr { get; set; }

    [XmlElement(ElementName = "bygningsnr", Namespace = "http://wfs.spatialserver.spatialsuite.com/")]
    public string Bygningsnr { get; set; }

    [XmlElement(ElementName = "vej_navn", Namespace = "http://wfs.spatialserver.spatialsuite.com/")]
    public string VejNavn { get; set; }

    [XmlElement(ElementName = "husnr", Namespace = "http://wfs.spatialserver.spatialsuite.com/")]
    public string Husnr { get; set; }

    [XmlElement(ElementName = "postnr", Namespace = "http://wfs.spatialserver.spatialsuite.com/")]
    public string Postnr { get; set; }

    [XmlElement(ElementName = "postbynavn", Namespace = "http://wfs.spatialserver.spatialsuite.com/")]
    public string Postbynavn { get; set; }

    [XmlElement(ElementName = "byg_anvend_kode_t", Namespace = "http://wfs.spatialserver.spatialsuite.com/")]
    public string BygAnvendKodeT { get; set; }

    [XmlElement(ElementName = "sikringsrum_ant", Namespace = "http://wfs.spatialserver.spatialsuite.com/")]
    public int SikringsrumAnt { get; set; }

    [XmlElement(ElementName = "geometri", Namespace = "http://wfs.spatialserver.spatialsuite.com/")]
    public Geometri Geometri { get; set; }

    [XmlAttribute("id", Namespace = "http://www.opengis.net/gml")]
    public string Id { get; set; }

    public string Adresse
    {
        get
        {
            return $"{VejNavn} {Husnr}, {Postnr} {Postbynavn}";
        }
    }
}

public class Geometri
{
    [XmlElement(ElementName = "Point", Namespace = "http://www.opengis.net/gml")]
    public GmlPoint Point { get; set; }
}

public class GmlPoint
{
    [XmlElement(ElementName = "pos", Namespace = "http://www.opengis.net/gml")]
    public string Pos { get; set; }

    [XmlAttribute("srsName")]
    public string SrsName { get; set; }

    [XmlAttribute("srsDimension")]
    public int SrsDimension { get; set; }

    public double Easting { get
        {
            return Convert.ToDouble(Pos.Split(" ")[0], CultureInfo.InvariantCulture);
        } }
    public double Northing { get
        {
            return Convert.ToDouble(Pos.Split(" ")[1], CultureInfo.InvariantCulture);
        } }
}
