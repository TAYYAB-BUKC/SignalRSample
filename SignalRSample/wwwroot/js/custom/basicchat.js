var basicChatConnection = new signalR.HubConnectionBuilder().withUrl("/hubs/basicchat").build();

$('#sendMessage').prop("disabled", true);

basicChatConnection.on("NewMessageReceived", function (sender, message) {
	$('#messagesList').append(`<li>${sender} - ${message}</li>`);
});

$('#sendMessage').on("click", function (event) {
	var sender = $('#senderEmail').val();
	var message = $('#chatMessage').val();
	var receiver = $('#receiverEmail').val();

	if (receiver.length > 0) {
		basicChatConnection.send("SendPrivateMessage", sender, receiver, message).catch(function (error) {
			console.log(error);
		});
	}
	else {
		basicChatConnection.send("SendMessage", sender, message).catch(function (error) {
			console.log(error);
		});
	}
	event.preventDefault();
});

basicChatConnection.start().then(function () {
	console.log("Connected to Basic Chat Hub");
	$('#sendMessage').prop("disabled", false);
}, function () {
	console.log("Failed to connect to Basic Chat Hub");
});