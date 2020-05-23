//js code for home page frontend logic
$(document).ready(function () {
    $(".input-validation-error").parent().css('margin-top', 20);

    document.getElementById("firstDateId").onchange = function () {
        console.log(this.value)
        var input = document.getElementById("secondDateId")
        input.setAttribute("min", this.value)
    }
});