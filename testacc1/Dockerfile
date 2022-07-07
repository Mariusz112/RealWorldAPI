#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["testacc1/testacc1.csproj", "testacc1/"]
RUN dotnet restore "testacc1/testacc1.csproj"
COPY . .
WORKDIR "/src/testacc1"
RUN dotnet build "testacc1.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "testacc1.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "testacc1.dll"]