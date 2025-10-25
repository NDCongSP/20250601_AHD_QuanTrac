// Simple HLS loader for <video> elements
// Usage: loadHlsPlayer('videoElementId', 'http://host:port/path/index.m3u8')
window.loadHlsPlayer = function (videoId, hlsUrl) {
    try {
        const video = document.getElementById(videoId);
        if (!video) {
            console.warn('loadHlsPlayer: video element not found', videoId);
            return;
        }
        if (!hlsUrl) {
            console.warn('loadHlsPlayer: empty url');
            return;
        }
        if (window.Hls && window.Hls.isSupported()) {
            const hls = new Hls({
                maxBufferLength: 30,
                liveSyncDurationCount: 3
            });
            hls.loadSource(hlsUrl);
            hls.attachMedia(video);
        } else if (video.canPlayType('application/vnd.apple.mpegurl')) {
            video.src = hlsUrl;
        } else {
            console.error('HLS is not supported in this browser');
        }
    } catch (err) {
        console.error('loadHlsPlayer error', err);
    }
}


