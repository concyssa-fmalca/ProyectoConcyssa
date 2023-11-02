let table = '';


window.onload = function () {
    var url = "ObtenerGrupoArticulo";
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

        let grupoarticulo = JSON.parse(data);
        let total_grupoarticulo = grupoarticulo.length;

        for (var i = 0; i < grupoarticulo.length; i++) {


            tr += '<tr>' +
                '<td>' + (i + 1) + '</td>' +
                '<td>' + grupoarticulo[i].Codigo.toUpperCase() + '</td>' +
                '<td>' + grupoarticulo[i].Descripcion.toUpperCase() + '</td>' +

                '<td><button class="btn btn-primary fa fa-pencil btn-xs" onclick="ObtenerDatosxID(' + grupoarticulo[i].IdGrupoArticulo + ')"></button>' +
                '<button class="btn btn-danger btn-xs  fa fa-trash" onclick="eliminar(' + grupoarticulo[i].IdGrupoArticulo + ')"></button>' +
                '<button class="btn btn-success btn-xs fa fa-usd" onclick = "cuentas(' + grupoarticulo[i].IdGrupoArticulo + ')" ></button ></td>' +
                '</tr>';
        }

        $("#tbody_grupoarticulo").html(tr);
        $("#spnTotalRegistros").html(total_grupoarticulo);

        table = $("#table_id").DataTable(lenguaje);

    });

}




function ModalNuevo() {
    $("#lblTituloModal").html("Nueva Grupo articulo");
    AbrirModal("modal-form");
    $("#chkActivo").prop("checked", true)
}




function GuardarGrupoArticulo() {
    let varIdGrupoArticulo = $("#txtId").val();
    let varCodigo = $("#txtCodigo").val();
    let varDescripcion = $("#txtDescripcion").val();
    let varEstado = false;

    if ($('#chkActivo')[0].checked) {
        varEstado = true;
    }

    $.post('UpdateInsertGrupoArticulo', {
        'IdGrupoArticulo': varIdGrupoArticulo,
        'Codigo': varCodigo,
        'Descripcion': varDescripcion,
        'Estado': varEstado
    }, function (data, status) {

        if (data == 1) {
            swal("Exito!", "Proceso Realizado Correctamente", "success")
            table.destroy();
            ConsultaServidor("ObtenerGrupoArticulo");
            limpiarDatos();

        } else {
            swal("Error!", "Ocurrio un Error")
            limpiarDatos();
        }

    });
}

function ObtenerDatosxID(varIdGrupoArticulo) {
    $("#lblTituloModal").html("Editar Grupo articulo");
    AbrirModal("modal-form");

    //console.log(varIdUsuario);

    $.post('ObtenerDatosxID', {
        'IdGrupoArticulo': varIdGrupoArticulo,
    }, function (data, status) {

        if (data == "Error") {
            swal("Error!", "Ocurrio un error")
            limpiarDatos();
        } else {
            let grupoarticulo = JSON.parse(data);
            //console.log(usuarios);
            $("#txtId").val(grupoarticulo[0].IdGrupoArticulo);
            $("#txtCodigo").val(grupoarticulo[0].Codigo);
            $("#txtDescripcion").val(grupoarticulo[0].Descripcion);
            if (grupoarticulo[0].Estado) {
                $("#chkActivo").prop('checked', true);
            }

        }

    });

}

function eliminar(varIdGrupoArticulo) {


    alertify.confirm('Confirmar', '¿Desea eliminar esta grupo articulo?', function () {
        $.post("EliminarGrupoArticulo", { 'IdGrupoArticulo': varIdGrupoArticulo }, function (data) {

            if (data == 0) {
                swal("Error!", "Ocurrio un Error")
                limpiarDatos();
            } else {
                swal("Exito!", "Grupo articulo Eliminada", "success")
                table.destroy();
                ConsultaServidor("ObtenerGrupoArticulo");
                limpiarDatos();
            }

        });

    }, function () { });

}


function limpiarDatos() {
    console.log("hola");
    $("#txtId").val("");
    $("#txtCodigo").val("");
    $("#txtDescripcion").val("");
    $("#chkActivo").prop('checked', false);
}



function limpiarDatos() {
    $("#txtdescripcion").val("");
    $("#txtCtaAjusteStockNegativo").val("");
    $("#txtCtaAumento").val("");
    $("#txtCtaCompensacionGasto").val("");
    $("#txtCtaCompensacionMercancias").val("");
    $("#txtCtaCompras").val("");
    $("#txtCtaComprasDevolucion").val("");
    $("#txtCtaContrapartidaRevalorizacionInventario").val("");
    $("#txtCtaCostoBienes").val("");
    $("#txtCtaCostosExtranjeros").val("");
    $("#txtCtaCostosMercanciasCompradas").val("");
    $("#txtCtaCreditoCompras").val("");
    $("#txtCtaCreditoVentas").val("");
    $("#txtCtaDesviacion").val("");
    $("#txtCtaDesviacionStockWIP").val("");
    $("#txtCtaDevolucionxVentas").val("");
    $("#txtCtaDiferenciaPrecios").val("");
    $("#txtCtaDiferenciaTipoCambio").val("");
    $("#txtCtaDotacion").val("");
    $("#txtCtaExistencias").val("");
    $("#txtCtaGasto").val("");
    $("#txtCtaIngresos").val("");
    $("#txtCtaIngresosExtranjeros").val("");
    $("#txtCtaReduccion").val("");
    $("#txtCtaReduccionLibroMayor").val("");
    $("#txtCtaRevalorizacionStock").val("");
    $("#txtCtaStockTrabajoEnCurso").val("");
    $("#txtCtaStockTransito").val("");
}
function cuentas(id) {
    limpiarDatos();
    AbrirModal("modal_cuentas");

    //let empresa_actual = $("#BDEmpresaActual").val();

    //$("#idGrupoArticuloCuenta").val("");
    //$("#modal_cuentas").modal();
    //$.post('ObtenerGrupoArticuloxID', { 'idGrupoArticulo': id, 'BDEmpresaActual': empresa_actual }, function (data, status) {
    //    //ff
    //    let datos = JSON.parse(data);
    //    datos = datos[0];
    //    $("#idGrupoArticuloCuenta").val(datos.idGrupoArticulo);
    //    $("#txtCtaAjusteStockNegativo").val(datos.CtaAjusteStockNegativo);
    //    $("#txtCtaAumento").val(datos.CtaAumento);
    //    $("#txtCtaCompensacionGasto").val(datos.CtaCompensacionGasto);
    //    $("#txtCtaCompensacionMercancias").val(datos.CtaCompensacionMercancias);
    //    $("#txtCtaCompras").val(datos.CtaCompras);
    //    $("#txtCtaComprasDevolucion").val(datos.CtaComprasDevolucion);
    //    $("#txtCtaContrapartidaRevalorizacionInventario").val(datos.CtaContrapartidaRevalorizacionInventario);
    //    $("#txtCtaCostoBienes").val(datos.CtaCostoBienes);
    //    $("#txtCtaCostosExtranjeros").val(datos.CtaCostosExtranjeros);
    //    $("#txtCtaCostosMercanciasCompradas").val(datos.CtaCostosMercanciasCompradas);
    //    $("#txtCtaCreditoCompras").val(datos.CtaCreditoCompras);
    //    $("#txtCtaCreditoVentas").val(datos.CtaCreditoVentas);
    //    $("#txtCtaDesviacion").val(datos.CtaDesviacion);
    //    $("#txtCtaDesviacionStockWIP").val(datos.CtaDesviacionStockWIP);
    //    $("#txtCtaDevolucionxVentas").val(datos.CtaDevolucionxVentas);
    //    $("#txtCtaDiferenciaPrecios").val(datos.CtaDiferenciaPrecios);
    //    $("#txtCtaDiferenciaTipoCambio").val(datos.CtaDiferenciaTipoCambio);
    //    $("#txtCtaDotacion").val(datos.CtaDotacion);
    //    $("#txtCtaExistencias").val(datos.CtaExistencias);
    //    $("#txtCtaGasto").val(datos.CtaGasto);
    //    $("#txtCtaIngresos").val(datos.CtaIngresos);
    //    $("#txtCtaIngresosExtranjeros").val(datos.CtaIngresosExtranjeros);
    //    $("#txtCtaReduccion").val(datos.CtaReduccion);
    //    $("#txtCtaReduccionLibroMayor").val(datos.CtaReduccionLibroMayor);
    //    $("#txtCtaRevalorizacionStock").val(datos.CtaRevalorizacionStock);
    //    $("#txtCtaStockTrabajoEnCurso").val(datos.CtaStockTrabajoEnCurso);
    //    $("#txtCtaStockTransito").val(datos.CtaStockTransito);


    //});
}


