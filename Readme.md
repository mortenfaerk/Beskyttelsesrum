# Beskyttelsesrum

En Blazor WebAssembly applikation til at finde beskyttelsesrum i nærheden af en given adresse i Danmark.

## 📋 Indholdsfortegnelse

- [Om Projektet](#om-projektet)
- [Funktioner](#funktioner)
- [Teknologier](#teknologier)
- [Forudsætninger](#forudsætninger)
- [Installation](#installation)
- [Kørsel af Projektet](#kørsel-af-projektet)
- [Projektstruktur](#projektstruktur)
- [Deployment til GitHub Pages](#deployment-til-github-pages)
- [API'er og Datakilder](#apier-og-datakilder)

## Om Projektet

Dette projekt er en webapplikation, der hjælper brugere med at finde beskyttelsesrum i nærheden af deres adresse. Applikationen bruger DAWA (Danmarks Adressers Web API) til adressevalidering og Herning Kommunes WFS-tjeneste til at hente information om beskyttelsesrum.

## Funktioner

- **Adressesøgning**: Autocomplete-funktionalitet til at finde danske adresser
- **Afstandsbaseret søgning**: Find beskyttelsesrum inden for en bestemt radius (1-50 km)
- **Sorterbar tabel**: Vis beskyttelsesrum sorteret efter afstand, adresse eller antal pladser
- **Responsivt design**: Fungerer på både desktop og mobile enheder
- **Paginering**: Nem navigation gennem søgeresultater

## Teknologier

Projektet er bygget med følgende teknologier:

### Framework

- **.NET 8.0**: Det primære framework
- **Blazor WebAssembly**: For klient-side web applikation

### NuGet Pakker

- `Blazor.Bootstrap` (v3.3.1): UI-komponenter og styling
- `GeoCoordinate.NetCore` (v1.0.0.1): Beregning af geografiske afstande
- `Microsoft.AspNetCore.Components.WebAssembly` (v8.0.14): Blazor WebAssembly runtime
- `Newtonsoft.Json` (v13.0.3): JSON serialisering/deserialisering
- `ProjNet` (v2.0.0): Koordinattransformation (WGS84 til UTM)
- `RestSharp` (v112.1.0): HTTP-klient til API-kald

### Eksterne API'er

- **DAWA API** (api.dataforsyningen.dk): Danmarks Adressers Web API
- **Herning Kommune WFS**: Web Feature Service til beskyttelsesrumdata

## Forudsætninger

For at køre dette projekt lokalt skal du have installeret:

- [.NET 8.0 SDK](https://dotnet.microsoft.com/download/dotnet/8.0) eller nyere
- En moderne webbrowser (Chrome, Edge, Safari - bemærk: Firefox kan have kompatibilitetsproblemer)
- (Valgfrit) [Visual Studio 2022](https://visualstudio.microsoft.com/) eller [Visual Studio Code](https://code.visualstudio.com/)

## Installation

1. **Klon repository:**

   ```bash
   git clone <repository-url>
   cd Beskyttelsesrum
   ```

2. **Gendan NuGet-pakker:**
   ```bash
   dotnet restore
   ```

## Kørsel af Projektet

### Via Kommandolinje

```bash
cd BeskyttelsesrumGUI
dotnet run
```

Applikationen vil være tilgængelig på `http://localhost:5100` eller `https://localhost:7096`

### Via Visual Studio

1. Åbn `Beskyttelsesrum.sln`
2. Vælg `BeskyttelsesrumGUI` som startup-projekt
3. Tryk F5 eller klik på "Start Debugging"

## Projektstruktur

```
BeskyttelsesrumGUI/
├── Layout/                    # Layout-komponenter
│   ├── MainLayout.razor      # Hovedlayout
│   └── NavMenu.razor         # Navigation
├── Models/                    # Datamodeller
│   ├── BeskyttelsesrumClasses.cs
│   ├── DAWAAdress.cs
│   └── Location.cs
├── Pages/                     # Razor-sider
│   ├── Home.razor
│   └── Adresseindtastning/
│       └── FindBeskyttelsesrum.razor
├── Services/                  # Tjenester til API-kald
│   ├── AdressValidationService.cs
│   └── BeskyttelsesrumService.cs
├── wwwroot/                   # Statiske filer
│   ├── index.html
│   ├── css/
│   └── js/
└── Program.cs                 # Applikationens indgangspunkt
```

## Deployment til GitHub Pages

Følg disse trin for at deploye applikationen til GitHub Pages:

### 1. Forbered Repository

Sørg for at dit GitHub repository er konfigureret, og at du har push-adgang.

### 2. Opdater Base Path

I `wwwroot/index.html`, opdater `<base>` tag til at matche dit repository-navn:

```html
<base href="/dit-repository-navn/" />
```

### 3. Tilføj 404-håndtering

Projektet inkluderer allerede `404.html` i `wwwroot/`-mappen, som håndterer routing til GitHub Pages.

### 4. Build Projektet til Release

```powershell
cd BeskyttelsesrumGUI
dotnet publish -c Release -o ../publish
```

### 5. Forbered Deployment-mappe

```powershell
# Kopier alle filer fra wwwroot
cd ../publish/wwwroot

# Tilføj .nojekyll fil for at undgå Jekyll-processing
New-Item -Path .nojekyll -ItemType File
```

### 6. Deploy til GitHub Pages

#### Option A: Manuel Deployment

```powershell
# Initialiser git i publish/wwwroot mappen
git init
git add -A
git commit -m "Deploy to GitHub Pages"
git branch -M gh-pages
git remote add origin https://github.com/dit-brugernavn/dit-repository.git
git push -f origin gh-pages
```

#### Option B: GitHub Actions (Ikke forsøgt anvendt)

Opret `.github/workflows/deploy.yml` i roden af dit repository:

```yaml
name: Deploy to GitHub Pages

on:
  push:
    branches: [main]
  workflow_dispatch:

jobs:
  deploy:
    runs-on: ubuntu-latest
    steps:
      - uses: actions/checkout@v3

      - name: Setup .NET
        uses: actions/setup-dotnet@v3
        with:
          dotnet-version: 8.0.x

      - name: Restore dependencies
        run: dotnet restore

      - name: Build and publish
        run: dotnet publish BeskyttelsesrumGUI/BeskyttelsesrumGUI.csproj -c Release -o release --nologo

      - name: Add .nojekyll file
        run: touch release/wwwroot/.nojekyll

      - name: Deploy to GitHub Pages
        uses: peaceiris/actions-gh-pages@v3
        with:
          github_token: ${{ secrets.GITHUB_TOKEN }}
          publish_dir: release/wwwroot
          force_orphan: true
```

### 7. Aktiver GitHub Pages

1. Gå til dit repository på GitHub
2. Klik på **Settings** > **Pages**
3. Under "Source", vælg `gh-pages` branch
4. Klik "Save"

Din applikation vil være tilgængelig på: `https://dit-brugernavn.github.io/dit-repository-navn/`

### Vigtige Noter for GitHub Pages

- **Base Path**: Husk at opdatere `<base href="/">` i `index.html` til at matche dit repository-navn
- **Caching**: GitHub Pages cacher aggressivt. Ved problemer, prøv hard refresh (Ctrl+Shift+R)
- **HTTPS**: GitHub Pages tvinger HTTPS, så alle API-kald skal også være HTTPS

## 🔌 API'er og Datakilder

### DAWA (Danmarks Adressers Web API)

- **Base URL**: `https://api.dataforsyningen.dk`
- **Endpoint**: `/autocomplete`
- **Formål**: Adressevalidering og autocomplete
- **Dokumentation**: [DAWA Dokumentation](https://dawadocs.dataforsyningen.dk/)

### Herning Kommune WFS

- **Base URL**: `https://webkort.herning.dk`
- **Endpoint**: `/wfs/spatialsuite/ows`
- **Service**: WFS (Web Feature Service)
- **Type**: `spatialsuite:bbr_beskyttelsesrum`
- **Format**: GML3
- **Koordinatsystem**: EPSG:25832 (UTM Zone 32N)

## Kendte Problemer

- **Firefox Kompatibilitet**: Applikationen viser en advarsel i Firefox, da der kan være kompatibilitetsproblemer med Blazor WebAssembly
- **CORS**: API-kald til eksterne tjenester kan fejle, hvis CORS ikke er konfigureret korrekt
