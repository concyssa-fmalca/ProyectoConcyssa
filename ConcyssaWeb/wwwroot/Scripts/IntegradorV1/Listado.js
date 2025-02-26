window.onload = function () {
    $("#txtFechaInicio").val(getCurrentDate())
    $("#txtFechaFin").val(getCurrentDateFinal())
    CargarObras()
    
    CargarTipoRegistro()
    CargarDatosTrabajo()
};





function CargarObras() {
    $.ajaxSetup({ async: false });
    $.post("/Obra/ObtenerObraxIdUsuarioSessionSinBase", function (data, status) {
        let obras = JSON.parse(data);
        llenarComboObra(obras, "cboObraFiltro", "Seleccione")
    });
}

function llenarComboObra(lista, idCombo, primerItem) {
    var contenido = "";
    if (primerItem != null) contenido = "<option value='0'>" + primerItem + "</option>";
    var nRegistros = lista.length;
    var nCampos;
    var campos;
    let ultimoindice = 0;
    for (var i = 0; i < nRegistros; i++) {
            if (lista.length > 0) { contenido += "<option value='" + lista[i].IdObra + "'>" + lista[i].Descripcion + "</option>"; ultimoindice = i }    

    }
    var cbo = document.getElementById(idCombo);
    if (cbo != null) cbo.innerHTML = contenido;
    $("#cboObraFiltro").prop("selectedIndex", 1)

}

function CargarTipoRegistro() {
    $.ajaxSetup({ async: false });
    $.post("/TipoRegistro/ObtenerTipoRegistrosAjax", { 'estado': 1 }, function (data, status) {
        let tipoRegistros = JSON.parse(data);
        llenarComboTipoRegistro(tipoRegistros, "cboTipoRegistro", "Seleccione")
    });
}


function llenarComboTipoRegistro(lista, idCombo, primerItem) {
    var contenido = "";
    if (primerItem != null) contenido = "<option value='0'>" + primerItem + "</option>";
    var nRegistros = lista.length;
    var nCampos;
    var campos;
    for (var i = 0; i < nRegistros; i++) {

        if (lista.length > 0) { contenido += "<option value='" + lista[i].IdTipoRegistro + "'>" + lista[i].NombTipoRegistro + "</option>"; }
        else { }
    }
    var cbo = document.getElementById(idCombo);
    if (cbo != null) cbo.innerHTML = contenido;
}


function changeTipoRegistro() {

    let varIdTipoRegistro = $("#cboTipoRegistro").val();
    let varIdObra = $("#cboObraFiltro").val();
    $.ajaxSetup({ async: false });
    $.post("/TipoRegistro/ObtenerSemanaAjax", { 'estado': 1, 'IdTipoRegistro': varIdTipoRegistro, 'IdObra': varIdObra }, function (data, status) {
        let tipoRegistros = JSON.parse(data);
        llenarComboSemana(tipoRegistros, "cboSemana", "Seleccione")

    });

}

function llenarComboSemana(lista, idCombo, primerItem) {
    var contenido = "";
    if (primerItem != null) contenido = "<option value='0'>" + primerItem + "</option>";
    var nRegistros = lista.length;
    var nCampos;
    var campos;
    for (var i = 0; i < nRegistros; i++) {

        if (lista.length > 0) { contenido += "<option value='" + lista[i].IdSemana + "'>" + lista[i].Descripcion + "</option>"; }
        else { }
    }
    var cbo = document.getElementById(idCombo);
    if (cbo != null) cbo.innerHTML = contenido;
}

function esRendicion() {
    if ($("#cboTipoRegistro").val() != 4) {
        $("#DivGiro").hide()
        $("#DivSemana").show()
        if ($("#cboTipoRegistro").val() == 5) {
            $("#cboCondicionPago").val(1)
        }
    } else {
        $("#DivGiro").show()
        $("#DivSemana").hide()
        CargarGiros()
    }
}
function CargarGiros() {
    let obraGiro = $("#cboObraFiltro").val()
    $.ajaxSetup({ async: false });
    $.post("/GestionGiro/ObtenerGiroParaBusqueda", { 'IdObra': obraGiro }, function (data, status) {
        let base = JSON.parse(data);
        llenarComboGiros(base, "cboGiro", "seleccione")

    });

}


function llenarComboGiros(lista, idCombo, primerItem) {
    console.log(lista)
    var contenido = "";
    if (primerItem != null) contenido = "<option value='0'>" + primerItem + "</option>";
    var nRegistros = lista.length;
    var nCampos;
    var campos;
    for (var i = 0; i < nRegistros; i++) {

        if (lista.length > 0) { contenido += "<option value='" + lista[i].IdGiro + "'>" + lista[i].Serie.toUpperCase() + "</option>"; }
        else { }
    }
    var cbo = document.getElementById(idCombo);
    if (cbo != null) cbo.innerHTML = contenido;
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

function listarOpch() {
    let varIdObraFiltro = $("#cboObraFiltro").val()
    let fechaIn = $("#txtFechaInicio").val()
    let fechaFn = $("#txtFechaFin").val()

    let IdRegistro = $("#cboTipoRegistro").val()
    if (IdRegistro == 0) {
        Swal.fire("Seleccione un Tipo de Registro")
        return
    } 
    let IdSemana = 0

    if (IdRegistro != 4) {
        IdSemana = $("#cboSemana").val()
        if (IdSemana == 0) {
            Swal.fire("Seleccione una Semana")
            return
        } 
    } else {
        IdSemana = $("#cboGiro").val()
        if (IdSemana == 0) {
            Swal.fire("Seleccione una Giro")
            return
        } 
    }


    tablepedido = $('#table_id').dataTable({
        paging: false,
        language: lenguaje_data,
        responsive: true,
        ajax: {
            url: 'ListarCamposxIdObra',
            type: 'POST',
            error: function (xhr, status, error) {
               
            },
            data: {
                'IdObra': varIdObraFiltro,
                'IdTipoRegistro': IdRegistro,
                'IdSemana': IdSemana,
                pagination: {
                    perpage: 50,
                },
            },
        },

        columnDefs: [
            //// {"className": "text-center", "targets": "_all"},
            //{
            //    data: null,
            //    targets: -1,
            //    orderable: false,
            //    render: function (data, type, full, meta) {
            //        return `<button class="btn btn-primary juntos fa fa-plus btn-xs" onclick="ObtenerDatosxIDOPCH(` + full.IdOPCH + `)"></button>`
            //               // <button class="btn btn-primary juntos fa fa-file-text btn-xs" onclick="VerDocsOrigen(` + full.IdOPCH + `)"></button>
            //    },
            //},
            {
                data: null,
                targets: 0,
                orderable: false,
                render: function (data, type, full, meta) {
                    return meta.row + 1
                },
            },
            {
                data: null,
                targets: 1,
                orderable: false,
                render: function (data, type, full, meta) {
                    let FechaDoc = full.FechaDocumento
                    const fechaTabla = new Date(FechaDoc);
                    const diaTabla = fechaTabla.getDate();
                    const mesTabla = fechaTabla.getMonth() + 1;
                    const anioTabla = fechaTabla.getFullYear();

                    // Añadir ceros iniciales si es necesario
                    const diaFormateado = diaTabla < 10 ? "0" + diaTabla : diaTabla;
                    const mesFormateado = mesTabla < 10 ? "0" + mesTabla : mesTabla;

                    const fechaFormateada = `${diaFormateado}/${mesFormateado}/${anioTabla}`;

                    return fechaFormateada
                },
            },
            {
                data: null,
                targets: 2,
                orderable: false,
                render: function (data, type, full, meta) {
                    return full.NombUsuario
                },
            },
            {
                data: null,
                targets: 3,
                orderable: false,
                render: function (data, type, full, meta) {
                    return full.NombTipoDocumentoOperacion
                },
            },
            {
                data: null,
                targets: 4,
                orderable: false,
                render: function (data, type, full, meta) {
                    if (full.Inventario == true) return 'PRODUCTOS'
                    return 'SERVICIOS'
                },
            },
            {
                data: null,
                targets: 5,
                orderable: false,
                render: function (data, type, full, meta) {
                    if (full.IdDocExtorno != 0) {
                        return '<p style="color:red">' + full.NombSerie + '-' + full.Correlativo + '</p>'
                    } else {
                        return full.NombSerie + '-' + full.Correlativo
                    }
                },
            },
            {
                data: null,
                targets: 6,
                orderable: false,
                render: function (data, type, full, meta) {
                    return full.TipoDocumentoRef
                },
            },
            {
                data: null,
                targets: 7,
                orderable: false,
                render: function (data, type, full, meta) {
                    return full.NumSerieTipoDocumentoRef.toUpperCase()

                },
            },
            {
                data: null,
                targets: 8,
                orderable: false,
                render: function (data, type, full, meta) {
                    return full.Proveedor
                },
            },
            {
                data: null,
                targets: 9,
                orderable: false,
                render: function (data, type, full, meta) {
                    return formatNumberDecimales(full.Total, 2)
                },
            },
            {
                data: null,
                targets: 10,
                orderable: false,
                render: function (data, type, full, meta) {
                    return full.NombObra
                },
            },
            {
                data: null,
                targets: 11,
                orderable: false,
                render: function (data, type, full, meta) {
                    return full.NombAlmacen
                },
            },
            {
                data: null,
                targets: 12,
                orderable: false,
                render: function (data, type, full, meta) {
                    return full.Moneda
                        + '<input style="display:none"   class="inputids" id="inputids' + meta.row + '" value=' + full.TablaOriginal + '-' + full.IdTablaOriginal + '-' + full.SapDocNum + '>'
                },
            },
            {
                data: null,
                targets: 13,
                orderable: false,
                render: function (data, type, full, meta) {
                    let TextoEnviado = '';
                    if (full.EnviadoPor == 1) {
                        TextoEnviado = 'Borrador - '
                    } else if (full.EnviadoPor == 2) {
                        TextoEnviado = 'Firme - '
                    }
                    return TextoEnviado + full.SapDocNum
                        
                },
            },


        ],
        "bDestroy": true
    }).DataTable();
}


function CargarDatosTrabajo() {
    $.ajaxSetup({ async: false });
    $.post("ObtenerListaDatosTrabajo", function (data, status) {
        if (data == "error") {
            contenido = "<option value='0'>NUEVO</option>"
            $("#cboDatosTrabajo").html(contenido)
        }
        let obras = JSON.parse(data);
        llenarComboObrae(obras, "cboDatosTrabajo", "NUEVO")
        
    });
}

function llenarComboObrae(lista, idCombo, primerItem) {
    var contenido = "";
    if (primerItem != null) contenido = "<option value='0'>" + primerItem + "</option>";
    var nRegistros = lista.length;
    var nCampos;
    var campos;
    let ultimoindice = 0;
    for (var i = 0; i < nRegistros; i++) {
        if (lista.length > 0) {
            let fechagc = lista[i].FechaCreacionTabla.split('T')[0]
         /*   let horagc = lista[i].FechaCreacionTabla.split('T')[1]*/
            contenido += "<option value='" + lista[i].GrupoCreacion + "'>" + lista[i].GrupoCreacion + ' | ' + fechagc + ' | '+ lista[i].EstadoEnviado+"</option>"; ultimoindice = i
        }
    }
    var cbo = document.getElementById(idCombo);
    if (cbo != null) cbo.innerHTML = contenido;
    //$("#cboDatosTrabajo").prop("selectedIndex", 1)

}


function abrirDatosTrabajo() {

    ////var elementosColumna = [];
    //var idsSeleccionados = []
    ////$("#table_id tr").each(function () {
    ////    var celda = $(this).find("td:eq(2)"); // eq(1) para la segunda columna
    ////    elementosColumna.push(celda.text());
    ////});
    ///*elementosColumna.shift();*/
    //for (var i = 0; i < elementosColumna.length; i++) {
    //    idsSeleccionados.push($("#inputids"+i).val())

    //}

    let idsEnPantalla = new Array();
    $(".inputids").each(function (indice, elemento) {
        idsEnPantalla.push($(elemento).val());
    });



    let idsSeleccionados = new Array();

    for (var i = 0; i < idsEnPantalla.length; i++) {
        if (idsEnPantalla[i].split('-')[2] == 0) {
            idsSeleccionados.push(idsEnPantalla[i])
        }
    }
    
    console.log(idsSeleccionados)
    console.log(idsSeleccionados.length)

 

    let varGrupoCreacion = 0

    $.post("ObtenerGrupoCreacionEnviarSap", function (data, status) {
        if (data > 0) {
            console.log(data)
            varGrupoCreacion = data
        } else {
            Swal.fire("Error!", "Ocurrió un Error", "error"); 
            return
        }
    });



    if ($("#cboDatosTrabajo").val() == 0) {

        if (idsSeleccionados[0] == undefined) {

            Swal.fire("Error!", "No hay Datos para Trabajar", "error");
            return
        }


        for (var i = 0; i < idsSeleccionados.length; i++) {
            console.log(idsSeleccionados[i])
            $.post("MoverAEnviarSap", { 'Tabla': idsSeleccionados[i].split('-')[0], 'Id': idsSeleccionados[i].split('-')[1], 'GrupoCreacion': varGrupoCreacion }, function (data, status) {
                if (data > 0) {
                    console.log('Copiado')
                    window.location.href = "Listado2?l=" + varGrupoCreacion

                } else {
                    console.log('No Copiado')
                    Swal.fire("Error!", "Error al crear marco de Trabajo", "error"); 
                }
            });
        }

    } else {
        window.location.href = "Listado2?l=" + $("#cboDatosTrabajo").val()
    }

    
}

function cambiarTextoBoton() {
    if ($("#cboDatosTrabajo").val() == 0) {
        $("#btn_abrirDatosTrabajo").text('Crear Datos de Trabajo')
    } else {
        $("#btn_abrirDatosTrabajo").text('Abrir Datos de Trabajo')
    }
}