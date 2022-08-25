let table = '';


window.onload = function () {
    var url = "ObtenerCuadrilla";
    ConsultaServidor(url);
    listarObra();
    listarGrupo();
    listarArea();
    listarEmpleados();
};


function ConsultaServidor(url) {

    $.post(url, function (data, status) {

        //console.log(data);
        if (data == "error") {
            table = $("#table_id").DataTable(lenguaje);
            return;
        }

        let tr = '';

        let cuadrilla = JSON.parse(data);
        let total_cuadrilla = cuadrilla.length;

        for (var i = 0; i < cuadrilla.length; i++) {


            tr += '<tr>' +
                '<td>' + (i + 1) + '</td>' +
                '<td>' + cuadrilla[i].Codigo.toUpperCase() + '</td>' +
                '<td>' + cuadrilla[i].Descripcion.toUpperCase() + '</td>' +
                '<td><button class="btn btn-primary fa fa-pencil btn-xs" onclick="ObtenerDatosxID(' + cuadrilla[i].IdCuadrilla + ')"></button>' +
                '<button class="btn btn-danger btn-xs  fa fa-trash" onclick="eliminar(' + cuadrilla[i].IdCuadrilla + ')"></button></td >' +
                '</tr>';
        }

        $("#tbody_cuadrilla").html(tr);
        $("#spnTotalRegistros").html(total_cuadrilla);

        table = $("#table_id").DataTable(lenguaje);

    });

}




function ModalNuevo() {
    $("#chkActivo").prop('checked', true);
    $("#lblTituloModal").html("Nueva Cuadrilla");
    AbrirModal("modal-form");
}




function GuardarCuadrilla() {
    let varIdCuadrilla = $("#txtId").val();
    let varCodigo = $("#txtCodigo").val();
    let varDescripcion = $("#txtDescripcion").val();
    let IdObra = $("#IdObra").val();
    let IdGrupo = $("#IdGrupo").val();
    let IdSubGrupo = $("#IdSubGrupo").val();
    let IdCapataz = $("#IdCapataz").val();
    let IdSupervisor = $("#IdSupervisor").val();
    let IdArea = $("#IdArea").val();
    let varEstado = false;

    if ($('#chkActivo')[0].checked) {
        varEstado = true;
    }

    $.post('UpdateInsertCuadrilla', {
        'IdCuadrilla': varIdCuadrilla,
        'Codigo': varCodigo,
        'Descripcion': varDescripcion,
        'IdObra': IdObra,
        'IdGrupo': IdGrupo,
        'IdSubGrupo': IdSubGrupo,
        'IdSupervisor': IdSupervisor,
        'IdCapataz': IdCapataz,
        'IdArea': IdArea,
        'Estado': varEstado
    }, function (data, status) {

        if (data == 1) {
            swal("Exito!", "Proceso Realizado Correctamente", "success")
            table.destroy();
            ConsultaServidor("ObtenerCuadrilla");
            limpiarDatos();

        } else {
            swal("Error!", "Ocurrio un Error")
            limpiarDatos();
        }

    });
}

function ObtenerDatosxID(varIdCuadrilla) {
    $("#lblTituloModal").html("Editar Cuadrilla");
    AbrirModal("modal-form");

    //console.log(varIdUsuario);

    $.post('ObtenerDatosxID', {
        'IdCuadrilla': varIdCuadrilla,
    }, function (data, status) {

        if (data == "Error") {
            swal("Error!", "Ocurrio un error")
            limpiarDatos();
        } else {
            let cuadrilla = JSON.parse(data);
            //console.log(usuarios);
            $("#txtId").val(cuadrilla[0].IdCuadrilla);
            $("#IdObra").val(cuadrilla[0].IdObra);
            $("#IdGrupo").val(cuadrilla[0].IdGrupo);
            $("#IdSubGrupo").val(cuadrilla[0].IdSubGrupo);
            $("#txtCodigo").val(cuadrilla[0].Codigo);
            $("#txtDescripcion").val(cuadrilla[0].Descripcion);
            $("#IdCapataz").val(cuadrilla[0].IdCapataz);
            $("#IdSupervisor").val(cuadrilla[0].IdSupervisor);
            $("#IdArea").val(cuadrilla[0].IdArea);
            if (cuadrilla[0].Estado) {
                $("#chkActivo").prop('checked', true);
            } 

            

        }

    });

}



function listarObra() {
    $.ajax({
        url: "../Obra/ObtenerObra",
        type: "GET",
        contentType: "application/json",
        dataType: "json",
        data: {
            'estado': 1
        },
        cache: false,
        contentType: false,
        success: function (datos) {
            $("#IdObra").html('');
            let options = `<option value="0">Seleccione</option>`;
            if (datos.length > 0) {

                for (var i = 0; i < datos.length; i++) {
                    options += `<option value="` + datos[i].IdObra + `">` + datos[i].Descripcion + `</option>`;
                }
                $("#IdObra").html(options);
            }
        }
    });
}

function listarGrupo() {
    $.ajax({
        url: "../Grupo/ObtenerGrupo",
        type: "GET",
        contentType: "application/json",
        dataType: "json",
        data: {
            'estado': 1
        },
        cache: false,
        contentType: false,
        success: function (datos) {
            $("#IdGrupo").html('');
            let options = `<option value="0">Seleccione</option>`;
            if (datos.length > 0) {

                for (var i = 0; i < datos.length; i++) {
                    options += `<option value="` + datos[i].IdGrupo + `">` + datos[i].Descripcion + `</option>`;
                }
                $("#IdGrupo").html(options);
            }
        }
    });
}


function listarSubGrupoxIdGrupo() {
    let idGrupo = $("#IdGrupo").val();
    $.ajax({
        url: "../Grupo/ObtenerSubGruposxIdGrupo",
        type: "GET",
        contentType: "application/json",
        dataType: "json",
        data: {
            'idGrupo': idGrupo
        },
        cache: false,
        contentType: false,
        success: function (datos) {
            $("#IdSubGrupo").html('');
            let options = `<option value="0">Seleccione</option>`;
            if (datos.length > 0) {

                for (var i = 0; i < datos.length; i++) {
                    options += `<option value="` + datos[i].IdGrupo + `">` + datos[i].Descripcion + `</option>`;
                }
                $("#IdSubGrupo").html(options);
            }
        }
    });
}

function listarArea() {
    $.ajax({
        url: "../Area/ObtenerArea",
        type: "GET",
        contentType: "application/json",
        dataType: "json",
        data: {
            'estado': 1
        },
        cache: false,
        contentType: false,
        success: function (datos) {
            $("#IdArea").html('');
            let options = `<option value="0">Seleccione</option>`;
            if (datos.length > 0) {

                for (var i = 0; i < datos.length; i++) {
                    options += `<option value="` + datos[i].IdArea + `">` + datos[i].Descripcion + `</option>`;
                }
                $("#IdArea").html(options);
            }
        }
    });
}



function listarEmpleados() {
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
            $("#IdSupervisor").html('');
            $("#IdCapataz").html('');
            let options = `<option value="0">Seleccione</option>`;
            if (datos.length > 0) {

                for (var i = 0; i < datos.length; i++) {
                    options += `<option value="` + datos[i].IdEmpleado + `">` + datos[i].RazonSocial + `</option>`;
                }
                $("#IdSupervisor").html(options);
                $("#IdCapataz").html(options);
            }
        }
    });
}



function eliminar(varIdLineaNegocio) {


    alertify.confirm('Confirmar', '¿Desea eliminar esta Cuadrilla?', function () {
        $.post("EliminarLineaNegocio", { 'IdLineaNegocio': varIdLineaNegocio }, function (data) {

            if (data == 0) {
                swal("Error!", "Ocurrio un Error")
                limpiarDatos();
            } else {
                swal("Exito!", "Cuadrilla Eliminada", "success")
                table.destroy();
                ConsultaServidor("ObtenerCuadrilla");
                limpiarDatos();
            }

        });

    }, function () { });

}


function limpiarDatos() {
    $("#txtId").val("");
    $("#txtCodigo").val("");
    $("#txtDescripcion").val("");
    $("#chkActivo").prop('checked', false);
}



