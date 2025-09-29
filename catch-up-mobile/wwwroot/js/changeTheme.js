function changeTheme(theme) {
    let themeLink = document.getElementById("themeStylesheet");
    themeLink.href = `css/${theme}.css`;
    document.documentElement.setAttribute('data-bs-theme', theme);
    localStorage.setItem("theme", theme);
}

function changeAutoTheme(auto) {
    localStorage.setItem("autoTheme", auto);
}

document.addEventListener("DOMContentLoaded", function () {
    let savedTheme = localStorage.getItem("theme") || "catchUpNight";
    changeTheme(savedTheme);
});