FROM mcr.microsoft.com/dotnet/sdk:5.0-buster-slim as build
WORKDIR /sudo
EXPOSE 80
COPY Shared/Shared/*.csproj Shared/Shared/
COPY Services/Basket/Basket/*.csproj Services/Basket/Basket/
RUN dotnet restore Services/Basket/Basket/*.csproj
COPY . .
RUN dotnet publish Services/Basket/Basket/*.csproj -c Release -o out
FROM mcr.microsoft.com/dotnet/aspnet:5.0-buster-slim as runtime
WORKDIR /sudo
COPY --from=build /sudo/out .
ENTRYPOINT [ "dotnet","Basket.dll" ]