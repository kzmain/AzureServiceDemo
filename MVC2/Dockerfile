FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 5242
EXPOSE 7242
#EXPOSE 80
#EXPOSE 443
#http://localhost:5242;https://localhost:7242

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["MVC2/MVC2.csproj", "MVC2/"]
RUN dotnet restore "MVC2/MVC2.csproj"
COPY . .
WORKDIR "/src/MVC2"
RUN dotnet build "MVC2.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "MVC2.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "MVC2.dll", "--urls", "http://0.0.0.0:5242;https://0.0.0.0:7242"]
