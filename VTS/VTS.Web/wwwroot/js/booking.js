//js code for home page frontend logic
$(document).ready(function () {
    $(".input-validation-error").parent().css('margin-top', 20);

    var today = new Date();
    var dd = today.getDate();
    var mm = today.getMonth() + 1;
    var yyyy = today.getFullYear();

    if (dd < 10) {
        dd = '0' + dd
    }
    if (mm < 10) {
        mm = '0' + mm
    }

    today = yyyy + '-' + mm + '-' + dd;
    document.getElementById("firstDateId").setAttribute("min", today);

    document.getElementById("firstDateId").onchange = function () {
        var input = document.getElementById("secondDateId")
        input.setAttribute("min", this.value)
    }
});