let lbl_houseJoined = document.getElementById("lbl_houseJoined");

let btn_gryffindor = document.getElementById("btn_gryffindor");
let btn_slytherin = document.getElementById("btn_slytherin");
let btn_hufflepuff = document.getElementById("btn_hufflepuff");
let btn_ravenclaw = document.getElementById("btn_ravenclaw");

let btn_un_gryffindor = document.getElementById("btn_un_gryffindor");
let btn_un_slytherin = document.getElementById("btn_un_slytherin");
let btn_un_hufflepuff = document.getElementById("btn_un_hufflepuff");
let btn_un_ravenclaw = document.getElementById("btn_un_ravenclaw");

let trigger_gryffindor = document.getElementById("trigger_gryffindor");
let trigger_slytherin = document.getElementById("trigger_slytherin");
let trigger_hufflepuff = document.getElementById("trigger_hufflepuff");
let trigger_ravenclaw = document.getElementById("trigger_ravenclaw");

var houseConnection = new signalR.HubConnectionBuilder().withUrl("/hubs/house").build();

houseConnection.start().then(OnHouseSuccessConnection, OnHouseFailedConnection);

function OnHouseSuccessConnection()
{
	console.log("Connected to House Hub");
}

function OnHouseFailedConnection()
{
	console.log("Failed to connection to House Hub");
}

// Subscribe House events
btn_gryffindor.addEventListener("click", function (event) {
	houseConnection.send("JoinHouse", "Gryffindor");
	event.preventDefault();
});

btn_slytherin.addEventListener("click", function (event) {
	houseConnection.send("JoinHouse", "Slytherin");
	event.preventDefault();
});

btn_hufflepuff.addEventListener("click", function (event) {
	houseConnection.send("JoinHouse", "Hufflepuff");
	event.preventDefault();
});

btn_ravenclaw.addEventListener("click", function (event) {
	houseConnection.send("JoinHouse", "Ravenclaw");
	event.preventDefault();
});

// Unsubscribe House events
btn_un_gryffindor.addEventListener("click", function (event) {
	houseConnection.send("LeaveHouse", "Gryffindor");
	event.preventDefault();
});

btn_un_slytherin.addEventListener("click", function (event) {
	houseConnection.send("LeaveHouse", "Slytherin");
	event.preventDefault();
});

btn_un_hufflepuff.addEventListener("click", function (event) {
	houseConnection.send("LeaveHouse", "Hufflepuff");
	event.preventDefault();
});

btn_un_ravenclaw.addEventListener("click", function (event) {
	houseConnection.send("LeaveHouse", "Ravenclaw");
	event.preventDefault();
});

// Toggle buttons based on Subcription/UnSubcription

houseConnection.on("SubscriptionStatus", (joinedGroups, houseName, hasSubscribed) => {
	lbl_houseJoined.innerText = joinedGroups;

	if (hasSubscribed) {
		switch (houseName) {
			case "Gryffindor":
				btn_gryffindor.style.display = "none";
				btn_un_gryffindor.style.display = "";
				break;
			case "Slytherin":
				btn_slytherin.style.display = "none";
				btn_un_slytherin.style.display = "";
				break;
			case "Hufflepuff":
				btn_hufflepuff.style.display = "none";
				btn_un_hufflepuff.style.display = "";
				break;
			case "Ravenclaw":
				btn_ravenclaw.style.display = "none";
				btn_un_ravenclaw.style.display = "";
				break;
			default:
				break;
		}
		toastr.success(`You have subscribed to ${houseName} successfully`);
	}
	else {
		switch (houseName) {
			case "Gryffindor":
				btn_gryffindor.style.display = "";
				btn_un_gryffindor.style.display = "none";
				break;
			case "Slytherin":
				btn_slytherin.style.display = "";
				btn_un_slytherin.style.display = "none";
				break;
			case "Hufflepuff":
				btn_hufflepuff.style.display = "";
				btn_un_hufflepuff.style.display = "none";
				break;
			case "Ravenclaw":
				btn_ravenclaw.style.display = "";
				btn_un_ravenclaw.style.display = "none";
				break;
			default:
				break;
		}
		toastr.success(`You have unsubscribed to ${houseName} successfully`);
	}
});

houseConnection.on("NewSubscriptionMessage", (message) => {
	toastr.success(message);
});

houseConnection.on("UnsubscriptionMessage", (message) => {
	toastr.info(message);
});

houseConnection.on("ShowTriggeredHouseNotification", (houseName) => {
	toastr.info(`New notification has been launched for ${houseName}.`);
});