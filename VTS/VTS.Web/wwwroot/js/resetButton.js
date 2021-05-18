//js code for resetting all the fields in the form
$(document).ready(function () {
    $(".clear-all").click(function () {
        const form = $('form[name="main-form"]');

        form.find(".selectpicker").each(function () {
            $(this).val('default');
            $(this).selectpicker('render');
        });

        form.find("input, textarea").val("");
    });
});