FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["ChatTeamChallenge.ApiServices/ChatTeamChallenge.ApiServices.csproj", "ChatTeamChallenge.ApiServices/"]
COPY ["ChatTeamChallenge.BLL/ChatTeamChallenge.BLL.csproj", "ChatTeamChallenge.BLL/"]
COPY ["ChatTeamChallenge.Common/ChatTeamChallenge.Common.csproj", "ChatTeamChallenge.Common/"]
COPY ["ChatTeamChallenge.DAL/ChatTeamChallenge.DAL.csproj", "ChatTeamChallenge.DAL/"]
RUN dotnet restore "ChatTeamChallenge.ApiServices/ChatTeamChallenge.ApiServices.csproj"
COPY . .
WORKDIR "/src/ChatTeamChallenge.ApiServices"
RUN dotnet build "ChatTeamChallenge.ApiServices.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "ChatTeamChallenge.ApiServices.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "ChatTeamChallenge.ApiServices.dll"]
