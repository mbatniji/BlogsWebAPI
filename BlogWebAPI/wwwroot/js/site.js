// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.


$(function() {

    var connection = new signalR.HubConnectionBuilder().withUrl("/chatHub", { transport: signalR.HttpTransportType.LongPolling }).build();
    connection.start().catch(function(err) {
        // return console.error(err.toString());
    });
   
    connection.on("notifyUser_broadcast", function (title, message, type) {
 
        if (type == "Add") {
            toastr.success(message, title);
        }
        if (type == "Update") {
            toastr.warning(message, title);
        }
        if (type == "Delete") {
            toastr.error(message, title);
        }      
        if (controllerName.toLowerCase() == "home" && actionName.toLowerCase() == "blogs") {
            $('#BlogsItemsTable').DataTable().ajax.reload();
        }
        if (controllerName.toLowerCase() == "home" && actionName.toLowerCase() == "blogdetails") {
            window.location = window.location;
        }
    });
 
});

 