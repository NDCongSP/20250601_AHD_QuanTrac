// Function to set the culture in the browser
window.setCulture = function (culture) {
    const html = document.documentElement;
    html.lang = culture;
    
    // Set the culture in the cookie
    const date = new Date();
    date.setTime(date.getTime() + (365 * 24 * 60 * 60 * 1000)); // 1 year
    const expires = "; expires=" + date.toUTCString();
    document.cookie = `culture=${culture}${expires}; path=/; SameSite=Lax`;
    
    // Dispatch a custom event that the culture has changed
    const event = new CustomEvent('cultureChanged', { detail: culture });
    window.dispatchEvent(event);
};
