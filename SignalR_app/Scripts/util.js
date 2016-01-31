$(function () {

    $('#chatBody').hide();
    $('#loginBlock').show();
    // link to automatic generated hub
    var chat = $.connection.chatHub;

    // declare variable, which calls hub when user is connecting
    chat.client.addMessage = function (name, message, color) {
        // add message to page
        var date = new Date(),
            dateDisplay = date.getHours() + ":" + date.getMinutes() + ":" + date.getSeconds(),
            c = color || "#333";
        
        $('#chatroom').append('<p><i style="margin:0 10px 0 0;">' + dateDisplay + '</i><b style="color:'+ c +'">' + htmlEncode(name)
            + '</b> : ' + htmlEncode(message) + '</p>');
    };

    // method, which will be called after each user successful connection
    chat.client.onConnected = function (id, userName, allUsers, isAuthorized) {

        $('#chatroom').empty();

        $('#loginBlock').hide();
        $('#chatBody').show();
        // specify inside hidden fields name and id of current user
        $('#hdId').val(id);
        $('#username').val(userName);
        $('#header').html('<h3>Welcome, ' + userName + '</h3>');
        $('#totalUsers').text(allUsers.length);
        $('#totalUsers').css({'color': allUsers.length > 1 ? "green" : "red" });

        // add all users
        for (var i = 0; i < allUsers.length; i++) {
            AddUser(allUsers[i].ConnectionId, allUsers[i].Name, isAuthorized);
        }
    };

    // add a new user
    chat.client.onNewUserConnected = function(id, name, isAuthorized) {

        AddUser(id, name, isAuthorized);
    };

    // remove user
    chat.client.onUserDisconnected = function(id, userName, allUsers) {

        $('#' + id).remove();
        $('#totalUsers').text(allUsers.length);
        $('#totalUsers').css({ 'color': allUsers.length > 1 ? "green" : "red" });
    };

    // open connection
    $.connection.hub.start().done(function () {

        $('#sendmessage').click(function () {
            // call method Send from hub
            chat.server.send($('#username').val(), $('#message').val());
            $('#message').val('');
        });

        // obtain login
        $("#btnLogin").click(function () {

            var name = $("#txtUserName").val();
            if (name.length > 0) {
                chat.server.connect(name);
            }
            else {
                alert("Please, enter your name.");
            }
        });
    });
});
// encode the html-tags
function htmlEncode(value) {
    var encodedValue = $('<div />').text(value).html();
    return encodedValue;
}
//add a new user
function AddUser(id, name, isAuthorized) {

    var userId = $('#hdId').val();

    /*if (userId != id) {

        $("#chatusers").append('<p id="' + id + '"><b>' + name + '</b></p>' + icon);
        $("#userList").append('<span id="' + id + '"><b>' + name + '</b>; </span>');
    }*/
    
    $("#userList").append('<span id="' + id + '"><b>' + name + '</b> , </span>');

}