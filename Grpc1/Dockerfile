FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 5251
EXPOSE 7251
#EXPOSE 80
#EXPOSE 443
#http://localhost:5251;https://localhost:7251

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["Grpc1/Grpc1.csproj", "Grpc1/"]
RUN dotnet restore "Grpc1/Grpc1.csproj"
COPY . .
WORKDIR "/src/Grpc1"
RUN dotnet build "Grpc1.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Grpc1.csproj" -c Release -o /app/publish

FROM base AS final
RUN sed -i 's/MinProtocol = TLSv1.2/MinProtocol = TLSv1/g' /etc/ssl/openssl.cnf
RUN sed -i 's/MinProtocol = TLSv1.2/MinProtocol = TLSv1/g' /usr/lib/ssl/openssl.cnf
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Grpc1.dll", "--urls", "http://0.0.0.0:5251"]
