
var advanceChatConnection = new signalR.HubConnectionBuilder().withUrl("/hubs/advancechat").build();

advanceChatConnection.on("UserConnected", function (userEmail) {
	AddMessage(`${userEmail} has open a connection!`);
});

advanceChatConnection.on("UserDisConnected", function (userEmail) {
	AddMessage(`${userEmail} has closed a connection!`);
});

advanceChatConnection.on("NewRoomAdded", function (userEmail, newRoomName) {
    AddMessage(`${userEmail} has created a new room named ${newRoomName}`);
    FillRoomDropDown();
});

advanceChatConnection.on("RoomDeleted", function (userEmail, roomName) {
    AddMessage(`${userEmail} has deleted a room named ${roomName}`);
    FillRoomDropDown();
});

advanceChatConnection.on("ReceivePublicMessage", function (userEmail, roomName, message) {
    AddMessage(`[Public Message - ${roomName}] - ${userEmail} sent ${message}`);
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
            advanceChatConnection.send("NewRoomAdded", json.name);
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

function DeleteRoom() {
    let id = $('#ddlDelRoom option:selected').val();

    if (id == null && id == '') {
        return;
    }

    /*POST*/
    $.ajax({
        url: `/ChatRooms/DeleteChatRoom/${id}`,
        dataType: "json",
        type: "DELETE",
        contentType: 'application/json; charset=utf-8',
        async: true,
        processData: false,
        cache: false,
        success: function (json) {
            /*DELETE ROOM COMPLETED SUCCESSFULLY*/
            advanceChatConnection.send("RoomDeleted", $('#ddlDelRoom option:selected').text());
            toastr.success("Room deleted successfully");
        },
        error: function (xhr) {
            alert('error');
        }
    });
}

function SendPublicMessage() {
    let roomName = $('#ddlSelRoom option:selected').text();
    let message = $('#txtPublicMessage').val();

    if (message == null && message == '') {
        return;
    }

    advanceChatConnection.send("SendPublicMessage", roomName, message);
}