window.ScrollToBottom = (elementName) => {
    element = document.getElementById(elementName);
    element.scrollTop = element.scrollHeight - element.clientHeight;
}

window.ScrollToSection = (elementName) => {
    element = document.getElementById(elementName);
    document.documentElement.scrollTop = document.body.scrollTop = element.offsetTop
}