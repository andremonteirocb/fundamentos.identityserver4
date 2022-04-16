#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-buster-slim AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/core/sdk:3.1-buster AS build
WORKDIR /src
COPY ["Fundamentos.IS4.Frete.Frete/Fundamentos.IS4.Frete.Frete.csproj", "Fundamentos.IS4.Frete.Frete/"]
RUN dotnet restore "Fundamentos.IS4.Frete.Frete/Fundamentos.IS4.Frete.Frete.csproj"
COPY . .
WORKDIR "/src/Fundamentos.IS4.Frete.Frete"
RUN dotnet build "Fundamentos.IS4.Frete.Frete.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Fundamentos.IS4.Frete.Frete.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Fundamentos.IS4.Frete.Frete.dll"]