
function changeTheme(theme) {
    document.documentElement.className = theme;
    localStorage.setItem("theme", theme);
}

function loadTheme() {
    var theme = localStorage.getItem("theme");

    if (theme == null || theme == "") {
        return;
    }

    $('select.theme-selector').val(theme);
    changeTheme(theme);
}

loadTheme();