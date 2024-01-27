let table = '';
let contador = 0;
let table_series;

window.onload = function () {
    var url = "ObtenerSeries";
    //ConsultaServidor(url);
    ObtenerSeriesDT();
    //ObtenerSeriesElectronicas();
};




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
            Swal.fire("Exito!", "Proceso Realizado Correctamente", "success")

            limpiarDatos();
        } else {
            Swal.fire("Error!", "Ocurrio un Error")
            limpiarDatos();
        }
        ObtenerSeriesDT()
        closePopup();
    });
}

//function ObtenerDatosxID(varIdSerie) {
//    $("#lblTituloModal").html("Editar Serie");
//    AbrirModal("modal-form");

//    //console.log(varIdUsuario);

//    $.post('ObtenerDatosxID', {
//        'IdSerie': varIdSerie,
//    }, function (data, status) {
//        var errorEmpresa = validarEmpresa(data);
//        if (errorEmpresa) {
//            return;
//        }


//        if (data == "Error") {
//            Swal.fire("Error!", "Ocurrio un error")
//            limpiarDatos();
//        } else {
//            let serie = JSON.parse(data);
//            //console.log(usuarios);
//            $("#txtId").val(serie[0].IdSerie);
//            $("#txtSerie").val(serie[0].Serie);
//            $("#Documento").val(serie[0].Documento)
//            $("#txtNumeroInicial").val(serie[0].NumeroInicial);
//            $("#txtNumeroFinal").val(serie[0].NumeroFinal);
//            if (serie[0].Estado) {
//                $("#chkActivo").prop('checked', true);
//            }

//        }

//    });

//}


function ObtenerDatosxIDNew(varIdDocumento) {
    $("#lblTituloModal").html("SERIES");
    $("#IdDocumentoModal").val(varIdDocumento);
    AbrirModal("modal-form");

    //console.log(varIdUsuario);

    table_series = $('#table_serienew').dataTable({
        language: lenguaje_data,
        responsive: true,
        ajax: {
            url: 'ObtenerSeriesxIdDocumento',
            type: 'POST',
            data: {
                'IdDocumento': varIdDocumento,
                pagination: {

                    perpage: 50,
                },
            },
        },

        columnDefs: [
            { "className": "text-center", "targets": "_all" },
            {
                data: null,
                targets: -1,
                orderable: false,
                render: function (data, type, full, meta) {
                    let button = `<button class="btn btn-primary  fa fa-floppy-o btn-xs" onclick="guardar(1,` + full.IdSerie + `)"></button><button class="btn btn-danger btn-xs fa fa-trash" onclick="eliminar(` + full.IdSerie + `)"></button>`;
                    `<button class="btn btn-danger btn-xs fa fa-trash" onclick="eliminar(` + full.IdSerie + `)"></button>`;
                    return button;

                },
            },
            {
                data: null,
                targets: 0,
                orderable: false,
                render: function (data, type, full, meta) {
                    return meta.row + 1
                },
            },
            {
                data: null,
                targets: 1,
                orderable: false,
                render: function (data, type, full, meta) {
                    return full.Serie
                },
            },
            {
                data: null,
                targets: 2,
                orderable: false,
                render: function (data, type, full, meta) {
                    return full.NumeroInicial
                },
            },
            {
                data: null,
                targets: 3,
                orderable: false,
                render: function (data, type, full, meta) {
                    return full.NumeroFinal
                },
            },
            {
                data: null,
                targets: 4,
                orderable: false,
                render: function (data, type, full, meta) {
                    if (full.IsArticulo) {
                        return `<input class="form-check-input " id="IsArticulo` + full.IdSerie + `" type="checkbox" checked>`
                    } else {
                        return `<input class="form-check-input " id="IsArticulo` + full.IdSerie + `" type="checkbox">`
                    }
                },
            },
            {
                data: null,
                targets: 5,
                orderable: false,
                render: function (data, type, full, meta) {
                    if (full.IsServicio) {
                        return `<input class="form-check-input " id="IsServicio` + full.IdSerie + `" type="checkbox" checked>`
                    } else {
                        return `<input class="form-check-input " id="IsServicio` + full.IdSerie + `" type="checkbox">`
                    }

                },
            },
            {
                data: null,
                targets: 6,
                orderable: false,
                render: function (data, type, full, meta) {
                    return full.NombIndicadorPeriodo
                },
            },
            {
                data: null,
                targets: 7,
                orderable: false,
                render: function (data, type, full, meta) {
                    if (full.Estado) {
                        return `<input class="form-check-input" id="Estado` + full.IdSerie + `"   type="checkbox" checked>`
                    } else {
                        return `<input class="form-check-input" id="Estado` + full.IdSerie + `"  type="checkbox">`
                    }
                },
            }

        ],
        "bDestroy": true
    }).DataTable();

}


function eliminar(varIdSerie) {



    Swal.fire({
        title: 'Desea Eliminar este Registro?',
        text: 'No podras revertir esta opcion',
        icon: 'warning',
        showCancelButton: true,
        confirmButtonColor: '#3085d6',
        cancelButtonColor: '#d33',
        cancelButtonText: 'No, cancelar',
        confirmButtonText: 'Si, Eliminar'
    }).then((result) => {
        if (result.isConfirmed) {
            $.post("EliminarSerie", { 'IdSerie': varIdSerie }, function (data) {

                var errorEmpresa = validarEmpresaUpdateInsert(data);
                if (errorEmpresa) {
                    return;
                }


                if (data == 0) {
                    Swal.fire("Error!", "Ocurrio un Error")
                    limpiarDatos();
                } else {
                    Swal.fire("Exito!", "Serie Eliminada", "success")

                }
                ObtenerSeriesDT()
                closePopup();
            });
            Swal.fire(
                'Eliminado!',
                'El Registro Fue Eliminado',
                'success'
            )
        }
    })

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
            url: 'ObtenerSeriesDTNew',
            type: 'POST',
            data: {
                pagination: {
                    perpage: 50,
                },
            },
        },

        columnDefs: [
            { "className": "text-center", "targets": "_all" },
            {
                data: null,
                targets: -1,
                orderable: false,
                render: function (data, type, full, meta) {
                    let button = `<button class="btn btn-primary  fa fa-eye btn-xs" onclick="ObtenerDatosxIDNew(` + full.IdDocumento + `)"></button>`;
                    //`<button class="btn btn-danger btn-xs fa fa-trash" onclick="eliminar(` + full[i].IdSerie + `)"></button>`;
                    return button;

                },
            },
            {
                data: null,
                targets: 0,
                orderable: false,
                render: function (data, type, full, meta) {
                    return meta.row + 1
                },
            },
            {
                data: null,
                targets: 1,
                orderable: false,
                render: function (data, type, full, meta) {
                    return full.NombDocumento
                },
            },
            //{
            //    data: null,
            //    targets: 2,
            //    orderable: false,
            //    render: function (data, type, full, meta) {
            //        return full.SerieDefecto
            //    },
            //},
            {
                data: null,
                targets: 2,
                orderable: false,
                render: function (data, type, full, meta) {
                    return full.NumeroInicial
                },
            },
            {
                data: null,
                targets: 3,
                orderable: false,
                render: function (data, type, full, meta) {
                    return full.NumeroFinal
                },
            }

        ],
        "bDestroy": true
    }).DataTable();
}





function addtrserie() {
    let tr = "";
    tr = `
    <tr id="tr`+ contador + `">    
        <td>-</td>
        <td> <input class="form-control input-sm" value="" id="serietr`+ contador + `"/> </td>
        <td> <input class="form-control input-sm" value="" id="numeroitr`+ contador + `"/> </td>
        <td> <input class="form-control input-sm" value="" id="numeroftr`+ contador + `"/> </td>
        <td> <input class="form-check-input " type="checkbox" id="articulo`+ contador + `" value=""> </td>
        <td> <input class="form-check-input "  type="checkbox" id="servicio`+ contador + `"> </td>
         <td> <select class="form-control" id="IdIndicadorPeriodo`+ contador + `"> <option value="0">SELECCIONE PERIODO</option> </select> </td>
         <td> <input class="form-check-input " type="checkbox" id="estado`+ contador + `" checked> </td>
        <td>
            <button style="margin:0px 0px 0px 0px;" class="btn btn-danger fa fa-times" onclick="removetr(`+ contador + `)"></button> 
            <button style="margin:0px 0px 0px 0px;"class="btn btn-success fa fa-floppy-o" onclick="guardar(0,`+ contador + `)"></button>
        </td>
    </tr>`


    $("#table_serienew tbody").append(tr);
    listarindicadorperiodo(contador);
    contador++

}

function listarindicadorperiodo(idcontador) {
    $.ajaxSetup({ async: false });
    $.post("/IndicadorPeriodo/ObtenerIndicadorPeriodo", function (data, status) {
        let datos = JSON.parse(data);
        let option = `<option value="0">SELECCIONE PERIODO</option>`;
        for (var i = 0; i < datos.length; i++) {
            option += `<option value="` + datos[i].IdIndicadorPeriodo + `">` + datos[i].Indicador + `</option>`;
        }
        $("#IdIndicadorPeriodo" + idcontador).html(option);
    });
}

function removetr(idcontador) {
    $("#tr" + idcontador).remove()
}






function guardar(bd, id) {
    let IdSerie = 0;
    let Serie = "";
    let NumeroInicial;
    let NumeroFinal;
    let Estado = false;
    let Documento;
    let IsArticulo = false;
    let IsServicio = false;
    let IdIndicadorPeriodo;
    Documento = $("#IdDocumentoModal").val();

    if (bd == 1) {
        IdSerie = id;
        if ($('#IsArticulo' + id).prop('checked')) {
            IsArticulo = true;
        }
        if ($('#IsServicio' + id).prop('checked')) {
            IsServicio = true;
        }
        if ($('#Estado' + id).prop('checked')) {
            Estado = true;
        }
    } else {
        IdSerie = 0;
        Serie = $("#serietr" + id).val();
        NumeroInicial = $("#numeroitr" + id).val();
        NumeroFinal = $("#numeroftr" + id).val();
        if ($("#articulo" + id).prop('checked')) {
            IsArticulo = true;
        }
        if ($("#servicio" + id).prop('checked')) {
            IsServicio = true;
        }
        if ($("#estado" + id).prop('checked')) {
            Estado = true;
        }
        IdIndicadorPeriodo = $("#IdIndicadorPeriodo" + id).val();
    }

    $.post('UpdateInsertSerie', {
        'IdSerie': IdSerie,
        'Serie': Serie,
        'NumeroInicial': NumeroInicial,
        'NumeroFinal': NumeroFinal,
        'Estado': Estado,
        'IsArticulo': IsArticulo,
        'IsServicio': IsServicio,
        'IdPeriodo': IdIndicadorPeriodo,
        'Documento': Documento
    }, function (data, status) {
        if (data > 0) {
            Swal.fire("Exito!", "Proceso Realizado Correctamente", "success")
            //$("#modal-form").modal('hide')
            limpiarDatos();
        } else {
            Swal.fire("Error!", "Ocurrio un Error")
            limpiarDatos();
        }
        ObtenerSeriesDT()
       // closePopup();
    });

}