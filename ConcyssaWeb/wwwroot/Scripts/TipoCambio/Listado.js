let table = '';


window.onload = function () {
    var url = "ObtenerTipoCambio";
    ConsultaServidor(url);
    //CargarDivision();
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
            let Fechao = division[i].Fecha.split('T')[0];
            let Fecha = Fechao.replaceAll("-", "");
            tr += '<tr>' +
                '<td>' + (i + 1) + '</td>' +
                '<td>' + division[i].Moneda.toUpperCase() + '</td>' +
                '<td>' + Fechao + '</td>' +
                '<td>' + division[i].TipoCambioCompra + '</td>' +
                '<td>' + division[i].TipoCambioVenta + '</td>' +
                '<td>' + division[i].Origen.toUpperCase() + '</td>' +             
                '<td><button class="btn btn-primary fa fa-pencil btn-xs" onclick="ObtenerDatosxID(' + division[i].IdMoneda + ',' + Fecha + ')"></button>' +
                '</td >' +
                '</tr>';
        }

        $("#tbody_tipocambio").html(tr);
        $("#spnTotalRegistros").html(total_division);

        table = $("#table_id").DataTable(lenguaje);

    });

}




function ActualizarTipoCambio() {

    $.post('ActualizarTipoCambio', {}, function (data, status) {

        if (data == 1) {
            swal("Exito!", "Proceso Realizado Correctamente", "success")
            table.destroy();
            ConsultaServidor("ObtenerTipoCambio");
            limpiarDatos();

        } else {
            swal("Error!", "Ocurrio un Error")
            limpiarDatos();
        }

    });






}

function GuardarTipoCambio() {
    let Id = $("#txtId").val().split('T');
    let IdMoneda = Id[0];
    let Fecha = Id[1];
    Fecha = Fecha.substring(0, 4) + '-' + Fecha.substring(4, 6) + '-' + Fecha.substring(6, 8);
    let  venta= $("#txtVenta").val();
    let  compra= $("#txtCompra").val();
    

    $.post('UpdateInsertTipoCambio', {
        'IdMoneda': IdMoneda,
        'Fecha': Fecha,
        'TipoCambioVenta': venta,
        'TipoCambioCompra': compra,
     
    }, function (data, status) {

        if (data == 1) {
            swal("Exito!", "Proceso Realizado Correctamente", "success")
            table.destroy();
            ConsultaServidor("ObtenerTipoCambio");
            limpiarDatos();

        } else {
            swal("Error!", "Ocurrio un Error")
            limpiarDatos();
        }

    });
}

function ObtenerDatosxID(IdMoneda, Fecha) {
    Fecha = Fecha.toString();
    $("#lblTituloModal").html("Editar Tipo Cambio");
    AbrirModal("modal-form");
    $("#txtId").val(IdMoneda +'T'+ Fecha);
   
    Fecha = Fecha.substring(0, 4) + '-' + Fecha.substring(4, 6) + '-' + Fecha.substring(6, 8);
    //Fecha = new Date(Fecha);//20230301   
    $.post('ObtenerDatosxID', {
        'IdMoneda': +IdMoneda,
        'Fecha': Fecha
    }, function (data, status) {

        if (data == "Error") {
            swal("Error!", "Ocurrio un error")
            limpiarDatos();
        } else {
            let division = JSON.parse(data);
            //console.log(usuarios);
            $("#txtMoneda").val(division[0].Moneda);
            $("#dtFecha").val(division[0].Fecha);
            $("#txtCompra").val(division[0].TipoCambioCompra);
            $("#txtVenta").val(division[0].TipoCambioVenta);
            

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
    $("#chkActivo").prop('checked', false);


}



