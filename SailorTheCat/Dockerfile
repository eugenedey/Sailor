FROM mcr.microsoft.com/dotnet/sdk:9.0 AS publish
WORKDIR "/dotNET"
RUN dotnet new console -o App -n DotNet.Docker
WORKDIR "/dotNET/SailorTheCat"
COPY . .
RUN dotnet restore "SailorTheCat.csproj"
RUN dotnet build "SailorTheCat.csproj" -c Release -o /dotNET/SailorTheCat/bin/Release/net9.0/build
RUN dotnet publish "SailorTheCat.csproj" -c Release -o /dotNET/SailorTheCat/bin/Release/net9.0/publish
 
FROM mcr.microsoft.com/dotnet/aspnet:9.0
WORKDIR /opt/inetpub/wwwroot
COPY --from=publish /dotNET/SailorTheCat/bin/Release/net9.0/publish .

EXPOSE 80
ENTRYPOINT ["dotnet", "SailorTheCat.dll"]