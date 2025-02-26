window.onload = function () {
    CargarBaseFiltro()
    $("#txtFechaInicio").val(getCurrentDate())
    $("#txtFechaFin").val(getCurrentDateFinal())
    //ListarSolicitudes()
    ListarSeries()
    ConsultaServidor()
    $(".cboArticuloPedidoTodo").select2()
    $("#cboAgregarArticulo").select2({
        dropdownParent: $("#ModalAgregar")
    })

}



function CargarBaseFiltro() {
    $.ajaxSetup({ async: false });
    $.post("/Base/ObtenerBasexIdUsuario", function (data, status) {
        let base = JSON.parse(data);
        llenarComboBaseFiltro(base, "cboObraFiltro")

    });

}

function llenarComboBaseFiltro(lista, idCombo, primerItem) {
    var contenido = "";
    if (primerItem != null) contenido = "<option value='0'>" + primerItem + "</option>";
    var nRegistros = lista.length;
    var nCampos;
    var campos;
    for (var i = 0; i < nRegistros; i++) {

        if (lista.length > 0) { contenido += "<option value='" + lista[i].IdBase + "'>" + lista[i].Descripcion.toUpperCase() + "</option>"; }
        else { }
    }
    var cbo = document.getElementById(idCombo);
    if (cbo != null) cbo.innerHTML = contenido;
    $("#cboObraFiltro").val($("#cboObraFiltro option:first").val()).change();

}
function ObtenerObraxIdBase() {
    let IdBase = $("#cboObraFiltro").val();
    $.ajaxSetup({ async: false });
    $.post("/Obra/ObtenerObraxIdUsuarioSession", { 'IdBase': IdBase }, function (data, status) {
        let obra = JSON.parse(data);
        llenarComboObra(obra, "IdObra", "seleccione")
    });
}

function llenarComboObra(lista, idCombo, primerItem) {
    var contenido = "";
    if (primerItem != null) contenido = "<option value='0'>" + primerItem + "</option>";
    var nRegistros = lista.length;
    var nCampos;
    var campos;
    for (var i = 0; i < nRegistros; i++) {

        if (lista.length > 0) { contenido += "<option value='" + lista[i].IdObra + "'>" + lista[i].Descripcion.toUpperCase() + "</option>"; }
        else { }
    }
    var cbo = document.getElementById(idCombo);
    if (cbo != null) cbo.innerHTML = contenido;
    $("#" + idCombo).prop("selectedIndex", 1)
}
function getCurrentDate() {
    var currentDate = new Date();
    var year = currentDate.getFullYear();
    var month = ('0' + (currentDate.getMonth() + 1)).slice(-2);
    var formattedDate = year + '-' + month + '-' + '01';
    return formattedDate;
}
function getCurrentDateFinal() {
    var date = new Date();

    var ultimoDia = new Date(date.getFullYear(), date.getMonth() + 1, 0);
    var year = date.getFullYear();
    var month = ('0' + (date.getMonth() + 1)).slice(-2);
    var formattedDate = year + '-' + month + '-' + ultimoDia.getDate();
    return formattedDate
}
function ListarSolicitudes() {
    let IdObra = $("#cboObraFiltro").val()
    $.post('/SolicitudDespacho/ObtenerSolicitudesDespachoxObra', { 'IdObra': IdObra }, function (data, status) {

        if (data == "error") {
            table = $("#table_id").DataTable(lenguaje);
            return;
        }

        let tr = '';

        let solicitudes = JSON.parse(data);
        console.log(solicitudes)
        // let total_solicitudes = solicitudes.length;

        for (var i = 0; i < solicitudes.length; i++) {
            let Fecha = (solicitudes[i].FechaContabilizacion).split('T')[0]
            Fecha = Fecha.split('-')[2] + "-" + Fecha.split('-')[1] + "-" + Fecha.split('-')[0]
            solicitudes[i].FechaContabilizacion = Fecha
            tr += '<tr>' +
                '<td><input type="hidden" value="' + (solicitudes[i].Id) + '" id="txtIdSolicitud' + i + '" class="form-control" style="max-width:60px" >' + (i + 1) + '</td>' +
                '<td>' + solicitudes[i].FechaContabilizacion + '</td>' +
                '<td>' + solicitudes[i].SerieyNum + '</td>' +
                '<td><select id="cboArticuloPedido' + i + '" class="form-control cboArticuloPedidoTodo" onchange="CargarProductosTodos(' + i + ',' + solicitudes[i].IdTipoProducto + ',' + solicitudes[i].IdObra + ')"></select></td>' +
                '<td>' + solicitudes[i].NombCuadrilla + '</td>' +
                '<td>' + solicitudes[i].Cantidad + '</td>' +
                '<td><input type="text" value="' + solicitudes[i].CantidadAtendida + '" id="txtCantidadAtendida' + i + '" class="form-control" style="max-width:60px"></td>' +
                '<td style="display:none">' +
                //ITEM OCULTOS
                '<input type="text" value="' + solicitudes[i].IdTipoProducto + '" id="txtIdTipoProducto' + i + '"></input>' +
                '<input type="text" value="' + solicitudes[i].IdItem + '" id="txtIdArticulo' + i + '"></input>' +
                '<td> ' +
                '</tr>';

            LlenarArticuloxPedido(i, solicitudes[i].IdItem)

        }

        $("#tbody_Solicitudes").html(tr);


        //table = $("#table_id").DataTable(lenguaje);

    });
}
function LlenarArticuloxPedido(contador, IdArticulo) {
    $.ajax({
        url: "/Articulo/ObtenerDatosxID",
        type: "POST",
        async: true,
        data: {
            'IdArticulo': IdArticulo
        },
        beforeSend: function () {

        },
        success: function (data) {


            if (data == "error") {
                //Swal.fire({
                //    icon: 'error',
                //    title: 'Error',
                //    text: 'Ocurrio un Error!'
                //})
                console.log("aqui BuscarClientexID");
            } else {
                let datos = JSON.parse(data);



                $('#cboArticuloPedido' + contador).select2({
                    language: "es",
                    width: '100%',
                    theme: "classic",
                    data: [
                        {
                            id: datos[0].IdArticulo,
                            text: datos[0].Descripcion1
                        },

                    ]
                });

                $('#cboArticuloPedido' + contador).val(datos[0].IdArticulo).trigger('change');


            }

        }
    }).fail(function () {

        Swal.fire({
            icon: 'error',
            title: 'Error',
            text: 'Comunicarse con el Area Soporte: smarcode@smartcode.pe !'
        })
    });

}




function CargarProductosTodos(contador, IdTipoProducto, IdObra) {

    let varTipoProducto = IdTipoProducto
    let varIdObra = IdObra
    //console.log(contador)
    //console.log(varTipoProducto);

    $("#cboArticuloPedido" + contador).select2({
        language: "es",
        width: '100%',
        //theme: "classic",
        async: false,
        ajax: {
            url: "/Articulo/ObtenerArticulosSelect2",
            type: "post",
            dataType: 'json',
            delay: 250,
            data: function (params) {

                return {
                    searchTerm: params.term, // search term
                    TipoProducto: varTipoProducto,
                    IdObra: varIdObra
                };



            },
            processResults: function (response) {

                var results = [];
                $.each(response, function (index, item) {
                    results.push({ id: item.IdArticulo, text: item.Codigo + " | " + item.Descripcion1 })
                });


                return { results }


            },
            cache: true,
        },
        placeholder: 'Ingrese Nombre de Producto',
        minimunInputLength: 3
    });
}
let Igeneral = 0;

function ConsultaServidor() {
    let FechaInicio = $("#txtFechaInicio").val()
    let FechaFin = $("#txtFechaFin").val()
    let IdBase = $("#cboObraFiltro").val()
    let IdObra = $("#IdObra").val()
    let EstadoSolicitud = $("#EstadoFiltro").val()
    let SerieFiltro = $("#cboSeries").val()
    $.ajaxSetup({ async: false });
    $.post('/DevolucionAdm/ObtenerDevolucionAdmAtender', { 'IdBase': IdBase,'IdObra':IdObra, 'FechaInicio': FechaInicio, 'FechaFin': FechaFin, 'EstadoDevolucion': EstadoSolicitud, 'SerieFiltro': SerieFiltro }, function (data, status) {
        if (data != 'error') {
            let SolicitudDespacho = JSON.parse(data);
            //console.log(SolicitudDespacho)
            let InsertarHTML = ''


            for (var i = 0; i < SolicitudDespacho.length; i++) {
                Igeneral = i;
                //console.log(SolicitudDespacho[i])
                //console.log(i)
                let tr = ''
                let Fecha = (SolicitudDespacho[i].FechaContabilizacion).split('T')[0]
                Fecha = Fecha.split('-')[2] + "-" + Fecha.split('-')[1] + "-" + Fecha.split('-')[0]
                InsertarHTML += `  <div class="panel" style="background-color:aliceblue">
                                <div class="row">
                                    <label class="col-md-2" style="max-width:130px;color:black">Serie y Número:</label>
                                    <p class="col-md-1">`+ SolicitudDespacho[i].SerieyNum + `</p>
                                    <label class="col-md-2" style="max-width:70px;color:black">Fecha:</label>
                                    <p class="col-md-1">`+ Fecha + `</p>
                                    <label for="cboAlmacen`+ i + `" class="col-md-1" style="color:black">Almacen:</label>
                                    <div class="col-md-4" style="margin-top:-3px">`;

                InsertarHTML += `<select id="cboAlmacen` + i + `" class="form-control" name="cboAlmacenGeneral" onchange="ObtenerStocks(` + i + `)" >`;


                $.ajax({
                    url: "/Almacen/ObtenerAlmacenxIdObra",
                    type: 'POST',
                    async: false,
                    dataType: 'json',
                    data: { 'IdObra': SolicitudDespacho[i].IdObra },
                    success: function (datos) {
                        //console.log(datos);

                        for (var k = 0; k < datos.length; k++) {
                            InsertarHTML += `<option value="` + datos[k].IdAlmacen + `">` + datos[k].Descripcion + `</option>`;
                        }

                    },
                    error: function () {
                        Swal("Error!", "Ocurrió un Error", "error");
                    }
                })



                InsertarHTML += `</select> 
                                </div>
                                    <button class="btn btn-primary" style="margin-top:0px" onclick="AgregarItem(`+ SolicitudDespacho[i].IdObra + `,` + SolicitudDespacho[i].IdTipoProducto + `,` + i + `)">Agregar Artículo</button>
                                </div>
                                <div class="row">
                                    <label class="col-md-2" style="max-width:130px;color:black">Centro de Costo:</label>
                                    <p class="col-md-5">`+ SolicitudDespacho[i].NombCuadrilla + `</p>
                                    <label for="cboResponsable" class="col-md-2" style="max-width:130px;color:black">Entregado A:</label>
                                    <div class="col-md-4" style="margin-top:-3px">
                                        <select id="cboResponsable`+ i + `" name="cboResponsableGeneral" class="form-control"></select>
                                    </div>
                                    <input style="display:none" id="Ocultos`+ i + `" value="0"/>
                                </div>
                                <div class="row">
                                    <table id="table_id`+ i + `" class="table" style="width:100%">
                                        <thead>
                                            <tr>
                                                <th>#</th>
                                                <th>Articulo</th>
                                                <th style="max-width:60px">Cantidad Solicitada</th>
                                                <th style="max-width:60px">Cantidad Atendida</th>
                                                <th style="max-width:60px">Cantidad A Atender</th>
                                                <th style="max-width:60px">Stock</th>
                                                <th style="max-width:60px">Precio Unitario</th>
                                                <th style="max-width:60px">Total</th>   
                                                <th style="max-width:60px">Accion</th>
                                            </tr>
                                        </thead>
                                        <tbody id="tbody_Solicitudes`+ i + `">`;
                for (var j = 0; j < SolicitudDespacho[i].Detalles.length; j++) {
                    let Saldo = +SolicitudDespacho[i].Detalles[j].Cantidad - +SolicitudDespacho[i].Detalles[j].CantidadAtendida
                    if (SolicitudDespacho[i].EstadoSolicitud == 1) {
                        if (SolicitudDespacho[i].Detalles[j].CantidadAtendida < SolicitudDespacho[i].Detalles[j].Cantidad) {
                            let IdFila = (i + 1).toString() + j.toString()

                            InsertarHTML += ` <tr id="FilaDetalle` + IdFila + `"> 
                                                    <td><input type="text" style="display:none" value="` + (SolicitudDespacho[i].Detalles[j].Id) + `" id="txtIdSolicitud` + i + `" class="form-control IdDetalleCol` + i + `" style="max-width:60px" >` + (j + 1) + `</td>
                                                    <td class="DescripcionTabla`+ i + `">` + SolicitudDespacho[i].Detalles[j].CodigoArticulo + `-` + SolicitudDespacho[i].Detalles[j].Descripcion + `</td>
                                                    <td class="CantidadPedida`+ i + `">` + SolicitudDespacho[i].Detalles[j].Cantidad + `</td>
                                                    <td>`+ SolicitudDespacho[i].Detalles[j].CantidadAtendida + `</td>
                                                    <td><input type="text" value="` + Saldo + `" id="txtCantidadAtendida` + i + `" onchange="VerificarEntregaStock(` + IdFila + `);ObtenerStocks(` + i + `)" class="form-control  EntregarDetalle` + i + ` NoPasarStock` + IdFila + `" style="max-width:60px"></td>
                                                    <td id="StockDetalle`+ IdFila + `" class="ClassStockDetalle` + i + `">d</td>
                                                    <td class="PrecioDetalle`+ i + `">Precio</td>
                                                    <td class="TotalDetalle`+ i + `">Total</td>
                                                    <td><button class="btn btn-xs btn-danger fa fa-times" onclick="EliminarFila(`+ IdFila + `,` + i + `)"></button></td>
                                                    <td style="display:none" class="IdItemDetalle`+ i + `" >` + SolicitudDespacho[i].Detalles[j].IdItem + `</td> 
                                                    </tr>`

                        }
                    } else {
                        let IdFila = (i + 1).toString() + j.toString()

                        InsertarHTML += ` <tr id="FilaDetalle` + IdFila + `"> 
                                                    <td><input type="text" style="display:none" value="` + (SolicitudDespacho[i].Detalles[j].Id) + `" id="txtIdSolicitud` + i + `" class="form-control IdDetalleCol` + i + `" style="max-width:60px" >` + (j + 1) + `</td>
                                                    <td class="DescripcionTabla`+ i + `">` + SolicitudDespacho[i].Detalles[j].CodigoArticulo + `-` + SolicitudDespacho[i].Detalles[j].Descripcion + `</td>
                                                    <td class="CantidadPedida`+ i + `">` + SolicitudDespacho[i].Detalles[j].Cantidad + `</td>
                                                    <td>`+ SolicitudDespacho[i].Detalles[j].CantidadAtendida + `</td>
                                                    <td><input type="text" value="` + SolicitudDespacho[i].Detalles[j].Cantidad + `" id="txtCantidadAtendida` + i + `" onchange="VerificarEntregaStock(` + IdFila + `);ObtenerStocks(` + i + `)" class="form-control  EntregarDetalle` + i + ` NoPasarStock` + IdFila + `" style="max-width:60px"></td>
                                                    <td id="StockDetalle`+ IdFila + `" class="ClassStockDetalle` + i + `">d</td>
                                                    <td class="PrecioDetalle`+ i + `">Precio</td>
                                                    <td class="TotalDetalle`+ i + `">Total</td>
                                                    <td><button class="btn btn-xs btn-danger fa fa-times" onclick="EliminarFila(`+ IdFila + `,` + i + `)"></button></td>
                                                    <td style="display:none" class="IdItemDetalle`+ i + `" >` + SolicitudDespacho[i].Detalles[j].IdItem + `</td> 
                                                    </tr>`
                    }
                }


                InsertarHTML += `  </tbody>
                                                    </table>
                                                </div>
                                            <div class=row>`
                if (SolicitudDespacho[i].EstadoDevolucion != 2) {
                    InsertarHTML += `<button class="btn btn-primary" style="margin-top:0px" onclick="GenerarEntrada(` + i + `,` + SolicitudDespacho[i].Id + `,'` + SolicitudDespacho[i].SerieyNum + `',` + SolicitudDespacho[i].IdCuadrilla + `)">Crear Entrada</button>
                                                <button class="btn btn-primary" style="margin-top:0px" onclick="CerrarSolicitud(`+ SolicitudDespacho[i].Id + `)">Cerrar Devolucion</button>`

                }
                InsertarHTML += `</div>
                                            </div>
                                            <br/>`;
                $("#SolicitudesCard").html(InsertarHTML);
                ObtenerEmpleados(i, SolicitudDespacho[i].IdSolicitante)
                //CargarAlmacen(SolicitudDespacho[i].IdObra, i)

                //ObtenerStocks(i)
                //console.log(i);
            }
        } else {
            $("#SolicitudesCard").empty()
        }
    });
    LlenarStocks()
}
function LlenarStocks() {
    //console.log(Igeneral)
    for (var i = 0; i <= Igeneral; i++) {
        //console.log(i)
        ObtenerStocks(i)
    }
}

function llenarComboAlmacen(lista, idCombo, primerItem) {
    var contenido = "";
    if (primerItem != null) contenido = "<option value='0'>" + primerItem + "</option>";
    var nRegistros = lista.length;
    var nCampos;
    var campos;

    for (var i = 0; i < nRegistros; i++) {

        if (lista.length > 0) { contenido += "<option value='" + lista[i].IdAlmacen + "'>" + lista[i].Descripcion.toUpperCase() + "</option>"; }
        else { }
    }
    var cbo = document.getElementById(idCombo);
    //console.log(cbo)
    $('#' + idCombo).html(contenido)
}
function ObtenerEmpleados(contador, IdEmpleado) {
    //console.log(IdEmpleado)
    $.ajaxSetup({ async: true });
    $.post("/Empleado/ObtenerEmpleadosPorUsuarioBase", function (data, status) {
        let empleados = JSON.parse(data);
        llenarComboEmpleados(empleados, "cboResponsable" + contador, "Seleccione", IdEmpleado)
    });
}

function llenarComboEmpleados(lista, idCombo, primerItem, IdEmpleado) {
    var contenido = "";
    if (primerItem != null) contenido = "<option value='0'>" + primerItem + "</option>";
    var nRegistros = lista.length;
    var nCampos;
    var campos;
    let ultimoindice = 0;
    for (var i = 0; i < nRegistros; i++) {

        if (lista.length > 0) { contenido += "<option value='" + lista[i].IdEmpleado + "'>" + lista[i].RazonSocial.toUpperCase() + "</option>"; ultimoindice = i }
        else { }
    }
    var cbo = document.getElementById(idCombo);
    //console.log(cbo)

    if (cbo != null) cbo.innerHTML = contenido;
    $("#" + idCombo).val(IdEmpleado)
}
function EliminarFila(IdFila, NroTable) {
    let HayEliminados = $("#Ocultos" + NroTable).val()
    HayEliminados++
    $("#Ocultos" + NroTable).val(HayEliminados)
    $("#FilaDetalle" + IdFila).remove();
}
function EliminarFilaCreada(IdFila) {

    $("#FilaDetalle" + IdFila).remove();
}
function VerificarEntregaStock(IdFila) {

    let ValorCantidadEntregar = $(".NoPasarStock" + IdFila).val();

    let CantidadEnStock = $("#StockDetalle" + IdFila).text();
    console.log(CantidadEnStock)
    console.log(ValorCantidadEntregar)
    if (+ValorCantidadEntregar > +CantidadEnStock) {
        Swal.fire(
            'Error!',
            'La Cantidad a Entregar no puede ser mayor al Stock Actual del almacen',
            'error'
        )
        $(".NoPasarStock" + IdFila).val(+CantidadEnStock)
        return;
    }
}
function AgregarItem(varIdObra, varTipoProducto, i) {

    $("#cboAgregarArticulo").val(0).change()
    $("#cboAgregarArticulo").text('')
    $("#txtIdFila").val(i)
    AbrirModal("modal-form");

    $("#cboAgregarArticulo").select2({
        language: "es",
        width: '100%',

        //theme: "classic",
        async: false,
        ajax: {
            url: "/Articulo/ObtenerArticulosSelect2",
            type: "post",
            dataType: 'json',
            delay: 250,
            data: function (params) {

                return {
                    searchTerm: params.term, // search term
                    TipoProducto: varTipoProducto,
                    IdObra: varIdObra
                };



            },
            processResults: function (response) {

                var results = [];
                $.each(response, function (index, item) {
                    results.push({ id: item.IdArticulo, text: item.Codigo + '-' + item.Descripcion1 })
                });


                return { results }


            },
            cache: true,
        },
        placeholder: 'Ingrese Nombre de Producto',
        minimunInputLength: 3
    });
}
let ContadorNuevoDetalle = 0
function agregarItemDetalle() {
    //if ($("#StockNuevoU").val() == 0) {
    //    Swal.fire('Error!', 'No Puede Agregar Articulos Sin Stock', 'error')
    //    return;
    //}
    //if (+$("#StockNuevoU").val() < +$("#cantidadNuevo").val()) {
    //    Swal.fire('Error!', 'No Puede Ingresar una cantidad Mayor al Stock Actual', 'error')
    //    return;
    //}
    if (+$("#cantidadNuevo").val() <= 0) {
        Swal.fire('Error!', 'La Cantidad no puede ser 0 ni negativo', 'error')
        return;
    }

    let nroTabla = $("#txtIdFila").val()
    ContadorNuevoDetalle++
    let trNuevo = ''
    trNuevo += ` <tr id="FilaDetalle` + ContadorNuevoDetalle + `"> 
                    <td><input style="display:none" type="text" value="` + 0 + `" id="txtIdSolicitud` + ContadorNuevoDetalle + `" class="form-control IdDetalleCol` + nroTabla + `" style="max-width:60px" ></td>
                    <td class="DescripcionTabla`+ nroTabla + `">` + $("#NombreCrear").val() + `</td>
                    <td class="CantidadPedida`+ nroTabla + `">` + $("#cantidadNuevo").val() + `</td>
                    <td>`+ 0 + `</td>
                    <td><input type="text" value="` + $("#cantidadNuevo").val() + `" id="txtCantidadAtendida` + ContadorNuevoDetalle + `" onchange="VerificarEntregaStock(` + ContadorNuevoDetalle + `);ObtenerStocks(` + nroTabla + `)" class="form-control EntregarDetalle` + nroTabla + ` NoPasarStock` + ContadorNuevoDetalle + `" style="max-width:60px"></td>
                    <td id="StockDetalle`+ ContadorNuevoDetalle + `" class="ClassStockDetalle` + nroTabla + `"><p id="Stock` + ContadorNuevoDetalle + `"></p></td>
                    <td class="PrecioDetalle`+ nroTabla + `">Precio</td>
                    <td class="TotalDetalle`+ nroTabla + `">Total</td>
                    <td><button class="btn btn-xs btn-danger fa fa-times" onclick="EliminarFilaCreada(`+ ContadorNuevoDetalle + `)"><button></td>
                    <td style="display:none" class="IdItemDetalle`+ nroTabla + `">` + $("#cboAgregarArticulo").val() + `</td>
                    </tr>`
    console.log(trNuevo)
    console.log(nroTabla)
    $("#table_id" + nroTabla).find('tbody').append(trNuevo)
    //$("#tbody_Solicitudes"+i).append(trNuevo)
    ObtenerStocks(nroTabla)
    closePopup()
}

function ObtenerStocks(Nro) {
    let IdAlmacen = $("#cboAlmacen" + Nro).val()
    let tablaNro = "table_id" + Nro
    //console.log(IdAlmacen)
    var elementosColumna = [];

    $("#table_id" + Nro + " tr").each(function () {
        var celda = $(this).find("td:eq(9)"); // eq(1) para la segunda columna
        elementosColumna.push(celda.text());
    });
    elementosColumna.shift();
    //console.log(elementosColumna)

    //Cantidades
    var celdasCuartaColumna = $("#table_id" + Nro + " tr td:nth-child(5)");;

    //STOCKS
    var celdasQuintaColumna = $("#table_id" + Nro + " tr td:nth-child(6)");

    //PRECIOS
    var celdasSextaColumna = $("#table_id" + Nro + " tr td:nth-child(7)");

    //TOTALES
    var celdasSeptimaColumna = $("#table_id" + Nro + " tr td:nth-child(8)");


    for (var j = 0; j < celdasQuintaColumna.length; j++) {

        //console.log(celdasQuintaColumna.eq(j));
        //celdasQuintaColumna.eq(j).text("dsadsadsa");
        $.ajax({
            url: "/Articulo/ObtenerStockxProducto",
            type: 'POST',
            async: false,
            dataType: 'json',
            data: { 'IdArticulo': elementosColumna[j], 'IdAlmacen': IdAlmacen },
            success: function (datos) {
                //console.log(datos);
                celdasQuintaColumna.eq(j).text(datos[0].Stock);
                celdasSextaColumna.eq(j).text(formatNumberDecimales(datos[0].PrecioPromedio, 2));
                let Total = datos[0].PrecioPromedio * celdasCuartaColumna.eq(j).find("input").val()
                celdasSeptimaColumna.eq(j).text(formatNumberDecimales(Total, 2))
            },
            error: function () {
                Swal("Error!", "Ocurrió un Error", "error");
            }
        })
    }


}
function obtenerStockUnitario() {
    //$("#cboAgregarArticulo").text('')
    let Idnum = $("#txtIdFila").val()
    let Art = $("#cboAgregarArticulo").val()
    let alm = $("#cboAlmacen" + Idnum).val()

    $.ajax({
        url: "/Articulo/ObtenerStockxProducto",
        type: 'POST',
        async: false,
        dataType: 'json',
        data: { 'IdArticulo': Art, 'IdAlmacen': alm },
        success: function (datos) {
            //console.log(datos);
            $("#StockNuevoU").val(datos[0].Stock);
            $("#NombreCrear").val(datos[0].Codigo + '-' + datos[0].Descripcion1)
        },
        error: function () {
            Swal.fire("Error!", "Ocurrió un Error", "error");
        }
    })
}


function GenerarEntrada(Num, IdSolicitud, numserie, cuadrilla) {
    let arrayStocks = new Array();
    $(".ClassStockDetalle" + Num).each(function (indice, elemento) {
        arrayStocks.push(+($(elemento).text()));
    });
    //for (var i = 0; i < arrayStocks.length; i++) {
    //    if (arrayStocks[i] == 0) {
    //        Swal.fire(
    //            'Error!',
    //            'El Articulo de la Fila N° ' + (i + 1) + ' No cuenta con stock',
    //            'error'
    //        )
    //        return
    //    }
    //}
    let arrayCantidadEntregar = new Array();
    $(".EntregarDetalle" + Num).each(function (indice, elemento) {
        arrayCantidadEntregar.push(+($(elemento).val()));
    });

    //for (var j = 0; j < arrayStocks.length; j++) {
    //    if (arrayStocks[j] < arrayCantidadEntregar[j]) {
    //        Swal.fire(
    //            'Error!',
    //            'No hay suficiente Stock para el Articulo de la Fila N° ' + (j + 1),
    //            'error'
    //        )
    //        return
    //    }
    //}

    let arrayIdDetalle = new Array();
    $(".IdDetalleCol" + Num).each(function (indice, elemento) {
        arrayIdDetalle.push(+($(elemento).val()));
    });

    let arrayDescripcionItem = new Array();
    $(".DescripcionTabla" + Num).each(function (indice, elemento) {
        arrayDescripcionItem.push(($(elemento).text()));
    });
    for (var l = 0; l < arrayDescripcionItem.length; l++) {
        arrayDescripcionItem[l] = ((arrayDescripcionItem[l]).split('-')).slice(1).join("-");
    }
    let arrayIdItemDetalle = new Array();
    $(".IdItemDetalle" + Num).each(function (indice, elemento) {
        arrayIdItemDetalle.push(($(elemento).text()));
    });
    let arrayPrecioDetalle = new Array();
    $(".PrecioDetalle" + Num).each(function (indice, elemento) {
        arrayPrecioDetalle.push(($(elemento).text()).replace(/,/g, ""));
    });
    let arrayCantidadPedidaDetalle = new Array();
    $(".CantidadPedida" + Num).each(function (indice, elemento) {
        arrayCantidadPedidaDetalle.push(($(elemento).text()).replace(/,/g, ""));
    });
    let TotalGeneral = 0;
    let arrayTotalDetalle = new Array();
    $(".TotalDetalle" + Num).each(function (indice, elemento) {
        arrayTotalDetalle.push(($(elemento).text()).replace(/,/g, ""));
        TotalGeneral += +($(elemento).text()).replace(/,/g, "")
    });
    let AnexoDetalle = [];
    let NroEliminados = $("#Ocultos" + Num).val()

    //REGISTRAR ELEMENTOS NUEVOS
    for (var k = 0; k < arrayIdDetalle.length; k++) {
        if (arrayIdDetalle[k] == 0) {
            $.ajax({
                url: "/SolicitudDespacho/UpdateInsertSolicitudDespachoDetalle",
                type: 'POST',
                async: false,
                dataType: 'json',
                data: {
                    'Id': arrayIdDetalle[k],
                    'IdDevolucionAdm': IdSolicitud,
                    'IdItem': arrayIdItemDetalle[k],
                    'Descripcion': arrayDescripcionItem[k],
                    'IdUnidadMedida': 1,
                    'Cantidad': arrayCantidadEntregar[k]
                },
                success: function (datos) {
                    //console.log(datos);
                    if (datos > 0) {
                        Swal.fire("Exito!", "Se Registro el Nuevo Detalle", "success");
                    } else { Swal.fire("Error!", "Ocurrió un Error", "error"); }
                },
                error: function () {
                    Swal.fire("Error!", "Ocurrió un Error", "error");
                }
            })
        }
    }

    //CREAR SALIDA
    let almacen = $("#cboAlmacen" + Num).val()
    let responsable = $("#cboResponsable" + Num).val()


    let detalles = [];
    if (arrayCantidadEntregar.length == arrayPrecioDetalle.length) {

        for (var m = 0; m < arrayIdItemDetalle.length; m++) {
            detalles.push({
                'IdArticulo': parseInt(arrayIdItemDetalle[m]),
                'DescripcionArticulo': arrayDescripcionItem[m],
                'IdDefinicionGrupoUnidad': 29,
                'IdAlmacen': almacen,
                'Cantidad': arrayCantidadEntregar[m],
                'Igv': 0,
                'PrecioUnidadBase': arrayPrecioDetalle[m],
                'PrecioUnidadTotal': arrayPrecioDetalle[m],
                'TotalBase': arrayTotalDetalle[m],
                'Total': arrayTotalDetalle[m],
                'CuentaContable': 1,
                'IdCentroCosto': 7,
                'IdAfectacionIgv': 1,
                'Descuento': 0,
                'Referencia': '',
                'IdOrigen': IdSolicitud,
                'TablaOrigen': 'Solicitud',
                'IdCuadrilla': cuadrilla,
                'IdResponsable': +responsable,

            })
        }

    }
    if (detalles.length == 0) {
        Swal.fire("Error!", "Error al Cargar Detalles", "error");
        return
    }
    console.log(detalles)
    console.log(TotalGeneral)


    let EsParcial = 0;
    for (var p = 0; p < arrayCantidadPedidaDetalle.length; p++) {
        if (arrayCantidadEntregar[p] < arrayCantidadPedidaDetalle[p]) {
            EsParcial++
        }
    }
    console.log(EsParcial)

    let IdOPCH = $("#txtId").val();
    let TablaOrigen = $("#txtOrigen").val();
    Swal.fire({
        title: 'DESEA GENERAR EL INGRESO?',
        html: "Verifique los siguientes campos antes de Proceder</br>" +
            "</br>" +
            "Serie Para Salida </br>" +
            "<Select id='cboSerieExtorno' class='form-control'></select>" +
            "</br>" +
            "Fecha De Documento </br>" +
            "<input id='FechDocExtorno' type='date' class='form-control'/>" +
            "</br>" +
            "Fecha de Contabilizacion </br>" +
            "<input id='FechContExtorno' type='date' disabled class='form-control'/>",
        icon: 'warning',
        showCancelButton: true,
        confirmButtonColor: '#3085d6',
        cancelButtonColor: '#d33',
        confirmButtonText: 'Si Generar!'
    }).then((result) => {
        if (result.isConfirmed) {

            let MovimientoEnviar = []

            MovimientoEnviar.push({
                detalles,
                AnexoDetalle,
                //cabecera
                'IdAlmacen': almacen,
                'IdTipoDocumento': 331,
                'IdSerie': $("#cboSerieExtorno").val(),
                'Correlativo': '',
                'IdMoneda': 1,
                'TipoCambio': 1,
                'FechaContabilizacion': $("#FechContExtorno").val(),
                'FechaDocumento': $("#FechDocExtorno").val(),
                'IdCentroCosto': 7,
                'Comentario': 'Generado al Atender la Devolucion N° ' + numserie,
                'SubTotal': TotalGeneral,
                'Impuesto': 0,
                'Total': TotalGeneral,
                'IdCuadrilla': 2582,
                'EntregadoA': 24151,
                'IdTipoDocumentoRef': 10,
                'NumSerieTipoDocumentoRef': '',
                'IdDestinatario': '',
                'IdMotivoTraslado': '',
                'IdTransportista': '',
                'PlacaVehiculo': '',
                'MarcaVehiculo': '',
                'NumIdentidadConductor': '',

                'NombreConductor': '',
                'ApellidoConductor': '',
                'LicenciaConductor': '',
                'TipoTransporte': '',

                'Peso': 0,
                'Bulto': 0,

                'SGI': '',
                'CodigoAnexoLlegada': '',
                'CodigoUbigeoLlegada': '',
                'DistritoLlegada': '',
                'DireccionLlegada': '',
                'EsDevolucionAdm': 1,

            })




            $.ajax({
                url: "/EntradaMercancia/UpdateInsertMovimientoDesdeString",
                type: "POST",
                async: true,
                data: {
                    'JsonDatosEnviar': JSON.stringify(MovimientoEnviar)

                },
                success: function (data) {
                    let datos = JSON.parse(data)
                    if (datos.status) {
                        let EstadoSolicitud
                        if (EsParcial > 0) {
                            EstadoSolicitud = 1
                        } else {
                            EstadoSolicitud = 2
                        }
                        if (NroEliminados > 0) {
                            EstadoSolicitud = 1
                        }
                        for (var i = 0; i < arrayCantidadEntregar.length; i++) {
                            $.ajax({
                                url: "/DevolucionAdm/AtencionConfirmada",
                                type: 'POST',
                                async: false,
                                dataType: 'json',
                                data: {
                                    'Cantidad': arrayCantidadEntregar[i],
                                    'IdDevolucion': IdSolicitud,
                                    'IdArticulo': arrayIdItemDetalle[i],
                                    'EstadoDevolucion': EstadoSolicitud,
                                },
                                success: function (datos) {
                                    //console.log(datos);
                                    if (datos > 0) {
                                        if (EstadoSolicitud == 1) {
                                            Swal.fire("Exito!", "Proceso Completado Correctamente,Aún tiene cantidades por atender, La Solicitud se Movió a Entregado Parcialmente", "success");
                                        } else if (EstadoSolicitud == 2) {
                                            Swal.fire("Exito!", "Proceso Completado Correctamente,Todos los articulos fueron atendidos, La Solicitud Fue Cerrada", "success");
                                        } else {
                                            Swal.fire("Exito!", "Proceso Completado Correctamente", "success");
                                        }
                                        ConsultaServidor()
                                    } else { Swal.fire("Error!", "Ocurrió un Error en Atender Pedido", "error"); }
                                },
                                error: function () {
                                    Swal.fire("Error!", "Ocurrió un Error", "error");
                                }
                            })
                        }


                        //$.post('ExtornoConfirmado', {
                        //    'IdOPDN': IdOPDN,
                        //    'EsServicio': TipoProductos,
                        //}, function (data, status) {

                        //    if (data != 0) {
                        //swal("Exito!", "Proceso Realizado Correctamente", "success")
                        //        CerrarModal()
                        //        listaropdnDT()
                        //    } else {
                        //        swal("Error!", "Ocurrio un Error")
                        //        CerrarModal()
                        //    }

                        //});


                    } else {
                        Swal.fire(
                            'Error!',
                            'Ocurrio un Error! ' + datos.mensaje,
                            'error'
                        )

                    }

                }
            }).fail(function () {
                Swal.fire(
                    'Error!',
                    'Comunicarse con el Area Soporte: smarcode@smartcode.pe !',
                    'error'
                )
            });
        }
    })


    $.post("/Serie/ObtenerSeries", { estado: 1 }, function (data, status) {
        let series = JSON.parse(data);
        llenarComboSerieExtorno(series, "cboSerieExtorno", "Seleccione")
    });

    console.log('bien')
}

function llenarComboSerieExtorno(lista, idCombo, primerItem) {

    var fechaHoy = new Date();
    var dia = String(fechaHoy.getDate()).padStart(2, "0");
    var mes = String(fechaHoy.getMonth() + 1).padStart(2, "0");
    var anio = fechaHoy.getFullYear();
    let FechaSalida = anio + "-" + mes + "-" + dia;


    $("#FechDocExtorno").val(FechaSalida)
    $("#FechContExtorno").val(FechaSalida)

    var contenido = "";
    if (primerItem != null) contenido = "<option value='0'>" + primerItem + "</option>";
    var nRegistros = lista.length;
    var nCampos;
    var campos;
    let ultimoindice = 0;

    for (var i = 0; i < nRegistros; i++) {
        if (lista[i].Documento == 1) {
            if (lista.length > 0) { contenido += "<option value='" + lista[i].IdSerie + "'>" + lista[i].Serie + "</option>"; ultimoindice = i }
            else { }
        }

    }
    var cbo = document.getElementById(idCombo);
    if (cbo != null) cbo.innerHTML = contenido;

    $("#" + idCombo).val(lista[ultimoindice].IdSerie).change();
}

function ListarSeries() {
    let FechaInicio = $("#txtFechaInicio").val()
    let FechaFin = $("#txtFechaFin").val()
    let IdBase = $("#cboObraFiltro").val()
    let IdObra = $("#IdObra").val()
    let EstadoSolicitud = $("#EstadoFiltro").val()
    let SerieFiltro = 0
    $.ajaxSetup({ async: false });
    $.post('/DevolucionAdm/ObtenerDevolucionAdmAtender', { 'IdBase': IdBase, 'IdObra': IdObra, 'FechaInicio': FechaInicio, 'FechaFin': FechaFin, 'EstadoSolicitud': EstadoSolicitud, 'SerieFiltro': SerieFiltro }, function (data, status) {
        try {
            let Series = JSON.parse(data);
            llenarComboSerieFiltro(Series, "cboSeries", "Todos")
        } catch (e) {
            $("#cboSeries").html('<option value="0">Todos</option>')
            ConsultaServidor();
        }
    });

}

function llenarComboSerieFiltro(lista, idCombo, primerItem) {
    var contenido = "";
    if (primerItem != null) contenido = "<option value='0'>" + primerItem + "</option>";
    var nRegistros = lista.length;
    var nCampos;
    var campos;
    for (var i = 0; i < nRegistros; i++) {

        if (lista.length > 0) { contenido += "<option value='" + lista[i].Id + "'>" + lista[i].SerieyNum + "</option>"; }
        else { }
    }
    var cbo = document.getElementById(idCombo);
    if (cbo != null) cbo.innerHTML = contenido;
    $("#cboSeries").val($("#cboSeries option:first").val());
    ConsultaServidor();
}
function CerrarSolicitud(IdDevolucion) {
    alertify.confirm('Confirmar', '¿Desea Cerrar Esta Solicitud?, No se podrá volver a atender la solicitud', function () {
        $.ajax({
            url: "/DevolucionAdm/CerrarDevolucionAdm",
            type: 'POST',
            async: false,
            dataType: 'json',
            data: {
                'IdDevolucion': IdDevolucion,
            },
            success: function (datos) {
                //console.log(datos);
                if (datos > 0) {
                    Swal.fire("Exito!", "Se Cerró la Solicitud", "success");
                    ConsultaServidor()
                } else { Swal.fire("Error!", "Ocurrió un Error", "error"); }
            },
            error: function () {
                Swal.fire("Error!", "Ocurrió un Error", "error");
            }
        })

    }, function () { });

}