
// Create Connection
var connection = new signalR.HubConnectionBuilder()
	.configureLogging(signalR.LogLevel.Information)
	.withUrl("/hubs/voting")
	.build();

// Invoke HUB methods AKA Send Notification to HUB

// Connect to methods that HUB invokes AKA Receive Notification from HUB
connection.on("UpdateVotingStatus", (voterACount, voterBCount, voterCCount) => {
	$('#voterACounter').text(voterACount);
	$('#voterBCounter').text(voterBCount);
	$('#voterCCounter').text(voterCCount);

});

// Start Connection
connection.start().then(OnSuccessConnection, OnFailedConnection);

function OnSuccessConnection() {
	console.log("Successfully connected to Voting HUB");
	WindowLoaded();
}

function OnFailedConnection() {
	console.log("Failed to connect to Voting HUB");
}