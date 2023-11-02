let table = '';


window.onload = function () {
    var url = "ObtenerNumeracion";
    ConsultaServidor(url);
    CargarBase();
    CargarTipoDocumento();
};


function ConsultaServidor(url) {

    $.post(url, function (data, status) {

        //console.log(data);
        if (data == "error") {
            table = $("#table_id").DataTable(lenguaje);
            return;
        }

        let tr = '';

        let division = JSON.parse(data);
        let total_division = division.length;

        for (var i = 0; i < division.length; i++) {
            let estado = division[i].Estado ? "Activo" : "Desactivado";

            tr += '<tr>' +
                '<td>' + (i + 1) + '</td>' +
                '<td>' + division[i].Base.toUpperCase() + '</td>' +
                '<td>' + division[i].TipoDocumento + '</td>' +
                '<td>' + division[i].Serie.toUpperCase() + '</td>' +
                '<td>' + division[i].NumeracionInicial + '</td>' +
                '<td>' + estado + '</td>' +
                '<td><button class="btn btn-primary fa fa-pencil btn-xs" onclick="ObtenerDatosxID(' + division[i].IdNumeracionDocumento + ')"></button>' +
                '<button class="btn btn-danger btn-xs  fa fa-trash" onclick="eliminar(' + division[i].IdNumeracionDocumento + ')"></button></td >' +
                '</tr>';
        }

        $("#tbody_numeracion").html(tr);
        $("#spnTotalRegistros").html(total_division);

        table = $("#table_id").DataTable(lenguaje);

    });

}




function ModalNuevo() {
    $("#lblTituloModal").html("Nueva Glosa Contable");
    AbrirModal("modal-form");
    $("#chkActivo").prop('checked', true)
}

function GuardarNumeracion() {
    let varIdNumeracion= $("#txtId").val();
    let varIdBase = $("#cboBase").val();
    let varIdTipoDocumento= $("#cboTipoDocumento").val();
    let varSerie = $("#txtSerie").val();
    let varNumeracionInicial = $("#txtNumeracionInicial").val(); 
    let varEstado = false;
    if ($('#chkActivo')[0].checked) {
        varEstado = true;
    }
    //Validaciones
    if (varIdBase == 0 || varIdBase == null) {
        Swal.fire(
            'Error!',
            'Complete el campo de Base',
            'error'
        )
        return;
    } if (varIdTipoDocumento == 0 || varIdTipoDocumento == null) {
        Swal.fire(
            'Error!',
            'Complete el campo de Tipo Documento',
            'error'
        )
        return;
    }
    if (varSerie == '' || varSerie == null) {
        Swal.fire(
            'Error!',
            'Complete el campo de Serie',
            'error'
        )
        return;
    }
    if (varNumeracionInicial == '' || varNumeracionInicial == null) {
        Swal.fire(
            'Error!',
            'Complete el campo de Numeracion Inicial',
            'error'
        )
        return;
    }


    $.post('UpdateInsertNumeracion', {
        'IdNumeracionDocumento': varIdNumeracion,
        'IdBase': varIdBase,
        'IdTipoDocumento': varIdTipoDocumento,
        'Serie': varSerie,
        'NumeracionInicial': varNumeracionInicial,
        'Estado': varEstado
    }, function (data, status) {

        if (data == 1) {
            swal("Exito!", "Proceso Realizado Correctamente", "success")
            table.destroy();
            ConsultaServidor("ObtenerNumeracion");
            limpiarDatos();

        } else {
            swal("Error!", "Ocurrio un Error")
            limpiarDatos();
        }

    });
}

function ObtenerDatosxID(varIdNumeracion) {
    $("#lblTituloModal").html("Editar Glosa Contanble");
    AbrirModal("modal-form");
    $("#txtId").val(varIdNumeracion);
    //console.log(varIdUsuario);

    $.post('ObtenerDatosxID', {
        'IdNumeracionDocumento': varIdNumeracion,
    }, function (data, status) {

        if (data == "Error") {
            swal("Error!", "Ocurrio un error")
            limpiarDatos();
        } else {
            let division = JSON.parse(data);
            //console.log(usuarios);
            $("#cboBase").val(division[0].IdBase);
            $("#cboTipoDocumento").val(division[0].IdTipoDocumento);
 
            $("#txtSerie").val(division[0].Serie);
            $("#txtNumeracionInicial").val(division[0].NumeracionInicial);
            if (division[0].Estado) {
                $("#chkActivo").prop('checked', true);
            }

        }

    });

}

function eliminar(varIdNumeracion) {


    alertify.confirm('Confirmar', '¿Desea eliminar esta Numeracion De Documento Electronico?', function () {
        $.post("EliminarNumeracion", { 'IdNumeracionDocumento': varIdNumeracion }, function (data) {

            if (data == 0) {
                swal("Error!", "Ocurrio un Error")
                limpiarDatos();
            } else {
                swal("Exito!", "Numeracion De Documento Electronico Eliminada", "success")
                table.destroy();
                ConsultaServidor("ObtenerNumeracion");
                limpiarDatos();
            }

        });

    }, function () { });

}

function CargarBase() {
    $.ajaxSetup({ async: false });
    $.post("/Base/ObtenerBase", { 'estado': 1 }, function (data, status) {
        let tipoRegistros = JSON.parse(data);
        llenarComboBase(tipoRegistros, "cboBase", "Seleccione")
    });
}


function llenarComboBase(lista, idCombo, primerItem) {
    var contenido = "";
    if (primerItem != null) contenido = "<option value='0'>" + primerItem + "</option>";
    var nRegistros = lista.length;
    var nCampos;
    var campos;
    for (var i = 0; i < nRegistros; i++) {

        if (lista.length > 0) { contenido += "<option value='" + lista[i].IdBase + "'>" + lista[i].Descripcion + "</option>"; }
        else { }
    }
    var cbo = document.getElementById(idCombo);
    if (cbo != null) cbo.innerHTML = contenido;
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



function limpiarDatos() {
    $("#txtId").val("");
    $("#cboBase").val(0);
    $("#cboTipoDocumento").val(0);
    $("#txtSerie").val("");
    $("#txtNumeracionInicial").val("");
    $("#chkActivo").prop('checked', false);


}



