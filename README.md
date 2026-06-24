# 20250601\_AHD\_QuanTrac

Quan trắc môi trường

Station  information:

* Tram Dau TIeng
* IP Public: 14.224.229.6
* Gateway: 192.168.0.1
admin
Pass: Dmc@5001
* IP server local: 192.168.0.100
* Ultra viewer: 121365864 - pass: kn@123456
* SQL server: sa - pass: *FTCuAGnnB\*Jyd%pW
dev1  - pass: pR*mBaG)@v(yn\*Wc
NAT port: local 1433 - public 9168
* WEB API: 14.224.229.6:9170
* Web UI: 14.224.229.6:9171 / http://dautieng.thuyloimiennam.vn/

  * user: admin@gmail.com
  * password: admin@gmail.comP1
  * TK API: api@gmail.com - api@gmail.comP1
* login Scada: user01 - Ahd@123

Dùng mediamtx



pas cam:

&#x09;admin - abcd1234 

&#x09;admin - dautieng2021
https://github.com/bluenviron/mediamtx/releases

MediaMTX là gì?

MediaMTX (trước đây tên là rtsp-simple-server) là phần mềm trung gian dùng để:

Nhận luồng RTSP từ camera IP, NVR, v.v.

Chuyển đổi thành các định dạng mà trình duyệt có thể xem được, như:

HLS (.m3u8) → xem được bằng Hls.js

WebRTC → xem được trực tiếp, không độ trễ cao

👉 Nói cách khác:
Trình duyệt KHÔNG thể xem RTSP trực tiếp, nhưng MediaMTX có thể "chuyển" nó sang dạng xem được.
Install mediaMTX vào windown service



Sửa config để force dùng HLS thay WebRTC khi xem qua trình duyệt — chỉ cần NAT TCP 8888 là đủ:

yamlhlsAlwaysRemux: yes   # ← đổi từ no thành yes, stream luôn sẵn sàng



Cách 1 — Đổi camera H.265 sang H.264 (tốt nhất)

Vào web config của từng camera .84 → .88:



Video/Audio → Video Encoding → Encoding Standard → H.264



paths:

&#x20; # example:

&#x20; Cam1:

&#x20;    source: rtsp://admin:abcd1234@192.168.0.161:554/Streaming/Channels/101

&#x20; Cam2:

&#x20;    source: rtsp://admin:abcd1234@192.168.0.162:554/Streaming/Channels/101

&#x20; Cam3:

&#x20;    source: rtsp://admin:abcd1234@192.168.0.163:554/Streaming/Channels/101

&#x20; Cam4:

&#x20;    source: rtsp://admin:abcd1234@192.168.0.164:554/Streaming/Channels/101

&#x20; Cam5:

&#x20;    source: rtsp://admin:abcd1234@192.168.0.165:554/Streaming/Channels/101

&#x20; Cam6:

&#x20;    source: rtsp://admin:abcd1234@192.168.0.166:554/Streaming/Channels/101

&#x20; Cam7:

&#x20;    source: rtsp://admin:abcd1234@192.168.0.167:554/Streaming/Channels/101

&#x20; Cam8:

&#x20;    source: rtsp://admin:abcd1234@192.168.0.168:554/Streaming/Channels/101

&#x20; Cam9:

&#x20;    source: rtsp://admin:abcd1234@192.168.0.169:554/Streaming/Channels/101

&#x20; Cam10:

&#x20;    source: rtsp://admin:abcd1234@192.168.0.170:554/Streaming/Channels/101

&#x20; Cam11:

&#x20;    source: rtsp://admin:dautieng2021@192.168.0.84:554/Streaming/Channels/101

&#x20; Cam12:

&#x20;    source: rtsp://admin:dautieng2021@192.168.0.85:554/Streaming/Channels/101

&#x20; Cam13:

&#x20;    source: rtsp://admin:dautieng2021@192.168.0.86:554/Streaming/Channels/101

&#x20; Cam14:

&#x20;    source: rtsp://admin:dautieng2021@192.168.0.87:554/Streaming/Channels/101

&#x20; Cam15:

&#x20;    source: rtsp://admin:dautieng2021@192.168.0.88:554/Streaming/Channels/101

