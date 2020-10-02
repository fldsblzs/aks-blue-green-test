FROM mcr.microsoft.com/dotnet/core/sdk:3.1-alpine AS build
WORKDIR /app

EXPOSE 80
EXPOSE 443

COPY . .
WORKDIR /app/src/Application/
RUN dotnet publish -c Release -o publish ./Application.csproj

FROM mcr.microsoft.com/dotnet/core/aspnet:3.1 AS runtime

WORKDIR /app
COPY --from=build /app/src/Application/publish ./
CMD ["dotnet", "Application.dll"]