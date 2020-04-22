//js code for transform effect on scroll
$(document).ready(function ()
{
    var skewed = document.querySelector('.skewed');

    // variable to indicate degree of skew (should be between [-2.5;0])
    var value = -2.5 + window.scrollY / 50;

    if (value <= 0) {
        skewed.style.transform = "skewY(" + value + "deg)"
    }

    window.addEventListener('scroll', function () {
        value = -2.5 + window.scrollY / 50;

        if (value <= 0) {
            skewed.style.transform = "skewY(" + value + "deg)"
        };
    })
});