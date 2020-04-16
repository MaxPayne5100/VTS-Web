//js code for transform effect on scroll
$(document).ready(function ()
{
    var skewed = document.querySelector('.skewed');
    var value = -2 + window.scrollY / 50;

    if (value <= 0) {
        skewed.style.transform = "skewY(" + value + "deg)"
    }

    window.addEventListener('scroll', function () {
        value = -2 + window.scrollY / 50;

        if (value <= 0) {
            skewed.style.transform = "skewY(" + value + "deg)"
        };
    })
});