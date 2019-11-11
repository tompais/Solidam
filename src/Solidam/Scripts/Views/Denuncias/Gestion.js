var tableDenuncias = $("#tableDenuncias");

var dataTable = tableDenuncias.DataTable({
    "responsive": true,
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
                var url = window.location.protocol + '//' + window.location.host + full.Propuesta;
                var anchorPropuesta = $("<a href='" + url + "' class='btn btn-success' >").text("Detalle");
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
            "autoWidth": true,
            "render": function(data, type, full, meta) {
                var divAcciones = $("<div class='d-flex justify-content-around align-items-center'>");
                var btnDesestimar = $("<button type='button' class='btn btn-success desestimar-btn' id-denuncia='" + full.IdDenuncia + "'>").text("Desestimar");
                var btnAceptar = $("<button type='button' class='btn btn-danger aceptar-btn' id-denuncia='" + full.IdDenuncia + "'>").text("Aceptar").click(function() {
                    modificarEstadoDenuncia(full.IdDenuncia, 2);
                });
                divAcciones.append(btnDesestimar);
                divAcciones.append(btnAceptar);
                return divAcciones.prop('outerHTML');
            }
        }
    ],
    "language": {
        "url": "//cdn.datatables.net/plug-ins/9dcbecd42ad/i18n/Spanish.json"
    },
    "drawCallback": function (settings) {
        $(".desestimar-btn").click(function() {
            modificarEstadoDenuncia(parseInt($(this).attr("id-denuncia")), 1);
        });

        $(".aceptar-btn").click(function () {
            modificarEstadoDenuncia(parseInt($(this).attr("id-denuncia")), 2);
        });
    }
}); 

function modificarEstadoDenuncia(idDenuncia, estado) {
    $.ajax({
        contentType: 'application/json; charset=UTF-8',
        cache: false,
        data: JSON.stringify({
            idDenuncia: idDenuncia,
            estado: estado
        }),
        method: "POST",
        url: "/Denuncias/ModificarEstadoDenuncia",
        success: function (data, extStatus, jqXHR) {
            dataTable.ajax.reload();
        }
    });
}