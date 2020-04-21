//js code for hamburger menu animation
$(document).ready(function () {
    $('.navbar-toggler').on('click', function () {
        $('.animated-navbar-toggler-icon').toggleClass('open');
    });
});