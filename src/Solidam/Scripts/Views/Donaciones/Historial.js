var tableHistorialDonaciones = $("#tableHistorialDonaciones");

var dataTable = tableHistorialDonaciones.DataTable({
    "responsive": true,
    "processing": true,
    "serverSide": true,
    "filter": true,
    "orderMulti": false,
    "pageLength": 10,
    "ajax": {
        "url": "/api/donacionesapi/" + window.idUsuario,
        "type": "POST",
        "datatype": "json"
    },
    "columnDefs": [
        {
            "targets": [1],
            "searchable": true,
            "orderable": true
        },
        {
            "targets": [0, 2, 3, 4, 5],
            "searchable": false,
            "orderable": true
        }
    ],
    "columns": [
        {
            "data": "FechaDonacion",
            "name": "FechaDonacion",
            "autoWidth": true,
            "render": function (data, type, full, meta) {
                return isStringNullOrEmpty(full.FechaDonacion) ? "Sin información" : moment(full.FechaDonacion, 'DD/MM/YYYY').format('DD/MM/YYYY');
            }
        },
        { "data": "Nombre", "name": "Nombre", "autoWidth": true },
        { "data": "TipoDonacion", "name": "TipoDonacion", "autoWidth": true },
        { "data": "Estado", "name": "Estado", "autoWidth": true },
        { "data": "TotalRecaudado", "name": "TotalRecaudado", "autoWidth": true },
        { "data": "MiDonacion", "name": "MiDonacion", "autoWidth": true },
        {
            "data": "Propuesta", "name": "Propuesta", "autoWidth": true,
            "render": function (data, type, full, meta) {
                var url = window.location.protocol + '//' + window.location.host + full.Propuesta;
                var anchorPropuesta = $("<a href='" + url + "' class='btn btn-success'>").text("Detalle");
                return anchorPropuesta.prop("outerHTML");
            }
        }
    ],
    "language": {
        "url": "//cdn.datatables.net/plug-ins/9dcbecd42ad/i18n/Spanish.json"
    }
});