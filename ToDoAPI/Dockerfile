# BUILD STAGE
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build

WORKDIR /src

COPY . .


RUN dotnet restore "./ToDoAPI.csproj"
RUN dotnet build "./ToDoAPI.csproj" -c Release -o /app/build
RUN dotnet publish "./ToDoAPI.csproj" -c Release -o /app/publish --no-restore

# SERVE STAGE
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime

WORKDIR /app

COPY --from=build /app/publish .

ENV ASPNETCORE_HTTP_PORTS=5000

EXPOSE 5000

ENTRYPOINT ["dotnet", "ToDoAPI.dll"]
