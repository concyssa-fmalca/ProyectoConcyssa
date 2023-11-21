let table = '';


window.onload = function () {
    listarObra();
    listarObraFiltro()
    listarArea();
    listarEmpleados();
    
};


function ConsultaServidor() {
    let varIdObra = $("#cboObras").val()
    $.post("ObtenerCuadrillaxIdObra", {'IdObra': varIdObra }, function (data, status) {

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
        if (table) { table.destroy()}
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
    //let CuentaServicios = $("#txtCuentaServicios").val();
    let CuentaMateriales = $("#txtCuentaMateriales").val();
    let varEstado = false;
    let EsTercero = false;

    if ($('#chkActivo')[0].checked) {
        varEstado = true;
    }

    if ($('#EsTercero')[0].checked) {
        EsTercero = true;
    }

    if (varCodigo == "" || varCodigo == undefined) {
        Swal.fire("Error", "El Campo Codigo es Obligatorio", "info")
        return
    }
    if (varDescripcion == "" || varDescripcion == undefined) {
        Swal.fire("Error", "El Campo Descripcion es Obligatorio", "info")
        return
    }
    if (IdObra == 0 || IdObra == undefined) {
        Swal.fire("Error", "El Campo Obra es Obligatorio", "info")
        return
    }
    if (IdGrupo == 0 || IdGrupo == undefined) {
        Swal.fire("Error", "El Campo Grupo es Obligatorio", "info")
        return
    }
    if (IdSubGrupo == 0 || IdSubGrupo == undefined) {
        Swal.fire("Error", "El Campo SubGrupo es Obligatorio", "info")
        return
    }
    if (IdCapataz == 0 || IdCapataz == undefined) {
        Swal.fire("Error", "El Campo Capataz es Obligatorio", "info")
        return
    }
    if (IdSupervisor == 0 || IdSupervisor == undefined) {
        Swal.fire("Error", "El Campo Supervisor es Obligatorio", "info")
        return
    }
    if (IdArea == 0 || IdArea == undefined) {
        Swal.fire("Error", "El Campo Area es Obligatorio", "info")
        return
    }
    //if (CuentaServicios == "" || CuentaServicios == undefined) {
    //    Swal.fire("Error", "El Campo Cuenta de Consumo - Servicios es Obligatorio", "info")
    //    return
    //}
    if (CuentaMateriales == "" || CuentaMateriales == undefined) {
        Swal.fire("Error", "El Campo Cuenta de Consumo - Materiales es Obligatorio", "info")
        return
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
        'Estado': varEstado,
        'EsTercero': EsTercero,
        //'CuentaServicios': CuentaServicios,
        'CuentaMateriales': CuentaMateriales
    }, function (data, status) {

        if (data == 1) {
            swal("Exito!", "Proceso Realizado Correctamente", "success")
            //table.destroy();
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
            listarGrupo()
            setTimeout(() => {
                $("#IdGrupo").val(cuadrilla[0].IdGrupo).change();
            }, 50);
            listarSubGrupoxIdGrupo()
            setTimeout(() => {
                $("#IdSubGrupo").val(cuadrilla[0].IdSubGrupo);
            }, 100);
           
   

            $("#txtCodigo").val(cuadrilla[0].Codigo);
            $("#txtDescripcion").val(cuadrilla[0].Descripcion);
            $("#IdCapataz").val(cuadrilla[0].IdCapataz);
            $("#IdSupervisor").val(cuadrilla[0].IdSupervisor);
            $("#IdArea").val(cuadrilla[0].IdArea);
            $("#txtCuentaMateriales").val(cuadrilla[0].CuentaMateriales);
            //$("#txtCuentaServicios").val(cuadrilla[0].CuentaServicios);

            if (cuadrilla[0].Estado) {
                $("#chkActivo").prop('checked', true);
            } 

            if (cuadrilla[0].EsTercero) {
                $("#EsTercero").prop('checked', true);
            } 

            

        }

    });

}



function listarObra() {
    $.ajax({
        url: "../Obra/ObtenerObraxIdUsuarioSessionFiltro",
        type: "GET",
        contentType: "application/json",
        dataType: "json",
        data: {
            'estado': 1
        },
        async: false,
        cache: false,
        contentType: false,
        success: function (datos) {
            $("#IdObra").html('');
            let options = `<option value="0">Seleccione</option>`;
            if (datos.length > 0) {

                for (var i = 0; i < datos.length; i++) {
                    options += `<option value="` + datos[i].IdObra + `">` + datos[i].Codigo +" - "+ datos[i].Descripcion + `</option>`;
                }
                $("#IdObra").html(options);
            }
        }
    });
    $("#cboObras").prop("selectedIndex", 1)
}
function listarObraFiltro() {
    $.ajax({
        url: "../Obra/ObtenerObraxIdUsuarioSessionFiltro",
        type: "GET",
        contentType: "application/json",
        dataType: "json",
        data: {
            'estado': 1
        },
        async:false,
        cache: false,
        contentType: false,
        success: function (datos) {
            $("#cboObras").html('');
            let options = `<option value="0">Seleccione</option>`;
            if (datos.length > 0) {

                for (var i = 0; i < datos.length; i++) {
                    options += `<option value="` + datos[i].IdObra + `">` + datos[i].Codigo + " - " + datos[i].Descripcion + `</option>`;
                }
                $("#cboObras").html(options);
               
            }
        }
    });
    $("#cboObras").prop("selectedIndex", 1)
    ConsultaServidor()
}

function listarGrupo() {
    $.ajax({
        url: "../Grupo/ObtenerGrupoxObra",
        type: "GET",
        contentType: "application/json",
        dataType: "json",
        data: {
            'IdObra': $("#IdObra").val()
        },
        cache: false,
        contentType: false,
        success: function (datos) {
            $("#IdGrupo").html('');
            let options = `<option value="0">Seleccione</option>`;
            if (datos.length > 0) {

                for (var i = 0; i < datos.length; i++) {
                    options += `<option value="` + datos[i].IdGrupo + `">` + datos[i].Codigo + " - " + datos[i].Descripcion + `</option>`;
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
        async: false,
        success: function (datos) {
            $("#IdSubGrupo").html('');
            let options = `<option value="0">Seleccione</option>`;
            if (datos.length > 0) {

                for (var i = 0; i < datos.length; i++) {
                    options += `<option value="` + datos[i].IdSubGrupo + `">` + datos[i].Codigo + " - " + datos[i].Descripcion + `</option>`;
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
        $.post("EliminarCuadrilla", { 'IdCuadrilla': varIdLineaNegocio }, function (data) {

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
    $("#EsTercero").prop('checked', false);

    $("#IdObra").val(0);
    $("#IdGrupo").val(0);
    $("#IdSubGrupo").val(0);
    $("#IdCapataz").val(0);
    $("#IdSupervisor").val(0);
    $("#IdArea").val(0);
    $("#txtCuentaMateriales").val('');
    //$("#txtCuentaServicios").val('');
    
}
function colocarCodigo() {
    console.log($("#IdSubGrupo").val())
    let CodigoUsar = 
    $.post('../Grupo/ObtenerSubGrupoxID', {
        'IdSubGrupo': $("#IdSubGrupo").val(),
    }, function (data, status) {

        if (data == "Error") {
            swal("Error!", "Ocurrio un error")
            limpiarDatos();
        } else {
            let grupo = JSON.parse(data);
            CodigoUsar = (grupo[0].Codigo);

            $.post('ObtenerNuevoCodigo', {
                'CodigoUsar': CodigoUsar,
                'IdObra': $("#IdObra").val()
            }, function (data, status) {

                if (data == "Error") {
                    swal("Error!", "Ocurrio un error")
                    limpiarDatos();
                } else {
                    let grupo = JSON.parse(data);
                    $("#txtCodigo").val(grupo[0].Codigo) 
                }

            });
        }

    });

}



