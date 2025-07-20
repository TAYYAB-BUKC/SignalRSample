

var notificationConnection = new signalR.HubConnectionBuilder().withUrl("/hubs/notification").build();

notificationConnection.start().then(OnNotificationSuccessConnection, OnNotificationFailedConnection);

notificationConnection.on("UpdateNotificationListAndCount", (notificationList) => {
	$('#notificationCounter').text(notificationList.length);
	var messages = '';
	for (var i = 0; i < notificationList.length; i++) {
		messages += `<li>${notificationList[i]}</li>`;
	}
	$('#messageList').html(messages);
});

$('#sendButton').on("click", function () {
	notificationConnection.send("AddNewNotification", $('#notificationInput').val());
});;

function OnNotificationSuccessConnection() {
	console.log("Connected to Notification Hub");
	notificationConnection.send("SentCurrentNotificationListToClients");
}

function OnNotificationFailedConnection() {
	console.log("Failed to connect to Notification Hub");
}