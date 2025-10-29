window.positionDropdownMenu = function (buttonId) {
    const button = document.getElementById(buttonId);
    const dropdown = document.querySelector('.dropdown-menu-custom');
    
    if (!button || !dropdown) return;
    
    const rect = button.getBoundingClientRect();
    const dropdownRect = dropdown.getBoundingClientRect();
    
    // Position directly below the button (adjusted position)
    let top = rect.bottom - 100;
    let left = rect.left - 50;
    
    // Check if dropdown would go off-screen to the bottom
    if (top + dropdownRect.height > window.innerHeight) {
        // Position above the button instead
        top = rect.top - dropdownRect.height - 102;
    }
    
    // Check if dropdown would go off-screen to the right
    if (left + dropdownRect.width > window.innerWidth) {
        // Align to the right edge of the button instead
        left = rect.right - dropdownRect.width;
    }
    
    // Check if dropdown would go off-screen to the left
    if (left < 0) {
        left = 5; // Small margin from left edge
    }
    
    dropdown.style.top = top + 'px';
    dropdown.style.left = left + 'px';
};

