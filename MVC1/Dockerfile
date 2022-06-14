FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 5241
EXPOSE 7241
#EXPOSE 80
#EXPOSE 443
#http://localhost:5241;https://localhost:7241

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["MVC1/MVC1.csproj", "MVC1/"]
RUN dotnet restore "MVC1/MVC1.csproj"
COPY . .
WORKDIR "/src/MVC1"
RUN dotnet build "MVC1.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "MVC1.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "MVC1.dll", "--urls", "http://0.0.0.0:5241;https://0.0.0.0:7241"]
