# 20250601_AHD_QuanTrac
Quan trắc môi trường

Station  information:
- Tram Dau TIeng
- IP Public: 14.224.229.6
- Gateway: 192.168.0.1
            admin
            Pass: Dmc@5001
- IP server local: 192.168.0.100

- Ultra viewer: 121365864 - pass: kn@123456

- SQL server: sa - pass: FTCuAGnnB*Jyd%pW
            dev1  - pass: pR*mBaG)@v(yn*Wc
            NAT port: local 1433 - public 9168

Dùng mediamtx
https://github.com/bluenviron/mediamtx/releases

MediaMTX là gì?

MediaMTX (trước đây tên là rtsp-simple-server) là phần mềm trung gian dùng để:

Nhận luồng RTSP từ camera IP, NVR, v.v.

Chuyển đổi thành các định dạng mà trình duyệt có thể xem được, như:

HLS (.m3u8) → xem được bằng Hls.js

WebRTC → xem được trực tiếp, không độ trễ cao

👉 Nói cách khác:
Trình duyệt KHÔNG thể xem RTSP trực tiếp, nhưng MediaMTX có thể "chuyển" nó sang dạng xem được.