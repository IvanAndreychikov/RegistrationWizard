FROM node:16 AS build-angular
WORKDIR /app

COPY ClientApp/package*.json ./

RUN npm install

COPY ClientApp/ ./

RUN npm run build --prod

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src

RUN apt-get update && apt-get install -y curl \
    && curl -fsSL https://deb.nodesource.com/setup_16.x | bash - \
    && apt-get install -y nodejs

COPY ["RegistrationWizard.csproj", "./"]
RUN dotnet restore "./RegistrationWizard.csproj"
COPY . .
WORKDIR "/src/."
RUN dotnet build "RegistrationWizard.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "RegistrationWizard.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
COPY --from=build-angular /app/dist/* ./wwwroot

ENTRYPOINT ["dotnet", "RegistrationWizard.dll"]