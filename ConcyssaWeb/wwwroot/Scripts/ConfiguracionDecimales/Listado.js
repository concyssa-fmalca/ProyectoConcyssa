let table = '';


window.onload = function () {
    var url = "ObtenerConfiguracionDecimales";
    ConsultaServidor(url);
};


function ConsultaServidor(url) {

    $.post(url, function (data, status) {

        if (data == "error") {
            return;
        }


        let configuraciondecimales = JSON.parse(data);

        //console.log(configuraciondecimales);
        $("#txtImportes").val(configuraciondecimales[0].Importes);
        $("#txtPrecios").val(configuraciondecimales[0].Precios);
        $("#txtCantidades").val(configuraciondecimales[0].Cantidades);
        $("#txtPorcentajes").val(configuraciondecimales[0].Porcentajes);
        //$("#txtUnidades").val(configuraciondecimales[0].Unidades);
        //$("#txtDecimales").val(configuraciondecimales[0].Decimales);
        $("#txtId").val(configuraciondecimales[0].IdConfiguracionDecimales);


    });

}


function GuardarConfiguracionDecimales() {

    let varIdConfiguracionDecimales = $("#txtId").val();
    let varImportes = $("#txtImportes").val();
    let varPrecios = $("#txtPrecios").val();
    let varCantidades = $("#txtCantidades").val();
    let varPorcentajes = $("#txtPorcentajes").val();
    let varUnidades = $("#txtUnidades").val();
    let varDecimales = $("#txtDecimales").val();


    $.post('UpdateInsertConfiguracionDecimales', {
        'IdConfiguracionDecimales': varIdConfiguracionDecimales,
        'Importes': varImportes,
        'Precios': varPrecios,
        'Cantidades': varCantidades,
        'Porcentajes': varPorcentajes,
        'Unidades': 0,
        'Decimales': 0
    }, function (data, status) {

        if (data == 1) {
            swal("Exito!", "Proceso Realizado Correctamente", "success")
        } else {
            swal("Error!", "Ocurrio un Error")
        }

    });
}