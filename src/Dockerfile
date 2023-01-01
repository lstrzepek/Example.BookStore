FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["Example.BookStore.WebApi/Example.BookStore.WebApi.csproj", "Example.BookStore.WebApi/"]
COPY ["Example.BookStore.Catalog.Contracts/Example.BookStore.Catalog.Contracts.csproj", "Example.BookStore.Catalog.Contracts/"]
COPY ["Example.BookStore.Catalog/Example.BookStore.Catalog.csproj", "Example.BookStore.Catalog/"]
RUN dotnet restore "Example.BookStore.WebApi/Example.BookStore.WebApi.csproj"
COPY . .
WORKDIR "/src/Example.BookStore.WebApi"
RUN dotnet build "Example.BookStore.WebApi.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Example.BookStore.WebApi.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Example.BookStore.WebApi.dll"]
