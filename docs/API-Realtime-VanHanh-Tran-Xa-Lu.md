# Tài liệu kết nối API — Dữ liệu realtime vận hành tràn xả lũ

Hệ thống cần kết nối **hai API** sau: (1) đăng nhập lấy JWT; (2) lấy dữ liệu hiển thị realtime (tổng hợp trạng thái trạm, tag, giá trị tính toán).

---

## Thông tin cung cấp cho khách hàng (điền tay)

| Mục | Giá trị |
|-----|---------|
| Domain / Base URL | |
| Tài khoản kết nối (ghi chú nếu có) | |
| Username / Email đăng nhập API | |
| Password | |

---

## API 1 — Đăng nhập, lấy token

**Mục đích:** Xác thực và nhận `token` (JWT) dùng cho các request có yêu cầu `Authorization: Bearer`.

| | |
|--|--|
| **Phương thức** | `POST` |
| **Đường dẫn** | `{BaseUrl}api/account/identity/loginasync` |
| **Header** | `Content-Type: application/json` |

### Sample — Request body

```json
{
  "emailAddress": "user@example.com",
  "password": "********",
  "remember": false
}
```

| Trường | Kiểu | Ghi chú |
|--------|------|---------|
| `emailAddress` | string | Email đăng nhập |
| `password` | string | Mật khẩu |
| `remember` | bool | Tùy chọn |

### Sample — Response (thành công)

```json
{
  "flag": true,
  "message": null,
  "token": "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9...",
  "refreshToken": "...",
  "expiration": "2026-03-21T12:00:00Z"
}
```

| Trường | Kiểu | Ghi chú |
|--------|------|---------|
| `flag` | bool | `true` khi đăng nhập thành công |
| `message` | string | Thông báo hoặc lỗi |
| `token` | string | JWT — gửi header `Authorization: Bearer {token}` |
| `refreshToken` | string | Dùng khi làm mới phiên (nếu triển khai) |
| `expiration` | string | Thời điểm hết hạn token |

---

## API 2 — Lấy dữ liệu realtime vận hành tràn

**Mục đích:** Trả về một bản ghi tổng hợp trạng thái trạm, tag và các giá trị tính toán (lưu lượng, mực nước, cống, …).

| | |
|--|--|
| **Phương thức** | `GET` |
| **Đường dẫn** | `{BaseUrl}api/FT02/GetRealtimeDisplay` |
| **Query** | Không |

**Header (nếu API yêu cầu xác thực):**

```http
Authorization: Bearer {token}
```

### Sample — Response body (rút gọn)

```json
{
  "locationId": 1,
  "locationName": "Tràn chính",
  "stations": [
    {
      "path": "/station/1",
      "stationId": 1,
      "stationName": "Trạm 1",
      "remote": false,
      "local": true,
      "door1_Aperture": 1.2,
      "door2_Aperture": 1.1,
      "fllow_Door1": 100.0,
      "fllow_Door2": 95.0,
      "total_Fllow": 195.0
    }
  ],
  "calculatorValue": {
    "api_Fllow_DauTieng": 120.0,
    "api_Fllow_BenSuc": 80.0,
    "q_cs1": 10.5,
    "q_cs2": 8.2,
    "w_cs1": 1000.0,
    "w_cs2": 950.0,
    "q_tr": 0.0,
    "w_tr": 0.0,
    "q_den": 0.0,
    "w_den": 0.0
  }
}
```

| Trường (cấp ngoài) | Kiểu | Ghi chú |
|--------------------|------|---------|
| `locationId` | int | Định danh vị trí hiển thị |
| `locationName` | string | Tên vị trí |
| `stations` | mảng object | Danh sách trạm; mỗi phần tử gồm thông tin trạm, cờ trạng thái (cửa, motor, …), giá trị đo — đầy đủ theo triển khai |
| `calculatorValue` | object | Các chỉ số tổng hợp, luồng API ngoài, cân bằng nước (Q, W, các `api_*`, …) — đầy đủ theo triển khai |

> Thuộc tính JSON thường dùng **camelCase**.
