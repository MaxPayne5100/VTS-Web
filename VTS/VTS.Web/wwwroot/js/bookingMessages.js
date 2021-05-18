$(document).ready(function () {
    let connection = new signalR.HubConnectionBuilder().withUrl("/bookingHub").build();
    connection.start();

    $('#Bookings').on('click', '.butt', function () {
        const userId = $("#HiddenField").val();
        const state = $("#StateSelect").find("option:selected").text()

        connection.invoke("SendMessage", userId, state).catch(function (err) {
            return console.error(err.toString());
        });
    });

    $('form[name="main-form"]').on('click', '.butt', function (event) {
        event.preventDefault();

        var isValid = $('form[name="main-form"]').find('[name="IsValid"]').val() == 'True';

        if (isValid) {
            toastr.options = {
                "closeButton": true,
                "debug": false,
                "newestOnTop": false,
                "progressBar": true,
                "positionClass": "toast-top-right",
                "preventDuplicates": true,
                "onclick": null,
                "showDuration": "300",
                "hideDuration": "300",
                "timeOut": "1500",
                "extendedTimeOut": "0",
                "showEasing": "swing",
                "hideEasing": "linear",
                "showMethod": "fadeIn",
                "hideMethod": "fadeOut"
            }

            toastr.options.onHidden = function () { $('form[name="main-form"]').submit(); }

            Command: toastr["info"]("Ваше бронювання очікує підтвердження")
        }
    });

    connection.on("ReceiveMessage", function (message) {
        toastr.options = {
            "closeButton": true,
            "debug": false,
            "newestOnTop": false,
            "progressBar": true,
            "positionClass": "toast-top-right",
            "preventDuplicates": true,
            "onclick": null,
            "showDuration": "300",
            "hideDuration": "300",
            "timeOut": "0",
            "extendedTimeOut": "0",
            "showEasing": "swing",
            "hideEasing": "linear",
            "showMethod": "fadeIn",
            "hideMethod": "fadeOut"
        }

        generalText = "Ваше бронювання "

        if (message == "Скасувати") {
            Command: toastr["error"](generalText + "скасовано")
        }
        else if (message == "Прийняти") {
            Command: toastr["success"](generalText + "підтверджено")
        }
    });
});