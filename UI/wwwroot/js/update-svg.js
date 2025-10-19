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
    if (element.id.includes('STT'))
        console.log(`${element} :${value}`)
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

