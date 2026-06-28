# 📚 Tài liệu dự án — AHD_QuanTrac

> Hệ thống Web giám sát & vận hành (SCADA-style) cho **Hồ Dầu Tiếng**.
> Chủ đầu tư vận hành: *Công ty TNHH MTV Khai thác Thủy lợi Miền Nam*.

Thư mục `Docs/` chứa tài liệu chính thức của dự án. Bắt đầu từ đây.

---

## 🗂️ Danh mục tài liệu

| # | Tài liệu | Đối tượng | Mô tả |
|---|----------|-----------|-------|
| 1 | [01-Architecture.md](01-Architecture.md) | Developer, Tech Lead, DevOps | Tài liệu kiến trúc: tầng, luồng dữ liệu, công nghệ, mô hình triển khai, hợp đồng API. |
| 2 | [02-User-Manual.md](02-User-Manual.md) | Người vận hành, Quản trị viên | Hướng dẫn sử dụng từng màn hình/chức năng (Markdown). |
| 2b | [Huong-Dan-Su-Dung-AHD-QuanTrac.docx](Huong-Dan-Su-Dung-AHD-QuanTrac.docx) | Người vận hành, Quản trị viên | **Bản Word** của hướng dẫn sử dụng (in/gửi/chỉnh sửa). |

> 📄 File `.docx` được sinh từ `02-User-Manual.md` bằng script [`md-to-docx.ps1`](md-to-docx.ps1)
> (cần Microsoft Word). Khi sửa nội dung, chỉ cần sửa file `.md` rồi tạo lại bản Word:
> ```powershell
> powershell -ExecutionPolicy Bypass -File Docs/md-to-docx.ps1 `
>   -InputMd Docs/02-User-Manual.md -OutputDocx Docs/Huong-Dan-Su-Dung-AHD-QuanTrac.docx
> ```

> Tài liệu tham chiếu kỹ thuật ngắn gọn cho AI/developer xem thêm: [`../CLAUDE.md`](../CLAUDE.md).
> Tài liệu API nghiệp vụ realtime: [`../Doc/`](../Doc/) (file `.docx`/`.pdf`).

---

## ⚡ Tóm tắt nhanh

- **Kiến trúc:** Clean Architecture (.NET 8) — `Domain` / `Application` / `Infrastructure` / `API`,
  cộng `UI` (Blazor WebAssembly) và tiện ích `URL_RTSP_Stream_CameraIP`.
- **Backend:** ASP.NET Core 8 Web API + EF Core 8 + SQL Server + ASP.NET Identity (JWT).
- **Frontend:** Blazor WebAssembly + Radzen.Blazor, đa ngôn ngữ (vi-VN/en-US).
- **Đặc trưng:** hợp đồng API dùng chung 2 chiều UI ↔ API qua **RestEase interface**.
- **Module nghiệp vụ:** đánh số `FT01`–`FT08` (xem chi tiết trong tài liệu kiến trúc).

## 🚀 Chạy nhanh (developer)

```bash
# Yêu cầu: .NET 8 SDK (đã ghim qua global.json: 8.0.4xx) + SQL Server
dotnet restore AHD.sln
dotnet build AHD.sln

# Cấu hình ConnectionStrings:DefaultConnection trong API/appsettings.<env>.json

dotnet run --project API/API.csproj   # Web API + Swagger (/swagger), tự seed DB
dotnet run --project UI/UI.csproj      # Blazor WebAssembly UI
```

---

*Cập nhật: 2026-06-28 · AHD_QuanTrac*
