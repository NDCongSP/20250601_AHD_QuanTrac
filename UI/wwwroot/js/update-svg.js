/**
 * Updates SVG elements based on the provided data
 * @param {Object} element - The DOM element to update
 * @param {*} value - The value to set
 * @param {Object} options - Configuration options
 * @param {number} [options.decimals=2] - Number of decimal places for numbers
 * @param {string} [options.prefix=''] - Prefix for the value
 * @param {string} [options.suffix=''] - Suffix for the value
 */
function updateElement(element, value, { decimals = 1, prefix = '', suffix = '' } = {}) {
    if (!element) return;
    if (typeof value === 'number') {
        // Only show decimals if the number has a fractional part
        //const formattedValue = Number.isInteger(value) ? value.toString() : value.toFixed(decimals);
        const formattedValue = value.toString();
        element.innerHTML = `${prefix}${formattedValue}${suffix}`;
        element.style.display = '';
        element.style.fill = value === 0 ? 'red' : '';
    } else if (typeof value === 'boolean') {
        element.style.display = value ? '' : 'none';
    } else {
        element.innerHTML = `${prefix}${value}${suffix}`;
    }
}

function formatVietnameseNumber(value) {
    const str = value.toString();
    const parts = str.split('.');

    // Add commas to integer part
    parts[0] = parts[0].replace(/\B(?=(\d{3})+(?!\d))/g, ',');

    // Join with dot for decimal part
    return parts.join('.');
}
function setRectPercent(filledId, value) {
    const rect = document.getElementById(filledId);
    const bg = document.getElementById(filledId.replace('color', 'bgcolor'));

    // Lấy thông tin từ rect nền
    const maxHeight = parseFloat(bg.getAttribute("height"));
    const yBase = parseFloat(bg.getAttribute("y")) + maxHeight;

    // Clamp percent [0, 100]
    const p = Math.max(0, Math.min(100, value / 510));

    // Tính toán chiều cao và y mới
    const height = maxHeight * p;
    const y = yBase - height;

    // Gán vào rect cần fill
    rect.setAttribute("height", height);
    rect.setAttribute("y", y);
}

function updateSvgWaterLevel(data) {
    for (const [key, value] of Object.entries(data)) {
        const element = document.getElementById(key);
        if (element) {
            updateElement(element, value, { decimals: 2 });
        }
    }
}

function updateSvgFillOverflow(location) {
    // If location is a JsonElement, get the raw value
    if (location && typeof location.getProperty === 'function') {
        location = JSON.parse(JSON.stringify(location));
    }

    if (!location.Stations || !location.CalculatorValue) return;

    // Process first 3 stations, sorted by StationId
    const stations = [...location.Stations]
        .sort((a, b) => a.StationId - b.StationId);

    // Process each station
    stations.forEach(station => {
        const { StationId: stationId } = station;
        const locationId = location.LocationId;

        station.Door1_Close = station.Door1_Close;
        station.Door1_Open = !station.Door1_Close;
        station.Door2_Close = station.Door2_Close;
        station.Door2_Open = !station.Door2_Close;
        station.Fllow_Door1 = station.Door1_Aperture_Final;
        station.Fllow_Door2 = station.Door2_Aperture_Final;

        // Process all properties of the station
        for (const [key, value] of Object.entries(station)) {
            if (['StationId', 'StationName', 'Path'].includes(key)) continue;

            // Define elements to update
            const elements = [
                document.getElementById(`Location${locationId}_Station${stationId}_${key}`),
                document.getElementById(`Location${locationId}_Station${stationId}_a${key}`),
                document.getElementById(`Location${locationId}_Station${stationId}_STT${key}`),
                document.getElementById(`Location${locationId}_Station${stationId}_color${key}`)
            ];

            // Update all elements
            elements.forEach(element => {
                if (element && element.id.includes('_color')) {
                    // For color elements, use setRectPercent if the value is a number
                    if (typeof value === 'number') {
                        setRectPercent(element.id, value);
                    }
                } else {
                    updateElement(element, value);
                }
            });
        }
    });

    // Process CalculatorValue
    for (const [key, value] of Object.entries(location.CalculatorValue)) {
        const element = document.getElementById(key);
        updateElement(element, value, { decimals: 1 });
    }
}


function updateSvgHoChua(location) {
    // If location is a JsonElement, get the raw value
    if (location && typeof location.getProperty === 'function') {
        location = JSON.parse(JSON.stringify(location));
    }

    // Nếu location là mảng => lấy phần tử đầu tiên
    if (Array.isArray(location)) {
        location = location[0];
    }

    // Lấy ra StationId = 4
    const station41 = location.stations.find(st => st.stationId === 4);

    const station4 = location.stations[3];
   

    const calc = location.calculatorValue;

    if (!station4 || !calc) {
        console.warn("Không tìm thấy stationId=4 hoặc CalculatorValue:", { station4, calc });
        return;
    }

    // Hiển thị lên HTML (ví dụ)
    //$("#Location1_Location_Info_Fllow_Ho_Final").innerHTML(station4.fllow_Ho_Final); // Fllow_Ho_Final = 24.4
    //$("#W1_ho").innerHTML(calc.w1_ho);        // API_Fllow_TL_CDD = 23.4

    _elementId = document.getElementById("Location1_Location_Info_Fllow_Ho_Final");
    if (_elementId) {
        _elementId.innerHTML = station4.fllow_Ho_Final;
    }

    _elementId = document.getElementById("CalculatorValue.W1_ho");
    if (_elementId) {
        _elementId.innerHTML = calc.w1_ho;
    }

    _elementId = document.getElementById("CalculatorValue.Q_den");
    if (_elementId) {
        _elementId.innerHTML = calc.q_den;
    }

    _elementId = document.getElementById("CalculatorValue.Q_di");
    if (_elementId) {
        _elementId.innerHTML = calc.q_di;
    }

    _elementId = document.getElementById("CalculatorValue.Q_tr");
    if (_elementId) {
        _elementId.innerHTML = calc.q_tr;
    }

    _elementId = document.getElementById("CalculatorValue.Q_cs1");
    if (_elementId) {
        _elementId.innerHTML = calc.q_cs1;
    }

    _elementId = document.getElementById("CalculatorValue.Q_cs2");
    if (_elementId) {
        _elementId.innerHTML = calc.q_cs2;
    }

    _elementId = document.getElementById("CalculatorValue.Q_cs3");
    if (_elementId) {
        _elementId.innerHTML = calc.q_cs3;
    }
}

// Dashboard interop helpers
window.dashboardSetRef = function (ref) {
    window.__dashboardRef = ref;
};

window.handleSvgClick = function (value) {
    try {
        if (window.__dashboardRef && typeof window.__dashboardRef.invokeMethodAsync === 'function') {
            window.__dashboardRef.invokeMethodAsync('OnSvgClicked', value);
        } else {
            console.warn('dashboardSetRef not initialized yet');
        }
    } catch (err) {
        console.error('handleSvgClick error', err);
    }
};

// FT03 Report (Chart.js hourly chart)
window.ft03Report = (function () {
    let chart;
    function renderHourlyChart(points) {
        const el = document.getElementById('ft03HourlyChart');
        if (!el) return;
        const labels = (points || []).map(p => p.label);
        const data = (points || []).map(p => (p && p.value != null && isFinite(p.value)) ? Number(p.value) : null);

        if (chart) {
            chart.destroy();
        }

        chart = new Chart(el.getContext('2d'), {
            type: 'line',
            data: {
                labels,
                datasets: [{
                    label: 'Giá trị',
                    data,
                    borderColor: '#0078D7',
                    backgroundColor: '#0078D733',
                    borderWidth: 2,
                    pointRadius: 2,
                    pointHoverRadius: 4,
                    tension: 0.3,
                    spanGaps: true // nối qua các điểm null
                }]
            },
            options: {
                responsive: true,
                maintainAspectRatio: false,
                plugins: {
                    legend: { display: false },
                    tooltip: {
                        callbacks: {
                            title: ctx => ctx[0]?.label || '',
                            label: ctx => `Giá trị: ${ctx.parsed.y?.toFixed(2)}`
                        }
                    }
                },
                scales: {
                    x: {
                        grid: { color: '#eee' },
                        title: { display: true, text: 'Giờ' }
                    },
                    y: {
                        grid: { color: '#eee' },
                        title: { display: true, text: 'Giá trị' }
                    }
                },
                elements: { line: { tension: 0.3 } }
            }
        });
    }
    return { renderHourlyChart };
})();