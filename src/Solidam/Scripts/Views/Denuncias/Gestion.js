var tableDenuncias = $("#tableDenuncias");

tableDenuncias.dataTable({
    "processing": true,
    "serverSide": true,
    "filter": true,
    "orderMulti": false,
    "pageLength": 10,
    "ajax": {
        "url": "/Denuncias/GetDenuncias",
        "type": "POST",
        "datatype": "json"
    },
    "columnDefs": [
            {
                "targets": [0],
                "visible": false,
                "searchable": false,
                "orderable": false
            },
            {
                "targets": [1],
                "searchable": false,
                "orderable": true
            },
            {
                "targets": [2],
                "searchable": true,
                "orderable": true
            },
            {
                "targets": [4],
                "searchable": true,
                "orderable": false
            }
    ],
    "columns": [
        { "data": "IdDenuncia", "name": "IdDenuncia", "autoWidth": true },
        {
            "data": "FechaCreacion",
            "name": "FechaCreacion",
            "autoWidth": true,
            "render": function(data, type, full, meta) {
                return moment(full.fechaCreacion).format('DD/MM/YYYY');
            }
        },
        { "data": "Motivo", "name": "Motivo", "autoWidth": true },
        {
            "data": "Propuesta",
            "name": "Propuesta",
            "autoWidth": true,
            "render": function (data, type, full, meta) {
                var url = window.location.protocol + '//' + window.location.host + '/' + full.Propuesta;
                var anchorPropuesta = $("<a href='" + url + "'>").text(url);
                return anchorPropuesta.prop("outerHTML");
            }
        },
        {
            "data": "Comentarios",
            "name": "Comentarios",
            "autoWidth": true
        },
        {
            "data": "Acciones",
            "name": "Acciones",
            "render": function(data, type, full, meta) {
                var divAcciones = $("<div class='d-flex justify-content-around align-items-center'>");
                var btnDesestimar = $("<button type='button' class='btn btn-success'>").text("Desestimar");
                var btnAceptar = $("<button type='button' class='btn btn-danger'>").text("Aceptar");
                divAcciones.append(btnDesestimar);
                divAcciones.append(btnAceptar);
                return divAcciones.prop('outerHTML');
            }
        }
    ],
    "language": {
        "url": "//cdn.datatables.net/plug-ins/9dcbecd42ad/i18n/Spanish.json"
    }
}); 