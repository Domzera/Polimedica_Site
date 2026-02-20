# Esse é meu degundo Dockerfile e deve funcionar com o projeto do site da Polimédica.

#Etapa de build
FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /app
COPY ["Polimedica.csproj", "." ]
COPY . .
RUN dotnet publish "Polimedica.csproj" -c Release -o /app/build

#Etapa de runtime
FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS runtime
WORKDIR /app
COPY --from=build /app/build .
EXPOSE 8080
ENV ASPNETCORE_ENVIRONMENT=Development
ENTRYPOINT ["dotnet", "Polimedica.dll"]