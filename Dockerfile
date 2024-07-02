# Use the official ASP.NET Core runtime as a parent image

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app

EXPOSE 80
EXPOSE 443

# Use the SDK image to build the application
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY ["BlogTest.Api/BlogTest.Api.csproj", "BlogTest.Api/"]
COPY ["Domain/Domain.csproj", "Domain/"]
COPY ["Infrastructure/Infrastructure.csproj", "Infrastructure/"]
RUN dotnet restore "BlogTest.Api/BlogTest.Api.csproj"
COPY . .
WORKDIR "/src/BlogTest.Api"
RUN dotnet build "BlogTest.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "BlogTest.Api.csproj" -c Release -o /app/publish

# Copy the build output to the runtime image
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "BlogTest.Api.dll"]