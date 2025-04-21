Gereksinimler:
- .NET 8
- PostgreSQL 15+


1. GEREKSINIMLERIN YUKLENMESI
#### .NET 8 

[https://dotnet.microsoft.com/en-us/download/dotnet/8.0] adresine git.
“.NET 8 SDK (x64)” paketini indir ve yükle.
Kurulum sonrası Başlat > Komut İstemi aç ve şunu yaz:
dotnet --version
Sonuç: 8.x.x yazmalı.


#### PostgreSQL 15+
https://www.postgresql.org/download/windows/ adresinden PostgreSQL'i indir.
https://www.pgadmin.org/download/pgadmin-4-windows/ adresinden pgAdmin indir.

2. *VERİTABANININ YÜKLENMESİ*
📁 Dump dosyasını yüklemek:
    pgAdmin uygulamasını aç.
    Sol menüden Servers > Databases sağ tıkla → Create > Database:
        Database Name: upeys
    Üst menüden Tools > Query Tool aç.
    Sol üstte File > Open menüsünden projenin ana dizininde olan dump.sql dosyasını seç.
    Sağ üstten ⚡ (Execute) tuşuna bas.
    Hata yoksa veritabanı hazır.

3. PROJEYI AÇ (VISUAL STUDIO/RIDER YUKLU DEĞİLSE)

cmd veya PowerShell aç → proje klasörüne geç:

cd "C:\\Users\\KullaniciAdi\\Downloads\\MesApp"
NuGet paketlerini yükle:
dotnet restore

appsettings.Development.json dosyasındaki bağlantı bilgilerini kontrol et:
"ConnectionStrings": {
  "DefaultConnection": "Host=localhost;Database=mes_db;Username=postgres;Password=yourpassword"
}

Uygulamayı başlat:
dotnet run --project MesApp.API

Tarayıcıdan şu URL’yi açarak test et:
http://localhost:5031/api/workstations/1

3. (OPSİYONEL) VISUAL STUDIO VEYA RIDER ILE

Visual Studio 2022+ yüklü ise .sln dosyasına çift tıkla.
MesApp.API projesini “Startup Project” olarak ayarla.
Ctrl + F5 tuşlarıyla çalıştır.

4. TEST
Projeyi çalıştırdıktan sonra(visaul studio'da arayüzden run veya terminalden dotnet run ile)
eğer bir hata alınmaz ise .NET projesi ve PostgreSQL veritabanı bağlantısı sağlanmıştır.

API TEST
https://www.postman.com/ adresinden Postman'i indirip kurun. Yeni bir hesap oluşturun.
Postman üyeliği oluşturulduktan sonra yeni bir workspace ile oluşturulan Endpointler test edileblir.
örn: GET http://localhost:5031/api/workstations/1
ile idsi 1 olan workstation için detaylara ulaşılacak.


