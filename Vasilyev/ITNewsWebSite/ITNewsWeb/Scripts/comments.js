$(function () {
    var comments = $.connection.commentsHub;
    comments.client.addNewMessageToPage = function (name, message, itemId) {
        if (name == "admin@admin.com") {
            $('#discussion').append('<p style="color:green; text-align:left; width:500px"><strong><img = src="https://www.phplivesupport.com/pics/icons/avatars/public/avatar_7.png" title="Admin">'
                + ' </strong> ' + htmlEncode(message) + '</p>');
        }
        else if (name != "admin@admin.com") {
            $('#discussion').append('<p style="color:blue;text-align:right;"><strong><img = src="https://www.phplivesupport.com/pics/icons/avatars/public/avatar_71.png" title="Peter">'
                + ' </strong> ' + htmlEncode(message) + '</p>');
        }
    };
    comments.client.initPage = function (itemId) {

    };

    //$('#displayname').val(prompt('Enter your name:', ''));
    $('#message').focus();
    $.connection.hub.start().done(function () {
        comments.server.initPage($('#itemId').val());
        $('#sendmessage').click(function () {
            comments.server.send($('#displayname').val(), $('#message').val(), $('#itemId').val());
            $('#message').val('').focus();
        });
    });
});

function htmlEncode(value) {
    var encodedValue = $('<div />').text(value).html();
    return encodedValue;
}