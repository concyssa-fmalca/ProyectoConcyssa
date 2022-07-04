let table = '';

window.onload = function () {
    var url = "ObtenerArticulosxSociedad";
    ConsultaServidor(url);
    listarUnidadMedida();
    listarCodigoUbso();
};

function listarUnidadMedida() {
    $.ajax({
        url: "../UnidadMedida/ObtenerUnidadMedidasxEstado",
        type: "GET",
        contentType: "application/json",
        dataType: "json",
        data: {
            'estado': 1
        },
        cache: false,
        contentType: false,
        success: function (datos) {
            $("#cboIdUnidadMedida").html('');
            let options = `<option value="0">Seleccione</option>`;
            if (datos.length > 0) {
                for (var i = 0; i < datos.length; i++) {
                    options += `<option value="` + datos[i].IdUnidadMedida +`">` + datos[i].Descripcion +`</option>`;
                }
                $("#cboIdUnidadMedida").html(options);
            }
        }
    });
}

function listarCodigoUbso() {
    $.ajax({
        url: "../CodigoUbso/ObtenerCodigoUbso",
        type: "GET",
        contentType: "application/json",
        dataType: "json",
        data: {
            'estado': 1
        },
        cache: false,
        contentType: false,
        success: function (datos) {
            $("#cboIdCodigoUbso").html('');
            let options = `<option value="0">Seleccione</option>`;
            if (datos.length > 0) {
                for (var i = 0; i < datos.length; i++) {
                    options += `<option value="` + datos[i].IdCodigoUbso + `">` + datos[i].Descripcion + `</option>`;
                }
                $("#cboIdCodigoUbso").html(options);
            }
        }
    });
}



function ConsultaServidor(url) {

    $.post(url, function (data, status) {
        if (data == "error") {
            table = $("#table_id").DataTable(lenguaje);
            return;
        }

        let tr = '';

        let articulos = JSON.parse(data);
        let total_articulos = articulos.length;
       
        for (var i = 0; i < articulos.length; i++) {
    
            tr += '<tr>' +
                '<td>' + (i + 1) + '</td>' +
                '<td>' + articulos[i].Codigo.toUpperCase() + '</td>' +
                '<td>' + articulos[i].Descripcion1.toUpperCase() + '</td>' +
                '<td>' + articulos[i].Descripcion2.toUpperCase() + '</td>' +
                '<td>' + articulos[i].ActivoFijo + '</td>' +
                '<td>' + articulos[i].IdCodigoUbso + '</td>' +
                '<td><button class="btn btn-primary fa fa-pencil btn-xs" onclick="ObtenerDatosxID(' + articulos[i].IdArticulo + ')"></button>' +
                '<button class="btn btn-danger btn-xs  fa fa-trash" onclick="eliminar(' + articulos[i].IdArticulo + ')"></button></td >' +
                '</tr>';
        }
        
        $("#tbody_Articulos").html(tr);
        $("#spnTotalRegistros").html(total_articulos);

        table = $("#table_id").DataTable(lenguaje);

    });

}


function ModalNuevo() {
    $("#lblTituloModal").html("Nuevo Articulo");
    AbrirModal("modal-form");
    CargarPerfiles();
    CargarSociedades();
}






function GuardarArticulo() {

    let varIdArticulo = $("#txtIdArticulo").val();
    let varCodigo = $("#txtCodigo").val();
    let varDescripcion1 = $("#txtDescripcion1").val();
    let varDescripcion2 = $("#txtDescripcion2").val();
    let varIdUnidadMedida = $("#cboIdUnidadMedida").val();
    let IdCodigoUbso = $("#cboIdCodigoUbso").val();
    let varEstadoActivoFijo = false;
    let varEstadoActivoCatalogo = false;

    if ($('#chkActivoFijo')[0].checked) {
        varEstadoActivoFijo = true;
    }
    if ($('#chkActivoCatalogo')[0].checked) {
        varEstadoActivoCatalogo = true;
    }

    $.post('UpdateInsertArticulo', {
        'IdArticulo': varIdArticulo,
        'Codigo': varCodigo,
        'Descripcion1': varDescripcion1,
        'Descripcion2': varDescripcion2,
        'IdUnidadMedida': varIdUnidadMedida,
        'IdCodigoUbso': IdCodigoUbso,
        'ActivoFijo': varEstadoActivoFijo,
        'ActivoCatalogo': varEstadoActivoCatalogo
    }, function (data, status) {

        if (data == 1) {
            swal("Exito!", "Proceso Realizado Correctamente", "success")
            table.destroy();
            ConsultaServidor("ObtenerUsuarios");
            limpiarDatos();
        } else {
            swal("Error!", "Ocurrio un Error")
            limpiarDatos();
        }

    });
}

function ObtenerDatosxID(varIdArticulo) {
    $("#lblTituloModal").html("Editar Usuario");
    AbrirModal("modal-form");

    //console.log(varIdUsuario);

    $.post('ObtenerDatosxID', {
        'IdArticulo': varIdArticulo,
    }, function (data, status) {

        if (data == "Error") {
            swal("Error!", "Ocurrio un error")
            limpiarDatos();
        } else {
            let articulos = JSON.parse(data);
            //console.log(usuarios);
            $("#txtId").val(articulos[0].IdArticulo);
            $("#txtCodigo").val(articulos[0].Codigo);
            $("#txtDescripcion1").val(articulos[0].Descripcion1);
            $("#txtDescripcion2").val(articulos[0].Descripcion2);
            $("#cboIdUnidadMedida").val(articulos[0].IdUnidadMedida);
            $("#cboIdCodigoUbso").val(articulos[0].IdCodigoUbso);
   
            if (articulos[0].ActivoFijo) {
                $("#chkActivoFijo").prop('checked', true);
            }

            if (articulos[0].ActivoCatalogo) {
                $("#chkActivoCatalogo").prop('checked', true);
            }

            

        }

    });

}

function eliminar(varIdUsuario) {


    alertify.confirm('Confirmar', '¿Desea eliminar este usuario?', function () {
        $.post("EliminarUsuario", { 'IdUsuario': varIdUsuario }, function (data) {

            if (data == 0) {
                swal("Error!", "Ocurrio un Error")
                limpiarDatos();
            } else {
                swal("Exito!", "Usuario Eliminado", "success")
                table.destroy();
                ConsultaServidor("ObtenerUsuarios");
                limpiarDatos();
            }

        });

    }, function () { });

}


function limpiarDatos() {
    $("#txtId").val("");
    $("#txtNombre").val("");
    $("#txtUsuario").val("");
    $("#txtContraseña").val("");
    $("#chkActivo").prop('checked', false);
}



