var dataTable;
var orderConnection = new signalR.HubConnectionBuilder().withUrl("/hubs/order").build();

$(document).ready(function () {
    loadDataTable();
});

function loadDataTable() {
    dataTable = $('#tblData').DataTable({
        "ajax": {
            "url": "/Order/GetAllOrder"
        },
        "columns": [
            { "data": "id", "width": "5%" },
            { "data": "name", "width": "15%" },
            { "data": "itemName", "width": "15%" },
            { "data": "count", "width": "15%" },
            {
                "data": "id",
                "render": function (data) {
                    return `
                        <div class="w-75 btn-group" role="group">
                        <a href=""
                        class="btn btn-primary mx-2"> <i class="bi bi-pencil-square"></i> </a>
                      
					</div>
                        `
                },
                "width": "5%"
            }
        ]
    });
}

orderConnection.on("NewOrderReceived", function () {
    dataTable.ajax.reload();
    toastr.success("1 new order received");
});

orderConnection.start().then(function () {
    console.log("Connected to Order Hub.");
}, function () {
    console.log("Failed to connect to Order Hub.");
});