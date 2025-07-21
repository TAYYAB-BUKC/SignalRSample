
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