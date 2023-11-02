let table = '';


window.onload = function () {
    var url = "ObtenerBase";
    ConsultaServidor(url);
    CargarDivision();
};


function ConsultaServidor(url) {

    $.post(url, function (data, status) {

        //console.log(data);
        if (data == "error") {
            table = $("#table_id").DataTable(lenguaje);
            return;
        }

        let tr = '';

        let base = JSON.parse(data);
        let total_base = base.length;

        for (var i = 0; i < base.length; i++) {


            tr += '<tr>' +
                '<td>' + (i + 1) + '</td>' +
                '<td>' + base[i].Codigo.toUpperCase() + '</td>' +
                '<td>' + base[i].Descripcion.toUpperCase() + '</td>' +
                '<td>' + base[i].Division.toUpperCase() + '</td>' +
                '<td><button  style="margin-top:0px !important;margin-bottom:0px !important;" class="btn btn-primary fa fa-pencil btn-xs" onclick="ObtenerDatosxID(' + base[i].IdBase + ')"></button>' +
                '<button style="margin-top:0px !important;margin-bottom:0px !important;" class="btn btn-danger btn-xs  fa fa-trash" onclick="eliminar(' + base[i].IdBase + ')"></button></td >' +
                '</tr>';
        }

        $("#tbody_base").html(tr);
        $("#spnTotalRegistros").html(total_base);

        table = $("#table_id").DataTable(lenguaje);

    });

}




function ModalNuevo() {
    $("#chkActivo").prop('checked', true)
    $("#lblTituloModal").html("Nueva Base");
    AbrirModal("modal-form");
}




function GuardarBase() {
    let varIdBase = $("#txtId").val();
    let varCodigo = $("#txtCodigo").val();
    let varDescripcion = $("#txtDescripcion").val();
    let varDivision = $("#cboDivision").val();

    let varSerieGuiaElectronica = $("#txtSerieGuiaElectronica").val();
    let varNumeroInicialGuiaElectronica = $("#txtNumeroInicialGuiaElectronica").val();

    let varEstado = false;

    if ($('#chkActivo')[0].checked) {
        varEstado = true;
    }

    $.post('UpdateInsertBase', {
        'IdBase': varIdBase,
        'Codigo': varCodigo,
        'Descripcion': varDescripcion,
        'Estado': varEstado,
        'IdDivision': varDivision,
        'SerieGuiaElectronica': varSerieGuiaElectronica,
        'NumeroInicialGuiaElectronica': varNumeroInicialGuiaElectronica

    }, function (data, status) {

        if (data == 1) {
            swal("Exito!", "Proceso Realizado Correctamente", "success")
            table.destroy();
            ConsultaServidor("ObtenerBase");
            limpiarDatos();

        } else {
            swal("Error!", "Ocurrio un Error")
            limpiarDatos();
        }

    });
}

function ObtenerDatosxID(varIdBase) {
    $("#lblTituloModal").html("Editar Base");
    AbrirModal("modal-form");

    //console.log(varIdUsuario);

    $.post('ObtenerDatosxID', {
        'IdBase': varIdBase,
    }, function (data, status) {

        if (data == "Error") {
            swal("Error!", "Ocurrio un error")
            limpiarDatos();
        } else {
            let base = JSON.parse(data);
            //console.log(usuarios);
            $("#txtId").val(base[0].IdBase);
            $("#txtCodigo").val(base[0].Codigo);
            $("#txtDescripcion").val(base[0].Descripcion);
            $("#cboDivision").val(base[0].IdDivision);

            $("#txtSerieGuiaElectronica").val(base[0].SerieGuiaElectronica);
            $("#txtNumeroInicialGuiaElectronica").val(base[0].NumeroInicialGuiaElectronica);

            if (base[0].Estado) {
                $("#chkActivo").prop('checked', true);
            }

        }

    });

}

function eliminar(varIdBase) {


    alertify.confirm('Confirmar', '¿Desea eliminar esta base?', function () {
        $.post("EliminarBase", { 'IdBase': varIdBase }, function (data) {

            if (data == 0) {
                swal("Error!", "Ocurrio un Error")
                limpiarDatos();
            } else {
                swal("Exito!", "Base Eliminada", "success")
                table.destroy();
                ConsultaServidor("ObtenerBase");
                limpiarDatos();
            }

        });

    }, function () { });

}
function CargarDivision() {
    $.ajaxSetup({ async: false });
    $.post("/Division/ObtenerDivision", { 'estado': 1 }, function (data, status) {
        let tipoRegistros = JSON.parse(data);
        llenarComboDivision(tipoRegistros, "cboDivision", "Seleccione")
    });
}


function llenarComboDivision(lista, idCombo, primerItem) {
    var contenido = "";
    if (primerItem != null) contenido = "<option value='0'>" + primerItem + "</option>";
    var nRegistros = lista.length;
    var nCampos;
    var campos;
    for (var i = 0; i < nRegistros; i++) {

        if (lista.length > 0) { contenido += "<option value='" + lista[i].IdDivision + "'>" + lista[i].Descripcion + "</option>"; }
        else { }
    }
    var cbo = document.getElementById(idCombo);
    if (cbo != null) cbo.innerHTML = contenido;
}

function limpiarDatos() {
    $("#txtId").val("");
    $("#txtCodigo").val("");
    $("#txtDescripcion").val("");
    $("#cboDivision").val(0);
    $("#txtSerieGuiaElectronica").val("");
    $("#txtNumeroInicialGuiaElectronica").val("");
    $("#chkActivo").prop('checked', false);
}



