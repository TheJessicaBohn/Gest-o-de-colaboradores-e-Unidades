FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /src
COPY ["Gestao-de-Colaboradores-e-Unidades/Gestao-de-Colaboradores-e-Unidades.csproj", "Gestao-de-Colaboradores-e-Unidades/"]
RUN dotnet restore "Gestao-de-Colaboradores-e-Unidades/Gestao-de-Colaboradores-e-Unidades.csproj"
COPY . .
WORKDIR "/src/Gestao-de-Colaboradores-e-Unidades"
RUN dotnet build "Gestao-de-Colaboradores-e-Unidades.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Gestao-de-Colaboradores-e-Unidades.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Gestao-de-Colaboradores-e-Unidades.dll"]
