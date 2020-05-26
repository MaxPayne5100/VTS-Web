//js code for deleting personal bookings
$(document).ready(function () {
    $('.close-button').on('click', function () {
        const url = $('button[data-target="#confirmModal"]').data('url');

        $.ajax({
            url: url,
            type: 'DELETE',
            success: function () {
                location.reload();
            },
            error: function (xhr, status, error) {
                console.log(xhr.responseText);
            }
        });
    });

});