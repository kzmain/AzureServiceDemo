FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 5252
EXPOSE 7252
#EXPOSE 80
#EXPOSE 443
#http://localhost:5252;https://localhost:7252

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["Grpc2/Grpc2.csproj", "Grpc2/"]
RUN dotnet restore "Grpc2/Grpc2.csproj"
COPY . .
WORKDIR "/src/Grpc2"
RUN dotnet build "Grpc2.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Grpc2.csproj" -c Release -o /app/publish

FROM base AS final
RUN sed -i 's/MinProtocol = TLSv1.2/MinProtocol = TLSv1/g' /etc/ssl/openssl.cnf
RUN sed -i 's/MinProtocol = TLSv1.2/MinProtocol = TLSv1/g' /usr/lib/ssl/openssl.cnf
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Grpc2.dll", "--urls", "http://0.0.0.0:5252"]
