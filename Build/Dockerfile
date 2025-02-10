FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /app
COPY ./../shoe-grab-crm/*.csproj ./shoe-grab-crm/
COPY ./../shoe-grab-common/ ./shoe-grab-common/
COPY ./../shoe-grab-crm/ ./shoe-grab-crm/

RUN dotnet restore ./shoe-grab-crm/*.csproj
RUN dotnet publish ./shoe-grab-crm/*.csproj -c Release -o /out

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
COPY --from=build /out .
EXPOSE 10031
ENTRYPOINT ["dotnet", "ShoeGrabCRM.dll"]
