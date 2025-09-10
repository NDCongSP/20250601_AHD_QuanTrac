function updateSvgWaterLevel(data) {
    //data is a object, so need check all property of object to update svg
    for (var key in data) {
        //check if exist id of key
        if (document.getElementById(key)) {
            const value = data[key];
            // Format number to 2 decimal places if it's a number
            const formattedValue = typeof value === 'number' ? value.toFixed(2) : value;
            document.getElementById(Nkey).innerHTML = formattedValue;
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
        .filter(s => s.StationId <= 3) // Get first 3 stations
        .sort((a, b) => a.StationId - b.StationId); // Sort by StationId
    
    // Process each station
    stations.forEach(station => {
        const stationId = station.StationId;
        const locationId = location.LocationId;
        
        // Process all properties of the station
        for (const [key, value] of Object.entries(station)) {
            if (key === 'StationId' || key === 'StationName' || key === 'Path') continue;
            
            // Format property name (Upper first letter)
            const elementId = `Location${locationId}_Station${stationId}_${key}`;
            const element = document.getElementById(elementId);
            
            if (element) {
                const formattedValue = typeof value === 'number' ? value.toFixed(3) : value;
                element.innerHTML = formattedValue;
            }
        }
    });
    
    // Process CalculatorValue
    const calculatorValues = location.CalculatorValue;
    for (const [key, value] of Object.entries(calculatorValues)) {
        const element = document.getElementById(key);
        
        if (element) {
            const formattedValue = typeof value === 'number' ? value.toFixed(3) : value;
            element.innerHTML = formattedValue;
        }
    }
}

