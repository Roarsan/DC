var dataTable;

$(document).ready(function () {
    $('#myTable').DataTable();
});

function loadDataTable() {
    dataTable = $('#tblData').DataTable({
        "ajax": {
            "url": "/Admin/Product/GetAll"
        },
        "columns": [
            { "data": "name", "width": "15%" },
            { "data": "colour","width": "15%" },
            { "data": "price", "width": "15%" },
            { "data": "size","width": "15%" },
            { "data": "type","width": "15%" }, ,
            { "data": "description","width": "15%" },
            { "data": "material", "width": "15%"},
        ]
    });
}