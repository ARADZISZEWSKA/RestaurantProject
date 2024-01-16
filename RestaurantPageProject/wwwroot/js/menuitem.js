
$(document).ready(function () {
    loadDataTable();
});

function loadDataTable() {
    dataTable = $('#tblData').DataTable({
        // Pobieranie danych z adresu URL
        "ajax": { url: '/Admin/MenuItems/getall' },
        // Definicja kolumn
        "columns": [
            { data: 'name', "width":"25%" },
            { data: 'description', "width": "40%" },
            { data: 'price', "width": "5%" },
            { data: 'category.name', "width": "15%" },
            {
                data: 'id',
                render: function (data) {
                    return `<div class="w-75 btn-group" role="group">
                    <a href="" class="btn btn-primary mx-2"> <i class="bi bi-pencil-square"></i> Edytuj </a>
                    </div>`
                },
                "width": "15%"
            }

        ],
        //ustawienia językowe
        language: {
            url: '//cdn.datatables.net/plug-ins/1.13.7/i18n/pl.json',
        },
    });
};

