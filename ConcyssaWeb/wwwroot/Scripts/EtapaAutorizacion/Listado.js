let table = '';


window.onload = function () {
    var url = "ObtenerEtapaAutorizacion";
    ConsultaServidor(url);

};



function ConsultaServidor(url) {

    $.post(url, function (data, status) {

        var errorEmpresa = validarEmpresa(data);
        if (errorEmpresa) {
            return;
        }

        console.log(data);
        if (data == "error") {
            table = $("#table_id").DataTable(lenguaje);
            return;
        }

        let tr = '';

        let etapaautorizacion = JSON.parse(data);
        let total_etapaautorizacion = etapaautorizacion.length;

        for (var i = 0; i < etapaautorizacion.length; i++) {


            tr += '<tr>' +
                '<td>' + (i + 1) + '</td>' +
                '<td>' + etapaautorizacion[i].NombreEtapa.toUpperCase() + '</td>' +
                '<td>' + etapaautorizacion[i].AutorizacionesRequeridas + '</td>' +
                '<td>' + etapaautorizacion[i].RechazosRequeridos + '</td>' +
                '<td><button class="btn btn-primary fa fa-pencil btn-xs" onclick="ObtenerDatosxID(' + etapaautorizacion[i].IdEtapaAutorizacion + ')"></button>' +
                '<button class="btn btn-danger btn-xs  fa fa-trash" onclick="eliminar(' + etapaautorizacion[i].IdEtapaAutorizacion + ')"></button></td >' +
                '</tr>';
        }

        $("#tbody_EtapaAutorizacion").html(tr);
        $("#spnTotalRegistros").html(total_etapaautorizacion);

        table = $("#table_id").DataTable(lenguaje);

    });

}

function ModalNuevo() {
    $("#lblTituloModal").html("Nueva Etapa de Autorizacion");
    AbrirModal("modal-form");
    $('#chkActivo').prop('checked', true);
    AgregarLinea();
}


function GuardarEtapaAutorizacion() {

    let varIdEtapaAutorizacion = $("#txtId").val();
    let varNombreEtapa = $("#txtNombreEtapa").val();
    let varDescripcionEtapa = $("#txtDescripcionEtapa").val();
    let varAutorizacionesRequeridas = $("#txtAutorizacionesRequeridas").val();
    let varRechazosRequeridos = $("#txtRechazosRequeridos").val();
    let varEstado = false;

    if ($('#chkActivo')[0].checked) {
        varEstado = true;
    }

    let arrayGeneral = new Array();
    let arrayIdEtapaAutorizacionDetalle = new Array();
    let arrayIdUsuario = new Array();
    let arrayIdDepartamento = new Array();

    $("input[name='txtIdEtapaAutorizacionDetalle[]']").each(function (indice, elemento) {
        arrayIdEtapaAutorizacionDetalle.push($(elemento).val());
    });
    $("select[name='cboUsuario[]']").each(function (indice, elemento) {
        arrayIdUsuario.push($(elemento).val());
    });
    $("select[name='cboDepartamento[]']").each(function (indice, elemento) {
        arrayIdDepartamento.push($(elemento).val());
    });

    for (var i = 0; i < arrayIdUsuario.length; i++) {
        arrayGeneral.push({ 'IdEtapaAutorizacionDetalle': arrayIdEtapaAutorizacionDetalle[i], 'IdUsuario': arrayIdUsuario[i], 'IdDepartamento': arrayIdDepartamento[i] })
    }

    //console.log(arrayGeneral);

    $.post('UpdateInsertEtapaAutorizacion', {
        'IdEtapaAutorizacion': varIdEtapaAutorizacion,
        'NombreEtapa': varNombreEtapa,
        'DescripcionEtapa': varDescripcionEtapa,
        'AutorizacionesRequeridas': varAutorizacionesRequeridas,
        'RechazosRequeridos': varRechazosRequeridos,
        'Estado': varEstado,
        'Detalle': arrayGeneral
    }, function (data, status) {

        var errorEmpresa = validarEmpresaUpdateInsert(data);
        if (errorEmpresa) {
            return;
        }


        if (data == 1) {
            swal("Exito!", "Proceso Realizado Correctamente", "success")
            table.destroy();
            ConsultaServidor("ObtenerEtapaAutorizacion");
            limpiarDatos();
        } else {
            swal("Error!", "Ocurrio un Error")
            limpiarDatos();
        }

    });
}


function ObtenerDatosxID(varIdEtapaAutorizacion) {
    $("#lblTituloModal").html("Editar Etapa de Autorizacion");
    AbrirModal("modal-form");

    //console.log(varIdUsuario);

    $.post('ObtenerDatosxID', {
        'IdEtapaAutorizacion': varIdEtapaAutorizacion,
    }, function (data, status) {

        var errorEmpresa = validarEmpresa(data);
        if (errorEmpresa) {
            return;
        }


        if (data == "Error") {
            swal("Error!", "Ocurrio un error")
            limpiarDatos();
        } else {
            let etapaautorizacion = JSON.parse(data);

            $("#txtId").val(etapaautorizacion[0].IdEtapaAutorizacion);
            $("#txtNombreEtapa").val(etapaautorizacion[0].NombreEtapa);
            $("#txtDescripcionEtapa").val(etapaautorizacion[0].DescripcionEtapa);
            $("#txtAutorizacionesRequeridas").val(etapaautorizacion[0].AutorizacionesRequeridas);
            $("#txtRechazosRequeridos").val(etapaautorizacion[0].RechazosRequeridos);
            if (etapaautorizacion[0].Estado) {
                $("#chkActivo").prop('checked', true);
            }

            let Detalles = etapaautorizacion[0].Detalles;
            console.log(Detalles);

            for (var i = 0; i < Detalles.length; i++) {

                AgregarLineaDetalle(i, Detalles[i].IdEtapaAutorizacionDetalle, Detalles[i].IdUsuario, Detalles[i].IdDepartamento);

            }
        }

    });

}


function eliminar(varIdEtapaAutorizacion) {


    alertify.confirm('Confirmar', '¿Desea eliminar esta etapa de autorizacion?', function () {
        $.post("EliminarEtapaAutorizacion", { 'IdEtapaAutorizacion': varIdEtapaAutorizacion }, function (data) {

            var errorEmpresa = validarEmpresaUpdateInsert(data);
            if (errorEmpresa) {
                return;
            }

            if (data == 0) {
                swal("Error!", "Ocurrio un Error")
                limpiarDatos();
            } else {
                swal("Exito!", "Etapa de Autorizacion Eliminado", "success")
                table.destroy();
                ConsultaServidor("ObtenerEtapaAutorizacion");
                limpiarDatos();
            }

        });

    }, function () { });

}



function limpiarDatos() {
    $("#txtId").val("");
    $("#txtNombreEtapa").val("");
    $("#txtDescripcionEtapa").val("");
    $("#txtAutorizacionesRequeridas").val("");
    $("#txtRechazosRequeridos").val("");
    $("#chkActivo").prop('checked', false);
}


let contador = 0;
function AgregarLinea() {

    let Departamentos;
    let Usuarios;

    $.ajaxSetup({ async: false });
    $.post("/Departamento/ObtenerDepartamentos", function (data, status) {
        var errorEmpresa = validarEmpresa(data);
        if (errorEmpresa) {
            return;
        }
        Departamentos = JSON.parse(data);
    });

    $.post("/Usuario/ObtenerUsuariosAutorizadores", function (data, status) {
        var errorEmpresa = validarEmpresa(data);
        if (errorEmpresa) {
            return;
        }
        Usuarios = JSON.parse(data);
    });

    let tr = '';

    tr += `<tr>
            <td><input style="display:none;" class="form-control" type="text" value="0" id="txtIdEtapaAutorizacionDetalle" name="txtIdEtapaAutorizacionDetalle[]"/></td>
           <td>
            <select class="form-control" name="cboUsuario[]" id="cboUsuario`+ contador + `"  onchange="ObtenerDepartamentoUsuario(` + contador + `)" >`;
    tr += `  <option value="0">Seleccione</option>`;
    for (var i = 0; i < Usuarios.length; i++) {
        tr += `  <option value="` + Usuarios[i].IdUsuario + `">` + Usuarios[i].NombreUsuario + `</option>`;
    }
    tr += `</select>
            </td>
            <td>
            <select class="form-control" name="cboDepartamento[]" id="cboDepartamento`+ contador + `">`;
    tr += `  <option value="0">Seleccione</option>`;
    for (var i = 0; i < Departamentos.length; i++) {
        tr += `  <option value="` + Departamentos[i].IdDepartamento + `">` + Departamentos[i].Descripcion + `</option>`;
    }
    tr += `</select>
            </td>
            <td><button class="btn btn-xs btn-danger borrar">-</button></td>
            </tr>`;

    $("#tabla").find('tbody').append(tr);
    contador++;
}

$(document).on('click', '.borrar', function (event) {
    event.preventDefault();
    $(this).closest('tr').remove();
});


function CerrarModal() {
    $("#tabla").find('tbody').empty();
    $.magnificPopup.close();
    limpiarDatos();
}


function ObtenerDepartamentoUsuario(contador) {

    let IdUsuario = $("#cboUsuario" + contador).val();

    $.ajaxSetup({ async: false });
    $.post("/Departamento/ObtenerDepartamentosxUsuario", { 'IdUsuario': IdUsuario }, function (data, status) {
        var errorEmpresa = validarEmpresa(data);
        if (errorEmpresa) {
            return;
        }

        let datos = JSON.parse(data);
        console.log(datos);
        $("#cboDepartamento" + contador).val(datos[0].IdDepartamento);
    });

}


function AgregarLineaDetalle(contador, IdEtapaAutorizacionDetalle, IdUsuario, IdDepartamento) {

    let Departamentos;
    let Usuarios;

    $.ajaxSetup({ async: false });
    $.post("/Departamento/ObtenerDepartamentos", function (data, status) {
        var errorEmpresa = validarEmpresa(data);
        if (errorEmpresa) {
            return;
        }
        Departamentos = JSON.parse(data);
    });

    $.post("/Usuario/ObtenerUsuariosAutorizadores", function (data, status) {
        var errorEmpresa = validarEmpresa(data);
        if (errorEmpresa) {
            return;
        }
        Usuarios = JSON.parse(data);
    });

    let tr = '';

    tr += `<tr>
            <td><input style="display:none;" class="form-control" type="text" value="`+ IdEtapaAutorizacionDetalle + `" id="txtIdEtapaAutorizacionDetalle" name="txtIdEtapaAutorizacionDetalle[]"/></td>
           <td>
            <select class="form-control" name="cboUsuario[]" id="cboUsuario`+ contador + `"  onchange="ObtenerDepartamentoUsuario(` + contador + `)">`;
    tr += `  <option value="0">Seleccione</option>`;
    for (var i = 0; i < Usuarios.length; i++) {
        if (Usuarios[i].IdUsuario == IdUsuario) {
            tr += `  <option value="` + Usuarios[i].IdUsuario + `" selected>` + Usuarios[i].NombreUsuario + `</option>`;
        } else {
            tr += `  <option value="` + Usuarios[i].IdUsuario + `">` + Usuarios[i].NombreUsuario + `</option>`;
        }

    }
    tr += `</select>
            </td>
            <td>
            <select class="form-control" name="cboDepartamento[]" id="cboDepartamento`+ contador + `">`;
    tr += `  <option value="0">Seleccione</option>`;
    for (var i = 0; i < Departamentos.length; i++) {
        if (Departamentos[i].IdDepartamento == IdDepartamento) {
            tr += `  <option value="` + Departamentos[i].IdDepartamento + `" selected>` + Departamentos[i].Descripcion + `</option>`;
        } else {
            tr += `  <option value="` + Departamentos[i].IdDepartamento + `">` + Departamentos[i].Descripcion + `</option>`;
        }

    }
    tr += `</select>
            </td>
            <td><button class="btn btn-xs btn-danger"  onclick="EliminarDetalle(`+ IdEtapaAutorizacionDetalle + `,this)">-</button></td>
            </tr>`;

    $("#tabla").find('tbody').append(tr);

}


function EliminarDetalle(IdEtapaAutorizacionDetalle, dato) {

    alertify.confirm('Confirmar', '¿Desea eliminar este item?', function () {

        $.post("EliminarDetalleAutorizacion", { 'IdEtapaAutorizacionDetalle': IdEtapaAutorizacionDetalle }, function (data, status) {

            var errorEmpresa = validarEmpresaUpdateInsert(data);
            if (errorEmpresa) {
                return;
            }

            if (data == 0) {
                swal("Error!", "Ocurrio un Error")

            } else {
                swal("Exito!", "Item Eliminado", "success")
                $(dato).closest('tr').remove();
            }

        });

    }, function () { });
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
