# Hướng dẫn sử dụng — AHD_QuanTrac

> Phiên bản: 1.0 · Cập nhật: 2026-06-28
> Đối tượng: Người vận hành (Operator), Quản trị viên (Admin), người xem thông tin công khai.
> Hệ thống giám sát & vận hành **Hồ Dầu Tiếng** — *Công ty TNHH MTV Khai thác Thủy lợi Miền Nam*.

---

## 1. Bắt đầu

### 1.1 Yêu cầu
- Trình duyệt hiện đại (Chrome, Edge, Firefox bản mới). Hỗ trợ máy tính & điện thoại (responsive).
- Đường dẫn (URL) hệ thống do quản trị viên cung cấp.

### 1.2 Giao diện chung
Sau khi mở hệ thống, màn hình gồm 3 vùng:

```
┌─────────────────────────────────────────────────────────────┐
│  [≡] [Logo]  TIÊU ĐỀ ĐƠN VỊ        [🌐 Ngôn ngữ] [🔔] [👤] [⚙] │  ← Thanh trên (Header)
├──────────┬──────────────────────────────────────────────────┤
│          │                                                   │
│  MENU    │              NỘI DUNG TRANG                        │
│  (bên    │                                                   │
│  trái)   │                                                   │
│          │                                                   │
├──────────┴──────────────────────────────────────────────────┤
│                Copyright © 2025 AHD - PT.                     │  ← Chân trang (Footer)
└─────────────────────────────────────────────────────────────┘
```

- **Nút ≡ / mũi tên:** mở/thu gọn menu bên trái. Trên điện thoại menu ẩn mặc định.
- **Logo:** bấm để về Trang chủ.
- **🌐 Ngôn ngữ:** chuyển **Tiếng Việt / English** (trang sẽ tải lại để áp dụng).
- **👤 Tài khoản:** hiện khi đã đăng nhập — gồm *Profile, Settings, Change Password, Logout*.
- **⚙ / Giao diện:** chuyển chế độ Sáng/Tối.

### 1.3 Phân loại chức năng theo quyền
- **Công khai (không cần đăng nhập):** Trang chủ, Bản tin vận hành, Hồ chứa, Mực nước, Cống số 1/2/3, Camera.
- **Cần đăng nhập (nhóm *Dữ liệu chủ / MasterData*):** Khai báo trạm, Quản lý hệ số, Cấu hình biểu đồ/mực nước/bảng nội suy, Quản lý người dùng, Vai trò, Phân quyền, Tài khoản SCADA.

---

## 2. Đăng nhập / Đăng xuất

### 2.1 Đăng nhập
1. Bấm **Đăng nhập** (góc phải header hoặc mục menu) → vào trang `/login`.
2. Nhập **tài khoản** và **mật khẩu**, bấm **Đăng nhập**.
3. Thành công: menu hiện thêm nhóm chức năng quản trị tương ứng quyền của bạn.

> Quy tắc mật khẩu: tối thiểu 8 ký tự, có chữ hoa, chữ thường, chữ số và ký tự đặc biệt.
> Nhập sai 5 lần liên tiếp có thể bị **khóa tạm thời** — liên hệ quản trị viên.

### 2.2 Đổi mật khẩu
- Menu **👤 → Change Password** (hoặc `/change-password`). Nhập mật khẩu hiện tại + mật khẩu mới.

### 2.3 Đăng xuất
- Menu **👤 → Logout**. Hệ thống xóa phiên và quay về Trang chủ.

> Phiên đăng nhập tự động gia hạn (refresh token) trong thời gian cho phép; sau đó cần đăng nhập lại.

---

## 3. Các màn hình giám sát (công khai)

### 3.1 Trang chủ / Bảng điều khiển — `/`
Tổng quan tình trạng vận hành. Là điểm vào chính của hệ thống.

### 3.2 Bản tin vận hành — `/operation-board`
Kho **file & báo cáo** (PDF) theo cây thư mục:
- Duyệt thư mục, mở/xem file PDF ngay trên trình duyệt.
- (Khi có quyền) Tạo thư mục, tải lên PDF, đổi tên, xóa mục.

### 3.3 Hồ chứa — `/reservoir`
Biểu đồ & thông số **hồ chứa** (mực nước hồ, dung tích, lưu lượng đến/đi…). Dữ liệu cập nhật theo thời gian.

### 3.4 Mực nước (Hạ lưu / SSG) — `/water-level`
Biểu đồ **mực nước** tại các điểm quan trắc hạ lưu / sông Sài Gòn.

### 3.5 Cống Số 1 / 2 / 3 — `/cong1`, `/cong2`, `/cong3`
Màn hình vận hành **cửa cống**: độ mở cửa, hành trình xi-lanh, áp suất dầu, nhiệt độ dầu, lưu lượng qua cửa… hiển thị realtime theo sơ đồ thiết bị.

### 3.6 Tràn xả lũ (Spillway) — `/spillway`
Tổng quan **tràn xả lũ**; xem chi tiết từng cửa tại `/spillway/gate-info`.

### 3.7 Camera — `/camera`
Xem **camera IP** giám sát hiện trường (luồng RTSP).

> **Mẹo đọc biểu đồ:** đa số biểu đồ cho phép rê chuột để xem giá trị tại thời điểm, và chọn
> khoảng thời gian. Đơn vị số dùng dấu chấm thập phân (vd `12.5`).

---

## 4. Báo cáo — `/report`

Trích xuất **dữ liệu vận hành theo thời gian** (Data Log – FT03):
1. Chọn **khoảng thời gian** (Từ ngày – Đến ngày) và/hoặc thông số cần xem.
2. Hệ thống hiển thị dữ liệu dạng bảng/biểu đồ; có thể lấy mẫu (sampling) theo tần suất để xem xu hướng.
3. (Tùy cấu hình) Xuất báo cáo ra **Excel/PDF**.

---

## 5. Dữ liệu chủ & Quản trị (cần đăng nhập)

Mở nhóm **MasterData** (biểu tượng 🛠 trên menu) để thấy các mục dưới đây.

### 5.1 Khai báo trạm / vị trí — `/location-management`
Quản lý danh mục **vị trí và trạm đo**:
- Xem danh sách trạm; **Thêm/Sửa** trạm tại `/add-edit-location/{Id?}`.
- Khai báo thông tin trạm phục vụ thu thập và hiển thị tag.

### 5.2 Quản lý hệ số — `/setting-management`
Cấu hình **hệ số tính toán** dùng cho các giá trị suy diễn (lưu lượng, dung tích…).

### 5.3 Cấu hình biểu đồ hồ chứa — `/reservoir-chart-setting`
Thiết lập tham số hiển thị cho **biểu đồ hồ chứa** (FT05).

### 5.4 Cấu hình dữ liệu mực nước — `/water-level-data-configuration`
Thiết lập dữ liệu/đường biểu diễn cho **mực nước** (FT07).

### 5.5 Cấu hình bảng nội suy — `/interpolation-table-data-configuration`
Khai báo **bảng nội suy** (FT06): các cặp giá trị để nội suy (vd quan hệ mực nước ↔ dung tích/lưu lượng).

---

## 6. Quản lý người dùng & phân quyền (Admin)

### 6.1 Người dùng — `/users`
- Danh sách tài khoản; **Thêm/Sửa** tại `/user-detail/{Id?}`.
- Gán **vai trò** cho người dùng, cập nhật thông tin, đặt lại trạng thái.

### 6.2 Vai trò (Roles) — `/roles`
- Tạo/sửa **vai trò**; chi tiết tại `/role-detail/{Id}`.
- Vai trò hệ thống: **Admin**, **Operator**, **System**, **Report**.

### 6.3 Phân quyền (Permissions) — `/permissions`
- Gán **quyền chi tiết** cho từng vai trò; chi tiết tại `/permission-detail/{Id}`.
- Quyết định mỗi vai trò được xem/thao tác chức năng nào.

### 6.4 Tài khoản SCADA — `/scada-users`
Quản lý tài khoản dùng cho hệ thống **SCADA/HMI** kết nối:
- Thêm/sửa tài khoản SCADA, **đổi mật khẩu**, **đặt lại mật khẩu**.

> **Nguyên tắc phân quyền tối thiểu:** chỉ cấp quyền vừa đủ cho công việc. Hạn chế số tài khoản Admin.

---

## 7. Bản đồ điều hướng nhanh (URL)

| Chức năng | Đường dẫn | Quyền |
|-----------|-----------|-------|
| Trang chủ / Dashboard | `/` | Công khai |
| Bản tin vận hành (file/PDF) | `/operation-board` | Công khai (sửa: cần quyền) |
| Hồ chứa | `/reservoir` | Công khai |
| Mực nước HL/SSG | `/water-level` | Công khai |
| Cống Số 1 / 2 / 3 | `/cong1` · `/cong2` · `/cong3` | Công khai |
| Tràn xả lũ | `/spillway` · `/spillway/gate-info` | Công khai |
| Camera | `/camera` | Công khai |
| Báo cáo | `/report` | Đăng nhập |
| Khai báo trạm | `/location-management` · `/add-edit-location/{Id?}` | Đăng nhập |
| Quản lý hệ số | `/setting-management` | Đăng nhập |
| Cấu hình biểu đồ hồ chứa | `/reservoir-chart-setting` | Đăng nhập |
| Cấu hình mực nước | `/water-level-data-configuration` | Đăng nhập |
| Cấu hình bảng nội suy | `/interpolation-table-data-configuration` | Đăng nhập |
| Người dùng | `/users` · `/user-detail/{Id}` | Admin |
| Vai trò | `/roles` · `/role-detail/{Id}` | Admin |
| Phân quyền | `/permissions` · `/permission-detail/{Id}` | Admin |
| Tài khoản SCADA | `/scada-users` | Admin |
| Đăng nhập / Đổi mật khẩu | `/login` · `/change-password` | — |

---

## 8. Xử lý sự cố thường gặp (FAQ)

| Hiện tượng | Nguyên nhân thường gặp | Cách xử lý |
|------------|------------------------|------------|
| Không đăng nhập được | Sai tài khoản/mật khẩu, hoặc bị khóa do nhập sai nhiều lần | Kiểm tra lại; chờ ít phút hoặc nhờ Admin mở khóa |
| Đang dùng bị "đăng xuất" | Phiên hết hạn | Đăng nhập lại |
| Không thấy mục quản trị trên menu | Chưa đăng nhập hoặc không đủ quyền | Đăng nhập bằng tài khoản có quyền phù hợp |
| Trang/biểu đồ không có dữ liệu | Chưa có dữ liệu trong khoảng đã chọn, hoặc nguồn thu thập gián đoạn | Đổi khoảng thời gian; báo quản trị kiểm tra nguồn dữ liệu |
| Camera không hiển thị | Mất kết nối camera/RTSP | Kiểm tra mạng/camera; báo kỹ thuật |
| Số hiển thị sai định dạng | Khác biệt vùng ngôn ngữ | Hệ thống dùng dấu chấm thập phân; đổi ngôn ngữ nếu cần |
| Tải lên PDF lỗi | File quá lớn / sai định dạng / thiếu quyền | Dùng PDF hợp lệ; kiểm tra quyền |

> Khi báo lỗi cho bộ phận kỹ thuật, hãy ghi rõ: **trang đang dùng (URL)**, **thao tác vừa làm**,
> **thời điểm**, và **ảnh chụp màn hình** nếu có.

---

## 9. Liên hệ hỗ trợ
- Quản trị hệ thống: *(điền thông tin liên hệ nội bộ)*.
- Đơn vị vận hành: Công ty TNHH MTV Khai thác Thủy lợi Miền Nam.

---

*Hướng dẫn sử dụng · AHD_QuanTrac · 2026-06-28. Một số nhãn/đường dẫn có thể thay đổi theo phiên bản triển khai.*
