let table = '';


window.onload = function () {
    var url = "ObtenerAlmacenDT";
    ConsultaServidor(url);

    listarObra();
};



function listarObra(){
    $.ajax({
        url: "../Obra/ObtenerObra",
        type: "GET",
        contentType: "application/json",
        dataType: "json",
        data: {
            'estado': 1
        },
        cache: false,
        contentType: false,
        success: function (datos) {
            $("#IdObra").html('');
            let options = `<option value="0">Seleccione</option>`;
            if (datos.length > 0) {

                for (var i = 0; i < datos.length; i++) {
                    options += `<option value="` + datos[i].IdObra + `">` + datos[i].Descripcion + `</option>`;
                }
                $("#IdObra").html(options);
            }
        }
    });
}

function ConsultaServidor(url) {
    table = $('#table_id').dataTable({
        language: lenguaje_data,
        responsive: true,
        ajax: {
            url: url,
            type: 'POST',
            data: {

                pagination: {
                    perpage: 50,
                },
            },
        },

        columnDefs: [
            // {"className": "text-center", "targets": "_all"},
            {
                targets: -1,
                orderable: false,
                render: function (data, type, full, meta) {
   
                    return `<button class="btn btn-primary fa fa-pencil btn-xs" onclick="ObtenerDatosxID(` + full.IdAlmacen + `)"></button>
                            <button class="btn btn-danger btn-xs  fa fa-trash" onclick="eliminar(` + full.IdAlmacen + `)"></button>`
                },
            },
            {
                targets: 0,
                orderable: false,
                render: function (data, type, full, meta) {
                    return meta.row+1
                },
            },
            {
                targets: 1,
                orderable: false,
                render: function (data, type, full, meta) {
                    return full.Codigo.toUpperCase()
                },
            },
            {
                targets: 2,
                orderable: false,
                render: function (data, type, full, meta) {
                    return full.Descripcion.toUpperCase()
                },
            }

        ],
        "bDestroy": true
    }).DataTable();
}




function ModalNuevo() {
    $("#lblTituloModal").html("Nuevo Almacen");
    AbrirModal("modal-form");
}




function GuardarAlmacen() {
    let varIdAlmacen = $("#txtId").val();
    let IdObra=$("#IdObra").val();
    let varCodigo = $("#txtCodigo").val();
    let varDescripcion = $("#txtDescripcion").val();
    let varEstado = false;

    if ($('#chkActivo')[0].checked) {
        varEstado = true;
    }

    $.post('UpdateInsertAlmacen', {
        'IdAlmacen': varIdAlmacen,
        'IdObra': IdObra,
        'Codigo': varCodigo,
        'Descripcion': varDescripcion,
        'Estado': varEstado
    }, function (data, status) {

        if (data == 1) {
            swal("Exito!", "Proceso Realizado Correctamente", "success")
            table.ajax.reload();
         
            limpiarDatos();

        } else {
            swal("Error!", "Ocurrio un Error")
            limpiarDatos();
        }

    });
}

function ObtenerDatosxID(varIdAlmacen) {
    $("#lblTituloModal").html("Editar Almacen");
    AbrirModal("modal-form");

    //console.log(varIdUsuario);

    $.post('ObtenerDatosxID', {
        'IdAlmacen': varIdAlmacen,
    }, function (data, status) {

        if (data == "Error") {
            swal("Error!", "Ocurrio un error")
            limpiarDatos();
        } else {
            let almacen = JSON.parse(data);
            //console.log(usuarios);
            $("#txtId").val(almacen[0].IdAlmacen);
            $("#IdObra").val(almacen[0].IdObra);
            $("#txtCodigo").val(almacen[0].Codigo);
            $("#txtDescripcion").val(almacen[0].Descripcion);
            if (almacen[0].Estado) {
                $("#chkActivo").prop('checked', true);
            }

        }

    });

}

function eliminar(varIdAlmacen) {


    alertify.confirm('Confirmar', '¿Desea eliminar este Almacen?', function () {
        $.post("EliminarAlmacen", { 'IdAlmacen': varIdAlmacen }, function (data) {

            if (data == 0) {
                swal("Error!", "Ocurrio un Error")
                limpiarDatos();
            } else {
                swal("Exito!", "Almacen Eliminada", "success")
                table.ajax.reload();
               
                limpiarDatos();
            }

        });

    }, function () { });

}


function limpiarDatos() {
    $("#txtId").val("");
    $("#txtCodigo").val("");
    $("#txtDescripcion").val("");
    $("#chkActivo").prop('checked', false);
    $("#IdObra").val(0);
}



