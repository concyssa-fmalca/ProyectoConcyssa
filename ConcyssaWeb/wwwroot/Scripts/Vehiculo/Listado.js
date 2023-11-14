let table = '';


window.onload = function () {
    var url = "ObtenerVehiculo";
    ConsultaServidor(url);
    listarMacas();
    listarBase();
    listarEmpleado();
};


function ConsultaServidor(url) {

    $.post(url, function (data, status) {

        //console.log(data);
        if (data == "error") {
            table = $("#table_id").DataTable(lenguaje);
            return;
        }

        let tr = '';

        let Vehiculo = JSON.parse(data);
        let total_Vehiculo = Vehiculo.length;

        console.log("hola");
        console.log(Vehiculo);

        for (var i = 0; i < Vehiculo.length; i++) {


            tr += '<tr>' +
                '<td>' + (i + 1) + '</td>' +
                '<td>' + Vehiculo[i].Placa + '</td>' +
                '<td>' + Vehiculo[i].MarcaDescripcion.toUpperCase() + '</td>' +
                //'<td>' + Vehiculo[i].CertificadoInscripcion + '</td>' +
                '<td>' + Vehiculo[i].BaseDescripcion.toUpperCase() + '</td>' +
                '<td>' + Vehiculo[i].ChoferDescripcion.toUpperCase() + '</td>' +
                '<td>' + Vehiculo[i].Condicion.toUpperCase() + '</td>' +

                '<td><button class="btn btn-primary fa fa-pencil btn-xs" onclick="ObtenerDatosxID(' + Vehiculo[i].IdVehiculo + ')"></button>' +
                '<button class="btn btn-danger btn-xs  fa fa-trash" onclick="eliminar(' + Vehiculo[i].IdVehiculo + ')"></button></td >' +
                '</tr>';
        }

        $("#tbody_Vehiculo").html(tr);
        $("#spnTotalRegistros").html(total_Vehiculo);

        table = $("#table_id").DataTable(lenguaje);

    });

}




function ModalNuevo() {
    $("#lblTituloModal").html("Nueva Vehiculo");
    AbrirModal("modal-form");
    $('#chkActivo').prop('checked', true)
}




function GuardarVehiculo() {
    let IdVehiculo = $("#txtId").val();
    let IdMarca = $("#cboIdMarca").val();
    let IdBase = $("#cboIdBase").val();
    let Condicion = $("#cboCondicion").val();
    let Placa = $("#txtPlaca").val();
    let CertificadoInscripcion = $("#txtCertfificadoInscripcion").val();    
    let IdChofer = $("#cboIdChofer").val();
    let Estado = false;

    if ($('#chkActivo')[0].checked) {
        Estado = true;
    }
    if (Placa == "" || Placa == undefined) {
        Swal.fire("Error", "El Campo Placa es Obligatorio", "info")
        return
    }
    if (CertificadoInscripcion == "" || CertificadoInscripcion == undefined) {
        Swal.fire("Error", "El Campo Certificado Inscripcion es Obligatorio", "info")
        return
    }
    if (IdMarca == 0 || IdMarca == undefined) {
        Swal.fire("Error", "El Campo Marca Inscripcion es Obligatorio", "info")
        return
    }
    if (IdBase == 0 || IdBase == undefined) {
        Swal.fire("Error", "El Campo Base Inscripcion es Obligatorio", "info")
        return
    }
    if (Condicion == 0 || Condicion == undefined) {
        Swal.fire("Error", "El Campo Condicion Inscripcion es Obligatorio", "info")
        return
    }
    if (IdChofer == 0 || IdChofer == undefined) {
        Swal.fire("Error", "El Campo Chofer Inscripcion es Obligatorio", "info")
        return
    }
    $.post('UpdateInsertVehiculo', {
        IdVehiculo, IdMarca, IdBase, Condicion, CertificadoInscripcion, IdChofer, Placa, Estado
    }, function (data, status) {

        if (data == 1) {
            swal("Exito!", "Proceso Realizado Correctamente", "success")
            //table.destroy();
            ConsultaServidor("ObtenerVehiculo");
            limpiarDatos();

        } else {
            swal("Error!", "Ocurrio un Error")
            limpiarDatos();
        }
    });
}


function listarMacas() {
    $.ajax({
        url: "../Marca/ObtenerMarca",
        type: "GET",
        contentType: "application/json",
        dataType: "json",
        data: {
            'estado': 1
        },
        cache: false,
        contentType: false,
        success: function (datos) {
            $("#cboIdMarca").html('');
            let options = `<option value="0" selected>Seleccione</option>`;
            if (datos.length > 0) {
                console.log(datos)
                for (var i = 0; i < datos.length; i++) {
                    options += `<option value="` + datos[i].IdMarca + `">` + datos[i].Descripcion + `</option>`;
                }
                $("#cboIdMarca").html(options);
            }
        }
    });
}


function listarBase() {
    $.ajax({
        url: "../Base/ObtenerBase",
        type: "GET",
        contentType: "application/json",
        dataType: "json",
        data: {
            'estado': 1
        },
        cache: false,
        contentType: false,
        success: function (datos) {
            $("#cboIdBase").html('');
            let options = `<option value="0" selected>Seleccione</option>`;
            if (datos.length > 0) {
               
                for (var i = 0; i < datos.length; i++) {
                    options += `<option value="` + datos[i].IdBase + `">` + datos[i].Descripcion + `</option>`;
                }
                $("#cboIdBase").html(options);
            }
        }
    });
}


function listarEmpleado() {
    $.ajax({
        url: "../Empleado/ObtenerEmpleados",
        type: "GET",
        contentType: "application/json",
        dataType: "json",
        data: {
            'estado': 1
        },
        cache: false,
        contentType: false,
        success: function (datos) {
            $("#cboIdChofer").html('');
            let options = `<option value="0" selected>Seleccione</option>`;
            if (datos.length > 0) {

                for (var i = 0; i < datos.length; i++) {
                    options += `<option value="` + datos[i].IdEmpleado + `">` + datos[i].RazonSocial + `</option>`;
                }
                $("#cboIdChofer").html(options);
            }
        }
    });
}



function ObtenerDatosxID(varIdVehiculo) {
    $("#lblTituloModal").html("Editar Vehiculo");
    AbrirModal("modal-form");

    //console.log(varIdUsuario);

    $.post('ObtenerDatosxID', {
        'IdVehiculo': varIdVehiculo,
    }, function (data, status) {

        if (data == "Error") {
            swal("Error!", "Ocurrio un error")
            limpiarDatos();
        } else {
            let Vehiculo = JSON.parse(data);
            //console.log(usuarios);
            $("#txtId").val(Vehiculo[0].IdVehiculo);
            $("#cboIdMarca").val(Vehiculo[0].IdMarca);
            $("#cboIdBase").val(Vehiculo[0].IdBase);
            $("#cboCondicion").val(Vehiculo[0].Condicion);
            $("#txtCertfificadoInscripcion").val(Vehiculo[0].CertificadoInscripcion);
            $("#cboIdChofer").val(Vehiculo[0].IdChofer);
            $("#txtPlaca").val(Vehiculo[0].Placa);
            
            if (Vehiculo[0].Estado) {
                $("#chkActivo").prop('checked', true);
            }

        }

    });

}

function eliminar(varIdLineaNegocio) {


    alertify.confirm('Confirmar', '¿Desea eliminar esta Vehiculo?', function () {
        $.post("EliminarVehiculo", { 'IdVehiculo': varIdLineaNegocio }, function (data) {

            if (data == 0) {
                swal("Error!", "Ocurrio un Error")
                limpiarDatos();
            } else {
                swal("Exito!", "Vehiculo Eliminada", "success")
                table.destroy();
                ConsultaServidor("ObtenerVehiculo");
                limpiarDatos();
            }

        });

    }, function () { });

}


function limpiarDatos() {
    $("#txtId").val("");
    $("#cboIdMarca").val(0);
    $("#cboIdBase").val(0);
    $("#cboCondicion").val(0);
    $("#txtCertfificadoInscripcion").val("");
    $("#cboIdChofer").val(0);
    $("#txtPlaca").val("");
}



