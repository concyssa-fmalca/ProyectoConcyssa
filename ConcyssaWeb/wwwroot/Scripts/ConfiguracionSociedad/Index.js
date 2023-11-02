window.onload = function () {

    var url = "ObtenerConfiguracionSociedad";
    ConsultaServidor(url);

};


function ConsultaServidor(url) {

    $.post(url, function (data, status) {

        let datos = JSON.parse(data);
        console.log(datos);
        $("#txtRuc").val(datos[0].Ruc);
        $("#txtRazonSocial").val(datos[0].RazonSocial);
        $("#txtDireccion").val(datos[0].Direccion);
        $("#txtId").val(datos[0].Id);
        $("#txtNombreBaseDatosSap").val(datos[0].NombreBDSAP);
        //console.log(data);


    });

}

function GuardarConfiguracion() {

    let Id = $("#txtId").val();
    let Ruc = $("#txtRuc").val();
    let RazonSocial = $("#txtRazonSocial").val();
    let Direccion =$("#txtDireccion").val();
    let NombreBDSAP = $("#txtNombreBaseDatosSap").val();


    $.post('UpdateInsertConfiguracionSociedad', {
        'Id': Id,
        'Ruc': Ruc,
        'RazonSocial': RazonSocial,
        'Direccion': Direccion,
        'NombreBDSAP': NombreBDSAP,
       
    }, function (data, status) {

        if (data == 1) {
            swal("Exito!", "Proceso Realizado Correctamente", "success")
          

        } else {
            swal("Error!", "Ocurrio un Error")
        }

    });

}