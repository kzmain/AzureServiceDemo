FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 5253
EXPOSE 7253
#EXPOSE 80
#EXPOSE 443
#http://localhost:5253;https://localhost:7253

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["Grpc3/Grpc3.csproj", "Grpc3/"]
RUN dotnet restore "Grpc3/Grpc3.csproj"
COPY . .
WORKDIR "/src/Grpc3"
RUN dotnet build "Grpc3.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Grpc3.csproj" -c Release -o /app/publish

FROM base AS final
RUN sed -i 's/MinProtocol = TLSv1.2/MinProtocol = TLSv1/g' /etc/ssl/openssl.cnf
RUN sed -i 's/MinProtocol = TLSv1.2/MinProtocol = TLSv1/g' /usr/lib/ssl/openssl.cnf
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Grpc3.dll", "--urls", "http://0.0.0.0:5253"]
