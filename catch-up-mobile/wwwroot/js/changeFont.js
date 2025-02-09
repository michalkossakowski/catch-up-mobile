function changeFontSize(size) {
    document.body.classList.remove("font-size-small", "font-size-medium", "font-size-large");
    document.body.classList.add(size);
}