# SDK image (build uchun)
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

COPY *.sln .
COPY EduTestApi/*.csproj ./EduTestApi/
COPY EduTest.Application/*.csproj ./EduTest.Application/
COPY EduTest.DataAccess/*.csproj ./EduTest.DataAccess/
COPY EduTest.Domain/*.csproj ./EduTest.Domain/

RUN dotnet restore

COPY . .
WORKDIR /src/EduTestApi
RUN dotnet publish -c Release -o /app/publish

# Runtime image
FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /app
COPY --from=build /app/publish .

ENTRYPOINT ["dotnet", "EduTestApi.dll"]
	