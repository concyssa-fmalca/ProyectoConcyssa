let table = '';


window.onload = function () {
    var url = "ObtenerGlosaContable";
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

        let division = JSON.parse(data);
        let total_division = division.length;

        for (var i = 0; i < division.length; i++) {
            let estado = division[i].Estado ? "Activo" : "Desactivado";

            tr += '<tr>' +
                '<td>' + (i + 1) + '</td>' +
                '<td>' + division[i].Codigo.toUpperCase() + '</td>' +
                '<td>' + division[i].Division + '</td>' +
                '<td>' + division[i].Descripcion.toUpperCase() + '</td>' +
                '<td>' + division[i].CuentaContable.toUpperCase() + '</td>' +
                '<td>' + estado + '</td>' +
                '<td><button class="btn btn-primary fa fa-pencil btn-xs" onclick="ObtenerDatosxID(' + division[i].IdGlosaContable + ')"></button>' +
                '<button class="btn btn-danger btn-xs  fa fa-trash" onclick="eliminar(' + division[i].IdGlosaContable + ')"></button></td >' +
                '</tr>';
        }

        $("#tbody_glosa").html(tr);
        $("#spnTotalRegistros").html(total_division);

        table = $("#table_id").DataTable(lenguaje);

    });

}




function ModalNuevo() {
    $("#lblTituloModal").html("Nueva Glosa Contable");
    AbrirModal("modal-form");
    $("#chkActivo").prop("checked", true)
}

function GuardarGlosaContable() {
    let varIdGlosaContable = $("#txtId").val();
    let varIdDivision = $("#cboDivision").val();
    let varCodigo = $("#txtCodigo").val();
    let varCuentaContable = $("#txtCuentaContable").val();
    let varDescripcion = $("#txtDescripcion").val();
    let varClasif = $("#cboClasif").val();
    let varEstado = false;

    if ($('#chkActivo')[0].checked) {
        varEstado = true;
    }

    //Validaciones
    if (varIdDivision == 0 || varIdDivision == null) {
        Swal.fire(
            'Error!',
            'Complete el campo de Division',
            'error'
        )
        return;
    }
    if (varCodigo == '' || varCodigo == null) {
        Swal.fire(
            'Error!',
            'Complete el campo de Codigo',
            'error'
        )
        return;
    }
    if (varCuentaContable == '' || varCuentaContable == null) {
        Swal.fire(
            'Error!',
            'Complete el campo de Cuenta Contable',
            'error'
        )
        return;
    }


    $.post('UpdateInsertGlosaContable', {
        'IdGlosaContable': varIdGlosaContable,
        'IdDivision': varIdDivision,
        'Codigo': varCodigo,
        'CuentaContable': varCuentaContable,
        'Descripcion': varDescripcion,
        'Estado': varEstado,
        'IdClasif': varClasif
    }, function (data, status) {

        if (data == 1) {
            swal("Exito!", "Proceso Realizado Correctamente", "success")
            table.destroy();
            ConsultaServidor("ObtenerGlosaContable");
            limpiarDatos();

        } else {
            swal("Error!", "Ocurrio un Error")
            limpiarDatos();
        }

    });
}

function ObtenerDatosxID(varIdGlosaContable) {
    $("#lblTituloModal").html("Editar Glosa Contanble");
    AbrirModal("modal-form");
    $("#txtId").val(varIdGlosaContable);
    //console.log(varIdUsuario);

    $.post('ObtenerDatosxID', {
        'IdGlosaContable': varIdGlosaContable,
    }, function (data, status) {

        if (data == "Error") {
            swal("Error!", "Ocurrio un error")
            limpiarDatos();
        } else {
            let division = JSON.parse(data);
            //console.log(usuarios);
            $("#txtCodigo").val(division[0].Codigo);
            $("#cboDivision").val(division[0].IdDivision);
            $("#txtCuentaContable").val(division[0].CuentaContable);
            $("#txtDescripcion").val(division[0].Descripcion);
            $("#cboClasif").val(division[0].IdClasif);
            if (division[0].Estado) {
                $("#chkActivo").prop('checked', true);
            }

        }

    });

}

function eliminar(varIdGlosaContable) {


    alertify.confirm('Confirmar', '¿Desea eliminar esta Glosa Contable?', function () {
        $.post("EliminarGlosaContable", { 'IdGlosaContable': varIdGlosaContable }, function (data) {

            if (data == 0) {
                swal("Error!", "Ocurrio un Error")
                limpiarDatos();
            } else {
                swal("Exito!", "Glosa Contable Eliminada", "success")
                table.destroy();
                ConsultaServidor("ObtenerGlosaContable");
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
    $("#cboDivision").val(0);
    $("#txtCodigo").val("");
    $("#txtDescripcion").val("");
    $("#txtCuentaContable").val("");
    $("#cboClasif").val(0);
    $("#chkActivo").prop('checked', false);


}



