
# YKSTayfa (MAUI .NET 8 / Android)

## Derleme
1) `YKSTayfa.sln` dosyasını Visual Studio 2022 ile aç.
2) NuGet paketlerini geri yükleyin.
3) Android emülatörde Debug çalıştırın.

## AAB üretmek
PowerShell:
```
dotnet publish -f net8.0-android -c Release /p:AndroidPackageFormat=aab /p:AndroidVersionCode=10000
```

## Veritabanı
- Paket içi: `Resources/Raw/ykstayfa.db`
- Uygulama ilk açılışta `AppDataDirectory/ykstayfa.db`'ye kopyalanır.
