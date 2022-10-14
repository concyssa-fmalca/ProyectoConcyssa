let table = '';


window.onload = function () {
    var url = "ObtenerSeries";
    //ConsultaServidor(url);
    ObtenerSeriesDT();
};


function ConsultaServidor(url) {

    $.post(url, function (data, status) {

        var errorEmpresa = validarEmpresa(data);
        if (errorEmpresa) {
            return;
        }

        //console.log(data);
        if (data == "error") {
            table = $("#table_id").DataTable(lenguaje);
            return;
        }

        let tr = '';

        let series = JSON.parse(data);
        let total_series = series.length;

        for (var i = 0; i < series.length; i++) {

            tr += '<tr>' +
                '<td>' + (i + 1) + '</td>' +
                '<td>' + series[i].Serie.toUpperCase() + '</td>' +
                '<td>' + series[i].NumeroInicial + '</td>' +
                '<td>' + series[i].NumeroFinal + '</td>' +
                '<td><button class="btn btn-primary fa fa-pencil btn-xs" onclick="ObtenerDatosxID(' + series[i].IdSerie + ')"></button>' +
                '<button class="btn btn-danger btn-xs  fa fa-trash" onclick="eliminar(' + series[i].IdSerie + ')"></button></td >' +
                '</tr>';
        }

        $("#tbody_Series").html(tr);
        $("#spnTotalRegistros").html(total_series);

        table = $("#table_id").DataTable(lenguaje);

    });

}




function ModalNuevo() {
    $("#lblTituloModal").html("Nueva Serie");
    AbrirModal("modal-form");
    $('#chkActivo').prop('checked', true);
}




function GuardarSerie() {

    let varIdSerie = $("#txtId").val();
    let varSerie = $("#txtSerie").val();
    let Documento = $("#Documento").val();
    let varNumeroInicial = $("#txtNumeroInicial").val();
    let varNumeroFinal = $("#txtNumeroFinal").val();
    let varEstado = false;

    if ($('#chkActivo')[0].checked) {
        varEstado = true;
    }

    $.post('UpdateInsertSerie', {
        'IdSerie': varIdSerie,
        'Serie': varSerie,
        'NumeroInicial': varNumeroInicial,
        'NumeroFinal': varNumeroFinal,
        'Estado': varEstado,
        'Documento': Documento
    }, function (data, status) {

        var errorEmpresa = validarEmpresaUpdateInsert(data);
        if (errorEmpresa) {
            return;
        }


        if (data == 1) {
            swal("Exito!", "Proceso Realizado Correctamente", "success")
    
            limpiarDatos();
        } else {
            swal("Error!", "Ocurrio un Error")
            limpiarDatos();
        }
        ObtenerSeriesDT()
        closePopup();
    });
}

function ObtenerDatosxID(varIdSerie) {
    $("#lblTituloModal").html("Editar Serie");
    AbrirModal("modal-form");

    //console.log(varIdUsuario);

    $.post('ObtenerDatosxID', {
        'IdSerie': varIdSerie,
    }, function (data, status) {
        var errorEmpresa = validarEmpresa(data);
        if (errorEmpresa) {
            return;
        }


        if (data == "Error") {
            swal("Error!", "Ocurrio un error")
            limpiarDatos();
        } else {
            let serie = JSON.parse(data);
            //console.log(usuarios);
            $("#txtId").val(serie[0].IdSerie);
            $("#txtSerie").val(serie[0].Serie);
            $("#Documento").val(serie[0].Documento)
            $("#txtNumeroInicial").val(serie[0].NumeroInicial);
            $("#txtNumeroFinal").val(serie[0].NumeroFinal);
            if (serie[0].Estado) {
                $("#chkActivo").prop('checked', true);
            }

        }

    });

}

function eliminar(varIdSerie) {


    alertify.confirm('Confirmar', '¿Desea eliminar esta serie?', function () {
        $.post("EliminarSerie", { 'IdSerie': varIdSerie }, function (data) {

            var errorEmpresa = validarEmpresaUpdateInsert(data);
            if (errorEmpresa) {
                return;
            }


            if (data == 0) {
                swal("Error!", "Ocurrio un Error")
                limpiarDatos();
            } else {
                swal("Exito!", "Serie Eliminada", "success")
              
            }
            ObtenerSeriesDT()
            closePopup();
        });

    }, function () { });

}


function limpiarDatos() {
    $("#txtId").val("");
    $("#txtSerie").val("");
    $("#txtNumeroInicial").val("");
    $("#txtNumeroFinal").val("");
    $("#chkActivo").prop('checked', false);
}





function validarEmpresa(rpta) {
    if (rpta == "SinBD") {   //Sin Session
        window.location.href = "/";
        return true;
    }
    return false;
}

function validarEmpresaUpdateInsert(rpta) {
    if (rpta == -999) {   //Sin Session
        window.location.href = "/";
        return true;
    }
    return false;
}


function ObtenerSeriesDT() {
    table = $('#table_id').dataTable({
        language: lenguaje_data,
        responsive: true,
        ajax: {
            url: 'ObtenerSeriesDT',
            type: 'POST',
            data: {
                pagination: {
                    perpage: 50,
                },
            },
        },

        columnDefs: [
             {"className": "text-center", "targets": "_all"},
            {
                targets: -1,
                orderable: false,
                render: function (data, type, full, meta) {
                    let button = `<button class="btn btn-info  fa fa-pencil btn-xs" onclick="ObtenerDatosxID(` + full.IdSerie + `)"></button><button class="btn btn-danger btn-xs fa fa-trash" onclick="eliminar(` + full.IdSerie + `)"></button>`;
                    //`<button class="btn btn-danger btn-xs fa fa-trash" onclick="eliminar(` + full[i].IdSerie + `)"></button>`;
                    return button;
                        
                },
            },
            {
                targets: 0,
                orderable: false,
                render: function (data, type, full, meta) {
                    return meta.row + 1
                },
            },
            {
                targets: 1,
                orderable: false,
                render: function (data, type, full, meta) {
                    let texto = "";
                    switch (full.Documento) {
                        case 1:
                            texto="ENTRADA MERCANCIA"
                            // code block
                            break;
                        case 2:
                            texto = "SALIDA MERCANCIA"
                            // code block
                            break;
                        case 3:
                            texto = "TRANFERENCIA MERCANCIA"
                            // code block
                            break;
                        case 4:
                            texto = "PEDIDO"
                            // code block
                            break;
                        case 5:
                            texto = "ENTREGA MERCANCIA"
                            // code block
                            break;
                        case 6:
                            texto = "FACTURA PROVEEDOR"
                            // code block
                            break;
                        case 7:
                            texto = "-"
                            // code block
                            break;
                        default:
                            texto="SIN RELACION"
                        // code block
                    }
                    return texto
                },
            },
            {
                targets: 2,
                orderable: false,
                render: function (data, type, full, meta) {
                    return full.Serie
                },
            },
            {
                targets: 3,
                orderable: false,
                render: function (data, type, full, meta) {
                    return full.NumeroInicial
                },
            },
            {
                targets: 4,
                orderable: false,
                render: function (data, type, full, meta) {
                    return full.NumeroFinal
                },
            }

        ],
        "bDestroy": true
    }).DataTable();
}