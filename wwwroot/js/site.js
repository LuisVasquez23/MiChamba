
window.addEventListener('scroll', function () {
    changeNavbarBackground();
});

window.addEventListener('DOMContentLoaded', function () {
    changeNavbarBackground();
});

const changeNavbarBackground = () => {
    let navbar = document.getElementById('navbar');
    let scrollTop = window.pageYOffset || document.documentElement.scrollTop;

    if (scrollTop > 0) {
        // Cuando se hace scroll hacia abajo
        navbar.style.background = '#3C486B';
    } else {
        // Cuando está en la parte superior
        navbar.style.background = 'rgba(60, 72, 107, 0.70)';
    }
}
