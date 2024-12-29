# Learn about building .NET container images:
# https://github.com/dotnet/dotnet-docker/blob/main/samples/README.md
FROM --platform=$BUILDPLATFORM mcr.microsoft.com/dotnet/sdk:9.0 AS build
ARG TARGETARCH
WORKDIR /source

# Copy project file and restore as distinct layers
COPY --link SailorTheCat/*.csproj .
RUN dotnet restore -a $TARGETARCH

# Copy source code and publish app
COPY --link SailorTheCat/. .
RUN dotnet build "SailorTheCat.csproj" -c Release -o /source/SailorTheCat/bin/Release/net9.0/build
RUN dotnet publish "SailorTheCat.csproj" -c Release -o /source/SailorTheCat/bin/Release/net9.0/publish

# Runtime stage
FROM mcr.microsoft.com/dotnet/aspnet:9.0
EXPOSE 8080
WORKDIR /opt/inetpub/wwwroot
COPY --from=publish /source/SailorTheCat/bin/Release/net9.0/publish .
USER $APP_UID
ENTRYPOINT ["./SailorTheCat"]
