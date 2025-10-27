// wwwroot/js/videoPlayer.js
window.loadHlsPlayer = (videoId, hlsUrl) => {
    var video = document.getElementById(videoId);
    if (Hls.isSupported()) {
        var hls = new Hls();
        hls.loadSource(hlsUrl);
        hls.attachMedia(video);
        hls.on(Hls.Events.MANIFEST_PARSED, function () {
            video.play();
        });
    } else if (video.canPlayType('application/vnd.apple.mpegurl')) {
        // Hỗ trợ HLS gốc trên Safari/iOS
        video.src = hlsUrl;
        video.addEventListener('loadedmetadata', function () {
            video.play();
        });
    }
};