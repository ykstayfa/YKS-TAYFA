# ===== BUILD STAGE =====
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Proje dosyasını kopyala ve restore et
COPY server/YKSTAYFA.API.csproj server/
RUN dotnet restore "server/YKSTAYFA.API.csproj"

# Tüm kaynakları kopyala ve publish et
COPY server/ server/
WORKDIR /src/server
RUN dotnet publish "YKSTAYFA.API.csproj" -c Release -o /app/publish --no-restore

# ===== RUNTIME STAGE =====
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS final
WORKDIR /app

# Publish çıktısını kopyala
COPY --from=build /app/publish .

# Program.cs PORT env'i okuyarak dinleyecek; burada ASPNETCORE_URLS set etmiyoruz
EXPOSE 8080
ENTRYPOINT ["dotnet", "YKSTAYFA.API.dll"]
