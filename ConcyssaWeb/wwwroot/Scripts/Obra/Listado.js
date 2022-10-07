let table = '';
let tablecatalogo;
//let table2 = $("#table_catalogoobra").DataTable(lenguaje);;

window.onload = function () {
    var url = "ObtenerObra";
    ConsultaServidor(url);
    listarBase();
    listarTipoObra();
    listarDivision();
};

function listarDivision() {
    $.ajax({
        url: "../Division/ObtenerDivision",
        type: "GET",
        contentType: "application/json",
        dataType: "json",
        data: {
            'estado': 1
        },
        cache: false,
        contentType: false,
        success: function (datos) {
            $("#IdDivision").html('');
            let options = `<option value="0">Seleccione</option>`;
            if (datos.length > 0) {

                for (var i = 0; i < datos.length; i++) {
                    options += `<option value="` + datos[i].IdDivision + `">` + datos[i].Descripcion + `</option>`;
                }
                $("#IdDivision").html(options);
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
            $("#IdBase").html('');
            let options = `<option value="0">Seleccione</option>`;
            if (datos.length > 0) {

                for (var i = 0; i < datos.length; i++) {
                    options += `<option value="` + datos[i].IdBase + `">` + datos[i].Descripcion + `</option>`;
                }
                $("#IdBase").html(options);
            }
        }
    });
}

function listarTipoObra() {
    $.ajax({
        url: "../TipoObra/ObtenerTipoObra",
        type: "GET",
        contentType: "application/json",
        dataType: "json",
        data: {
            'estado': 1
        },
        cache: false,
        contentType: false,
        success: function (datos) {
            $("#IdTipoObra").html('');
            let options = `<option value="0">Seleccione</option>`;
            if (datos.length > 0) {

                for (var i = 0; i < datos.length; i++) {
                    options += `<option value="` + datos[i].IdTipoObra + `">` + datos[i].Descripcion + `</option>`;
                }
                $("#IdTipoObra").html(options);
            }
        }
    });
}

function ConsultaServidor(url) {

    $.post(url, function (data, status) {

        //console.log(data);
        if (data == "error") {
            table = $("#table_id").DataTable(lenguaje);
            return;
        }

        let tr = '';

        let obra = JSON.parse(data);
        let total_obra = obra.length;

        for (var i = 0; i < obra.length; i++) {
            tr += '<tr>' +
                '<td>' + (i + 1) + '</td>' +
                '<td>' + obra[i].Codigo.toUpperCase() + '</td>' +
                '<td>' + obra[i].Descripcion.toUpperCase() + '</td>' +
                '<td>' + obra[i].DescripcionCorta.toUpperCase() + '</td>' +
                '<td>' + obra[i].DescripcionBase.toUpperCase() + '</td>' +
                '<td>'+
                    '<button class="btn btn-primary fa fa-pencil btn-xs" onclick = "ObtenerDatosxID(' + obra[i].IdObra + ')" ></button > ' +
                    '<button class="btn btn-danger btn-xs  fa fa-trash" onclick="eliminar(' + obra[i].IdObra + ')"></button>' +
                    '<button class="btn btn-danger btn-xs" onclick="uicatalogoproducto(' + obra[i].IdObra + ')">C</button>' +
                    //'<button class="btn btn-danger btn-xs" onclick="descargarcatatalogo(' + obra[i].IdObra + ')">D</button>' + 
                '</td >' +
                '</tr>';
        }

        $("#tbody_obra").html(tr);
        $("#spnTotalRegistros").html(total_obra);

        table = $("#table_id").DataTable(lenguaje);

    });

}


function uicatalogoproducto(IdObra) {
    AbrirModal("modal-catalogoproducto");
    CargarCatalogoProductoxIdObra(IdObra);

}

function CargarCatalogoProductoxIdObra(IdObra) {
    $("#txtIdObraCatalogoProducto").val(IdObra);
    tablecatalogo = $('#table_catalogoobra').dataTable({
        language: lenguaje_data,
        responsive: true,
        ajax: {
            url: "../Obra/CargarCatalogoxIdObra",
            type: 'POST',
            data: {
                IdObra: IdObra,
                pagination: {
                    perpage: 50,
                },
            },
        },

        columnDefs: [
            // {"className": "text-center", "targets": "_all"},
      
            {
                targets: 0,
                orderable: false,
                render: function (data, type, full, meta) {
                    return meta.row + 1
                },
            },
            {
                targets: 1,
                orderable: false,
                render: function (data, type, full, meta) {
                    return full.DescripcionArticulo.toUpperCase()
                },
            },
            {
                targets: 2,
                orderable: false,
                render: function (data, type, full, meta) {
                    if (full.IdTipoProducto == 0) {
                        return ` <input type="radio" name="tipoproducto` + full.IdArticulo + `" value="1" idProducto="` + full.IdArticulo + `">`
                    }
                    if (full.IdTipoProducto == 1) {
                        return ` <input type="radio" name="tipoproducto` + full.IdArticulo + `" value="1" idProducto="` + full.IdArticulo + `" checked>`;
                    }

                    if (full.IdTipoProducto == 2) {
                        return ` <input type="radio" name="tipoproducto` + full.IdArticulo + `" value="1" idProducto="` + full.IdArticulo + `">`;
                    }

                }
            },
            {
                targets: 3,
                orderable: false,
                render: function (data, type, full, meta) {
                    if (full.IdTipoProducto == 0) {
                        return ` <input type="radio" name="tipoproducto` + full.IdArticulo + `" value="2" idProducto="` + full.IdArticulo + `">`
                    }
                    if (full.IdTipoProducto == 1) {
                        return ` <input type="radio" name="tipoproducto` + full.IdArticulo + `" value="2" idProducto="` + full.IdArticulo + `">`
                    }

                    if (full.IdTipoProducto == 2) {
                        return ` <input type="radio" name="tipoproducto` + full.IdArticulo + `" value="2" checked idProducto="` +full.IdArticulo + `">`;
                    }
                },
            }

        ],
        "bDestroy": true
    }).DataTable();




    //$.ajax({
    //    url: "../Obra/CargarCatalogoxIdObra",
    //    type: "GET",
    //    contentType: "application/json",
    //    dataType: "json",
    //    data: {
    //        'IdObra': IdObra
    //    },
    //    cache: false,
    //    contentType: false,
    //    success: function (datos) {

    //        $("#txtIdObraCatalogoProducto").val(IdObra);
    //        let tr = '';
    //        for (var i = 0; i < datos.length; i++) {
    //            tr += `<tr>
    //                <td>`+ (i + 1) + `</td>
    //                <td>`+ datos[i].DescripcionArticulo + `</td>`;
    //            if (datos[i].IdTipoProducto==0) {
    //                tr += `<td>` + ` <input type="radio" name="tipoproducto` + datos[i].IdArticulo + `" value="1" idProducto="` + datos[i].IdArticulo +`">` + `</td>
    //                <td>` + ` <input type="radio" name="tipoproducto` + datos[i].IdArticulo + `" value="2" idProducto="` + datos[i].IdArticulo +`">` + `</td>`;
    //            }

    //            if (datos[i].IdTipoProducto == 1) {
    //                tr += `<td>` + ` <input type="radio" name="tipoproducto` + datos[i].IdArticulo + `" value="1" idProducto="` + datos[i].IdArticulo +`" checked>` + `</td>
    //                <td>`+ ` <input type="radio" name="tipoproducto` + datos[i].IdArticulo + `" value="2" idProducto="` + datos[i].IdArticulo +`">` + `</td>`;
    //            }

    //            if (datos[i].IdTipoProducto == 2) {
    //                tr += `<td>` + ` <input type="radio" name="tipoproducto` + datos[i].IdArticulo + `" value="1" idProducto="` + datos[i].IdArticulo +`">` + `</td>
    //                <td>`+ ` <input type="radio" name="tipoproducto` + datos[i].IdArticulo + `" value="2" checked idProducto="` + datos[i].IdArticulo +`">` + `</td>`;
    //            }
                
    //            tr+=`</tr>`;
    //        }

    //        $("#tbody_catalogo").html(tr);
    //        table2 = $("#table_catalogoobra").DataTable(lenguaje);
    //    }
    //});
}



function ModalNuevo() {
    $("#lblTituloModal").html("Nueva Obra");
    AbrirModal("modal-form");
}




function GuardarObra() {
    let varIdObra = $("#txtId").val();
    let IdBase = $("#IdBase").val();
    let txtCodigo = $("#txtCodigo").val();
    let txtDescripcion = $("#txtDescripcion").val();
    let txtDescripcionCorta = $("#txtDescripcionCorta").val();
    let IdTipoObra = $("#IdTipoObra").val();
    let IdDivision = $("#IdDivision").val();

    let Direccion = $("#txtDireccion").val();
    let VisibleInternet = false;
    if ($('#chkIntranet')[0].checked) {
        VisibleInternet = true;
    }

    let ContratoMantenimiento = false;
    if ($('#chkContrato')[0].checked) {
        ContratoMantenimiento = true;
    }

    let Estado = false;
    if ($('#chkActivo')[0].checked) {
        Estado = true;
    }

    $.post('UpdateInsertObra', {
        'IdObra': varIdObra,
        'IdBase': IdBase,
        'Codigo': txtCodigo,
        'Descripcion': txtDescripcion,
        'DescripcionCorta': txtDescripcionCorta,
        'IdTipoObra': IdTipoObra,
        'IdDivision': IdDivision,
        'VisibleInternet': VisibleInternet,
        'ContratoMantenimiento': ContratoMantenimiento,        
        'Estado': Estado,
        'Direccion': Direccion
    }, function (data, status) {

        if (data == 1) {
            swal("Exito!", "Proceso Realizado Correctamente", "success")
            table.destroy();
            ConsultaServidor("ObtenerObra");
            limpiarDatos();

        } else {
            swal("Error!", "Ocurrio un Error")
            limpiarDatos();
        }

    });
}

function GuardarObraCatalogo() {
    let detalles = [];
    let IdObra=$("#txtIdObraCatalogoProducto").val();
    $('input:radio:checked').each(function (val,obj) { // find unique names
        detalles.push({
            'IdArticulo': parseInt(obj.attributes['idproducto'].value) ,
            'IdTipoProducto': parseInt(obj.value),
            'IdObra': parseInt(IdObra)

        });
    });
    $.post('UpdateInsertObraCatalogoProducto', {
        'detalles': detalles
    }, function (data, status) {
        if (data == 1) {
            swal("Exito!", "Proceso Realizado Correctamente", "success")
            table.destroy();
            ConsultaServidor("ObtenerObra");
            limpiarDatos();

        } else {
            swal("Error!", "Ocurrio un Error")
            limpiarDatos();
        }

    });
}





function ObtenerDatosxID(varIdObra) {
    $("#lblTituloModal").html("Editar Obra");
    AbrirModal("modal-form");

    //console.log(varIdUsuario);

    $.post('ObtenerDatosxID', {
        'IdObra': varIdObra,
    }, function (data, status) {

        if (data == "Error") {
            swal("Error!", "Ocurrio un error")
            limpiarDatos();
        } else {
            let obra = JSON.parse(data);
            //console.log(usuarios);
            $("#txtId").val(obra[0].IdObra);
            $("#IdBase").val(obra[0].IdBase);
            $("#txtCodigo").val(obra[0].Codigo);
            $("#txtDescripcion").val(obra[0].Descripcion);
            $("#txtDescripcionCorta").val(obra[0].DescripcionCorta);
            $("#txtDireccion").val(obra[0].Direccion);
            $("#IdTipoObra").val(obra[0].IdTipoObra);
            $("#IdDivision").val(obra[0].IdDivision);
            if (obra[0].VisibleInternet) {
                $("#chkIntranet").prop('checked', true);
            }
            if (obra[0].ContratoMantenimiento) {
                $("#chkContrato").prop('checked', true);
            }
            if (obra[0].Estado) {
                $("#chkActivo").prop('checked', true);
            }

        }

    });

}

function eliminar(varIdObra) {


    alertify.confirm('Confirmar', '¿Desea eliminar esta Obra?', function () {
        $.post("EliminarObra", { 'IdObra': varIdObra }, function (data) {

            if (data == 0) {
                swal("Error!", "Ocurrio un Error")
                limpiarDatos();
            } else {
                swal("Exito!", "Obra Eliminada", "success")
                table.destroy();
                ConsultaServidor("ObtenerObra");
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

    $("#IdBase").val(0);
    $("#txtDescripcionCorta").val("");
    $("#txtDireccion").val("");
    $("#IdTipoObra").val(0);
    $("#IdDivision").val(0);
}



