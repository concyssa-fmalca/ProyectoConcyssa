let table;
window.onload = function () {
    listarperiodocontabledt();
}

function ModalNuevo() {
    ListarIndicadorPeriodo();
    $("#lblTituloModal").html("Nuevo Periodo Contable");
    AbrirModal("modal-form");
}

function GuardarIndicadorPeriodo() {
    if ($("#Indicador").val() == '' || $("#Indicador").val() == null) {
        Swal.fire(
            'Error!',
            'Registre un Indicador de Periodo',
            'error'
        )
        return;
    }


    $.ajax({
        url: "UpdateInsertIndicadorPeriodo",
        type: "POST",
        async: true,
        data: {
            'Indicador': $("#Indicador").val(),
            'IdIndicadorPeriodo': $("#IdIndicadorPeriodo").val(),
        },
        beforeSend: function () {
            Swal.fire({
                title: "Cargando...",
                text: "Por favor espere",
                showConfirmButton: false,
                allowOutsideClick: false
            });
        },
        success: function (data) {
            if (data > 0) {
                Swal.fire(
                    'Correcto',
                    'Proceso Realizado Correctamente',
                    'success'
                )
                CerrarModal();

                //swal("Exito!", "Proceso Realizado Correctamente", "success")
                listarperiodocontabledt()

            } else {
                Swal.fire(
                    'Error!',
                    'Ocurrio un Error!',
                    'error'
                )

            }


        }
    }).fail(function () {
        Swal.fire(
            'Error!',
            'Comunicarse con el Area Soporte: smarcode@smartcode.pe !',
            'error'
        )
    });


}





function listarperiodocontabledt() {
    table = $('#table_periodocontable').dataTable({
        language: lenguaje_data,
        responsive: true,
        ajax: {
            url: 'ObtenerPeriodoContableDT',
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
                    return `<button class="btn btn-primary fa fa-pencil btn-xs" style="margin:0px 0px 0px 0px !important" onclick="ObtenerDatosxID(` + full.IdPeriodoContable + `)"></button>
                        <button class="btn btn-danger btn-xs  fa fa-trash"  style="margin:0px 0px 0px 0px !important"  onclick="eliminar(` + full.IdPeriodoContable + `)"></button>`
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
                    return full.CodigoPeriodo
                },
            },
            {
                targets: 2,
                orderable: false,
                render: function (data, type, full, meta) {
                    return full.NombrePeriodo
                },
            },
            {
                targets: 3,
                orderable: false,
                render: function (data, type, full, meta) {
                    let NombEstatus = "";
                    switch (full.StatusPeriodo) {
                        case 1:
                            NombEstatus="DESBLOQUEADOS"
                            break;
                        case 2:
                            NombEstatus = "DESBLOQUEADOS EXCEPTO VENTAS"
                            break;
                        case 3:
                            NombEstatus = "CIERRE DEL PERIODO"
                            break;
                        case 4:
                            NombEstatus = "BLOQUEADO"
                            break;
                       
                        default:
                            NombEstatus = "SIN ESTADO"
                    }
                    return NombEstatus
                },
            },
            {
                targets: 4,
                orderable: false,
                render: function (data, type, full, meta) {
                    return full.FechaContabilizacionI.split("T")[0]
                },
            },
            {
                targets: 5,
                orderable: false,
                render: function (data, type, full, meta) {
                    return full.FechaContabilizacionF.split("T")[0]
                },
            }

        ],
        "bDestroy": true
    }).DataTable();
}


function ObtenerDatosxID(IdPeriodoContable) {
    $("#lblTituloModal").html("Editar Indicador Periodo");
    AbrirModal("modal-form");
    ListarIndicadorPeriodo();
    $.post('ObtenerPeriodoContablexId', {
        'IdPeriodoContable': IdPeriodoContable,
    }, function (data, status) {
        if (data == "Error") {
            swal("Error!", "Ocurrio un error")
            limpiarDatos();
        } else {
            let PeriodoContable = JSON.parse(data);
            $("#IdPeriodoContable").val(PeriodoContable.IdPeriodoContable)
            $("#CodigoPeriodo").val(PeriodoContable.CodigoPeriodo);
            $("#NombrePeriodo").val(PeriodoContable.NombrePeriodo);
            $("#SubPeriodo").val(PeriodoContable.SubPeriodo);
            $("#IdIndicadorPeriodo").val(PeriodoContable.IdIndicadorPeriodo);
            $("#StatusPeriodo").val(PeriodoContable.StatusPeriodo);
            $("#CodigoPeriodo").val(PeriodoContable.CodigoPeriodo);
            $("#InicioEjercicio").val(PeriodoContable.InicioEjercicio.split("T")[0]);
            $("#Ejercicio").val(PeriodoContable.Ejercicio)
            let detalles = PeriodoContable.Detalles;
            $("#FechaContabilizacionI").val(detalles[0].FechaInicio.split("T")[0]);
            $("#FechaContabilizacionF").val(detalles[0].FechaFinal.split("T")[0]);
            $("#FechaVencimientoI").val(detalles[1].FechaInicio.split("T")[0]);
            $("#FechaVencimientoF").val(detalles[1].FechaFinal.split("T")[0]);
            $("#FechaDocumentoI").val(detalles[2].FechaInicio.split("T")[0]);
            $("#FechaDocumentoF").val(detalles[2].FechaFinal.split("T")[0]);
        }

    });
}


function CerrarModal() {
    $.magnificPopup.close();
    limpiarDatos();
}

function limpiarDatos() {

    $("#IdPeriodoContable").val(0)
    $("#CodigoPeriodo").val('');
    $("#NombrePeriodo").val('');
    $("#SubPeriodo").val(0);
    $("#IdIndicadorPeriodo").val(0);
    $("#StatusPeriodo").val(0);
    $("#CodigoPeriodo").val('');
    $("#InicioEjercicio").val('');
    $("#Ejercicio").val('');
    $("#FechaContabilizacionI").val('');
    $("#FechaContabilizacionF").val('');
    $("#FechaVencimientoI").val('');
    $("#FechaVencimientoF").val('');
    $("#FechaDocumentoI").val('');
    $("#FechaDocumentoF").val('');



}

function eliminar(Id) {
    $.ajax({
        url: "EliminarInidicadorPeriodo",
        type: "POST",
        async: true,
        data: {
            'IdIndicadorPeriodo': Id
        },
        beforeSend: function () {
            Swal.fire({
                title: "Cargando...",
                text: "Por favor espere",
                showConfirmButton: false,
                allowOutsideClick: false
            });
        },
        success: function (data) {
            if (data > 0) {
                Swal.fire(
                    'Correcto',
                    'Proceso Realizado Correctamente',
                    'success'
                )
                listarperiodocontabledt()

            } else {
                Swal.fire(
                    'Error!',
                    'Ocurrio un Error!',
                    'error'
                )

            }


        }
    }).fail(function () {
        Swal.fire(
            'Error!',
            'Comunicarse con el Area Soporte: smarcode@smartcode.pe !',
            'error'
        )
    });
}


function ListarIndicadorPeriodo() {
    $.post("/IndicadorPeriodo/ObtenerIndicadorPeriodo", function (data, status) {
        let datos = JSON.parse(data);
        let option = `<option value="0">SELECCIONE PERIODO</option>`;
        for (var i = 0; i < datos.length; i++) {
            option += `<option value="` + datos[i].IdIndicadorPeriodo + `">` + datos[i].Indicador +`</option>`;
        }
        $("#IdIndicadorPeriodo").html(option);
    });
}

function Guardar() {
    $("#CodigoPeriodo").val();
    $("#NombrePeriodo").val();
    $("#SubPeriodo").val();
    $("#IdIndicadorPeriodo").val();
    $("#StatusPeriodo").val();
    $("#InicioEjercicio").val();
    $("#Ejercicio").val();

    $("#FechaContabilizacionI").val();
    $("#FechaContabilizacionF").val();
    $("#FechaVencimientoI").val();
    $("#FechaVencimientoF").val();
    $("#FechaDocumentoI").val();
    $("#FechaDocumentoF").val();


    $.ajax({
        url: "UpdateInsertPeriodoContable",
        type: "POST",
        async: true,
        data: {
          
            //cabecera
            'IdPeriodoContable': $("#IdPeriodoContable").val(),
            'CodigoPeriodo': $("#CodigoPeriodo").val(),
            'NombrePeriodo': $("#NombrePeriodo").val(),
            'SubPeriodo': $("#SubPeriodo").val(),
            'IdIndicadorPeriodo': $("#IdIndicadorPeriodo").val(),
            'StatusPeriodo': $("#StatusPeriodo").val(),
            'InicioEjercicio': $("#InicioEjercicio").val(),
            'Ejercicio': $("#Ejercicio").val(),
            'FechaContabilizacionI': $("#FechaContabilizacionI").val(),
            'FechaContabilizacionF': $("#FechaContabilizacionF").val(),
            'FechaVencimientoI': $("#FechaVencimientoI").val(),
            'FechaVencimientoF': $("#FechaVencimientoF").val(),
            'FechaDocumentoI': $("#FechaDocumentoI").val(),
            'FechaDocumentoF': $("#FechaDocumentoF").val(),
            
        },
        beforeSend: function () {
            Swal.fire({
                title: "Cargando...",
                text: "Por favor espere",
                showConfirmButton: false,
                allowOutsideClick: false
            });
        },
        success: function (data) {
            if (data > 0) {
                Swal.fire(
                    'Correcto',
                    'Proceso Realizado Correctamente',
                    'success'
                )
                CerrarModal();
                listarperiodocontabledt()

            } else {
                Swal.fire(
                    'Error!',
                    'Ocurrio un Error!',
                    'error'
                )

            }


        }
    }).fail(function () {
        Swal.fire(
            'Error!',
            'Comunicarse con el Area Soporte: smarcode@smartcode.pe !',
            'error'
        )
    });
}


