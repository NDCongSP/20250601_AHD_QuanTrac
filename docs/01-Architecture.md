# Tài liệu Kiến trúc — AHD_QuanTrac

> Phiên bản: 1.0 · Cập nhật: 2026-06-28
> Đối tượng đọc: Developer, Tech Lead, DevOps.
> Tài liệu này mô tả kiến trúc tổng thể, phân tầng, luồng dữ liệu, công nghệ và mô hình triển khai.

---

## 1. Giới thiệu

**AHD_QuanTrac** là hệ thống Web giám sát & vận hành kiểu SCADA cho **Hồ Dầu Tiếng**. Hệ thống:

- Thu thập / lưu trữ dữ liệu quan trắc (mực nước, lưu lượng, độ mở cửa, áp suất dầu, nhiệt độ…).
- Hiển thị vận hành thời gian thực: hồ chứa, mực nước hạ lưu, cửa cống (Cống Số 1/2/3), tràn xả lũ.
- Vẽ biểu đồ hồ chứa / mực nước, tra bảng nội suy.
- Xem camera IP (RTSP), quản lý file & báo cáo PDF (bản tin vận hành).
- Quản lý người dùng, vai trò, phân quyền (RBAC) và tài khoản SCADA.

### 1.1 Thuộc tính chất lượng hướng tới
| Thuộc tính | Cách hệ thống đáp ứng |
|------------|------------------------|
| Maintainability | Clean Architecture, tách tầng rõ ràng, hợp đồng API tập trung. |
| Consistency (UI↔API) | Dùng chung interface RestEase → giảm lệch hợp đồng. |
| Security | JWT Bearer + Refresh token, ASP.NET Identity, RBAC + Permission. |
| Portability (deploy) | UI là WASM tĩnh (host độc lập), API tách riêng. |
| Evolvability | EF Core Code-First + Migrations versioned. |

---

## 2. Bức tranh tổng thể (C4 — Context)

```
            ┌─────────────────────────────────────────────────────────┐
            │                     Người dùng                          │
            │   (Vận hành viên · Quản trị viên · Khách xem công khai)  │
            └───────────────┬─────────────────────────────────────────┘
                            │ HTTPS (trình duyệt)
                            ▼
            ┌─────────────────────────────┐        REST/JSON + JWT
            │  UI — Blazor WebAssembly     │ ───────────────────────────┐
            │  (chạy trong trình duyệt)    │                            │
            └─────────────────────────────┘                            ▼
                                                       ┌────────────────────────────┐
                                                       │  API — ASP.NET Core 8        │
                                                       │  (Controllers + JWT + Swagger)│
                                                       └───────────────┬──────────────┘
                                                                       │ EF Core 8
                                                                       ▼
                                                       ┌────────────────────────────┐
                                                       │   SQL Server (DB)            │
                                                       └────────────────────────────┘

  Hệ thống ngoài:
   • Camera IP (RTSP)  ── tiện ích URL_RTSP_Stream_CameraIP / luồng stream → UI
   • Nguồn dữ liệu SCADA/PLC ── ghi dữ liệu quan trắc (tag) vào DB (qua API/tiến trình thu thập)
```

---

## 3. Kiến trúc giải pháp (Clean Architecture)

Solution `AHD.sln` gồm **6 project**:

| Project | Loại | Vai trò | Phụ thuộc |
|---------|------|---------|-----------|
| `Domain` | Class lib | Lõi nghiệp vụ: entity, enum, model, interface tag | (không) |
| `Application` | Class lib | Hợp đồng nghiệp vụ (interface RestEase), DTO, `Result<T>`, route | `Domain` |
| `Infrastructure` | Class lib | Hiện thực phía server: EF Core, repository, DI, JWT, CORS | `Application`, `Domain` |
| `API` | ASP.NET Core Web API | Host API: controller, Swagger, seed DB | `Application`, `Infrastructure` |
| `UI` | Blazor WebAssembly | Giao diện người dùng | `Application`, `Domain` |
| `URL_RTSP_Stream_CameraIP` | Tiện ích | Xử lý/chuyển luồng RTSP camera IP | — |

### 3.1 Quy tắc phụ thuộc (Dependency Rule)

```
        UI ─────────────┐
                         ├──▶ Application ──▶ Domain
        API ──▶ Infrastructure ──┘
```

| Tầng | ĐƯỢC tham chiếu | KHÔNG tham chiếu |
|------|-----------------|------------------|
| Domain | (chỉ BCL) | App, Infra, API, UI |
| Application | Domain | Infra, API, UI |
| Infrastructure | Application, Domain | API, UI |
| API | Application, Infrastructure | UI |
| UI | Application, Domain | Infrastructure, API (chỉ gọi qua HTTP) |

> Nguyên tắc: phụ thuộc luôn hướng vào trong (về phía `Domain`). UI và API là hai "đầu" độc lập,
> chỉ giao tiếp qua mạng (REST/JSON), không tham chiếu mã lẫn nhau.

### 3.2 Sơ đồ thư mục rút gọn

```
AHD.sln
├── Domain/
│   ├── Entities/            FT01..FT08, Authentication/*, ScadaUser, BaseEntity/IGenericEntity
│   ├── Enums/               EnumPermissionScada, EnumMode, EnumStatus
│   ├── Models/              FT03DataPoint, FT05/FT07 chart models
│   ├── NotTable/            Model không map DB + interface tag (ICalculatorValue, ITag*…)
│   └── Interceptors/        Auditable SaveChanges interceptors
├── Application/
│   ├── Services/            IFT01..IFT08, IScadaUser, Authen/*  ← RestEase interfaces
│   │   └── Base/IRepository.cs   CRUD chuẩn
│   ├── DTOs/                Request/Response DTO, GeneralResponse
│   └── Extentions/          ApiRoutes, Result<T>, JwtHelper, ConstantExtention, Pagings
├── Infrastructure/
│   ├── Data/                ApplicationDbContext (IdentityDbContext), DbInitializer
│   ├── Migrations/          EF Core migrations (SQL Server)
│   ├── Repositories/        RepositoryFTxxServices (implement IFTxx), Repository facade
│   └── IoC/DependencyInjection/  ServiceContainer, ServiceAddScoped
├── API/
│   ├── Controllers/         FT0xController : BaseController<TId,T>, IFTxx ; AccountController…
│   ├── Controllers/Base/    BaseController<TId,T> — CRUD generic + audit
│   └── Program.cs           Swagger, JWT, CORS "UI", seed DB
├── UI/
│   ├── Pages/               Dashboard, CuaCong, Spillway, Reservoir, WaterLevel, Camera, Report,
│   │                        FileManagement, LocationManagement, SettingManagement, ScadaUser,
│   │                        Users, Roles, Permissions, Authentication…
│   ├── Layouts/ Shared/ Models/ Services/ Core/ Extensions/
│   ├── Resources/           Localization vi-VN / en-US (.resx)
│   └── Program.cs           RestEase HttpClient + Polly retry + JWT handler + Radzen
└── global.json              Ghim SDK .NET 8 (8.0.4xx) cho build/publish ổn định
```

---

## 4. Mô hình hợp đồng API dùng chung (điểm cốt lõi)

Đây là đặc trưng kiến trúc quan trọng nhất của dự án.

`IRepository<TId, T>` và các interface `IFT01..IFT08`, `IScadaUser` (trong `Application/Services`)
được **dùng chung cho cả 3 vai trò**:

```
                     ┌──────────────────────────────────────────┐
                     │   Application/Services/IFTxx (interface)   │
                     │   [Get]/[Post] route + Result<T>          │
                     └──────────────────────────────────────────┘
                        ▲                ▲                   ▲
        implement       │                │ implement         │ generate client
   (truy cập DB)        │                │ (HTTP endpoint)    │ (gọi HTTP)
                        │                │                    │
 Infrastructure/        │       API/Controllers/             UI/Program.cs
 RepositoryFTxxServices │       FT0xController :              .UseWithRestEaseClient<IFTxx>()
                        │       BaseController<TId,T>, IFTxx
```

- **UI**: RestEase sinh HTTP client từ interface → gọi API như gọi method C#.
- **API Controller**: *implement* chính interface đó (qua `BaseController`), expose route REST.
- **Infrastructure Repository**: *implement* interface đó để truy cập DB qua EF Core.

> Lợi ích: 1 nơi định nghĩa hợp đồng → UI và API không bao giờ lệch chữ ký. Khi đổi API,
> sửa interface trước, trình biên dịch sẽ buộc cả hai đầu cập nhật.

### 4.1 Chuẩn hóa kết quả: `Result<T>`
Mọi endpoint service/controller trả `Result<T>` (`Application/Extentions/Result.cs`) — bọc trạng thái
thành công/lỗi + dữ liệu, thay vì trả entity trần.

### 4.2 Route tập trung: `ApiRoutes`
Toàn bộ route hardcode khai báo tại `Application/Extentions/ApiRoutes.cs` (vd `FT03.GetSampled`,
`Identity.Login`). Không rải chuỗi route trong code.

---

## 5. Module nghiệp vụ (FT01–FT08)

| Module | Ý nghĩa | Service / Controller | Trang UI chính |
|--------|---------|----------------------|----------------|
| FT01 | Quản lý Vị trí/Trạm + Cấu hình hệ thống | `IFT01` / `FT01Controller` | `LocationManagement`, `SettingManagement` |
| FT02 | Hiển thị realtime (RealtimeDisplay) | `IFT02` / `FT02Controller` | `Dashboard`, `CuaCong`, `Spillway` |
| FT03 | Data Log — lịch sử vận hành, báo cáo theo thời gian | `IFT03` / `FT03Controller` | `Report` |
| FT04 | (Entity dự phòng — **chưa có** service/controller) | — (chỉ `DbSet<FT04>`) | — |
| FT05 | Biểu đồ Hồ Chứa | `IFT05` / `FT05Controller` | `Reservoir` |
| FT06 | Bảng nội suy (Interpolation) | `IFT06` / `FT06Controller` | `InterpolationTableDataConfiguration` |
| FT07 | Biểu đồ Mực Nước | `IFT07` / `FT07Controller` | `WaterLevel` |
| FT08 | Quản lý File / Folder / PDF | `IFT08` / `FT08Controller` | `FileManagement` (Bản tin vận hành) |

> ⚠️ Đánh số **không liên tục**: không có `FT04` service và DI nhảy FT03 → FT05.
> Khi thêm module, kiểm tra `Infrastructure/IoC/DependencyInjection/ServiceAddScoped.cs` trước.

### 5.1 Mô hình Tag (dữ liệu quan trắc)
Entity dữ liệu (vd `FT03`) hiện thực các interface tag trong `Domain/NotTable/interfaces`:

| Interface | Nhóm tag | Ví dụ field |
|-----------|----------|-------------|
| `ITagsStationsDouble` | Tag thiết bị tại trạm | `Door1_Aperture`, `HT_Cylinder1_1`, `Total_Fllow` |
| `ITagLocationInfo` | Tag vị trí/hồ | `Fllow_Ho` |
| `ICalculatorValue` | Giá trị tính toán (prefix `API_*`) | `API_Fllow_DauTieng`, `API_ChanDap` |

---

## 6. Luồng dữ liệu runtime

### 6.1 Đọc/ghi dữ liệu nghiệp vụ (UI → API → DB)

```
[Trang/Component Blazor]
   │ inject IFTxx (RestEase) — trực tiếp hoặc qua Repository facade
   ▼
[HttpClient "UI"]
   ├─ AuthenticationHeaderHandler         (gắn Bearer token)
   ├─ Polly RetryRefreshTokenHandler      (tự refresh token khi 401)
   │  HTTP (route từ ApiRoutes)
   ▼
[API Controller : BaseController<TId,T>, IFTxx]
   │ gắn audit khi Insert/Update: CreateAt/CreateOperatorId/UpdateAt/UpdateOperatorId
   ▼
[RepositoryFTxxServices]  ── business logic + truy vấn
   ▼
[ApplicationDbContext → SQL Server]
```

### 6.2 Xác thực & phiên đăng nhập

```
Login (UI) ──▶ AccountController (api/account/identity/loginasync)
                 │ kiểm tra ASP.NET Identity, sinh JWT + Refresh token
                 ▼
            JWT lưu phía UI (LocalStorage/SessionStorage qua Blazored)
                 │
   Mỗi request ──▶ AuthenticationHeaderHandler gắn 'Authorization: Bearer <token>'
                 │
   Token hết hạn (401) ──▶ Polly RetryRefreshTokenHandler gọi refresh-token,
                            lấy token mới rồi retry request.
```

- Cấu hình JWT trong `API/appsettings.*.json` (`Jwt:Key/Issuer/Audience/ExpiryTime`).
- Identity password policy & lockout cấu hình trong `ServiceContainer.cs`.

---

## 7. Bảo mật & phân quyền

- **Xác thực:** JWT Bearer (server validate ký HMAC-SHA256, `ClockSkew = 0`).
- **Định danh:** ASP.NET Identity (`ApplicationUser : IdentityUser`), bảng Identity chuẩn.
- **Phân quyền (RBAC):**
  - Vai trò hệ thống: `Admin`, `Operator`, `System`, `Report` (xem `EnumPermissionScada`,
    `ConstantExtention.Roles`).
  - Policy phía UI (`UI/Program.cs`): `Admin`, `Operator`, `System`, `AdminAndSystem`,
    `AdminAndOperator` — kết hợp role + claim `Permission`.
  - Quản lý permission chi tiết: bảng `Permissions` + `RoleToPermission` (gán quyền theo vai trò).
- **CORS:** policy `"UI"`; dev bật `AnyClient=true`, prod whitelist `Cors:AllowedOrigins`.

> ⚠️ **Rủi ro hiện tại (cần xử lý trước khi production):** `Jwt:Key` và connection string đang
> nằm trong `appsettings*.json` trong repo. Cần chuyển sang biến môi trường / secret manager,
> để trống các giá trị nhạy cảm trong file commit.

---

## 8. Dữ liệu & truy cập

- **ORM:** EF Core 8, Code-First. `ApplicationDbContext : IdentityDbContext<ApplicationUser>`.
- **DbSet chính:** `FT01s`..`FT08*`, `ScadaUsers`, `Permissions`, `RoleToPermissions`,
  `RefreshTokens`, `MstUserSettings`, các bảng Identity.
- **Migrations:** thư mục `Infrastructure/Migrations` (versioned). Đổi schema ⇒ thêm migration mới,
  **không** sửa DB thủ công.
- **Seeding:** `DbInitializer.InitializeAsync` chạy lúc API khởi động (`API/Program.cs`).
- **Truy cập bổ sung:** có `Dapper` (truy vấn nhanh khi cần), `Bogus` (sinh dữ liệu giả/test).

### 8.1 Lệnh migration
```bash
dotnet ef migrations add <TenMigration> --project Infrastructure --startup-project API
dotnet ef database update --project Infrastructure --startup-project API
```

---

## 9. Công nghệ chính

| Hạng mục | Công nghệ |
|----------|-----------|
| Nền tảng | .NET 8 (ghim qua `global.json` band 8.0.4xx) |
| Backend | ASP.NET Core 8 Web API, EF Core 8, SQL Server, ASP.NET Identity |
| Auth | JWT Bearer + Refresh Token |
| Hợp đồng API | RestEase (interface dùng chung UI ↔ API) |
| Frontend | Blazor WebAssembly 8, Radzen.Blazor, Localization (vi-VN/en-US) |
| Resilience | Polly (retry + auto refresh token) |
| Mapping | AutoMapper, Mapster |
| Validation | FluentValidation |
| Excel/PDF/QR | ClosedXML, ClosedXML.Report, Magicodes.IE.Pdf, QRCoder.Core, BarcodeLib |
| Lưu trữ phía UI | Blazored.LocalStorage / SessionStorage |
| Tài liệu API | Swagger / Swashbuckle (`/swagger`) |

---

## 10. Mô hình triển khai (Deployment)

```
┌───────────────────────┐     ┌────────────────────────┐     ┌──────────────────┐
│ Web server tĩnh        │     │ App server (.NET 8)     │     │ SQL Server        │
│ phục vụ UI (WASM)      │ ──▶ │ API (Kestrel/IIS)       │ ──▶ │ Database          │
│ (wwwroot publish)      │HTTPS│ /swagger, /api/*         │ EF  │                  │
└───────────────────────┘     └────────────────────────┘     └──────────────────┘
        ▲
        │ (camera RTSP) URL_RTSP_Stream_CameraIP
```

- **Build/publish UI:** `dotnet publish UI/UI.csproj -c Release -o <out>` → thư mục tĩnh `wwwroot`.
  Cần **.NET 8 SDK band 8.0.4xx** + workload `wasm-tools` (đã ghim qua `global.json`; nếu thiếu chạy
  `dotnet workload restore`).
- **Cấu hình UI → API:** `UI/wwwroot/appsettings.json` → `ApiUrl:ApiBaseUrl`.
- **Cấu hình API:** `API/appsettings.<env>.json` (`ConnectionStrings`, `Jwt`, `Cors`).
  `Environment` chọn file override (`development` | `testing` | `production`).

---

## 11. Ghi chú & nợ kỹ thuật (Tech Debt)

| # | Vấn đề | Mức độ | Gợi ý |
|---|--------|--------|-------|
| 1 | Secret (`Jwt:Key`, connection string) nằm trong appsettings repo | Cao | Chuyển sang env/secret, để trống trong commit |
| 2 | `AutoMapper 13.0.1` có cảnh báo lỗ hổng (NU1903) | Trung bình | Nâng cấp bản vá |
| 3 | FT04 có entity nhưng chưa có service/controller | Thấp | Hoàn thiện hoặc loại bỏ |
| 4 | Nhiều cảnh báo nullable (CS8618…) | Thấp | Bổ sung `required`/nullable dần |
| 5 | Tên sai chính tả lịch sử (`Fllow`, `Extentions`, `Paramatters`) | Thấp | Giữ nguyên để không vỡ mapping/route; chỉ đổi khi refactor có chủ đích |
| 6 | Trang trong `UI/Pages/Old/*` là bản cũ | Thông tin | Không dùng cho luồng chính |

---

## 12. Quyết định kiến trúc (ADR tóm tắt)

| ID | Quyết định | Lý do |
|----|-----------|-------|
| ADR-1 | Clean Architecture 4 tầng | Tách lõi nghiệp vụ khỏi hạ tầng |
| ADR-2 | RestEase interface dùng chung UI & API | Một hợp đồng, tránh lệch hai đầu |
| ADR-3 | Mọi response bọc `Result<T>` | Chuẩn hóa lỗi/thành công |
| ADR-4 | Blazor WebAssembly (không Server) | Triển khai tĩnh, tách hẳn API |
| ADR-5 | EF Core Code-First + Migrations (SQL Server) | Schema versioned |

---

*Tài liệu kiến trúc · AHD_QuanTrac · 2026-06-28. Khi mâu thuẫn với mã nguồn, xác minh lại theo mã rồi cập nhật tài liệu.*
