FROM mcr.microsoft.com/dotnet/core/aspnet:3.0-buster-slim AS base
WORKDIR /app
EXPOSE 5000


FROM mcr.microsoft.com/dotnet/core/sdk:3.0-buster AS build
WORKDIR /src
COPY to-do.csproj .
RUN dotnet restore "to-do.csproj"
COPY . .
RUN dotnet build "to-do.csproj" -c Release -o /app/build


FROM build AS publish
RUN dotnet publish "to-do.csproj" -c Release -o /app/publish


FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "to-do.dll"]