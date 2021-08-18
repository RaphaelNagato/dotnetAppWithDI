FROM mcr.microsoft.com/dotnet/core/runtime:3.1 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/core/sdk:3.1 AS build
WORKDIR /src
COPY ["ConsoleApp.UI/ConsoleApp.UI.csproj", "ConsoleApp.UI/"]
COPY ["ConsoleApp.Data/ConsoleApp.Data.csproj", "ConsoleApp.Data/"]
RUN dotnet restore "ConsoleApp.UI/ConsoleApp.UI.csproj"
COPY . .
WORKDIR "/src/ConsoleApp.UI"
RUN dotnet build "ConsoleApp.UI.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "ConsoleApp.UI.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
COPY --from=publish /src/ConsoleApp.Data/Data/Users.json /Data
#ENTRYPOINT ["dotnet", "ConsoleApp.UI.dll"]
CMD ASPNETCORE_URLS=http://*:$PORT dotnet ConsoleApp.UI.dll # for Production
