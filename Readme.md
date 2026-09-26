# IBAS Support WebApp

## Formål

Denne løsning er bygget til at afhjælpe IBAS' behov for at håndtere kundehenvendelser
om vores cykler digitalt, i stedet for at alle henvendelser skal gå manuelt gennem
receptionen. Support-medarbejdere kan oprette nye henvendelser og få et samlet overblik
over indkomne sager i én webapplikation.

Løsningen er en **.NET Blazor WebApp**, der bruger **Azure CosmosDB** (NoSQL, SQL API)
som datalager for kundehenvendelser.

## Funktioner

- **Opret henvendelse** (`/create-support`) — formular til registrering af en ny
  kundehenvendelse med navn, email, telefon, beskrivelse og kategori.
- **Se henvendelser** (`/support-list`) — tabel-overblik over alle registrerede
  henvendelser, sorteret efter nyeste først.

## Arkitektur

- `Models/SupportMessage.cs` — datamodel for en kundehenvendelse, med validering via
  DataAnnotations.
- `Services/CosmosDbService.cs` — serviceklasse der håndterer forbindelse til og
  forespørgsler mod CosmosDB, registreret som en singleton via dependency injection
  i `Program.cs`.
- `Components/Pages/CreateSupport.razor` og `SupportList.razor` — UI-siderne.

## Opsætning af CosmosDB

Databasen kan genskabes fra bunden med følgende Azure CLI-kommandoer:

```bash
az group create --name IBasSupportRG --location westeurope

export DBACCOUNT="ibas-db-account"-$RANDOM
export RESGRP="IBasSupportRG"
az cosmosdb create --name $DBACCOUNT --resource-group $RESGRP --enable-free-tier true

export DATABASE="IBasSupportDB"
az cosmosdb sql database create --account-name $DBACCOUNT \
  --resource-group $RESGRP --name $DATABASE

export CONTAINER="ibassupport"
az cosmosdb sql container create --account-name $DBACCOUNT \
  --resource-group $RESGRP --database-name $DATABASE \
  --name $CONTAINER --partition-key-path "/category"
```

> På Windows skal `--partition-key-path` skrives med dobbelt skråstreg (`"//category"`).

Connection stringen findes i Azure Portal under CosmosDB-kontoen → **Keys** →
**PRIMARY CONNECTION STRING**, og skal sættes lokalt med User Secrets (aldrig i
`appsettings.json` eller git):

```bash
dotnet user-secrets init
dotnet user-secrets set "ConnectionStrings:CosmosDb" "<connection-string>"
```

## Kørsel lokalt

```bash
dotnet run
```

Naviger til den URL, der vises i konsollen.

## Status

**Hvad er lavet:**
- Datamodel designet og udarbejdet som JSON-eksempler (M4.02).
- CosmosDB-konto, database og container oprettet via Azure CLI (M4.03).
- Blazor-webapp med sider til at oprette og se kundehenvendelser, forbundet til
  CosmosDB (M4.04).

**Hvad mangler:**


**Næste skridt:**
- Mulighed for at redigere eller slette en eksisterende henvendelse.
- Deployment af webapp'en til Azure (kører i øjeblikket kun lokalt).
- Automatisk oprettelse af CosmosDB-infrastruktur ved deployment (i stedet for
  manuelle `az`-kommandoer). 
- Det næste naturlige skridt er at deploye applikationen til en Azure App Service eller VM, så løsningen er tilgængelig for support-medarbejdere uden for udviklingsmiljøet.