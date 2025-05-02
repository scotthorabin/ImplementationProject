// Please see documentation at https://learn.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.


// This is for smooth scrolling and implementation of buttons for topics

    document.addEventListener("DOMContentLoaded", function () {
        const slider = document.getElementById("topicsGrid");
        const scrollAmount = slider.clientWidth / 2; // Scroll half of the container width

        window.scrollTopicsLeft = function () {
            slider.scrollBy({ left: -scrollAmount, behavior: "smooth" });
        };

        window.scrollTopicsRight = function () {
            slider.scrollBy({ left: scrollAmount, behavior: "smooth" });
        };
    });

function menuToggle() {
    const toggleMenu = document.querySelector('.menu');
    toggleMenu.classList.toggle('active')
}