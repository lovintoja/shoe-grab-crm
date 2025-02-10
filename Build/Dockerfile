FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /app
COPY ./../shoe-grab-crm/*.csproj ./shoe-grab-crm/
RUN dotnet restore ./shoe-grab-crm/*.csproj
COPY ./../shoe-grab-crm/ ./shoe-grab-crm/
RUN dotnet publish ./shoe-grab-crm/*.csproj -c Release -o /out

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
COPY --from=build /out .
EXPOSE 10031
ENV DB_CONNECTION_STRING="Host=your-db-vm-ip;Port=5432;Database=your-db-name;Username=your-username;Password=your-password;"
ENTRYPOINT ["dotnet", "shoe-grab-crm.dll"]
