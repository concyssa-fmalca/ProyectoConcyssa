window.onload = function () {
    CargarBaseFiltro()
    $("#txtFechaInicio").val(getCurrentDate())
    $("#txtFechaFin").val(getCurrentDateFinal())
    $("#txtFechaInicioRpt").val(getCurrentDate())
    $("#txtFechaFinRpt").val(getCurrentDateFinal())
    //ListarSolicitudes()
    ListarSeries()
    CargarObra() 
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
    $("#cboObraFiltro").val($("#cboObraFiltro option:first").val());

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
    $.post('/SolicitudDespacho/ObtenerSolicitudesDespachoxObra', {'IdObra' : IdObra}, function (data, status) {

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
                '<td><input type="hidden" value="' + (solicitudes[i].Id) + '" id="txtIdSolicitud' + i +'" class="form-control" style="max-width:60px" >'+(i+1)+'</td>' +
                '<td>' + solicitudes[i].FechaContabilizacion + '</td>' +
                '<td>' + solicitudes[i].SerieyNum + '</td>' +
                '<td><select id="cboArticuloPedido' + i + '" class="form-control cboArticuloPedidoTodo" onchange="CargarProductosTodos(' + i + ',' + solicitudes[i].IdTipoProducto + ',' + solicitudes[i].IdObra +')"></select></td>' +
                '<td>' + solicitudes[i].NombCuadrilla + '</td>' +
                '<td>' + solicitudes[i].Cantidad + '</td>' +
                '<td><input type="text" value="' + solicitudes[i].CantidadAtendida + '" id="txtCantidadAtendida' + i + '" class="form-control" style="max-width:60px"></td>' +
                '<td style="display:none">'+
                    //ITEM OCULTOS
                '<input type="text" value="' + solicitudes[i].IdTipoProducto + '" id="txtIdTipoProducto' + i + '"></input>' +
                '<input type="text" value="' + solicitudes[i].IdItem + '" id="txtIdArticulo' + i + '"></input>' +
                '<td> '+
                '</tr>';

            LlenarArticuloxPedido(i,solicitudes[i].IdItem)
            
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
                            text:datos[0].Descripcion1
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




function CargarProductosTodos(contador, IdTipoProducto,IdObra) {

    let varTipoProducto = IdTipoProducto
    let varIdObra = IdObra
    //console.log(contador)
    //console.log(varTipoProducto);

    $("#cboArticuloPedido"+contador).select2({
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
    let EstadoSolicitud = $("#EstadoFiltro").val()
    let SerieFiltro = $("#cboSeries").val()
    $.ajaxSetup({ async: false });
    $.post('/SolicitudDespacho/ObtenerSolicitudesDespachoAtender', { 'IdBase': IdBase, 'FechaInicio': FechaInicio, 'FechaFin': FechaFin, 'EstadoSolicitud': EstadoSolicitud, 'SerieFiltro': SerieFiltro }, function (data, status) {
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
                                </div>`
                                  /*  <button class="btn btn-primary" style="margin-top:0px" onclick="AgregarItem(`+ SolicitudDespacho[i].IdObra + `,` + SolicitudDespacho[i].IdTipoProducto + `,` + i + `)">Agregar Artículo</button>*/
                               +` </div>
                                <div class="row">
                                    <label class="col-md-2" style="max-width:130px;color:black">Centro de Costo:</label>
                                    <p class="col-md-5">`+ SolicitudDespacho[i].NombCuadrilla + `</p>
                                    <label for="cboResponsable" class="col-md-2" style="max-width:130px;color:black">Entregado A:</label>
                                    <div class="col-md-4" style="margin-top:-3px">
                                        <select id="cboResponsable`+ i + `" name="cboResponsableGeneral" class="form-control"></select>
                                    </div>
                                    <input style="display:none" id="Ocultos`+i+`" value="0"/>
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
                                                    <td><input type="text" value="` + Saldo + `" id="txtCantidadAtendida` + i + `" onchange="ValidarCantidadNoMayor(` + IdFila + `,` + Saldo +`);  VerificarEntregaStock(` + IdFila + `);ObtenerStocks(` + i + `)" class="form-control  EntregarDetalle` + i + ` NoPasarStock` + IdFila + `" style="max-width:60px"></td>
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
                                                    <td><input type="text" value="` + SolicitudDespacho[i].Detalles[j].Cantidad + `" id="txtCantidadAtendida` + i + `" onchange="ValidarCantidadNoMayor(` + IdFila + `,` + Saldo +`);VerificarEntregaStock(` + IdFila + `);ObtenerStocks(` + i + `)" class="form-control  EntregarDetalle` + i + ` NoPasarStock` + IdFila + `" style="max-width:60px"></td>
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
                                            <label class="col-md-2" style="max-width:130px;color:black">Comentario:</label><input disabled value="`+SolicitudDespacho[i].Comentario+`" id="txtComentario`+ i +`" class="form-control" type="text">
                                            <div class=row>`
                                                if (SolicitudDespacho[i].EstadoSolicitud != 2) {
                InsertarHTML+=                  `<button class="btn btn-primary" style="margin-top:0px" onclick="GenerarSalida(`+ i + `,` + SolicitudDespacho[i].Id + `,'` + SolicitudDespacho[i].SerieyNum + `',` + SolicitudDespacho[i].IdCuadrilla + `)">Crear Salida</button>
                                                <button class="btn btn-primary" style="margin-top:0px" onclick="CerrarSolicitud(`+ SolicitudDespacho[i].Id+`)">Cerrar Solicitud</button>`

                                                 }
                InsertarHTML +=             `</div>
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
                    results.push({ id: item.IdArticulo, text: item.Codigo +'-'+  item.Descripcion1 })
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
    if ($("#StockNuevoU").val() == 0) {
        Swal.fire('Error!', 'No Puede Agregar Articulos Sin Stock', 'error')
        return;
    }
    if (+$("#StockNuevoU").val() < +$("#cantidadNuevo").val()) {
        Swal.fire('Error!', 'No Puede Ingresar una cantidad Mayor al Stock Actual', 'error')
        return;
    }
    if (+$("#cantidadNuevo").val() <= 0) {
        Swal.fire('Error!', 'La Cantidad no puede ser 0 ni negativo', 'error')
        return;
    }

    let nroTabla = $("#txtIdFila").val()
    ContadorNuevoDetalle++
    let trNuevo = ''
    trNuevo += ` <tr id="FilaDetalle` + ContadorNuevoDetalle + `"> 
                    <td><input style="display:none" type="text" value="` + 0 + `" id="txtIdSolicitud` + ContadorNuevoDetalle + `" class="form-control IdDetalleCol` + nroTabla +`" style="max-width:60px" ></td>
                    <td class="DescripcionTabla`+ nroTabla + `">` + $("#NombreCrear").val() + `</td>
                    <td class="CantidadPedida`+ nroTabla +`">`+ $("#cantidadNuevo").val() + `</td>
                    <td>`+ 0 + `</td>
                    <td><input type="text" value="` + $("#cantidadNuevo").val() + `" id="txtCantidadAtendida` + ContadorNuevoDetalle + `" onchange="VerificarEntregaStock(` + ContadorNuevoDetalle + `);ObtenerStocks(` + nroTabla +`)" class="form-control EntregarDetalle` + nroTabla + ` NoPasarStock` + ContadorNuevoDetalle +`" style="max-width:60px"></td>
                    <td id="StockDetalle`+ ContadorNuevoDetalle +`" class="ClassStockDetalle`+nroTabla+`"><p id="Stock`+ ContadorNuevoDetalle + `"></p></td>
                    <td class="PrecioDetalle`+ nroTabla +`">Precio</td>
                    <td class="TotalDetalle`+ nroTabla +`">Total</td>
                    <td><button class="btn btn-xs btn-danger fa fa-times" onclick="EliminarFilaCreada(`+ ContadorNuevoDetalle + `)"><button></td>
                    <td style="display:none" class="IdItemDetalle`+ nroTabla +`">`+ $("#cboAgregarArticulo").val() +`</td>
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
    let tablaNro = "table_id"+Nro
    //console.log(IdAlmacen)
    var elementosColumna = [];

    $("#table_id"+Nro+" tr").each(function () {
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
            celdasSeptimaColumna.eq(j).text(formatNumberDecimales(Total,2))
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
            $("#NombreCrear").val(datos[0].Codigo+'-'+datos[0].Descripcion1)
        },
        error: function () {
            Swal.fire("Error!", "Ocurrió un Error", "error");
        }
    })
}


function GenerarSalida(Num,IdSolicitud,numserie,cuadrilla) {
    let arrayStocks = new Array();
    $(".ClassStockDetalle"+Num).each(function (indice, elemento) {
        arrayStocks.push(+($(elemento).text()));
    });
    for (var i = 0; i < arrayStocks.length; i++) {
        if (arrayStocks[i] == 0) {
            Swal.fire(
                'Error!',
                'El Articulo de la Fila N° '+(i+1)+' No cuenta con stock',
                'error'
            )
            return
        }
    }
    let arrayCantidadEntregar = new Array();
    $(".EntregarDetalle" + Num).each(function (indice, elemento) {
        arrayCantidadEntregar.push(+($(elemento).val()));
    });

    for (var j = 0; j < arrayStocks.length; j++) {
        if (arrayStocks[j] < arrayCantidadEntregar[j]) {
            Swal.fire(
                'Error!',
                'No hay suficiente Stock para el Articulo de la Fila N° ' + (j + 1),
                'error'
            )
            return
        }
    }

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
        arrayPrecioDetalle.push(($(elemento).text()).replace(/,/g,""));
    });
    let arrayCantidadPedidaDetalle = new Array();
    $(".CantidadPedida" + Num).each(function (indice, elemento) {
        arrayCantidadPedidaDetalle.push(($(elemento).text()).replace(/,/g, ""));
    });
    let TotalGeneral = 0;
    let arrayTotalDetalle = new Array();
    $(".TotalDetalle" + Num).each(function (indice, elemento) {
        arrayTotalDetalle.push(($(elemento).text()).replace(/,/g, ""));
        TotalGeneral +=  +($(elemento).text()).replace(/,/g, "")
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
                'IdSolicitudDespacho': IdSolicitud,
                'IdItem': arrayIdItemDetalle[k] ,
                'Descripcion' : arrayDescripcionItem[k],
                'IdUnidadMedida' : 1,
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
        title: 'DESEA GENERAR LA SALIDA?',
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
            $.ajax({
                url: "/SalidaMercancia/UpdateInsertMovimiento",
                type: "POST",
                async: true,
                data: {
                    detalles,
                    AnexoDetalle,
                    //cabecera
                    'IdAlmacen': almacen,
                    'IdTipoDocumento': 332,
                    'IdSerie': $("#cboSerieExtorno").val(),
                    'Correlativo': '',
                    'IdMoneda': 1,
                    'TipoCambio': 1,
                    'FechaContabilizacion': $("#FechContExtorno").val(),
                    'FechaDocumento': $("#FechDocExtorno").val(),
                    'IdCentroCosto': 7,
                    'Comentario': $("#txtComentario"+Num).val() + ' / ' + numserie,
                    'SubTotal': TotalGeneral,
                    'Impuesto': 0,
                    'Total': TotalGeneral,
                    'IdCuadrilla': 2582,
                    'EntregadoA': 24151,
                    'IdTipoDocumentoRef': 1,
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
                    'OrigenDespacho': 'Generado al Atender la Solicitud de Despacho N° ' + numserie,


                },
                success: function (data) {
                    if (data > 0) {
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
                                url: "/SolicitudDespacho/AtencionConfirmada",
                                type: 'POST',
                                async: false,
                                dataType: 'json',
                                data: {
                                    'Cantidad': arrayCantidadEntregar[i],
                                    'IdSolicitud': IdSolicitud,
                                    'IdArticulo': arrayIdItemDetalle[i],
                                    'EstadoSolicitud': EstadoSolicitud,
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
                            'Ocurrio un Error!',
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
        if (lista[i].Documento == 2) {
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
    let EstadoSolicitud = $("#EstadoFiltro").val()
    let SerieFiltro = 0
    $.ajaxSetup({ async: false });
    $.post('/SolicitudDespacho/ObtenerSolicitudesDespachoAtender', { 'IdBase': IdBase, 'FechaInicio': FechaInicio, 'FechaFin': FechaFin, 'EstadoSolicitud': EstadoSolicitud, 'SerieFiltro': SerieFiltro }, function (data, status) {
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
function CerrarSolicitud(IdSolicitudCerrar) {
    alertify.confirm('Confirmar', '¿Desea Cerrar Esta Solicitud?, No se podrá volver a atender la solicitud', function () {
        $.ajax({
            url: "/SolicitudDespacho/CerrarSolicitud",
            type: 'POST',
            async: false,
            dataType: 'json',
            data: {
                'IdSolicitud': IdSolicitudCerrar,
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
function ModalReporte() {
    AbrirModal("modal-reporte");
}


function CargarObra() {
    $.ajaxSetup({ async: false });
    $.post("/Obra/ObtenerObraxIdUsuarioSessionSinBase", {}, function (data, status) {
        try {
            let bases = JSON.parse(data);
            llenarComboObra(bases, "cboObraReporte", "Seleccione")
        } catch (e) {
            $("#IdObra").html("<option value='0'>TODOS</option>")
        }
    });
}

function llenarComboObra(lista, idCombo, primerItem) {
    var contenido = "";
    if (primerItem != null) contenido = "<option value='0'>" + primerItem + "</option>";
    var nRegistros = lista.length;
    var nCampos;
    var campos;
    for (var i = 0; i < nRegistros; i++) {

        if (lista.length > 0) { contenido += "<option value='" + lista[i].IdObra + "'>" + lista[i].Descripcion + "</option>"; }
        else { }
    }
    var cbo = document.getElementById(idCombo);
    if (cbo != null) cbo.innerHTML = contenido;
    $("#"+idCombo).prop("selectedIndex",1)
}

function ObtenerReporte() {

    Swal.fire({
        title: "Generando Reporte...",
        text: "Por favor espere",
        showConfirmButton: false,
        allowOutsideClick: false
    });


    $.ajaxSetup({ async: false });
    $.post("GenerarReporte", { 'IdObra': $("#cboObraReporte").val(), 'FechaInicio': $("#txtFechaInicioRpt").val(), 'FechaFin': $("#txtFechaFinRpt").val() }, function (data, status) {
        let datos;
        if (validadJson(data)) {
            let datobase64;
            datobase64 = "data:application/octet-stream;base64,"
            datos = JSON.parse(data);
            //datobase64 += datos.Base64ArchivoPDF;
            //$("#reporteRPT").attr("download", 'Reporte.' + "pdf");
            //$("#reporteRPT").attr("href", datobase64);
            //$("#reporteRPT")[0].click();
            verBase64PDF(datos)
            Swal.fire(
                'Correcto',
                'Reporte Generado',
                'success'
            )
        } else {
            Swal.fire(
                'Error',
                'No se pudo Cargar el Reporte',
                'error'
            )
        }
    });
}
function verBase64PDF(datos) {
    //var b64 = "JVBERi0xLjcNCiWhs8XXDQoxIDAgb2JqDQo8PC9QYWdlcyAyIDAgUiAvVHlwZS9DYXRhbG9nPj4NCmVuZG9iag0KMiAwIG9iag0KPDwvQ291bnQgMS9LaWRzWyA0IDAgUiBdL1R5cGUvUGFnZXM+Pg0KZW5kb2JqDQozIDAgb2JqDQo8PC9DcmVhdGlvbkRhdGUoRDoyMDIyMDkyODE2NDAzMCkvQ3JlYXRvcihQREZpdW0pL1Byb2R1Y2VyKFBERml1bSk+Pg0KZW5kb2JqDQo0IDAgb2JqDQo8PC9Db250ZW50cyA1IDAgUiAvTWVkaWFCb3hbIDAgMCA2MTIgNzkyXS9QYXJlbnQgMiAwIFIgL1Jlc291cmNlczw8L0ZvbnQ8PC9GMSA2IDAgUiA+Pi9Qcm9jU2V0IDcgMCBSID4+L1R5cGUvUGFnZT4+DQplbmRvYmoNCjUgMCBvYmoNCjw8L0ZpbHRlci9GbGF0ZURlY29kZS9MZW5ndGggMzExPj5zdHJlYW0NCnicvZPNasMwEITvBr/DHFtot7LiH/mYtMmhECjU9C5i2VGw5WDJpfTpayckhUCDSqGSDjsHfcPOshzPYbAowoBhun0dBg+rCIzxDEUVBklGsyxhyDgnLhhDUYbBDeZ41e2+UXh5WmGlx+IWxS4MlsWRdmRE7MBIc+LJ+DUVglImToxiqy3GJ2Fb2TQoVdsZ63rpdGdA+7JCNZHvvdhpTBmLT+zdYB2qrsdgFbSB2yq86d4NssFabbbS6I2FG1zXa9lYwrrrFZz6cIS5KdFO0sc24ZQl/GR7AfCRPiZckIjPuf0K/5P0sY1SEnl6tbfFmJ+p7/A5HR9zH18WUx6fR/nnVr1zTnJOec79cvbhjQUT/z63ZNzZaHZ9bhdy+a7MQRMeO+O0GVSJcQn3slbgIKJv3y8shB6ADQplbmRzdHJlYW0NCmVuZG9iag0KNiAwIG9iag0KPDwvQmFzZUZvbnQvSGVsdmV0aWNhL0VuY29kaW5nL1dpbkFuc2lFbmNvZGluZy9OYW1lL0YxL1N1YnR5cGUvVHlwZTEvVHlwZS9Gb250Pj4NCmVuZG9iag0KNyAwIG9iag0KWy9QREYvVGV4dF0NCmVuZG9iag0KeHJlZg0KMCA4DQowMDAwMDAwMDAwIDY1NTM1IGYNCjAwMDAwMDAwMTcgMDAwMDAgbg0KMDAwMDAwMDA2NiAwMDAwMCBuDQowMDAwMDAwMTIyIDAwMDAwIG4NCjAwMDAwMDAyMDkgMDAwMDAgbg0KMDAwMDAwMDM0MyAwMDAwMCBuDQowMDAwMDAwNzI2IDAwMDAwIG4NCjAwMDAwMDA4MjUgMDAwMDAgbg0KdHJhaWxlcg0KPDwNCi9Sb290IDEgMCBSDQovSW5mbyAzIDAgUg0KL1NpemUgOC9JRFs8NEY2MkQwQTkwNDlFOUM1N0NGQzRCODEzRTVCNjhDNUI+PDRGNjJEMEE5MDQ5RTlDNTdDRkM0QjgxM0U1QjY4QzVCPl0+Pg0Kc3RhcnR4cmVmDQo4NTUNCiUlRU9GDQo=";
    var b64 = datos.Base64ArchivoPDF;
    // aquí convierto el base64 en caracteres
    var characters = atob(b64);
    // aquí convierto todo a un array de bytes usando el codigo de cada caracter:
    var bytes = new Array(characters.length);
    for (var i = 0; i < characters.length; i++) {
        bytes[i] = characters.charCodeAt(i);
    }
    // en este punto ya tengo un array de bytes,
    // (supongo que es algo similar a lo que te llega de respuesta)
    // el siguiente paso sería convertir este array en un typed array
    // para construir el blob correctamente:
    var chunk = new Uint8Array(bytes);

    // se construye el blob con el mime type respectivo
    var blob = new Blob([chunk], {
        type: 'application/pdf'
    });

    // se crea un object url con el blob para usarlo:
    var url = URL.createObjectURL(blob);

    // y de esta manera simplemente lo abro en una nueva ventana:
    window.open(url, '_blank');
}

function validadJson(json) {
    try {
        object = JSON.parse(json);
        return true;
    } catch (error) {
        return false;
    }
}

function ImportarExcel() {


    $.post('GenerarExcel', {
        'IdObra': $("#cboObraReporte").val(), 'FechaInicio': $("#txtFechaInicioRpt").val(), 'FechaFin': $("#txtFechaFinRpt").val() 
    }, function (data, status) {
        if (data == "ERROR") {
            Swal.fire("Error!", "No se pudo generar el Archivo Excel", "error")
        } else if (data == "SIN DATOS") {
            Swal.fire("Sin Datos!", "No se encontraron Datos", "info")
        } else {
            window.open("/Anexos/" + data + ".xlsx", '_blank', 'noreferrer');
        }
    });



}

function ValidarCantidadNoMayor(IdFila, Saldo) {
    let ValorCantidadEntregar = $(".NoPasarStock" + IdFila).val();

    if (ValorCantidadEntregar > Saldo) {
        Swal.fire("No puede asignar una cantidad mayor a la Solicitada");
        $(".NoPasarStock" + IdFila).val(Saldo).change();
    }


}