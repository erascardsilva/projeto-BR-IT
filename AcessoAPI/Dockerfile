###  Erasmo Cardoso


#  SDK do .NET como base
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build

WORKDIR /app

COPY ["AcessoAPI.csproj", "./"]
RUN dotnet restore "AcessoAPI.csproj"

COPY . .

RUN dotnet build "AcessoAPI.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "AcessoAPI.csproj" -c Release -o /app/publish --no-restore

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS final
WORKDIR /app

COPY --from=publish /app/publish .

#  ambiente para a URL da aplicação
ENV ASPNETCORE_URLS=http://+:5000  

# ponto de entrada 
ENTRYPOINT ["dotnet", "AcessoAPI.dll"]

#  porta
EXPOSE 5000
