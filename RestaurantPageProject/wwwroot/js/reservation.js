$(document).ready(function () {
    loadDataTable();
});

function loadDataTable() {
    dataTable = $('#tblReservations').DataTable({
        // Pobieranie danych z adresu URL
        "ajax": { url: '/Admin/ReservationAdmin/GetAll' },
        // Definicja kolumn
        "columns": [
            { data: 'name', "width": "10%" },
            { data: 'lastName', "width": "15%" },
            { data: 'numberOfPeople', "width": "10%" },
            { data: 'phoneNumber', "width": "10%" },
            { data: 'date', "width": "15%" },
            { data: 'selectedHour', "width": "10%" },
            { data: 'note', "width": "20%" },
            {
                data: 'id',
                render: function (data) {
                    return `<div class="w-75 btn-group" role="group">
                        <a href="/admin/reservationadmin/edit/${data}" class="btn btn-primary mx-2"> <i class="bi bi-pencil-square"></i> Edytuj </a>           
                        <a href="/admin/reservationadmin/delete/${data}" class="btn btn-danger mx-2"> <i class="bi bi-trash-fill"></i> Usuń </a>
                        </div>`
                },
                "width": "15%"
            }
        ],
        // Ustawienia językowe
        language: {
            url: '//cdn.datatables.net/plug-ins/1.13.7/i18n/pl.json',
        },
    });
};
