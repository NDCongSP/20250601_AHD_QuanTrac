function formatVietnameseNumber(value) {
    if (value === null || value === undefined || value === '') return '';
    
    const str = value.toString();
    const parts = str.split('.');
    
    // Add commas to integer part
    parts[0] = parts[0].replace(/\B(?=(\d{3})+(?!\d))/g, ',');
    
    // Join with dot for decimal part
    return parts.join('.');
}

function parseVietnameseNumber(value) {
    if (value === null || value === undefined || value === '') return null;
    
    // Remove commas and convert to number
    const cleanValue = value.toString().replace(/,/g, '');
    const num = parseFloat(cleanValue);
    return isNaN(num) ? null : num;
}

function formatVietnameseNumbers() {
    // Find all RadzenNumeric input elements
    const numericInputs = document.querySelectorAll('.rz-numeric input[type="text"]');
    
    numericInputs.forEach(input => {
        // Format on blur (when user leaves the field)
        input.addEventListener('blur', function() {
            const value = this.value;
            if (value && !isNaN(parseVietnameseNumber(value))) {
                const formatted = formatVietnameseNumber(value);
                this.value = formatted;
            }
        });
        
        // Parse on focus (when user enters the field)
        input.addEventListener('focus', function() {
            const value = this.value;
            if (value) {
                const parsed = parseVietnameseNumber(value);
                if (parsed !== null) {
                    this.value = parsed.toString();
                }
            }
        });
        
        // Format on input change
        input.addEventListener('input', function() {
            const value = this.value;
            if (value && !isNaN(parseVietnameseNumber(value))) {
                // Only format if the value is complete (not while typing)
                if (!value.includes('.')) {
                    const formatted = formatVietnameseNumber(value);
                    if (formatted !== value) {
                        this.value = formatted;
                    }
                }
            }
        });
    });
}

function parseVietnameseNumbers(config) {
    if (!config) return;
    
    // List of numeric properties in OffsetConfig
    const numericProperties = [
        'HT_Cylinder1_1', 'HT_Cylinder1_2', 'HT_Cylinder2_1', 'HT_Cylinder2_2',
        'Door1_Aperture', 'Door2_Aperture', 'S1_Temp_Oil',
        'Pressure_Oil_Door1', 'Pressure_Oil_Door2',
        'Fllow_Door1', 'Fllow_Door2', 'Fllow_Ho'
    ];
    
    numericProperties.forEach(prop => {
        if (config[prop] !== null && config[prop] !== undefined) {
            const value = config[prop].toString();
            const parsed = parseVietnameseNumber(value);
            if (parsed !== null) {
                config[prop] = parsed;
            }
        }
    });
}

// Make functions available globally
window.formatVietnameseNumbers = formatVietnameseNumbers;
window.formatVietnameseNumber = formatVietnameseNumber;
window.parseVietnameseNumber = parseVietnameseNumber;
window.parseVietnameseNumbers = parseVietnameseNumbers;
