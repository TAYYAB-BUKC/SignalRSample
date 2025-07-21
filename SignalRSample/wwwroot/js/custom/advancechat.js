
var advanceChatConnection = new signalR.HubConnectionBuilder().withUrl("/hubs/advancechat").build();

advanceChatConnection.on("UserConnected", function (userEmail) {
	AddMessage(`${userEmail} has open a connection!`);
});

advanceChatConnection.on("UserDisConnected", function (userEmail) {
	AddMessage(`${userEmail} has closed a connection!`);
});

function AddMessage(message) {
	if (message != '' && message != null) {
		$('#messagesList').append(`<li>${message}</li>`);
	}
}

advanceChatConnection.start().then(function OnSuccess() {
	console.log("Connected to Advance Chat Hub");
}, function OnFailure() {
	console.log("Failed to connect to Advance Chat Hub");
});

function AddNewRoom() {
    var roomName = $('#createRoomName').val();

    if (roomName == null && roomName == '') {
        return;
    }

    /*POST*/
    $.ajax({
        url: '/ChatRooms/PostChatRoom',
        dataType: "json",
        type: "POST",
        contentType: 'application/json; charset=utf-8',
        data: JSON.stringify({ id: 0, name: roomName }),
        async: true,
        processData: false,
        cache: false,
        success: function (json) {
            /*ADD ROOM COMPLETED SUCCESSFULLY*/
            $('#createRoomName').val('');
        },
        error: function (xhr) {
            alert('error');
        }
    });
}

$(function () {
    FillRoomDropDown();
    FillUserDropDown();
});

function FillUserDropDown() {
    $.getJSON('/ChatRooms/GetChatUsers')
        .done(function (json) {
            var ddlSelUser = document.getElementById("ddlSelUser");
            ddlSelUser.innerText = null;

            json.forEach(function (item) {
                var newOption = document.createElement("option");
                newOption.text = item.userName;
                newOption.value = item.id;
                ddlSelUser.add(newOption);
            });
        })
        .fail(function (jqxhr, textStatus, error) {
            console.log("Request Failed: " + jqxhr.detail);
        });
}

function FillRoomDropDown() {
    $.getJSON('/ChatRooms/GetChatRooms')
        .done(function (json) {
            var ddlDelRoom = document.getElementById("ddlDelRoom");
            var ddlSelRoom = document.getElementById("ddlSelRoom");

            ddlDelRoom.innerText = null;
            ddlSelRoom.innerText = null;

            json.forEach(function (item) {
                var newOption = document.createElement("option");
                newOption.text = item.name;
                newOption.value = item.id;
                ddlDelRoom.add(newOption);

                var newOption1 = document.createElement("option");
                newOption1.text = item.name;
                newOption1.value = item.id;
                ddlSelRoom.add(newOption1);
            });
        })
        .fail(function (jqxhr, textStatus, error) {
            console.log("Request Failed: " + jqxhr.detail);
        });
}