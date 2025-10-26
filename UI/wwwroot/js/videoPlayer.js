// wwwroot/js/videoPlayer.js
window.loadHlsPlayer = (videoId, hlsUrl) => {
    var video = document.getElementById(videoId);
    if (!video) {
        console.error("Không tìm thấy video: " + videoId);
        return;
    }

    if (Hls.isSupported()) {
        var hls = new Hls();
        hls.loadSource(hlsUrl);
        hls.attachMedia(video);
        hls.on(Hls.Events.MANIFEST_PARSED, function () {
            // THÊM DÒNG NÀY:
            video.muted = true;
            video.play().catch(e => console.warn("Trình duyệt có thể đã chặn tự động phát: ", e));
        });
    } else if (video.canPlayType('application/vnd.apple.mpegurl')) {
        // Hỗ trợ HLS gốc trên Safari/iOS
        video.src = hlsUrl;
        video.addEventListener('loadedmetadata', function () {
            // THÊM DÒNG NÀY:
            video.muted = true;
            video.play().catch(e => console.warn("Trình duyệt có thể đã chặn tự động phát: ", e));
        });
    }
};