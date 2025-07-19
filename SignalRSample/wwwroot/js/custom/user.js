// Create Connection
var connection = new signalR.HubConnectionBuilder().withUrl("/hubs/user").build();

// Invoke HUB methods AKA Send Notification to HUB
function WindowLoaded() {
	// send will not be able to get the response from the server after calling the method
	//connection.send("AddView").then((value) => {
	//	console.log("Using Send");
	//	console.log(value);
	//});

	// invoke will be able to get the response from the server after calling the method
	connection.invoke("AddView").then((value) => {
		console.log("Using Invoke");
		console.log(value);
	});
}

// Connect to methods that HUB invokes AKA Receive Notification from HUB
connection.on("UpdateTotalViews", (totalViews) => {
	$('#totalViews').text(totalViews);
});

connection.on("UpdateTotalUsers", (totalUsers) => {
	$('#totalUsers').text(totalUsers);
});

// Start Connection
connection.start().then(OnSuccessConnection, OnFailedConnection);

function OnSuccessConnection() {
	console.log("Successfully connected to HUB");
	WindowLoaded();
}

function OnFailedConnection() {
	console.log("Failed to connect to HUB");
}