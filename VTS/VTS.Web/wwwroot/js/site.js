// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

//js code for hamburger menu animation
$(document).ready(function () {
    $('.navbar-toggler').on('click', function () {
        $('.animated-navbar-toggler-icon').toggleClass('open');
    });
});