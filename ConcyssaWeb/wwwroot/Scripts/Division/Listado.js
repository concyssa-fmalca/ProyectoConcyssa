let table = '';


window.onload = function () {
    var url = "ObtenerDivision";
    ConsultaServidor(url);
 
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


            tr += '<tr>' +
                '<td>' + (i + 1) + '</td>' +
                '<td>' + division[i].Codigo.toUpperCase() + '</td>' +
                '<td>' + division[i].Descripcion.toUpperCase() + '</td>' +
                '<td><button class="btn btn-primary fa fa-pencil btn-xs" onclick="ObtenerDatosxID(' + division[i].IdDivision + ')"></button>' +
                '<button class="btn btn-danger btn-xs  fa fa-trash" onclick="eliminar(' + division[i].IdDivision + ')"></button></td >' +
                '</tr>';
        }

        $("#tbody_division").html(tr);
        $("#spnTotalRegistros").html(total_division);

        table = $("#table_id").DataTable(lenguaje);

    });

}

function CargarIdClasif() {
    $.ajaxSetup({ async: false });
    $.post("/IntegradorV1/ObtenerClasif", function (data, status) {
        let base = JSON.parse(data);
        llenarComboIdClasif(base, "cboIdClasif", "seleccione")

    });

}


function llenarComboIdClasif(lista, idCombo, primerItem) {
    var contenido = "";
    if (primerItem != null) contenido = "<option value='0'>" + primerItem + "</option>";
    var nRegistros = lista.length;
    var nCampos;
    var campos;
    for (var i = 0; i < nRegistros; i++) {

        if (lista.length > 0) { contenido += "<option value='" + lista[i].Code + "'>" + lista[i].Name.toUpperCase() + "</option>"; }
        else { }
    }
    var cbo = document.getElementById(idCombo);
    if (cbo != null) cbo.innerHTML = contenido;
    $("#cboBaseFiltro").prop("selectedIndex", 1);
    listarOpch();
}


function ModalNuevo() {
    $("#lblTituloModal").html("Nueva Division");
    AbrirModal("modal-form");
    $("#chkActivo").prop("checked", true)
}




function GuardarDivision() {
    let varIdDivision = $("#txtId").val();
    let varCodigo = $("#txtCodigo").val();
    let varDescripcion = $("#txtDescripcion").val();
    let varCuentaContable = $("#txtCuentaContable").val();
    let varCuentaContableInv = $("#txtCuentaContableInv").val();

    let varEstado = false;

    if ($('#chkActivo')[0].checked) {
        varEstado = true;
    }
    if (varCodigo == "" || varCodigo == undefined) {
        Swal.fire("Error", "El Campo Codigo es Obligatorio", "info")
        return
    }
    if (varDescripcion == "" || varDescripcion == undefined) {
        Swal.fire("Error", "El Campo Descripcion es Obligatorio", "info")
        return
    }
    if (varCuentaContable == "" || varCuentaContable == undefined) {
        Swal.fire("Error", "El Campo Cuenta Contable Compra es Obligatorio", "info")
        return
    }
    if (varCuentaContableInv == "" || varCuentaContableInv == undefined) {
        Swal.fire("Error", "El Campo Cuenta Contable Inventario es Obligatorio", "info")
        return
    }
    $.post('UpdateInsertDivision', {
        'IdDivision': varIdDivision,
        'Codigo': varCodigo,
        'Descripcion': varDescripcion,
        'Estado': varEstado,
        'CuentaContable': varCuentaContable,
        'CuentaContableInv': varCuentaContableInv
    }, function (data, status) {

        if (data == 1) {
            swal("Exito!", "Proceso Realizado Correctamente", "success")
            table.destroy();
            ConsultaServidor("ObtenerDivision");
            limpiarDatos();

        } else {
            swal("Error!", "Ocurrio un Error")
            limpiarDatos();
        }

    });
}

function ObtenerDatosxID(varIdDivision) {
    $("#lblTituloModal").html("Editar Division");
    AbrirModal("modal-form");

    //console.log(varIdUsuario);

    $.post('ObtenerDatosxID', {
        'IdDivision': varIdDivision,
    }, function (data, status) {

        if (data == "Error") {
            swal("Error!", "Ocurrio un error")
            limpiarDatos();
        } else {
            let division = JSON.parse(data);
            //console.log(usuarios);
            $("#txtId").val(division[0].IdDivision);
            $("#txtCodigo").val(division[0].Codigo);
            $("#txtDescripcion").val(division[0].Descripcion);
            $("#txtCuentaContable").val(division[0].CuentaContable);
            $("#txtCuentaContableInv").val(division[0].CuentaContableInv);
      
            if (division[0].Estado) {
                $("#chkActivo").prop('checked', true);
            }

        }

    });

}

function eliminar(varIdDivision) {


    alertify.confirm('Confirmar', '¿Desea eliminar esta Division?', function () {
        $.post("EliminarDivision", { 'IdDivision': varIdDivision }, function (data) {

            if (data == 0) {
                swal("Error!", "Ocurrio un Error")
                limpiarDatos();
            } else {
                swal("Exito!", "Division Eliminada", "success")
                table.destroy();
                ConsultaServidor("ObtenerDivision");
                limpiarDatos();
            }

        });

    }, function () { });

}


function limpiarDatos() {
    $("#txtId").val("");
    $("#txtCodigo").val("");
    $("#txtDescripcion").val("");
    $("#txtCuentaContable").val("");
    $("#txtCuentaContableInv").val("");

    $("#chkActivo").prop('checked', false);
}



