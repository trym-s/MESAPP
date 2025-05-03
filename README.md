MESApp Kurulum ve Başlangıç Rehberi
Gereksinimler
    .NET 8
    PostgreSQL 15+
    MQTT destekli Python scripti (Raspberry Pi gibi cihazlarda çalıştırmak için)

    [Opsiyonel] Postman (API testleri için)

1. GEREKSİNİMLERİN YÜKLENMESİ
✅ .NET 8

    https://dotnet.microsoft.com/en-us/download/dotnet/8.0 adresine git.

    “.NET 8 SDK (x64)” paketini indir ve yükle.

    Kurulum sonrası terminal aç (Başlat > Komut İstemi) ve kontrol et:

dotnet --version

Beklenen çıktı: 8.x.x
✅ PostgreSQL 15+ ve pgAdmin

    https://www.postgresql.org/download/windows/ üzerinden PostgreSQL’i indir.

    https://www.pgadmin.org/download/pgadmin-4-windows/ üzerinden pgAdmin’i yükle.

2. VERİTABANININ YÜKLENMESİ

📁 Dump dosyasını pgAdmin ile yüklemek için:

    pgAdmin uygulamasını aç.

    Sol menüden Servers > Databases → sağ tık → Create > Database:

        Database Name: mes_db

    Üst menüden Tools > Query Tool aç.

    File > Open ile projedeki dump.sql dosyasını seç.

    Sağ üstten ⚡ Execute tuşuna bas.

    Hata yoksa veritabanı hazır!

3. PROJEYİ AÇ (Terminal ile)

    cmd veya PowerShell aç → proje klasörüne geç:

cd "C:\Users\KullaniciAdi\Downloads\MesApp"

    NuGet paketlerini yükle:

dotnet restore

    appsettings.Development.json içinde veritabanı bağlantısını kontrol et:

"ConnectionStrings": {
  "DefaultConnection": "Host=localhost;Database=mes_db;Username=postgres;Password=yourpassword"
}

    Uygulamayı başlat:

dotnet run --project MesApp.API

    Tarayıcıdan test et:

http://localhost:5031/api/workstations/1

4. MQTT İLE ENTEGRASYON

Python scripti ile seri porttan gelen veriler MQTT üzerinden .NET backend'e aktarılır.
⚙️ Python MQTT Scripti

Raspberry Pi veya Linux tabanlı cihazlarda:
RaspberryPi/script.py  ismindeki dosyayı çalıştırabilirsiniz:


5. (OPSİYONEL) VISUAL STUDIO veya RIDER İLE

    .sln dosyasına çift tıkla.

    MesApp.API projesini “Startup Project” yap.

    Ctrl + F5 ile başlat.

6. API TESTİ

    https://www.postman.com/ üzerinden Postman'i indir.

    Yeni bir workspace oluştur.

    Örnek istek:

GET http://localhost:5031/api/workstations/1
