FROM mcr.microsoft.com/dotnet/sdk:5.0 as build
RUN dotnet --version
WORKDIR /src

# Copy projects to dir src
COPY Chat.Core/*.csproj ./Chat.Core/
COPY Chat.Database/*.csproj ./Chat.Database/
COPY Chat.Web/*.csproj ./Chat.Web/

COPY . .
# Restore and publish projects to /app/out
WORKDIR /src/Chat.Core
RUN dotnet restore
RUN dotnet publish -c Release -o /app/out

WORKDIR /src/Chat.Database
RUN dotnet restore
RUN dotnet publish -c Release -o /app/out

WORKDIR /src/Chat.Web
RUN dotnet restore
RUN dotnet publish -c Release -o /app/out

# Run web app
FROM mcr.microsoft.com/dotnet/aspnet:5.0
WORKDIR /app
COPY --from=build /app/out .
ENTRYPOINT ["dotnet", "Chat.Web.dll"]
