$(document).ready(function() {
    $('#convertForm').on('submit', function(event) {
        event.preventDefault();

        $.ajax({
            url: '/Home/ConvertCurrency/',
            type: 'POST',
            data: $(this).serialize(),
            success: function(data) {
                $('#resultLabel').text(data.result);
                $('#historyTable').append('<tr><td>' + data.date + '</td><td>' + data.source + '</td><td>' + data.target + '</td><td>' + data.value + '</td><td>' + data.result + '</td></tr>');
            }
        });
    });
});