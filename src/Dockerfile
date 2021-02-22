FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-buster-slim AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/core/sdk:3.1-buster AS build
COPY /src .
WORKDIR /ValueBlue.MovieSearch.Api
RUN dotnet restore ValueBlue.MovieSearch.Api.csproj
RUN dotnet build ValueBlue.MovieSearch.Api.csproj -c Release -o /app/build

FROM build AS publish
RUN dotnet publish ValueBlue.MovieSearch.Api.csproj -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .

ENTRYPOINT ["dotnet", "ValueBlue.MovieSearch.Api.dll"]