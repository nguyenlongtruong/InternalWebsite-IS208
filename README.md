# DemoWebNoiBo
# Đồ án Quản lý dự án Công nghệ thông tin UIT

Để chạy dự án các bạn hãy thực hiện theo các hướng dẫn sau
## 1. Cài đặt dotnet
<p> Tải dotnet tại: https://dotnet.microsoft.com/en-us/download</p><br>
    Tiến hành cài đặt dotnet.

## 2. Cài đặt hệ quản trị cơ sở dữ liệu SQL Sever và cơ sở dữ liệu
<p> Tải SQL Server tại: https://www.microsoft.com/en-us/sql-server/sql-server-downloads</p>
<p>Nếu bạn chưa biết cách cấu hình SQL Sever bạn có thể thực hiện theo hướng dẫn sau: https://howkteam.vn/course/huong-dan-cai-dat/cai-dat-sql-server-2019-4058</p>

<p> Tải cơ sở dữ liệu và chạy dữ liệu bằng SQL Sever: https://github.com/KhaiNoob/CSDL-WebNoiBo

##  3. Kết nối với cơ sở dự liệu
<p> Tại file appsettings.json của dự án </p> <br>
 "ConnectionStrings": {
    "WebsiteNoiBoCongTyContext": "Data Source=DESKTOP-8RNBQGL\\SQLSERVER;Initial Catalog=WebsiteNoiBo;Integrated Security=True;Connect Timeout=30;Encrypt=False;Trust Server Certificate=False;Application Intent=ReadWrite;Multi Subnet Failover=False"
  }

 <p> thay đổi DESKTOP-8RNBQGL\\SQLSERVER bằng SQL Server của bạn, bạn có thể xenmtrong SQL Server tại phần Properties</p>

## 4. Chạy dự án
<p> Tại Terminal, chạy lệnh dotnet build để tiến hành build dự án, sau đó chạy lệnh dotnet run để chạy dự án</p>
  







     