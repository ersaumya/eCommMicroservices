#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-buster-slim AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/core/sdk:3.1-buster AS build
WORKDIR /src
COPY ["src/Microservices/OrderMicroservice/Order.API/Order.API.csproj", "src/Microservices/OrderMicroservice/Order.API/"]
COPY ["src/Microservices/OrderMicroservice/Order.Infrastructure/Order.Infrastructure.csproj", "src/Microservices/OrderMicroservice/Order.Infrastructure/"]
COPY ["src/Microservices/OrderMicroservice/Order.Core/Order.Core.csproj", "src/Microservices/OrderMicroservice/Order.Core/"]
COPY ["src/Microservices/OrderMicroservice/Order.Application/Order.Application.csproj", "src/Microservices/OrderMicroservice/Order.Application/"]
COPY ["src/Common/EventBusRabbitMQ/EventBusRabbitMQ.csproj", "src/Common/EventBusRabbitMQ/"]
RUN dotnet restore "src/Microservices/OrderMicroservice/Order.API/Order.API.csproj"
COPY . .
WORKDIR "/src/src/Microservices/OrderMicroservice/Order.API"
RUN dotnet build "Order.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Order.API.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Order.API.dll"]
