FROM mcr.microsoft.com/dotnet/sdk:3.1 as build
WORKDIR /sudo
EXPOSE 80
COPY Shared/Shared/*.csproj Shared/Shared/
COPY IdentityServer/IdentityServer/*.csproj IdentityServer/IdentityServer/
RUN dotnet restore IdentityServer/IdentityServer/*.csproj
COPY . .
RUN dotnet publish IdentityServer/IdentityServer/*.csproj -c Release -o out
FROM mcr.microsoft.com/dotnet/aspnet:3.1 as runtime
WORKDIR /sudo
COPY --from=build /sudo/out .
ENTRYPOINT [ "dotnet","IdentityServer.dll" ]