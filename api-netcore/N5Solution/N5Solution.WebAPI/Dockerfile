#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

#Depending on the operating system of the host machines(s) that will build or run the containers, the image specified in the FROM statement may need to be changed.
#For more information, please see https://aka.ms/containercompat

FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["N5Solution.WebAPI/N5Solution.WebAPI.csproj", "N5Solution.WebAPI/"]
COPY ["N5Solution.Core/N5Solution.Core.csproj", "N5Solution.Core/"]
COPY ["N5Solution.Services/N5Solution.Services.csproj", "N5Solution.Services/"]
COPY ["N5Solution.Infraestructure/N5Solution.Infraestructure.csproj", "N5Solution.Infraestructure/"]
RUN dotnet restore "N5Solution.WebAPI/N5Solution.WebAPI.csproj"
COPY . .
WORKDIR "/src/N5Solution.WebAPI"
RUN dotnet build "N5Solution.WebAPI.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "N5Solution.WebAPI.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "N5Solution.WebAPI.dll"]