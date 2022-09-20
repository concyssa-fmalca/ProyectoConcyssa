
let table = '';


window.onload = function () {
    var url = "ObtenerModeloAutorizacion";
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

        let modeloautorizacion = JSON.parse(data);
        let total_modeloautorizacion = modeloautorizacion.length;

        for (var i = 0; i < modeloautorizacion.length; i++) {


            tr += '<tr>' +
                '<td>' + (i + 1) + '</td>' +
                '<td>' + modeloautorizacion[i].NombreModelo.toUpperCase() + '</td>' +
                '<td>' + modeloautorizacion[i].DescripcionModelo.toUpperCase() + '</td>' +
                '<td><button class="btn btn-primary fa fa-pencil btn-xs" onclick="ObtenerDatosxID(' + modeloautorizacion[i].IdModeloAutorizacion + ')"></button>' +
                '<button class="btn btn-danger btn-xs  fa fa-trash" onclick="eliminar(' + modeloautorizacion[i].IdModeloAutorizacion + ')"></button></td >' +
                '</tr>';
        }

        $("#tbody_ModeloAutorizacion").html(tr);
        $("#spnTotalRegistros").html(total_modeloautorizacion);

        table = $("#table_id").DataTable(lenguaje);

    });

}




function GuardarModeloAutorizacion() {


    if (!$('#chkSolicitudCompra').prop('checked')) {
        swal("Info!", "Debe Seleccionar Documento")
        return;
    }


    let varIdModeloAutorizacion = $("#txtId").val();
    let varNombreModelo = $("#txtNombreModelo").val();
    let varDescripcionModelo = $("#txtDescripcionModelo").val();
    let varEstado = false;

    if ($('#chkActivo')[0].checked) {
        varEstado = true;
    }

    //autores
    let arrayGeneralAutor = new Array();
    let arrayIdModeloAutorizacionAutor = new Array();
    let arrayIdUsuario = new Array();

    $("input[name='txtIdModeloAutorizacionAutor[]']").each(function (indice, elemento) {
        arrayIdModeloAutorizacionAutor.push($(elemento).val());
    });
    $("select[name='cboUsuario[]']").each(function (indice, elemento) {
        arrayIdUsuario.push($(elemento).val());
    });

    for (var i = 0; i < arrayIdUsuario.length; i++) {
        arrayGeneralAutor.push({ 'IdModeloAutorizacionAutor': arrayIdModeloAutorizacionAutor[i], 'IdAutor': arrayIdUsuario[i] });
    }
    //autores

    //etapas
    let arrayGeneralEtapa = new Array();
    let arrayIdModeloAutorizacionEtapa = new Array();
    let arrayIdEtapa = new Array();

    $("input[name='txtIdModeloAutorizacionEtapa[]']").each(function (indice, elemento) {
        arrayIdModeloAutorizacionEtapa.push($(elemento).val());
    });
    $("select[name='cboEtapa[]']").each(function (indice, elemento) {
        arrayIdEtapa.push($(elemento).val());
    });

    for (var i = 0; i < arrayIdEtapa.length; i++) {
        arrayGeneralEtapa.push({ 'IdModeloAutorizacionEtapa': arrayIdModeloAutorizacionEtapa[i], 'IdEtapa': arrayIdEtapa[i] });
    }
    //etapas

    //documento
    let arrayGeneralDocumento = new Array();
    let varIdModeloAutorizacionDocumento = $("#txtIdModeloAutorizacionDocumento").val();
    let varcheckSolicitudCompra = 0;
    if ($('#chkSolicitudCompra')[0].checked) {
        varcheckSolicitudCompra = 1;
    }
    arrayGeneralDocumento.push({ 'IdModeloAutorizacionDocumento': varIdModeloAutorizacionDocumento, 'IdDocumento': varcheckSolicitudCompra });
    //documento


    //condicion
    let arrayGeneralCondicion = new Array();
    let arrayIdModeloAutorizacionCondicion = new Array();
    let arrayCondicion = new Array();
    $("input[name='txtIdModeloAutorizacionCondicion[]']").each(function (indice, elemento) {
        arrayIdModeloAutorizacionCondicion.push($(elemento).val());
    });
    $("input[name='txtCondicion[]']").each(function (indice, elemento) {
        arrayCondicion.push($(elemento).val());
    });
    for (var i = 0; i < arrayCondicion.length; i++) {
        arrayGeneralCondicion.push({ 'IdModeloAutorizacionCondicion': arrayIdModeloAutorizacionCondicion[i], 'Condicion': arrayCondicion[i] });
    }
    //condicion

    $.post('UpdateInsertModeloAutorizacion', {
        'IdModeloAutorizacion': varIdModeloAutorizacion,
        'NombreModelo': varNombreModelo,
        'DescripcionModelo': varDescripcionModelo,
        'Estado': varEstado,
        'DetalleAutor': arrayGeneralAutor,
        'DetalleEtapa': arrayGeneralEtapa,
        'DetalleDocumento': arrayGeneralDocumento,
        'DetalleCondicion': arrayGeneralCondicion
    }, function (data, status) {

        var errorEmpresa = validarEmpresaUpdateInsert(data);
        if (errorEmpresa) {
            return;
        }

        if (data == 1) {
            swal("Exito!", "Proceso Realizado Correctamente", "success")
            table.destroy();
            ConsultaServidor("ObtenerModeloAutorizacion");
            limpiarDatos();
        } else {
            swal("Error!", "Ocurrio un Error")
            limpiarDatos();
        }

    });
}





function ModalNuevo() {
    $("#lblTituloModal").html("Nuevo Modelo de Autorizacion");
    AbrirModal("modal-form");
    $('#chkActivo').prop('checked', true);
    AgregarLinea();
    AgregarLineaEtapas();
    AgregarLineaDocumentos();
    AgregarLineaCondiciones();
}


function CerrarModal() {
    $("#tabla").find('tbody').empty();
    $("#tablaEtapas").find('tbody').empty();
    $("#tablaDocumentos").find('tbody').empty();
    $("#tablaCondiciones").find('tbody').empty();
    $.magnificPopup.close();

    limpiarDatos();
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

    $.post("/Usuario/ObtenerUsuarios", function (data, status) {
        var errorEmpresa = validarEmpresa(data);
        if (errorEmpresa) {
            return;
        }
        Usuarios = JSON.parse(data);
    });

    let tr = '';

    tr += `<tr>
            <td><input style="display:none;" class="form-control" type="text" value="0" id="txtIdModeloAutorizacionAutor" name="txtIdModeloAutorizacionAutor[]"/></td>
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

let contador1 = 0;
function AgregarLineaEtapas() {

    let Etapas;
    $.ajaxSetup({ async: false });
    $.post("/EtapaAutorizacion/ObtenerEtapaAutorizacion", function (data, status) {
        var errorEmpresa = validarEmpresa(data);
        if (errorEmpresa) {
            return;
        }
        Etapas = JSON.parse(data);
    });

    let tr = '';

    tr += `<tr>
            <td><input style="display:none;" class="form-control" type="text" value="0" id="txtIdModeloAutorizacionEtapa" name="txtIdModeloAutorizacionEtapa[]"/></td>
           <td>
            <select class="form-control" name="cboEtapa[]" id="cboEtapa`+ contador1 + `" onchange="ObtenerDescripcionEtapa(` + contador1 + `)">`;
    tr += `  <option value="0">Seleccione</option>`;
    for (var i = 0; i < Etapas.length; i++) {
        tr += `  <option value="` + Etapas[i].IdEtapaAutorizacion + `">` + Etapas[i].NombreEtapa + `</option>`;
    }
    tr += `</select>
            </td>
            <td>
                <input  class="form-control" type="text"  id="txtDescripcionEtapa`+ contador1 + `" name="txtDescripcionEtapa[]"/>
            </td>
            <td><button class="btn btn-xs btn-danger borrar">-</button></td>
            </tr>`;

    $("#tablaEtapas").find('tbody').append(tr);
    contador1++;
}



function AgregarLineaCondiciones() {


    let tr = '';

    tr += `<tr>
            <td><input style="display:none;" class="form-control" type="text" value="0" id="txtIdModeloAutorizacionCondicion" name="txtIdModeloAutorizacionCondicion[]"/></td>
           <td>
            <input class="form-control" type="text" id="txtCondicion" name="txtCondicion[]"/>
            </td>
            <td><button class="btn btn-xs btn-danger borrar">-</button></td>
            </tr>`;

    $("#tablaCondiciones").find('tbody').append(tr);
}



//let contador2 = 0;
function AgregarLineaDocumentos() {


    let tr = '';

    tr += `<tr>
            <td><input style="display:none;" class="form-control" type="text" value="0" id="txtIdModeloAutorizacionDocumento" name="txtIdModeloAutorizacionDocumento[]"/></td>
            <td>
             <div class="checkbox-custom col-xs-6">
                  <input type="checkbox" id="chkSolicitudCompra" />
                  <label for="chkSolicitudCompra">Solicitud de Compra</label>
              </div>
            </td>
            </tr>`;

    $("#tablaDocumentos").find('tbody').append(tr);
    //contador2++;
}



function ObtenerDescripcionEtapa(contador) {

    let IdEtapa = $("#cboEtapa" + contador).val();
    $.ajaxSetup({ async: false });
    $.post("/EtapaAutorizacion/ObtenerDatosxID", { 'IdEtapaAutorizacion': IdEtapa }, function (data, status) {
        var errorEmpresa = validarEmpresa(data);
        if (errorEmpresa) {
            return;
        }
        let datos = JSON.parse(data);
        $("#txtDescripcionEtapa" + contador).val(datos[0].DescripcionEtapa);
    });

}



$(document).on('click', '.borrar', function (event) {
    event.preventDefault();
    $(this).closest('tr').remove();
});



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



function openContenido(evt, Name) {
    var i, tabcontent, tablinks;
    tabcontent = document.getElementsByClassName("tabcontent");
    for (i = 0; i < tabcontent.length; i++) {
        tabcontent[i].style.display = "none";
    }
    tablinks = document.getElementsByClassName("tablinks");
    for (i = 0; i < tablinks.length; i++) {
        tablinks[i].className = tablinks[i].className.replace(" active", "");
    }
    document.getElementById(Name).style.display = "block";
    evt.currentTarget.className += " active";
}



function ObtenerDatosxID(varIdModeloAutorizacion) {
    $("#lblTituloModal").html("Editar Modelo de Autorizacion");
    AbrirModal("modal-form");

    //console.log(varIdUsuario);

    $.post('ObtenerDatosxID', {
        'IdModeloAutorizacion': varIdModeloAutorizacion,
    }, function (data, status) {

        var errorEmpresa = validarEmpresa(data);
        if (errorEmpresa) {
            return;
        }


        if (data == "Error") {
            swal("Error!", "Ocurrio un error")
            limpiarDatos();
        } else {
            let modeloautorizacion = JSON.parse(data);

            $("#txtId").val(modeloautorizacion[0].IdModeloAutorizacion);
            $("#txtNombreModelo").val(modeloautorizacion[0].NombreModelo);
            $("#txtDescripcionModelo").val(modeloautorizacion[0].DescripcionModelo);
            if (modeloautorizacion[0].Estado) {
                $("#chkActivo").prop('checked', true);
            }
            console.log(modeloautorizacion);
            let DetallesAutor = modeloautorizacion[0].DetallesAutor;
            let DetallesEtapa = modeloautorizacion[0].DetallesEtapa;
            let DetallesDocumento = modeloautorizacion[0].DetallesDocumento;
            let DetallesCondicion = modeloautorizacion[0].DetallesCondicion;

            console.log(DetallesAutor);

            for (var i = 0; i < DetallesAutor.length; i++) {
                AgregarLineaDetalleAutor(i, DetallesAutor[i].IdModeloAutorizacionAutor, DetallesAutor[i].IdAutor, DetallesAutor[i].IdDepartamento)
            }
            for (var i = 0; i < DetallesEtapa.length; i++) {
                AgregarLineaDetalleEtapa(i, DetallesEtapa[i].IdModeloAutorizacionEtapa, DetallesEtapa[i].IdEtapa, DetallesEtapa[i].DescripcionEtapa)
            }
            for (var i = 0; i < DetallesDocumento.length; i++) {
                AgregarLineaDetalleDocumentos(i, DetallesDocumento[i].IdModeloAutorizacionDocumento, DetallesDocumento[i].IdDocumento)
            }
            for (var i = 0; i < DetallesCondicion.length; i++) {
                AgregarLineaDetalleCondicion(i, DetallesCondicion[i].IdModeloAutorizacionCondicion, DetallesCondicion[i].Condicion)
            }

        }

    });

}


function AgregarLineaDetalleDocumentos(contador, IdModeloAutorizacionDocumento, IdDocumento) {

    let tr = '';

    tr += `<tr>
            <td><input style="display:none;" class="form-control" type="text" value="`+ IdModeloAutorizacionDocumento + `" id="txtIdModeloAutorizacionDocumento" name="txtIdModeloAutorizacionDocumento[]"/></td>
            <td>
             <div class="checkbox-custom col-xs-6">
                  <input type="checkbox" id="chkSolicitudCompra" />
                  <label for="chkSolicitudCompra">Solicitud de Compra</label>
              </div>
            </td>
            </tr>`;

    $("#tablaDocumentos").find('tbody').append(tr);

    if (IdDocumento == 1) {
        $('#chkSolicitudCompra').prop('checked', true);
    }

}


function AgregarLineaDetalleCondicion(contador1, IdModeloAutorizacionCondicion, Condicion) {

    let tr = '';

    tr += `<tr>
            <td><input style="display:none;" class="form-control" type="text" value="`+ IdModeloAutorizacionCondicion + `" id="txtIdModeloAutorizacionCondicion" name="txtIdModeloAutorizacionCondicion[]"/></td>
            <td>
                <input  class="form-control" type="text" value="`+ Condicion + `"  id="txtCondicion` + contador1 + `" name="txtCondicion[]"/>
            </td>
            <td><button class="btn btn-xs btn-danger" onclick="EliminarModeloAutorizacionDetalleCondicion(`+ IdModeloAutorizacionCondicion + `,this)">-</button></td>
            </tr>`;

    $("#tablaCondiciones").find('tbody').append(tr);
}



function AgregarLineaDetalleEtapa(contador1, IdModeloAutorizacionEtapa, IdEtapa, DescripcionEtapa) {

    let Etapas;
    $.ajaxSetup({ async: false });
    $.post("/EtapaAutorizacion/ObtenerEtapaAutorizacion", function (data, status) {
        var errorEmpresa = validarEmpresa(data);
        if (errorEmpresa) {
            return;
        }
        Etapas = JSON.parse(data);
    });

    let tr = '';

    tr += `<tr>
            <td><input style="display:none;" class="form-control" type="text" value="`+ IdModeloAutorizacionEtapa + `" id="txtIdModeloAutorizacionEtapa" name="txtIdModeloAutorizacionEtapa[]"/></td>
           <td>
            <select class="form-control" name="cboEtapa[]" id="cboEtapa`+ contador1 + `" onchange="ObtenerDescripcionEtapa(` + contador1 + `)">`;
    tr += `  <option value="0">Seleccione</option>`;
    for (var i = 0; i < Etapas.length; i++) {
        if (Etapas[i].IdEtapaAutorizacion == IdEtapa) {
            tr += `  <option value="` + Etapas[i].IdEtapaAutorizacion + `" selected>` + Etapas[i].NombreEtapa + `</option>`;
        } else {
            tr += `  <option value="` + Etapas[i].IdEtapaAutorizacion + `">` + Etapas[i].NombreEtapa + `</option>`;
        }

    }
    tr += `</select>
            </td>
            <td>
                <input  class="form-control" type="text" value="`+ DescripcionEtapa + `"  id="txtDescripcionEtapa` + contador1 + `" name="txtDescripcionEtapa[]"/>
            </td>
            <td><button class="btn btn-xs btn-danger" onclick="EliminarModeloAutorizacionDetalleEtapa(`+ IdModeloAutorizacionEtapa + `,this)">-</button></td>
            </tr>`;

    $("#tablaEtapas").find('tbody').append(tr);
}

function AgregarLineaDetalleAutor(contador, IdModeloAutorizacionAutor, IdAutor, IdDepartamento) {

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

    $.post("/Usuario/ObtenerUsuarios", function (data, status) {
        var errorEmpresa = validarEmpresa(data);
        if (errorEmpresa) {
            return;
        }
        Usuarios = JSON.parse(data);
    });

    let tr = '';

    tr += `<tr>
            <td><input style="display:none;" class="form-control" type="text" value="`+ IdModeloAutorizacionAutor + `" id="txtIdModeloAutorizacionAutor" name="txtIdModeloAutorizacionAutor[]"/></td>
           <td>
            <select class="form-control" name="cboUsuario[]" id="cboUsuario`+ contador + `"  onchange="ObtenerDepartamentoUsuario(` + contador + `)" >`;
    tr += `  <option value="0">Seleccione</option>`;
    for (var i = 0; i < Usuarios.length; i++) {
        if (Usuarios[i].IdUsuario == IdAutor) {
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
            <td><button class="btn btn-xs btn-danger" onclick="EliminarModeloAutorizacionDetalleAutor(`+ IdModeloAutorizacionAutor + `,this)">-</button></td>
            </tr>`;

    $("#tabla").find('tbody').append(tr);
    //contador++;
}



function limpiarDatos() {
    $("#txtId").val("");
    $("#txtNombreModelo").val("");
    $("#txtDescripcionModelo").val("");

}

function EliminarModeloAutorizacionDetalleAutor(IdModeloAutorizacionAutor, dato) {

    alertify.confirm('Confirmar', '¿Desea eliminar este item?', function () {

        $.post("EliminarModeloAutorizacionDetalleAutor", { 'IdModeloAutorizacionAutor': IdModeloAutorizacionAutor }, function (data, status) {

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

function EliminarModeloAutorizacionDetalleEtapa(IdModeloAutorizacionEtapa, dato) {

    alertify.confirm('Confirmar', '¿Desea eliminar este item?', function () {

        $.post("EliminarModeloAutorizacionDetalleEtapa", { 'IdModeloAutorizacionEtapa': IdModeloAutorizacionEtapa }, function (data, status) {

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

function EliminarModeloAutorizacionDetalleCondicion(IdModeloAutorizacionCondicion, dato) {

    alertify.confirm('Confirmar', '¿Desea eliminar este item?', function () {

        $.post("EliminarModeloAutorizacionDetalleCondicion", { 'IdModeloAutorizacionCondicion': IdModeloAutorizacionCondicion }, function (data, status) {

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

