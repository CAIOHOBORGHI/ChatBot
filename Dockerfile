FROM mcr.microsoft.com/dotnet/sdk:5.0 as build
RUN dotnet --version
WORKDIR /src

# Copy projects and restore dependencies
COPY Chat.Database/*.csproj ./Chat.Database/
COPY Chat.Web/*.csproj ./Chat.Web/

COPY . .
WORKDIR /src/Chat.Database
RUN dotnet restore
RUN dotnet publish -c Release -o /app/out

WORKDIR /src/Chat.Web
RUN dotnet restore
RUN dotnet publish -c Release -o /app/out

# Build da imagem
FROM mcr.microsoft.com/dotnet/aspnet:5.0
WORKDIR /app
COPY --from=build /app/out .
ENTRYPOINT ["dotnet", "Chat.Web.dll"]
