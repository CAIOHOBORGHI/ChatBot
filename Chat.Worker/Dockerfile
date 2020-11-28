FROM mcr.microsoft.com/dotnet/runtime:5.0-buster-slim AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:5.0-buster-slim AS build
WORKDIR /src
COPY ["./Chat.Worker.csproj", "Chat.Worker/"]
COPY ["../Chat.Core/Chat.Core.csproj", "Chat.Core/"]
RUN dotnet restore "Chat.Worker/Chat.Worker.csproj"
COPY . .
WORKDIR "/src/Chat.Worker"

FROM build AS publish
RUN dotnet publish -c Release -o /app/out

FROM base AS final
WORKDIR /app
COPY --from=publish /app/out .
ENTRYPOINT [ "dotnet", "Chat.Worker.dll" ]