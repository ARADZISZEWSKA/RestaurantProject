
$(document).ready(function () {
    loadDataTable();
});

function loadDataTable() {
    dataTable = $('#tblData').DataTable({
        // Pobieranie danych z adresu URL
        "ajax": { url: '/Admin/MenuItems/getall' },
        // Definicja kolumn
        "columns": [
            { data: 'name', "width":"20%" },
            { data: 'description', "width": "40%" },
            { data: 'price', "width": "5%" },
            { data: 'category.name', "width": "15%" },
            {
                data: 'id',
                render: function (data) {
                    return `<div class="w-75 btn-group" role="group">
                    <a href="/admin/menuitems/upsert?id=${data}" class="btn btn-primary mx-2"> <i class="bi bi-pencil-square"></i> Edytuj </a>
                    <a href="/admin/menuitems/delete/${data}" class="btn btn-danger mx-2"> <i class="bi bi-trash-fill"></i> Usuń </a>
                    </div>`
                },
                "width": "20%"
            }

        ],
        //ustawienia językowe
        language: {
            url: '//cdn.datatables.net/plug-ins/1.13.7/i18n/pl.json',
        },
    });
};

