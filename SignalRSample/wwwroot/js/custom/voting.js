
// Create Connection
var votingConnection = new signalR.HubConnectionBuilder()
	.configureLogging(signalR.LogLevel.Information)
	.withUrl("/hubs/voting")
	.build();

// Invoke HUB methods AKA Send Notification to HUB

// Connect to methods that HUB invokes AKA Receive Notification from HUB
votingConnection.on("UpdateVotingStatus", (voterACount, voterBCount, voterCCount) => {
	$('#voterACounter').text(voterACount);
	$('#voterBCounter').text(voterBCount);
	$('#voterCCounter').text(voterCCount);

});

// Start Connection
votingConnection.start().then(OnVotingHubSuccessConnection, OnVotingHubFailedConnection);

function OnVotingHubSuccessConnection() {
	console.log("Successfully connected to Voting HUB");
	WindowLoaded();
}

function OnVotingHubFailedConnection() {
	console.log("Failed to connect to Voting HUB");
}