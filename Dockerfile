FROM mcr.microsoft.com/dotnet/sdk:6.0 AS app-build

#COPY
WORKDIR /app
COPY . .

#Restore
RUN dotnet restore BluePrint.sln

#Build
WORKDIR /app/BluePrint.WebApi
RUN dotnet build BluePrint.WebApi.csproj -c Release --no-restore

#Test
WORKDIR /app
RUN dotnet test --verbosity m

#Pulbish
WORKDIR /app/MapiFesto.WebApi
RUN dotnet publish BluePrint.WebApi.csproj -c Release -o /app/out --no-build --no-restore

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS final
WORKDIR /app
EXPOSE 80
COPY --from=app-build /app/out .
ENTRYPOINT ["dotnet", "BluePrint.WebApi.dll","--environment=Development"]