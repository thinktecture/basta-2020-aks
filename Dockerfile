    FROM mcr.microsoft.com/dotnet/core/sdk:3.1-buster AS build

    WORKDIR /src
    COPY ./src/Thinktecture.AKS.Sample.csproj ./
    RUN dotnet restore "./Thinktecture.AKS.Sample.csproj"
    COPY ./src/. .

    RUN dotnet build "Thinktecture.AKS.Sample.csproj" -c Release -o /app/build
    RUN dotnet publish "Thinktecture.AKS.Sample.csproj" -c Release -o /app/publish

    FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-buster-slim
    WORKDIR /app
    COPY --from=build /app/publish .
    EXPOSE 5000
    ENTRYPOINT ["dotnet", "Thinktecture.AKS.Sample.dll"]
