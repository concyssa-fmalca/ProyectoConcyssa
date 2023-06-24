let table = '';
let tableDetalle = '';
let contador = 0;
let table_series;
let nameFile = "";



function Empleados() {
    CargarEncargado(3)
}
function Proveedores() {
    CargarEncargado(2)
}

function CargarEncargado(tipo = 0) {

    if (tipo == 2) {

        //$.ajaxSetup({ async: false });
        //$.post("/Proveedor/ObtenerProveedores", { estado: 1 }, function (data, status) {
        //    let proveedores = JSON.parse(data);
        //    llenarComboProveedor(proveedores, "cboEncargado", "Seleccione")
        //    $("#cboEncargado").select2()
        //});
        CargarProveedor();
        $("#cboEncargado").select2();

        $("#divSolicitante").hide();
        $("#divProveedor").show();

        $("#cboSolicitante").val("0");
    }
    else {
        if (tipo == 3) {
            //$.ajaxSetup({ async: false });
            //$.post("/Empleado/ObtenerEmpleados", { estado: 1 }, function (data, status) {
            //    let proveedores = JSON.parse(data);
            //    llenarComboEmpleado(proveedores, "cboEncargado", "Seleccione")
            //    $("#cboSolicitante").select2()
            //});
            CargarSolicitante();
            $("#cboSolicitante").select2();

            $("#divSolicitante").show();
            $("#divProveedor").hide();

            $("#cboEncargado").val("0");
        }
        else {

            $("#cboEncargado").val(null).trigger('change');
            $("#cboEncargado").empty();
        }

    }

}

function llenarComboProveedor(lista, idCombo, primerItem) {
    var contenido = "";
    if (primerItem != null) contenido = "<option value='0'>" + primerItem + "</option>";
    var nRegistros = lista.length;
    var nCampos;
    var campos;

    for (var i = 0; i < nRegistros; i++) {

        if (lista.length > 0) { contenido += "<option value='" + lista[i].IdProveedor + "'>" + lista[i].RazonSocial + "</option>"; }
        else { }
    }
    var cbo = document.getElementById(idCombo);
    if (cbo != null) cbo.innerHTML = contenido;
}


function CargarSolicitante() {

    $.ajaxSetup({ async: false });
    $.post("/Empleado/ObtenerEmpleados", { estado: 1 }, function (data, status) {
        let proveedores = JSON.parse(data);
        llenarComboEmpleado(proveedores, "cboSolicitante", "Seleccione")
        $("#cboSolicitante").select2()
    });


}

function llenarComboEmpleado(lista, idCombo, primerItem) {
    var contenido = "";
    if (primerItem != null) contenido = "<option value='0'>" + primerItem + "</option>";
    var nRegistros = lista.length;
    var nCampos;
    var campos;

    for (var i = 0; i < nRegistros; i++) {

        if (lista.length > 0) { contenido += "<option value='" + lista[i].IdEmpleado + "'>" + lista[i].RazonSocial + "</option>"; }
        else { }
    }
    var cbo = document.getElementById(idCombo);
    if (cbo != null) cbo.innerHTML = contenido;
}


function CargarProveedor() {
    $.ajaxSetup({ async: false });
    $.post("/Proveedor/ObtenerProveedores", { estado: 1 }, function (data, status) {
        let proveedores = JSON.parse(data);
        llenarComboProveedor(proveedores, "IdProveedor", "Seleccione")
        llenarComboProveedor(proveedores, "cboEncargado", "Seleccione")

    });

}
function CargarTipoDocumento() {
    $.ajaxSetup({ async: false });
    $.post("/TiposDocumentos/ObtenerTiposDocumentos", { 'estado': 1 }, function (data, status) {
        let tipoRegistros = JSON.parse(data);
        llenarComboTipoDocumento(tipoRegistros, "cboTipoDocumento", "Seleccione")
    });
}


function llenarComboTipoDocumento(lista, idCombo, primerItem) {
    var contenido = "";
    if (primerItem != null) contenido = "<option value='0'>" + primerItem + "</option>";
    var nRegistros = lista.length;
    var nCampos;
    var campos;
    for (var i = 0; i < nRegistros; i++) {

        if (lista.length > 0) { contenido += "<option value='" + lista[i].IdTipoDocumento + "'>" + lista[i].Descripcion + "</option>"; }
        else { }
    }
    var cbo = document.getElementById(idCombo);
    if (cbo != null) cbo.innerHTML = contenido;
}
function CargarMoneda() {
    $.ajaxSetup({ async: false });
    $.post("/Moneda/ObtenerMonedas", function (data, status) {
        let monedas = JSON.parse(data);
        llenarComboMoneda(monedas, "cboMoneda", "Seleccione")
    });
}

function llenarComboMoneda(lista, idCombo, primerItem) {
    var contenido = "";
    if (primerItem != null) contenido = "<option value='0'>" + primerItem + "</option>";
    var nRegistros = lista.length;
    var nCampos;
    var campos;
    for (var i = 0; i < nRegistros; i++) {

        if (lista.length > 0) { contenido += "<option value='" + lista[i].IdMoneda + "'>" + lista[i].Descripcion + "</option>"; }
        else { }
    }
    var cbo = document.getElementById(idCombo);
    if (cbo != null) cbo.innerHTML = contenido;
    $("#cboMoneda").val(1);
}

function CargarObra(tipo = 0) {

    $.ajaxSetup({ async: false });
    $.post("/Obra/ObtenerObraxIdUsuarioSessionSinBase", function (data, status) {
        let monedas = JSON.parse(data);
        if (tipo == 0)
            llenarComboObra(monedas, "cboObra", "Seleccione")
        llenarComboObra(monedas, "cboObraM", "Seleccione")
    });
}

function llenarComboObra(lista, idCombo, primerItem) {
    var contenido = "";
    if (primerItem != null) contenido = "<option value='0'>" + primerItem + "</option>";
    var nRegistros = lista.length;
    var nCampos;
    var campos;
    for (var i = 0; i < nRegistros; i++) {

        if (lista.length > 0) { contenido += "<option value='" + lista[i].IdObra + "'>" + lista[i].Descripcion + "</option>"; }
        else { }
    }
    var cbo = document.getElementById(idCombo);
    if (cbo != null) cbo.innerHTML = contenido;
    //$("#cboMoneda").val(1);
}
function CargarTipoRegistro(tipo = 0) {
    $.ajaxSetup({ async: false });
    $.post("/TipoRegistro/ObtenerTipoRegistrosAjax", function (data, status) {
        let monedas = JSON.parse(data);
        if (tipo == 0) {
            llenarComboTipoRegistro(monedas, "cboTipoRegistro", "Seleccione")
        }
        llenarComboTipoRegistro(monedas, "cboTipoRegistroM", "Seleccione")

    });
}

function llenarComboTipoRegistro(lista, idCombo, primerItem) {
    var contenido = "";
    if (primerItem != null) contenido = "<option value='0'>" + primerItem + "</option>";
    var nRegistros = lista.length;
    var nCampos;
    var campos;
    for (var i = 0; i < nRegistros; i++) {

        if (lista.length > 0) { contenido += "<option value='" + lista[i].IdTipoRegistro + "'>" + lista[i].NombTipoRegistro + "</option>"; }
        else { }
    }
    var cbo = document.getElementById(idCombo);
    if (cbo != null) cbo.innerHTML = contenido;
    //  $("#cboMoneda").val(1);
}
function CargarSemana(Tipo = 0) {
    let tipo = 0;
    let obra = 0;
    if (Tipo != 0) {
        tipo = $('#cboTipoRegistro').val();
        obra = $('#cboObra').val();
    } else {
        tipo = $('#cboTipoRegistroM').val();
        obra = $('#cboObraM').val();
    }
    $.ajaxSetup({ async: false });
    $.post("/TipoRegistro/ObtenerSemanaAjax", { estado: 1, IdTipoRegistro: +tipo, IdObra: obra }, function (data, status) {
        let monedas = JSON.parse(data);
        if (Tipo != 0)
            llenarComboSemana(monedas, "cboSemana", "Seleccione");
        else
            llenarComboSemana(monedas, "cboSemanaM", "Seleccione");
    });
}

function llenarComboSemana(lista, idCombo, primerItem) {
    var contenido = "";
    if (primerItem != null) contenido = "<option value='0'>" + primerItem + "</option>";
    var nRegistros = lista.length;
    var nCampos;
    var campos;
    for (var i = 0; i < nRegistros; i++) {

        if (lista.length > 0) { contenido += "<option value='" + lista[i].IdSemana + "'>" + lista[i].Descripcion + "</option>"; }
        else { }
    }
    var cbo = document.getElementById(idCombo);
    if (cbo != null) cbo.innerHTML = contenido;
    //$("#cboMoneda").val(1);
}

function CargarEstados() {

    $.ajaxSetup({ async: false });
    $.post("/Estados/ObtenerEstados", { Modulo: 2 }, function (data, status) {
        let monedas = JSON.parse(data);
        llenarComboEstados(monedas, "cboEstado", "TODOS");

    });
}

function llenarComboEstados(lista, idCombo, primerItem) {
    var contenido = "";
    if (primerItem != null) contenido = "<option value='0'>" + primerItem + "</option>";
    var nRegistros = lista.length;
    var nCampos;
    var campos;
    for (var i = 0; i < nRegistros; i++) {

        if (lista.length > 0) { contenido += "<option value='" + lista[i].Id + "'>" + lista[i].Descripcion + "</option>"; }
        else { }
    }
    var cbo = document.getElementById(idCombo);
    if (cbo != null) cbo.innerHTML = contenido;
    //$("#cboMoneda").val(1);
}

function CargarEstadosGiro(IdGiro = 0, IdEstadoGiro = 0) {

    $.ajaxSetup({ async: false });
    $.post("/Estados/ObtenerEstadoUsuario", function (data, status) {
        let estado = JSON.parse(data);
        //if()
        if (estado.Id > 0) {
            $("#cboEstadosGiroCol").show();
            $.post("/Estados/ObtenerEstadosUsuario", { IdGiro: IdGiro }, function (data, status) {
                let monedas = JSON.parse(data);
                //if()
                llenarComboEstadosGiro(monedas, "cboEstadosGiro", "Seleccione", IdEstadoGiro);
            });
        }
        else {
            $("#cboEstadosGiroCol").hide();
        }

    });

}

function llenarComboEstadosGiro(lista, idCombo, primerItem, IdEstadoGiro) {
    var contenido = "";
    if (primerItem != null) contenido = "<option value='0'>" + primerItem + "</option>";
    var nRegistros = lista.length;
    var nCampos;
    var campos;
    for (var i = 0; i < nRegistros; i++) {

        if (lista.length > 0) { contenido += "<option value='" + lista[i].Id + "'>" + lista[i].Descripcion + "</option>"; }
        else { }
    }
    var cbo = document.getElementById(idCombo);
    if (cbo != null) cbo.innerHTML = contenido;
    $("#cboEstadosGiro").val(IdEstadoGiro).change();
}

function escapeTags(str) {
    return String(str)
        .replace(/&/g, '&amp;')
        .replace(/"/g, '&quot;')
        .replace(/</g, '&lt;').replace(/>/g, '&gt;');
}

function cargarUpload() {
    var idbtn = document.getElementById('uploadBtn'),
        progressBar = document.getElementById('progressBar'),
        progressOuter = document.getElementById('progressOuter'),
        msgBox = document.getElementById('msgBox');

    var uploader = new ss.SimpleUpload({
        button: idbtn,
        url: 'UpLoadFile',
        name: 'uploadfile',
        multipart: true,
        hoverClass: 'hover',
        multiple: true,
        focusClass: 'focus',

        responseType: 'json',
        startXHR: function () {
            progressOuter.style.display = 'block';
            this.setProgressBar(progressBar);
        },

        onSubmit: function () {
            msgBox.innerHTML = '';
            idbtn.innerHTML = 'Cargando...';
        },

        onComplete: function (filename, response) {
            idbtn.innerHTML = 'Examinar';
            progressOuter.style.display = 'none';

            if (!response) {
                msgBox.innerHTML = 'Inactiva la carga del archivo';
                return;
            }

            if (response.success === true) {
                msgBox.style.display = 'block';
                msgBox.innerHTML = '<strong><a href="/Requerimiento/' + escapeTags(filename) + '"target="_blank" > ' + escapeTags(filename) + '</a></strong> ';
                nameFile = filename;


            } else {
                if (response.msg) {
                    msgBox.innerHTML = escapeTags(response.msg);

                } else {
                    msgBox.innerHTML = 'Hay un error en la carga del archivo.';
                }
            }
        },
        onError: function () {
            progressOuter.style.display = 'none';
            msgBox.innerHTML = 'Hay un error con el plugin';
        }
    });
}

function onchangeTipoRegistro() {
    CargarSemana();
}
function onchangeTipoRegistroM() {
    CargarSemana(2);
    ObtenerGiros();
}
function onchangeObra() {

    ObtenerGiros();
}
function onchangeSemana() {

    ObtenerGiros();
}
function onchangeEstado() {

    ObtenerGiros();
}

function onchangeObra() {
    CargarSemana();

}


window.onload = function () {
    $("#cboEstadosGiroCol").hide();
    cargarUpload();

    CargarObra();
    CargarTipoRegistro();
    CargarSemana();
    CargarEncargado();
    CargarSolicitante();
    CargarProveedor();
    ObtenerGiros();
    CargarMoneda();
    CargarTipoDocumento();
    CargarEstados();
    KeyPressNumber($("#txtMonto"));


    $("#rdProveedor").prop('checked', true).click();
    $("#divSolicitante").hide();
};




function ModalNuevo() {
    $("#lblTituloModal").html("Giro Nuevo");
    AbrirModal("modal-form");
    nameFile = "";
    $("#cboEstadosGiroCol").hide();

    CargarProveedor();
    $("#cboTipoRegistroM").val("4").change();

    $("#IdProveedor").select2();
    $("#btnGrabarCabecera").prop("disabled", true);

    if (tableDetalle) {
        tableDetalle.destroy();
    }

}

function Guardar() {


    let tipo = 0;
    if ($('#rdProveedor')[0].checked) {
        tipo = 2;
    }
    if ($('#rdEmpleado')[0].checked) {
        tipo = 3;
    }

    let IdEncargado = $("#cboEncargado").val();
    let IdSocilitante = $("#cboSolicitante").val();
    let IdObra = $("#cboObraM").val();
    let IdTipoRegistro = $("#cboTipoRegistroM").val();
    let IdSemana = $("#cboSemanaM").val();
    let IdEstadoGiro = $("#cboEstadosGiro").val();



    if (IdObra == 0 || IdObra == null) {
        Swal.fire(
            'Error!',
            'Complete el campo de Obra',
            'error'
        )
        return;
    }
    if (IdTipoRegistro == 0 || IdTipoRegistro == null) {
        Swal.fire(
            'Error!',
            'Complete el campo de Tipo Registro',
            'error'
        )
        return;
    }
    if (IdSemana == 0 || IdSemana == null) {
        Swal.fire(
            'Error!',
            'Complete el campo de Semana',
            'error'
        )
        return;
    }

    if ($("#rdEmpleado").is(':checked')) {
        if (IdSocilitante == 0 || IdSocilitante == null) {
            Swal.fire(
                'Error!',
                'Complete el campo de Solci',
                'error'
            )
            return;
        }
    }


    if ($("#rdProveedor").is(':checked')) {
        if (IdEncargado == 0 || IdEncargado == null) {
            Swal.fire(
                'Error!',
                'Complete el campo de Encargado',
                'error'
            )
            return;
        }
    }







    let ArrayGeneral = new Array();
    let arrayIdId = new Array();
    let arrayIdProveedor = new Array();
    let arrayTipoDocumento = new Array();
    let arrayDescripcionDocumento = new Array();
    let arrayNumeroDocumento = new Array();
    let arrayIdMoneda = new Array();
    let arrayMonto = new Array();
    let arrayComentario = new Array();
    let arrayAnexo = new Array();


    $("input[name='txtId[]']").each(function (indice, elemento) {
        arrayIdId.push($(elemento).val());
    });
    $("input[name='txtIdProveedor[]']").each(function (indice, elemento) {
        arrayIdProveedor.push($(elemento).val());
    });
    $("input[name='txtIdTipoDocumento[]']").each(function (indice, elemento) {
        arrayTipoDocumento.push($(elemento).val());
    });
    $("input[name='txtNumeroDocumento[]']").each(function (indice, elemento) {
        arrayNumeroDocumento.push($(elemento).val());
    });
    $("input[name='txtIdMoneda[]']").each(function (indice, elemento) {
        arrayIdMoneda.push($(elemento).val());
    });
    $("input[name='txtMonto[]']").each(function (indice, elemento) {
        arrayMonto.push($(elemento).val());
    });
    $("input[name='txtComentario[]']").each(function (indice, elemento) {
        arrayComentario.push($(elemento).val());
    });
    $("input[name='txtAnexo[]']").each(function (indice, elemento) {
        arrayAnexo.push($(elemento).val());
    });



    for (var i = 0; i < arrayIdProveedor.length; i++) {
        ArrayGeneral.push({
            'IdGiroDetalle': arrayIdId[i],
            'IdProveedor': arrayIdProveedor[i],
            'IdTipoDocumento': arrayTipoDocumento[i],
            'NroDocumento': arrayNumeroDocumento[i],
            'IdMoneda': arrayIdMoneda[i],
            'Monto': arrayMonto[i],
            'Comentario': arrayComentario[i],
            'Anexo': arrayAnexo[i]
        });
    }




    $.post('UpdateInsertGiro', {
        'IdGiro': +$("#txtId").val(),
        'IdObra': +IdObra,
        'Tipo': tipo,
        'IdTipoRegistro': +IdTipoRegistro,
        'IdSemana': +IdSemana,
        'IdResponsable': +IdEncargado,
        'IdSolicitante': IdSocilitante,
        'IdEstadoGiro': IdEstadoGiro,
        'Estado': true,
        'DetalleGiro': ArrayGeneral
    }, function (data, status) {
        if (data != "") {
            swal("Exito!", "Proceso Realizado Correctamente", "success")
            let format = data.split('-')
            $("#txtId").val(format[0]);
            var id = $("#txtId").val();
            //CargarEstadosGiro(id, format[1])
            $("#btnGrabarCabecera").html("Grabar");

            //limpiarDatos();
            //limpiarDatosDetalle();

            $("#tabla_detalle").find('tbody').empty();
            $("#cboObraM").val("0");
            $("#cboSemanaM").val("0");
            $("#rdProveedor").prop('checked', false);
            $("#rdEmpleado").prop('checked', false);
            $("#txtId").val("");
            $("#IdProveedor").val("0").change();
            $("#cboTipoDocumento").val("0");
            $("#txtNroDocumento").val("");
            $("#cboMoneda").val("0");
            $("#txtComentario").val("");
            $("#cboSolicitante").val("0").change();
            $("#cboEncargado").val("0").change();
            $("#txtMonto").val("");
            $("#msgBox").text("");

        } else {
            swal("Error!", "Ocurrio un Error")

        }
        ObtenerGiros();

    });
}


function GuardarDetalle() {
    let IdProveedor = $("#IdProveedor").val();
    let NombreProveedor = $("#IdProveedor").find('option:selected').text();
    let cboTipoDocumento = $("#cboTipoDocumento").val();
    let DescripcionTipoDocumento = $("#cboTipoDocumento").find('option:selected').text();
    let NroDocumento = $("#txtNroDocumento").val();
    let moneda = $("#cboMoneda").val();
    let Descripcionmoneda = $("#cboMoneda").find('option:selected').text();
    let monto = quitarFormato($("#txtMonto").val());
    let Comentario = $("#txtComentario").val();
    let Estado = true;


    //if (+$("#txtId").val() == 0 || $("#txtId").val() == null) {
    //    Swal.fire(
    //        'Error!',
    //        'Aun  no se tiene un Giro creado',
    //        'error'
    //    )
    //    return;
    //}
    let arrayIdProveedor = new Array();
    let arrayNumeroDocumento = new Array();


    if ($('table#tabla_detalle tbody tr').length > 0) {

        $("input[name='txtIdProveedor[]']").each(function (indice, elemento) {
            arrayIdProveedor.push($(elemento).val());
        });

        $("input[name='txtNumeroDocumento[]']").each(function (indice, elemento) {
            arrayNumeroDocumento.push($(elemento).val());
        });

    }

    let Validacion = 0;
    for (var i = 0; i < arrayIdProveedor.length; i++) {
        if (arrayIdProveedor[i] == IdProveedor && arrayNumeroDocumento[i] == NroDocumento) {
            Validacion++;
        }
    }

    if (Validacion > 0) {
        Swal.fire(
            'Error!',
            'Los datos ya existen',
            'error'
        )
        return;
    }






    if (IdProveedor == 0 || IdProveedor == null) {
        Swal.fire(
            'Error!',
            'Complete el campo de Proveerdor',
            'error'
        )
        return;
    }
    if (cboTipoDocumento == 0 || cboTipoDocumento == null) {
        Swal.fire(
            'Error!',
            'Complete el campo de Tipo Documento',
            'error'
        )
        return;
    }
    if (NroDocumento.trim() == "" || NroDocumento == null) {
        Swal.fire(
            'Error!',
            'Complete el campo de Número Documento',
            'error'
        )
        return;
    }
    if (moneda == 0 || moneda == null) {
        Swal.fire(
            'Error!',
            'Complete el campo de Moneda',
            'error'
        )
        return;
    }
    if (monto == null) {
        Swal.fire(
            'Error!',
            'Complete el campo de Monto',
            'error'
        )
        return;
    }



    var tr = ``;


    tr += `<tr>
            <td style="display:none"><input name="txtId[]" value="0" hidden/></td>
            <td>`+ NombreProveedor + `<input  name="txtIdProveedor[]" value="` + IdProveedor + `" hidden/><input name="txtNombreProveedor[]" value="` + NombreProveedor + `" hidden/></td>
            <td>`+ DescripcionTipoDocumento + `<input  name="txtIdTipoDocumento[]" value="` + cboTipoDocumento + `" hidden/><input name="txtDescripcionTipoDocumento[]" value="` + DescripcionTipoDocumento + `" hidden/></td>
            <td>`+ NroDocumento + `<input name="txtNumeroDocumento[]" value="` + NroDocumento + `" hidden/></td>
            <td>`+ Descripcionmoneda + `<input name="txtIdMoneda[]" value="` + moneda + `" hidden/><input  name="txtDescripcionMoneda[]" value="` + Descripcionmoneda + `" hidden/></td>
            <td>`+ formatNumber(monto) + `<input name="txtMonto[]" value="` + monto + `" hidden/></td>
            
            <td>`+ Comentario + `<input name="txtComentario[]" value="` + Comentario + `" hidden/></td>
            <td><input hidden name="txtAnexo[]" value="` + escapeTags(nameFile) + `" /><strong><a href="/Requerimiento/` + escapeTags(nameFile) + `"target="_blank" > ` + escapeTags(nameFile) + `</a></strong></td>
            <td><button id="EliminarGiro" class="btn btn-danger btn-xs fa fa-trash juntos" onclick="EliminarGiro(this)"></button></td>
            </tr>`;


    $("#tbody_Detalle").append(tr);

    //tableDetalle = $("#tabla_detalle").dataTable({ "bDestroy": true, language: lenguaje_data });

    $("#btnGrabarCabecera").prop("disabled", false);

    LimpiarDetalle();
    //$.post('UpdateInsertGiroDetalle', {
    //    'IdGiroDetalle': +$("#txtIdDetalle").val(),
    //    'IdGiro': +$("#txtId").val(),
    //    'IdProveedor': +IdProveedor,
    //    'IdTipoDocumento': +cboTipoDocumento,
    //    'NroDocumento': NroDocumento,
    //    'IdMoneda': moneda,
    //    'Monto': monto,
    //    'Comentario': $("#txtComentario").val(),
    //    'Estado': Estado,
    //    'Anexo': nameFile,
    //}, function (data, status) {
    //    if (data > 0) {
    //        swal("Exito!", "Proceso Realizado Correctamente", "success")
    //        LimpiarDetalle();
    //    } else {
    //        swal("Error!", "Ocurrio un Error")
    //        LimpiarDetalle();
    //    }

    //    ObtenerGiroDetalles();
    //    table
    //        .clear()
    //        .draw();
    //    ObtenerGiros();
    //});


}





function formatNumber(num) {
    if (!num || num == 'NaN') return '-';
    if (num == 'Infinity') return '&#x221e;';
    num = num.toString().replace(/\$|\,/g, '');
    if (isNaN(num))
        num = "0";
    sign = (num == (num = Math.abs(num)));
    num = Math.floor(num * 100 + 0.50000000001);
    cents = num % 100;
    num = Math.floor(num / 100).toString();
    if (cents < 10)
        cents = "0" + cents;
    for (var i = 0; i < Math.floor((num.length - (1 + i)) / 3); i++)
        num = num.substring(0, num.length - (4 * i + 3)) + ',' + num.substring(num.length - (4 * i + 3));
    return (((sign) ? '' : '-') + num + '.' + cents);
}



function EliminarGiro(dato) {


    $(dato).closest('tr').remove();

    if ($('table#tabla_detalle tbody tr').length > 0) {
    } else {
        $("#btnGrabarCabecera").prop("disabled", true);
    }
}




function eliminar(IdGiro) {

    alertify.confirm('Confirmar', '¿Desea eliminar este Giro?', function () {
        $.post("EliminarGiro", { 'IdGiro': IdGiro }, function (data) {

            if (data == 0) {
                swal("Error!", "Ocurrio un Error")
                limpiarDatos();
            } else {
                swal("Exito!", "Giro  Eliminado", "success")

            }
            ObtenerGiros();
            closePopup();
        });

    }, function () { });


}


function limpiarDatosDetalle() {
    $("#txtIdDetalle").val("");
    $("#IdProveedor").val("0");
    $("#cboTipoDocumento").val("");
    $("#txtNroDocumento").val("");
    $("#cboMoneda").val(1).change();
    $("#txtMonto").val("");
    $("#txtComentario").val("");
    filenamereset(false);
    nameFile = "";


}
function limpiarDatos() {
    $("#cboEstadosGiroCol").hide();
    nameFile = ""
    $("#txtId").val("");
    $("#txtIdDetalle").val("");
    $("#cboObraM").val("");
    $("#cboTipoRegistroM").val("");
    $("#CboSemanaM").val("");
    $("#cboEncargado").val("0");
    $("#cboSolicitante").val("0");
    $("#rdProveedor").prop('checked', false);
    $("#rdEmpleado").prop('checked', false);
    $("#cboObraM").empty();
    $("#cboTipoRegistroM").empty();
    $("#CboSemanaM").empty();
    $("#cboEncargado").empty();
    $("#cboSolicitante").empty();
    CargarEstadosGiro();
    CargarTipoRegistro(2);
    CargarObra(2);
    CargarSemana();
    CargarSolicitante();
    CargarEncargado();
    filenamereset(false);
    if (tableDetalle) {
        tableDetalle
            .clear()
            .draw();
    }


}
function cerrarModal() {
    let IdGiro = +$("#txtId").val();

    if (IdGiro > 0 && !VerificionGiroDetalles()) {
        alertify.confirm('Confirmar', '¿Desea Salir, El Giro no tiene detalle, se Eliminara la cabecera  ?', function () {
            $.ajaxSetup({ async: false });
            $.post("EliminarGiro", { 'IdGiro': IdGiro }, function (data) {
                ObtenerGiros();
                closePopup();
            });
            closePopup();
            limpiarDatos();
            limpiarDatosDetalle();

        }, function () {

            $("#IdProveedor").focus();
        });
    }
    else {
        closePopup();
        limpiarDatos();
        limpiarDatosDetalle();
    }


}
function ObtenerGiros() {
    table = $('#table_id').dataTable({
        language: lenguaje_data,
        responsive: true,
        ajax: {
            url: 'ObtenerGiro',
            type: 'POST',
            data: {
                pagination: {
                    perpage: 50,
                },
                Idobra: +$("#cboObra").val(),
                IdTipoRegistro: +$("#cboTipoRegistro").val(),
                IdSemana: +$("#cboSemana").val(),
                IdEstadoGiro: +$("#cboEstado").val(),
            },
        },

        columnDefs: [
            { "targets": "_all" },
            {
                targets: -1,
                orderable: false,
                width: "100px",
                render: function (data, type, full, meta) {
                    let button = `
                                  <button class="btn btn-danger editar juntos  fa fa-edit btn-xs " onclick="ObtenerDatosxID(` + full.IdGiro + `)"></button>
                                  <button class="btn btn-danger borrar btn-xs fa fa-trash " onclick="eliminar(` + full.IdGiro + `)"></button>`;
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

                    return full.NombSerie + '-' + full.Correlativo
                },
            },
            {
                targets: 2,
                orderable: false,
                render: function (data, type, full, meta) {

                    return full.Obra
                },
            },
            {
                targets: 3,
                orderable: false,
                render: function (data, type, full, meta) {
                    return full.Semana
                },
            },
            {
                targets: 4,
                orderable: false,
                render: function (data, type, full, meta) {
                    let OrdenGiro = "";
                    if (full.Responsable == "") {
                        OrdenGiro = full.Solicitante
                    } else {
                        OrdenGiro = full.Responsable
                    }
                    return OrdenGiro
                },
            },
            {
                targets: 5,
                orderable: false,
                render: function (data, type, full, meta) {
                    return full.Fecha.split('T')[0]
                },
            },
            {
                targets: 6,
                orderable: false,
                render: function (data, type, full, meta) {
                    return formatNumber(full.MontoSoles)
                },
            },
            {
                targets: 7,
                orderable: false,
                render: function (data, type, full, meta) {
                    return formatNumber(full.MontoDolares)
                },
            },
            {
                targets: 8,
                orderable: false,
                render: function (data, type, full, meta) {
                    return (full.EstadoGiro).toUpperCase()

                },
            },
            {
                targets: 9,
                orderable: false,
                render: function (data, type, full, meta) {
                    let button = `<button class="btn btn-sm-red estados fa fa-th-list fa-lg " onclick="modalHistorialEstado(` + full.IdGiro + `)"></button>`;
                    //`<button class="btn btn-danger btn-xs fa fa-trash" onclick="eliminar(` + full[i].IdSerie + `)"></button>`;
                    return button;

                },
            },
            





        ],
        "bDestroy": true
    }).DataTable();
}

function ObtenerGiroDetalles() {
    let varIdGiro = +$("#txtId").val();
    tableDetalle = $('#tabla_detalle').dataTable({
        language: lenguaje_data,
        responsive: true,
        ajax: {
            url: 'ObtenerGiroDetalle',
            type: 'POST',
            data: {
                "IdGiro": varIdGiro,
                pagination: {
                    perpage: 50,
                },
            },
        },

        columnDefs: [
            { "targets": "_all" },
            {
                targets: -1,
                width: "100px",
                orderable: false,
                render: function (data, type, full, meta) {
                    //let button = `<button class="btn btn-danger editar  fa fa-edit btn-xs " onclick="ObtenerGiroDetallexID(` + full.IdGiroDetalle + `)"></button><button class="btn btn-danger borrar btn-xs fa fa-trash juntos" onclick="EliminarDetalle(` + full.IdGiroDetalle + `)"></button>`;
                    //`<button class="btn btn-danger btn-xs fa fa-trash" onclick="eliminar(` + full[i].IdSerie + `)"></button>`;
                    let button = `<button class="btn btn-danger borrar btn-xs fa fa-trash juntos" onclick="EliminarDetalle(` + full.IdGiroDetalle + `)"></button>`;
                    return button;

                },
            },
            {
                targets: 0,
                orderable: false,
                render: function (data, type, full, meta) {
                    let td = `<input hidden name="txtIdProveedor[]" value="` + full.IdProveedor + `"/><input hidden name="txtId[]" value="` + full.IdGiroDetalle + `"/>`;
                    return full.Proveedor + td
                },
            },

            {
                targets: 1,
                orderable: false,
                render: function (data, type, full, meta) {
                    let td = `<input hidden name="txtIdTipoDocumento[]" value="` + full.IdTipoDocumento + `" />`;
                    return full.TipoDocumento + td
                },
            },

            {
                targets: 2,
                orderable: false,
                render: function (data, type, full, meta) {
                    let td = `<input hidden name="txtNumeroDocumento[]" value="` + full.NroDocumento + `" />`;
                    return full.NroDocumento + td
                },
            },
            {
                targets: 3,
                orderable: false,
                render: function (data, type, full, meta) {
                    let td = `<input hidden name="txtIdMoneda[]" value="` + full.IdMoneda + `" />`;
                    return full.Moneda + td
                },
            },
            {
                targets: 4,
                orderable: false,
                render: function (data, type, full, meta) {
                    let td = `<input hidden name="txtMonto[]" value="` + full.Monto + `" />`;
                    return formatNumber(full.Monto) + "" + td
                },
            },

            {
                targets: 5,
                orderable: false,
                render: function (data, type, full, meta) {
                    let td = `<input hidden name="txtComentario[]" value="` + full.Comentario + `" />`;
                    return full.Comentario + td
                },
            },
            {
                targets: 6,
                orderable: false,
                render: function (data, type, full, meta) {
                    return '<input hidden name="txtAnexo[]" value="' + escapeTags(full.Documento) + '" /> <strong><a href="/Requerimiento/' + escapeTags(full.Documento) + '"target="_blank" > ' + escapeTags(full.Documento) + '</a></strong> ';

                },
            },




        ],
        "bDestroy": true
    }).DataTable();
}

function VerificionGiroDetalles() {

    let flag = false;
    $.ajaxSetup({ async: false });
    $.post("ObtenerGiroDetalle", { IdGiro: +$("#txtId").val() }, function (data, status) {
        let monedas = JSON.parse(data);
        flag = monedas.iTotalRecords > 0

    });
    return flag;
}

const formatear = f => {
    const año = f.getFullYear();
    const mes = ("0" + (f.getMonth() + 1)).substr(-2);
    const dia = ("0" + f.getDate()).substr(-2);
    return `${año}-${mes}-${dia}`
}



function ObtenerDatosxID(Id) {


    $("#lblTituloModal").html("Editar Giro");
    AbrirModal("modal-form");

    $("#IdProveedor").select2();


    $.post('ObtenerGiroxID', {
        'IdGiro': Id,
    }, function (data, status) {

        if (data == "Error") {
            swal("Error!", "Ocurrio un error")
            limpiarDatos();
        } else {
            $("#txtId").val(Id);
            ObtenerGiroDetalles();


            let dato = JSON.parse(data);
            CargarEstadosGiro(Id, dato.IdEstadoGiro);
            console.log(dato);
            CargarEncargado(2);
            CargarSolicitante(2);

            if (dato.Tipo == 2) {
                $("#rdProveedor").prop('checked', true);
                $("#rdEmpleado").prop('checked', false);
                $('#rdProveedor').click()
            } else {
                $("#rdEmpleado").prop('checked', true);
                $("#rdProveedor").prop('checked', false);
                $('#rdEmpleado').click()
            }
            //console.log(usuarios);

            $("#cboSolicitante").val(dato.IdSolicitante).trigger('change.select2')
            $("#cboEncargado").val(dato.IdResponsable).trigger('change.select2');
            $("#cboObraM").val(dato.IdObra).trigger('change.select2');
            $("#cboTipoRegistroM").val(dato.IdTipoRegistro).change();;
            $("#cboSemanaM").val(dato.IdSemana);
            $("#btnGrabarCabecera").html("Grabar");


        }

    });


    $("#cboEstadosGiroCol").hide();


}


function ObtenerGiroDetallexID(Id) {


    $.post('ObtenerGiroDetallesxID', {
        'IdGiroDetalle': Id,
    }, function (data, status) {

        if (data == "Error") {
            swal("Error!", "Ocurrio un error")
            limpiarDatos();
        } else {
            $("#txtIdDetalle").val(Id);
            let dato = JSON.parse(data);
            CargarGiroDetalle(dato);
            $("#guardarDetalle").html("Grabar");


        }

    });

}

function LimpiarDetalle() {

    limpiarDatosDetalle();
    $("#guardarDetalle").html("Agregar");

}
function EliminarDetalle(IdGiroDetalle) {

    alertify.confirm('Confirmar', '¿Desea eliminar este Detalle?', function () {
        $.post("EliminarGiroDetalle", { 'IdGiroDetalle': IdGiroDetalle }, function (data) {



            if (data == 0) {
                swal("Error!", "Ocurrio un Error")
                limpiarDatos();
            } else {
                swal("Exito!", "Detalle  Eliminado", "success")

            }
            ObtenerGiroDetalles();
            table
                .clear()
                .draw();
            ObtenerGiros();
        });

    }, function () { });
    limpiarDatosDetalle();

    $("#guardarDetalle").html("Agregar");

}

function CargarGiroDetalle(dato) {

    $("#IdProveedor").val(dato.IdProveedor);
    $("#cboMoneda").val(dato.IdMoneda);
    $("#cboTipoDocumento").val(dato.IdTipoDocumento);
    $("#txtNroDocumento").val(dato.NroDocumento);
    $("#txtMonto").val(FormatMiles(dato.Monto));
    $("#txtComentario").val(dato.Comentario);
    filenamereset(true, dato);
    nameFile = dato.Anexo;

}
function filenamereset(flag, dato = null) {

    let msgBox = document.getElementById('msgBox');
    if (flag) {
        msgBox.style.display = 'block';
        msgBox.innerHTML = '<strong><a href="/Requerimiento/' + escapeTags(dato.Anexo) + '"target="_blank" > ' + escapeTags(dato.Anexo) + '</a></strong> ';

    } else {
        msgBox.style.display = 'none';
        msgBox.innerHTML = '';

    }

}





function modalHistorialEstado(IdGiro) {
    $("#ModalHistorialEstados").modal();
    $("#div_modelos_aprobaciones").html('');

    //console.log(IdGiro);
    //return;

    let tablee = "";
    $.post("DatosSolicitudModeloAprobaciones", { 'IdGiro': IdGiro }, function (data, status) {
        let datos = JSON.parse(data);
        //console.log(datos);

        //return;

        if (datos.ListGiroModelo.length > 0) {
            for (var i = 0; i < datos.ListGiroModelo.length; i++) {
                tablee += ` <p>ETAPA:` + (i + 1) + ` </p>
                        <table class="table" id="tabla_modal_estado">
                                        <thead>
                                            <tr>
                                                <th>Proveedor</th>
                                                <th>NroDocumento</th>
                                                <th>Aprobador</th>
                                                <th>Aprobacion</th>
                                            </tr>
                                        </thead>
                                    <tbody>`;
                if (datos.ListGiroModelo[i].ListModeloAprobacionesDTO.length > 0) {
                    console.log(datos.ListGiroModelo[i].ListModeloAprobacionesDTO)

                    for (var j = 0; j < datos.ListGiroModelo[i].ListModeloAprobacionesDTO.length; j++) {
                        tablee += `<tr>
                            <td>`+ datos.ListGiroModelo[i].ListModeloAprobacionesDTO[j].Proveedor + `</td>
                            <td>`+ datos.ListGiroModelo[i].ListModeloAprobacionesDTO[j].NroDocumento + `</td>
                            <td>`+ datos.ListGiroModelo[i].ListModeloAprobacionesDTO[j].Autorizador.toUpperCase() + `</td>
                           <td>`+ datos.ListGiroModelo[i].ListModeloAprobacionesDTO[j].NombEstado + `</td>
                            </tr>`;
                    }
                }
                tablee += `</tbody></table>`;
                tablee += `</br>`;
            }

        }
        $("#div_modelos_aprobaciones").html(tablee);
        //console.log(datos.ListSolicitudRqModelo);
    })
}


function RerporteGiroSemana(IdSemana, IdObra) {
    $.ajaxSetup({ async: false });
   
    $.post("GenerarReporteSemana", { 'NombreReporte': 'GiroSemanaObra', 'Formato': 'PDF', 'IdSemana': IdSemana, 'IdObra': IdObra }, function (data, status) {
        let datos;
        if (validadJson(data)) {
            let datobase64;
            datobase64 = "data:application/octet-stream;base64,"
            datos = JSON.parse(data);
        
            //datobase64 += datos.Base64ArchivoPDF;
            //$("#reporteRPT").attr("download", 'Reporte.' + "pdf");
            //$("#reporteRPT").attr("href", datobase64);
            //$("#reporteRPT")[0].click();
            verBase64PDF(datos)
        } else {
            respustavalidacion
        }
    });
}

function validadJson(json) {
    try {
        object = JSON.parse(json);
        return true;
    } catch (error) {
        return false;
    }
}

function verBase64PDF(datos) {

    //var b64 = "JVBERi0xLjcNCiWhs8XXDQoxIDAgb2JqDQo8PC9QYWdlcyAyIDAgUiAvVHlwZS9DYXRhbG9nPj4NCmVuZG9iag0KMiAwIG9iag0KPDwvQ291bnQgMS9LaWRzWyA0IDAgUiBdL1R5cGUvUGFnZXM+Pg0KZW5kb2JqDQozIDAgb2JqDQo8PC9DcmVhdGlvbkRhdGUoRDoyMDIyMDkyODE2NDAzMCkvQ3JlYXRvcihQREZpdW0pL1Byb2R1Y2VyKFBERml1bSk+Pg0KZW5kb2JqDQo0IDAgb2JqDQo8PC9Db250ZW50cyA1IDAgUiAvTWVkaWFCb3hbIDAgMCA2MTIgNzkyXS9QYXJlbnQgMiAwIFIgL1Jlc291cmNlczw8L0ZvbnQ8PC9GMSA2IDAgUiA+Pi9Qcm9jU2V0IDcgMCBSID4+L1R5cGUvUGFnZT4+DQplbmRvYmoNCjUgMCBvYmoNCjw8L0ZpbHRlci9GbGF0ZURlY29kZS9MZW5ndGggMzExPj5zdHJlYW0NCnicvZPNasMwEITvBr/DHFtot7LiH/mYtMmhECjU9C5i2VGw5WDJpfTpayckhUCDSqGSDjsHfcPOshzPYbAowoBhun0dBg+rCIzxDEUVBklGsyxhyDgnLhhDUYbBDeZ41e2+UXh5WmGlx+IWxS4MlsWRdmRE7MBIc+LJ+DUVglImToxiqy3GJ2Fb2TQoVdsZ63rpdGdA+7JCNZHvvdhpTBmLT+zdYB2qrsdgFbSB2yq86d4NssFabbbS6I2FG1zXa9lYwrrrFZz6cIS5KdFO0sc24ZQl/GR7AfCRPiZckIjPuf0K/5P0sY1SEnl6tbfFmJ+p7/A5HR9zH18WUx6fR/nnVr1zTnJOec79cvbhjQUT/z63ZNzZaHZ9bhdy+a7MQRMeO+O0GVSJcQn3slbgIKJv3y8shB6ADQplbmRzdHJlYW0NCmVuZG9iag0KNiAwIG9iag0KPDwvQmFzZUZvbnQvSGVsdmV0aWNhL0VuY29kaW5nL1dpbkFuc2lFbmNvZGluZy9OYW1lL0YxL1N1YnR5cGUvVHlwZTEvVHlwZS9Gb250Pj4NCmVuZG9iag0KNyAwIG9iag0KWy9QREYvVGV4dF0NCmVuZG9iag0KeHJlZg0KMCA4DQowMDAwMDAwMDAwIDY1NTM1IGYNCjAwMDAwMDAwMTcgMDAwMDAgbg0KMDAwMDAwMDA2NiAwMDAwMCBuDQowMDAwMDAwMTIyIDAwMDAwIG4NCjAwMDAwMDAyMDkgMDAwMDAgbg0KMDAwMDAwMDM0MyAwMDAwMCBuDQowMDAwMDAwNzI2IDAwMDAwIG4NCjAwMDAwMDA4MjUgMDAwMDAgbg0KdHJhaWxlcg0KPDwNCi9Sb290IDEgMCBSDQovSW5mbyAzIDAgUg0KL1NpemUgOC9JRFs8NEY2MkQwQTkwNDlFOUM1N0NGQzRCODEzRTVCNjhDNUI+PDRGNjJEMEE5MDQ5RTlDNTdDRkM0QjgxM0U1QjY4QzVCPl0+Pg0Kc3RhcnR4cmVmDQo4NTUNCiUlRU9GDQo=";
    var b64 = datos.Base64ArchivoPDF;
    // aquí convierto el base64 en caracteres
    var characters = atob(b64);
    // aquí convierto todo a un array de bytes usando el codigo de cada caracter:
    var bytes = new Array(characters.length);
    for (var i = 0; i < characters.length; i++) {
        bytes[i] = characters.charCodeAt(i);
    }
    // en este punto ya tengo un array de bytes,
    // (supongo que es algo similar a lo que te llega de respuesta)
    // el siguiente paso sería convertir este array en un typed array
    // para construir el blob correctamente:
    var chunk = new Uint8Array(bytes);

    // se construye el blob con el mime type respectivo
    var blob = new Blob([chunk], {
        type: 'application/pdf'
    });

    // se crea un object url con el blob para usarlo:
    var url = URL.createObjectURL(blob);

    // y de esta manera simplemente lo abro en una nueva ventana:
    window.open(url, '_blank');
}