FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

COPY *.sln .
COPY Entidades.csproj ./
RUN dotnet restore "Entidades.csproj"

COPY . ./
RUN dotnet publish "Entidades.csproj" -c Release -o /app/publish

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime
WORKDIR /app
COPY --from=build /app/publish ./

ENV ASPNETCORE_URLS="http://+:80"
EXPOSE 80

COPY wait-for-db.sh ./
RUN chmod +x ./wait-for-db.sh

ENTRYPOINT ["/bin/bash", "-c", "./wait-for-db.sh"]
