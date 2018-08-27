FROM microsoft/dotnet:2.1-runtime AS base
WORKDIR /app

FROM microsoft/dotnet:2.1-sdk AS build
WORKDIR /src

COPY Invitae.CohortAnalysis/Invitae.CohortAnalysis.ConsoleApplication.csproj Invitae.CohortAnalysis/
COPY Invitae.CohortAnalysis.Interfaces/Invitae.CohortAnalysis.Interfaces.csproj Invitae.CohortAnalysis.Interfaces/
COPY Invitae.CohortAnalysis.Domain/Invitae.CohortAnalysis.Domain.csproj Invitae.CohortAnalysis.Domain/
COPY Invitae.CohortAnalysis.Services/Invitae.CohortAnalysis.Services.csproj Invitae.CohortAnalysis.Services/
COPY Invitae.CohortAnalysis.Services.Test Invitae.CohortAnalysis.Services.Test/
COPY Invitae.CohortAnalysis.Data/Invitae.CohortAnalysis.Data.csproj Invitae.CohortAnalysis.Data/
COPY Invitae.CohortAnalysis.Business/Invitae.CohortAnalysis.Business.csproj Invitae.CohortAnalysis.Business/
COPY Invitae.CohortAnalysis.Helpers/Invitae.CohortAnalysis.Helpers.csproj Invitae.CohortAnalysis.Helpers/

RUN dotnet restore Invitae.CohortAnalysis/Invitae.CohortAnalysis.ConsoleApplication.csproj

COPY . .

WORKDIR /src/Invitae.CohortAnalysis

RUN dotnet build Invitae.CohortAnalysis.ConsoleApplication.csproj -c Release -o /app

FROM build AS publish
RUN dotnet publish Invitae.CohortAnalysis.ConsoleApplication.csproj -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .

COPY --from=build /src/Invitae.CohortAnalysis/appsettings.json .

ENTRYPOINT ["dotnet", "Invitae.CohortAnalysis.ConsoleApplication.dll"]
