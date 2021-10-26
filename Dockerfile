FROM mcr.microsoft.com/dotnet/sdk:5.0 AS app-build

#COPY
WORKDIR /app
COPY . .

#Restore
RUN dotnet restore LoyaltyPrime.Assignment.sln

#Build dotnet
WORKDIR /app/LoyaltyPrime.WebApi
RUN dotnet build LoyaltyPrime.WebApi.csproj -c Release --no-restore
#End Build dotnet

#Pulbish
RUN dotnet publish LoyaltyPrime.WebApi.csproj -c Release -o /app/out --no-build --no-restore

FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS final
WORKDIR /app
EXPOSE 80
COPY --from=app-build /app/out .
ENTRYPOINT ["dotnet", "LoyaltyPrime.WebApi.dll","--environment=Development"]