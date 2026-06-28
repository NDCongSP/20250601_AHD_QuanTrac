# CLAUDE.md — Project Intelligence File
> Đọc file này **trước tiên** mỗi khi bắt đầu làm việc với project.
> Dành cho: Claude Code · Claude Cowork · Cursor · Copilot · Windsurf
> Cập nhật lần cuối: 2026-06-26 — xem `## 5. CHANGELOG`

---

## 📌 MỤC LỤC

1. [Project Overview](#1-project-overview)
2. [Architecture Manual](#2-architecture-manual)
3. [Coding Standards & Comment Rules](#3-coding-standards--comment-rules)
4. [Session Memory & Context Linking](#4-session-memory--context-linking)
5. [Changelog — Edit Log](#5-changelog--edit-log)
6. [Unit Test Guidelines](#6-unit-test-guidelines)
7. [Performance Optimization Rules](#7-performance-optimization-rules)
8. [How to Use This File](#8-how-to-use-this-file)

---

## 1. PROJECT OVERVIEW

```yaml
project_name:     "AHD_QuanTrac"
version:          "0.1.0"
language:         "C# (.NET 8)"
framework:        "ASP.NET Core Web API + Blazor WebAssembly"
solution:         "AHD.sln"
package_manager:  "NuGet (dotnet CLI / Visual Studio 2022)"
database:         "SQL Server (EF Core 8 + ASP.NET Identity)"
primary_author:   "NDCongSP <ndcong08cddv02@gmail.com>"
repo:             "<URL chưa cấu hình>"
default_branch:   "main"
env:              "development"   # development | testing | production
```

### Mục tiêu
> **AHD_QuanTrac** là hệ thống Web giám sát & vận hành (SCADA-style) cho **Hồ Dầu Tiếng**.
> Hệ thống thu thập / lưu trữ dữ liệu quan trắc và hiển thị vận hành theo thời gian thực:
> mực nước hồ, lưu lượng, cửa cống (Cống Số 1/2/3), tràn xả lũ (Spillway), biểu đồ hồ chứa,
> biểu đồ mực nước, camera RTSP, quản lý file/báo cáo PDF, và phân quyền người dùng.

### Khái niệm domain quan trọng
- **Quan Trắc** = monitoring (đo đạc / giám sát thông số).
- **Tag**: 1 thông số đo (vd `Door1_Aperture`, `HT_Cylinder1_1`, `Total_Fllow`). Có 3 nhóm tag
  qua interface: `ITagsStationsDouble` (tag thiết bị trạm), `ITagLocationInfo` (tag vị trí/hồ),
  `ICalculatorValue` (giá trị tính toán, prefix `API_*`).
- **Location / Station**: vị trí và trạm đo (quản lý ở FT01).
- **FTxx**: mỗi module nghiệp vụ được đánh số. Bảng tham chiếu:

| Module | Ý nghĩa                              | Service / Controller          | UI Page chính                        |
|--------|-------------------------------------|-------------------------------|--------------------------------------|
| FT01   | Quản lý Vị trí/Trạm + Config hệ thống | `IFT01` / `FT01Controller`    | `LocationManagement`, `SettingManagement` |
| FT02   | Hiển thị realtime (RealtimeDisplay)  | `IFT02` / `FT02Controller`    | `Dashboard`, `CuaCong`, `Spillway`   |
| FT03   | Data Log — lịch sử dữ liệu vận hành, báo cáo theo thời gian | `IFT03` / `FT03Controller` | `Report`, `FT03ReportDialog`         |
| FT04   | (Entity dự phòng — chưa có service/controller) | — (chỉ có `DbSet<FT04>`) | —                                    |
| FT05   | Biểu đồ Hồ Chứa (Chart Hồ Chứa)      | `IFT05` / `FT05Controller`    | `Reservoir`                          |
| FT06   | Bảng nội suy (Interpolation Table)   | `IFT06` / `FT06Controller`    | `InterpolationTableDataConfiguration`|
| FT07   | Biểu đồ Mực Nước (Chart Mực Nước)    | `IFT07` / `FT07Controller`    | `WaterLevel`                         |
| FT08   | Quản lý File / Folder / PDF          | `IFT08` / `FT08Controller`    | `FileManagement`                     |

> ⚠️ Đánh số có "lỗ hổng": **không có FT04 service** và Repository nhảy từ FT03 → FT05.
> Khi thêm module mới đừng giả định FTxx liên tục — kiểm tra `ServiceAddScoped.cs` trước.

### Ràng buộc quan trọng
- [ ] **Hợp đồng API là single source of truth**: interface trong `Application/Services/*` (RestEase).
      API Controller *implement* interface đó, Blazor UI *gọi* interface đó qua RestEase HttpClient.
      Khi sửa chữ ký API → sửa interface trước, cả 2 đầu sẽ tự bám theo.
- [ ] Mọi trả về của API dùng `Result<T>` (`Application/Extentions/Result.cs`), không trả raw entity.
- [ ] Tất cả route hardcode tập trung tại `Application/Extentions/ApiRoutes.cs` — không rải string route.
- [ ] **Không commit secret thật**: `Jwt:Key`, connection string đang để trong `appsettings*.json`.
      Connection string production phải để rỗng trong repo, cấu hình qua môi trường/secret.
- [ ] Schema DB thay đổi → **thêm EF Migration** (`Infrastructure/Migrations`), không sửa DB thủ công.
- [ ] UI mặc định culture `vi-VN`, dấu thập phân `.`, group `,` (xem `UI/Program.cs`).

---

## 2. ARCHITECTURE MANUAL

### 2.1 Sơ đồ thư mục (Clean Architecture, 6 project)

```
AHD.sln
├── Domain/                  # Tầng lõi — KHÔNG phụ thuộc project nào
│   ├── Entities/            # FT01..FT08, Authentication/*, ScadaUser, BaseEntity/IGenericEntity
│   ├── Enums/               # EnumPermissionScada, EnumMode, EnumStatus
│   ├── Models/              # FT03DataPoint, FT05/FT07 chart models (DTO nội bộ domain)
│   ├── NotTable/            # Model không map DB + interfaces (ICalculatorValue, ITag*…)
│   └── Interceptors/        # Auditable SaveChanges interceptors
│
├── Application/             # Hợp đồng nghiệp vụ + DTO — phụ thuộc Domain
│   ├── Services/            # IFT01..IFT08, IScadaUser, Authen/*  ← RestEase interfaces
│   │   └── Base/IRepository.cs   # CRUD chuẩn (GetAll/GetById/Insert/Update/Delete/Range)
│   ├── DTOs/                # Request/Response DTO, GeneralResponse, Result-related
│   └── Extentions/          # ApiRoutes, Result<T>, JwtHelper, ConstantExtention, Pagings
│
├── Infrastructure/          # Hiện thực phía server — phụ thuộc Application + Domain
│   ├── Data/                # ApplicationDbContext (IdentityDbContext), DbInitializer
│   ├── Migrations/          # EF Core migrations (SQL Server)
│   ├── Repositories/        # RepositoryFTxxServices (implement IFTxx), Repository facade
│   └── IoC/DependencyInjection/  # ServiceContainer (DI, JWT, CORS, Identity), ServiceAddScoped
│
├── API/                     # ASP.NET Core Web API host (net8.0) — đầu IMPLEMENT hợp đồng
│   ├── Controllers/         # FT0xController : BaseController<TId,T>, IFTxx ; AccountController…
│   ├── Controllers/Base/    # BaseController<TId,T> — CRUD generic + audit (CreateAt/By…)
│   └── Program.cs           # Swagger, JWT, CORS "UI", seed DB lúc khởi động
│
├── UI/                      # Blazor WebAssembly (net8.0) — đầu CONSUME hợp đồng
│   ├── Pages/               # Dashboard, CuaCong, Spillway, Reservoir, WaterLevel, Camera,
│   │                        #   Report, FileManagement, LocationManagement, SettingManagement,
│   │                        #   ScadaUser, Users, Roles, Permissions, Authentication…
│   ├── Layouts/ Shared/ Models/ Services/ Core/ Extensions/
│   ├── Resources/           # Localization vi-VN / en-US (.resx)
│   └── Program.cs           # RestEase HttpClient + Polly retry + JWT handler + Radzen
│
└── URL_RTSP_Stream_CameraIP/   # Tiện ích xử lý stream RTSP camera IP
```

### 2.2 Luồng dữ liệu (Data Flow)

```
[Blazor UI Page/Component]
     │ inject IFTxx (RestEase client) — qua Repository facade hoặc trực tiếp
     ▼
[RestEase HttpClient "UI"]  ── AuthenticationHeaderHandler (Bearer)
     │ + Polly RetryRefreshTokenHandler (auto refresh token)
     │ HTTP (route lấy từ ApiRoutes)
     ▼
[API Controller : BaseController<TId,T>, IFTxx]   ← implement đúng interface UI gọi
     │ gắn audit (CreateAt/CreateOperatorId/UpdateAt/UpdateOperatorId)
     ▼
[Infrastructure Repository (RepositoryFTxxServices)]  ←── business logic + EF Core
     │
     ▼
[ApplicationDbContext → SQL Server]
```

> **Lưu ý kiến trúc cốt lõi**: `IRepository<TId,T>` và `IFTxx` được **dùng chung 2 chiều**.
> - Ở UI: RestEase sinh HTTP client từ interface (`Program.cs` → `.UseWithRestEaseClient<IFTxx>()`).
> - Ở API: Controller kế thừa `BaseController` *implement* chính interface đó.
> - Ở Infrastructure: `RepositoryFTxxServices` *implement* interface đó để truy cập DB.
> Cùng một interface, 3 vai trò khác nhau tùy tầng. Sửa 1 chỗ phải nghĩ tới cả 3.

### 2.3 Quy tắc phân tầng (Layer Rules)

| Layer            | Được phép tham chiếu                       | KHÔNG được tham chiếu              |
|------------------|--------------------------------------------|-----------------------------------|
| Domain           | (không) — chỉ BCL                          | Application, Infrastructure, API, UI |
| Application       | Domain                                      | Infrastructure, API, UI           |
| Infrastructure   | Application, Domain                          | API, UI                           |
| API              | Application, Infrastructure                  | UI                                |
| UI (Blazor WASM) | Application, Domain                          | Infrastructure, API (chỉ qua HTTP) |

### 2.4 Tech stack chính

| Hạng mục        | Công nghệ                                                              |
|-----------------|-----------------------------------------------------------------------|
| Backend         | ASP.NET Core 8 Web API, EF Core 8, SQL Server, ASP.NET Identity       |
| Auth            | JWT Bearer + Refresh Token, role/permission (`EnumPermissionScada`)   |
| API contract    | RestEase (interface dùng chung UI ↔ API)                              |
| Frontend        | Blazor WebAssembly 8, Radzen.Blazor, Localization (vi-VN/en-US)        |
| Resilience      | Polly (retry + auto refresh token)                                    |
| Mapping         | AutoMapper, Mapster                                                    |
| Validation      | FluentValidation                                                       |
| Excel / PDF     | ClosedXML, ClosedXML.Report, Magicodes.IE.Pdf, QRCoder.Core, BarcodeLib |
| Storage (UI)    | Blazored.LocalStorage / SessionStorage                                |
| Fake data       | Bogus                                                                  |

### 2.5 Quyết định kiến trúc (ADR)

| ID    | Ngày       | Quyết định                                          | Lý do                              | Trạng thái |
|-------|------------|-----------------------------------------------------|------------------------------------|------------|
| ADR-1 | 2025-07-22 | Clean Architecture 4 tầng (Domain/App/Infra/API)    | Tách lõi nghiệp vụ khỏi hạ tầng    | Accepted   |
| ADR-2 | 2025-07-22 | Dùng RestEase interface chung cho UI & API          | 1 hợp đồng, tránh lệch UI/Backend  | Accepted   |
| ADR-3 | 2025-07-22 | Mọi response bọc trong `Result<T>`                  | Chuẩn hóa lỗi/thành công           | Accepted   |
| ADR-4 | 2025-07-22 | Blazor WebAssembly (không Server) cho UI            | Triển khai tĩnh, tách hẳn API      | Accepted   |
| ADR-5 | 2025-07-22 | EF Core Code-First + Migrations trên SQL Server      | Versioned schema                   | Accepted   |

---

## 3. CODING STANDARDS & COMMENT RULES

> Quy ước **C# / .NET**. Project bật `Nullable` + `ImplicitUsings`. `GlobalUsing.cs` ở mỗi project.

### 3.1 Comment

#### XML doc comment cho public type/method (khuyến nghị, nhất là Service & DTO)
```csharp
/// <summary>
/// Lấy data log FT03 trong khoảng thời gian, dùng cho báo cáo vận hành.
/// </summary>
/// <param name="fromDate">Mốc bắt đầu (null = không giới hạn dưới).</param>
/// <param name="toDate">Mốc kết thúc (null = không giới hạn trên).</param>
/// <returns><see cref="Result{T}"/> chứa danh sách <see cref="FT03DataPoint"/>.</returns>
Task<Result<List<FT03DataPoint>>> GetByFromDateToDateAsync(DateTime? fromDate, DateTime? toDate);
```

#### Inline comment — chỉ giải thích **tại sao**, không lặp lại code
```csharp
// ✅ Đúng — giải thích lý do
o.Password.RequiredUniqueChars = 0; // Bỏ ràng buộc ký tự khác nhau theo yêu cầu vận hành SCADA

// ❌ Sai — lặp lại code
o.Password.RequiredLength = 8; // đặt độ dài bằng 8
```

#### TODO / FIXME / HACK / PERF
```csharp
// TODO(username, YYYY-MM-DD): Bổ sung FT04 service khi nghiệp vụ chốt
// FIXME(username, YYYY-MM-DD): Đưa Jwt:Key ra secret, không để trong appsettings
// PERF(username, YYYY-MM-DD): GetSampledAsync — cân nhắc index theo CreateAt
```

### 3.2 Naming conventions (C#)

| Loại                       | Convention        | Ví dụ trong repo                        |
|----------------------------|-------------------|-----------------------------------------|
| Class / Interface / Enum   | PascalCase        | `RepositoryFT03Services`, `FT03DataPoint`|
| Interface                  | `I` + PascalCase  | `IFT03`, `IRepository`, `ICalculatorValue`|
| Method                     | PascalCase + verb, async ⇒ hậu tố `Async` | `GetByFromDateToDateAsync` |
| Property / public field    | PascalCase        | `LocationName`, `Door1_Aperture`        |
| Local var / param          | camelCase         | `fromDate`, `paramName`                  |
| Private field              | `_camelCase`      | `_repository`                           |
| Constant                   | PascalCase / UPPER | `ApiRoutes.GetAll`, `MAX_RETRY`         |
| File (.cs)                 | = tên type (PascalCase) | `FT03Controller.cs`               |
| Blazor component           | PascalCase.razor + code-behind `.razor.cs` | `LocationManagement.razor(.cs)` |
| DTO                        | hậu tố `RequestDTO` / `ResponseDTO` | `LoginRequestDTO`, `GetRoleResponseDTO` |

> ⚠️ Trong repo có vài tên sai chính tả lịch sử (`Fllow` = Flow, `Extentions` = Extensions,
> `Paramatters` = Parameters). **Giữ nguyên** để không vỡ mapping/route; chỉ đổi khi refactor có chủ đích.

### 3.3 Code style
```text
- Indent 4 spaces; encoding UTF-8.
- Bật nullable reference types — xử lý null tường minh, tránh '!' bừa bãi.
- async/await xuyên suốt (không .Result/.Wait()); method async hậu tố Async.
- Route: dùng hằng trong ApiRoutes, không hardcode chuỗi rải rác.
- Trả về Result<T> ở mọi service/controller endpoint.
- File-scoped namespace (vd Application/Infrastructure) được ưu tiên cho file mới.
```

---

## 4. SESSION MEMORY & CONTEXT LINKING

> **Mục đích:** Giúp AI duy trì ngữ cảnh giữa các phiên mà không cần đọc lại toàn bộ code.

### 4.1 Active Context — Việc đang làm

```yaml
active_context:
  current_task:     "Phát triển UI (branch feature/UI) — các trang quản lý & hiển thị vận hành"
  related_modules:
    - "UI/Pages/* (Dashboard, CuaCong, Spillway, Reservoir, WaterLevel, FileManagement)"
    - "FT01 (Location/Station + Config), FT03 (Data Log/Report), FT08 (File)"
  recent_work:
    - "DbInitializer + FT08 repository + FileManagement UI"
    - "Sluice gate UI pages + FT03 entity (ICalculatorValue)"
    - "Station management UI + DB init"
  blocked_by:       "(chưa ghi nhận)"
  next_step:        "(cập nhật sau mỗi session)"
  last_session:     "2026-06-26"
  open_questions:
    - "FT04 có triển khai service không, hay bỏ?"
    - "Chiến lược đưa Jwt:Key / connection string ra khỏi appsettings?"
```

### 4.2 Quyết định đã chốt (Decision Log)

| ID      | Ngày       | Quyết định                                   | Ai quyết | File liên quan                       |
|---------|------------|----------------------------------------------|----------|--------------------------------------|
| DEC-001 | 2025-07-22 | JWT Bearer + Refresh token cho auth          | Team     | `Infrastructure/IoC/.../ServiceContainer.cs` |
| DEC-002 | 2025-07-22 | Seed DB lúc API khởi động (`DbInitializer`)  | Dev      | `API/Program.cs`, `Infrastructure/Data/DbInitializer.cs` |
| DEC-003 | 2025-07-22 | CORS policy "UI", bật `AnyClient` ở dev      | Dev      | `ServiceContainer.cs`, `appsettings.json` |

### 4.3 Hướng dẫn AI đọc context

Khi bắt đầu session mới, AI **PHẢI**:
1. Đọc `active_context` → biết đang làm gì.
2. Đọc `CHANGELOG` gần nhất + `git log` → biết đã thay đổi gì.
3. Đọc `Decision Log` + ADR → tránh đề xuất lại phương án đã chốt.
4. **Không** hỏi lại những gì đã ghi trong file này.

---

## 5. CHANGELOG — EDIT LOG

> Ghi **mọi thay đổi đáng kể**, mới nhất lên đầu.
> Format: `[TYPE] <File/Module> — Mô tả`
> Types: `FEAT` · `FIX` · `REFACTOR` · `PERF` · `TEST` · `DOCS` · `CHORE` · `BREAK`

---

### [2026-06-28] — Bộ tài liệu dự án

```
[DOCS]  Docs/README.md            — Trang index tài liệu
[DOCS]  Docs/01-Architecture.md   — Tài liệu kiến trúc (tầng, luồng dữ liệu, FT01..FT08, deploy, tech debt)
[DOCS]  Docs/02-User-Manual.md    — Hướng dẫn sử dụng (đăng nhập, các màn hình, quản trị, FAQ)
[DOCS]  Docs/Huong-Dan-Su-Dung-AHD-QuanTrac.docx — Bản Word của hướng dẫn sử dụng
[CHORE] Docs/md-to-docx.ps1       — Script chuyển Markdown → Word (Word COM) để tái tạo bản .docx
```

---

### [2026-06-26] — Cập nhật tài liệu + Fix publish UI

```
[FIX]   global.json (mới)  — Ghim SDK về band .NET 8 (8.0.422, rollForward latestFeature)
                             để sửa lỗi publish Blazor WASM:
                             "workload wasm-tools-net8 must be installed" + ILLink.Tasks 8.0.21 not found.
[DOCS]  CLAUDE.md  — Viết lại theo đúng kiến trúc thực tế của AHD_QuanTrac
                     (.NET 8 Clean Architecture + Blazor WASM, RestEase contract, FT01..FT08)
```

**Chi tiết FIX (publish UI):**
- **Nguyên nhân:** Repo không có `global.json` → máy tự chọn **SDK .NET 10 (10.0.201)** để build.
  Workload `wasm-tools-net8` trong SDK .NET 10 ghim `Microsoft.NET.ILLink.Tasks 8.0.21`, nhưng
  cache NuGet chỉ có 8.0.25/8.0.28 → publish (Blazor WASM trimming/AOT) báo "package not found".
- **Cách sửa:** thêm `global.json` ghim SDK `8.0.422`. Band .NET 8 dùng workload **`wasm-tools`**
  (đã bao net8, không cần `wasm-tools-net8` riêng), manifest 8.0.28 khớp ILLink.Tasks có sẵn.
- **Lưu ý môi trường:** lần đầu chuyển band, `dotnet workload` sẽ tự đồng bộ/cài lại workload cho
  band 8.0 (android/ios/wasm-tools…). Nếu VS/CLI vẫn báo thiếu workload, chạy:
  `dotnet workload restore` (tại thư mục gốc, sau khi đã có `global.json`).
- **Kết quả kiểm chứng:** `dotnet publish UI/UI.csproj -c Release` chạy hết bước emcc native link +
  wasm-opt, **exit code 0** (chỉ còn warning NU1903 AutoMapper 13.0.1 + các CS8618 nullable — không chặn build).

---

### [~2025-09] — Các session gần đây (suy ra từ git log)

```
[FEAT]  Infrastructure/Data/DbInitializer.cs  — DB initializer
[FEAT]  Infrastructure/Repositories/RepositoryFT08Services.cs — FT08 repository
[FEAT]  UI/Pages/FIleManagement/*             — UI quản lý file
[FEAT]  UI/Pages/Spillway/*                   — UI tràn xả lũ (sluice gate)
[FEAT]  Domain/Entities/FT03.cs               — Entity FT03 + interface ICalculatorValue
[FEAT]  UI/Pages/LocationManagement/*         — UI quản lý trạm/vị trí
[CHORE] Infrastructure/Migrations/*           — Nhiều migration cập nhật FT03/FT05/FT07/Chart
```

> 💡 Lịch sử chi tiết xem `git log`. Bổ sung entry mới ở **đầu** mục này sau mỗi thay đổi đáng kể.

---

## 6. UNIT TEST GUIDELINES

> ⚠️ **Hiện trạng:** repo **chưa có project test**. Khi thêm test, theo quy ước dưới đây.

### 6.1 Khung đề xuất
- Framework: **xUnit** (+ `FluentAssertions`, `Moq`/`NSubstitute`).
- Cấu trúc: tạo `tests/` ngang hàng các project, mỗi project 1 test project:
  `Domain.Tests`, `Application.Tests`, `Infrastructure.Tests`, `API.Tests`.
- EF Core: test repository bằng `Microsoft.EntityFrameworkCore.InMemory` hoặc SQLite in-memory.

### 6.2 Mẫu test (xUnit)
```csharp
public class FT03RepositoryTests
{
    [Fact]
    public async Task GetByFromDateToDateAsync_Should_Return_Points_Within_Range()
    {
        // ARRANGE
        await using var db = TestDb.Create();        // InMemory ApplicationDbContext
        var sut = new RepositoryFT03Services(db);

        // ACT
        var result = await sut.GetByFromDateToDateAsync(from, to);

        // ASSERT
        result.Succeeded.Should().BeTrue();
        result.Data.Should().OnlyContain(p => p.Time >= from && p.Time <= to);
    }
}
```

### 6.3 Naming test
```
MethodName_Should_<expected>_When_<condition>
MethodName_Should_Throw_<Exception>_When_<invalidInput>
```

### 6.4 Ưu tiên coverage
| Tầng            | Mục tiêu | Ghi chú                                   |
|-----------------|----------|-------------------------------------------|
| Infrastructure/Repositories | Cao | Chứa business logic + truy vấn — test trước |
| Application (logic trong service/extension) | Cao | `Result<T>`, JwtHelper, paging |
| API Controllers | Trung bình | Test integration với `WebApplicationFactory` |
| UI (Blazor)     | Thấp     | bUnit cho component then chốt (nếu cần)    |

### 6.5 Lệnh chạy test (sau khi có project test)
```bash
dotnet test AHD.sln                 # chạy toàn bộ
dotnet test tests/Infrastructure.Tests
dotnet test --collect:"XPlat Code Coverage"
```

---

## 7. PERFORMANCE OPTIMIZATION RULES

### 7.1 Nguyên tắc chung
```
RULE-PERF-01: Đo trước khi tối ưu (profiler / EF logging), không đoán.
RULE-PERF-02: Ghi PERF comment + ngày trước khi đổi code nhạy performance.
RULE-PERF-03: Mỗi tối ưu có số liệu trước/sau trong CHANGELOG.
RULE-PERF-04: Không tối ưu sớm làm giảm readability.
```

### 7.2 Backend / EF Core Checklist
```markdown
- [ ] Query data log (FT03) lớn: có index theo cột lọc thời gian (CreateAt) & paging.
- [ ] Dùng AsNoTracking() cho truy vấn chỉ đọc (report, chart, list).
- [ ] Tránh N+1: Include/projection thay vì loop query.
- [ ] Lấy dữ liệu chart/realtime: chỉ select cột cần (projection sang Model/DTO).
- [ ] GetSampledAsync (FT03): downsample ở DB, không kéo full rồi lọc ở memory.
- [ ] Không trả entity nặng (FT03 có ~100+ cột) khi UI chỉ cần vài tag.
```

### 7.3 Frontend (Blazor WASM) Checklist
```markdown
- [ ] Virtualize danh sách/bảng dài (Radzen DataGrid virtualization) khi > 200 dòng.
- [ ] Tránh re-render thừa: dùng @key, ShouldRender khi hợp lý.
- [ ] Realtime/chart: tiết lưu (throttle/debounce) tần suất cập nhật.
- [ ] Tận dụng Polly + cache token; tránh gọi refresh-token dồn dập.
- [ ] Lazy load trang nặng (camera RTSP, chart) khi có thể.
```

---

## 8. HOW TO USE THIS FILE

### 8.1 Dành cho AI Assistant
```
1. LUÔN đọc file này trước khi viết code.
2. Tôn trọng kiến trúc 4 tầng + quy tắc RestEase contract (Section 2.2).
3. Khi thêm module FTxx mới: thêm entity (Domain) → interface IFTxx (Application/Services)
   → route (ApiRoutes) → repository (Infrastructure) → đăng ký DI (ServiceAddScoped)
   → controller (API) → đăng ký RestEase client (UI/Program.cs) → UI page.
4. Đổi schema ⇒ tạo EF migration; không sửa DB tay.
5. Endpoint mới trả Result<T>; route khai báo trong ApiRoutes.
6. Cập nhật active_context + thêm entry CHANGELOG sau mỗi thay đổi đáng kể.
7. Tham chiếu Decision Log/ADR trước khi đề xuất công nghệ mới.
8. KHÔNG hỏi lại điều đã có trong file này.
```

### 8.2 Lệnh build / run (dotnet CLI)
```bash
# Khôi phục & build toàn solution
dotnet restore AHD.sln
dotnet build AHD.sln

# Chạy API (Swagger ở /swagger)
dotnet run --project API/API.csproj

# Chạy UI (Blazor WASM)
dotnet run --project UI/UI.csproj

# EF Core migration (chạy từ thư mục gốc; startup = API)
dotnet ef migrations add <TenMigration> --project Infrastructure --startup-project API
dotnet ef database update --project Infrastructure --startup-project API
```

> Lưu ý: cấu hình `ConnectionStrings:DefaultConnection` (SQL Server) trong `API/appsettings.<env>.json`
> trước khi chạy. DB sẽ được seed qua `DbInitializer` lúc API khởi động.

### 8.3 Template prompt
```
# Task mới:
"Đọc CLAUDE.md. Task: [mô tả]. Module FTxx liên quan: [...].
Tuân thủ luồng thêm module ở Section 8.1. Cập nhật active_context + CHANGELOG khi xong."

# Fix bug:
"Đọc CLAUDE.md Section 2 + 4. Bug: [mô tả]. Expected: [...].
Ghi FIX vào CHANGELOG sau khi sửa."

# Review:
"Đọc CLAUDE.md Section 3. Review [file] theo conventions C#/.NET.
Liệt kê vi phạm: [Line] [Rule] [Gợi ý]."
```

### 8.4 Maintenance
| Việc                                | Tần suất    | Người phụ trách |
|-------------------------------------|-------------|-----------------|
| Cập nhật `active_context`           | Mỗi session | Dev đang làm     |
| Thêm entry `CHANGELOG`              | Mỗi commit  | Dev đang làm     |
| Cập nhật ADR / Decision Log         | Khi phát sinh | Người quyết định |
| Rà soát Performance Checklist       | Mỗi release | Tech Lead        |

---

> **Lưu ý:** File này là *single source of truth* cho AI assistant.
> Khi code mâu thuẫn với CLAUDE.md → ưu tiên CLAUDE.md, rồi sửa code/tài liệu cho nhất quán.

---
*CLAUDE.md · AHD_QuanTrac · Cập nhật: 2026-06-26*
