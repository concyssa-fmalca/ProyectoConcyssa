
let table = "";
let DecimalesImportes = 0;
let DecimalesPrecios = 0;
let DecimalesCantidades = 0;
let DecimalesPorcentajes = 0;


function ObtenerConfiguracionDecimales() {
    $.post("/ConfiguracionDecimales/ObtenerConfiguracionDecimales", function (data, status) {

        let datos = JSON.parse(data);
        DecimalesCantidades = datos[0].Cantidades;
        DecimalesImportes = datos[0].Importes;
        DecimalesPrecios = datos[0].Precios;
        DecimalesPorcentajes = datos[0].Porcentajes;
    });
}

function CargarBaseFiltro() {
    $.ajaxSetup({ async: false });
    $.post("/Base/ObtenerBasexIdUsuario", function (data, status) {
        let base = JSON.parse(data);
        llenarComboBaseFiltro(base, "cboBaseFiltro", "seleccione")

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
    $("#cboBaseFiltro").prop("selectedIndex", 1);
    ObtenerObraxIdBase()

}

function ObtenerObraxIdBase() {
    let IdBaseFiltro = $("#cboBaseFiltro").val();

    $.ajaxSetup({ async: false });
    $.post("/Obra/ObtenerObraxIdUsuarioSession", { 'IdBase': IdBaseFiltro }, function (data, status) {

        if (validadJson(data)) {
            let obra = JSON.parse(data);
            llenarComboObra(obra, "cboObraFiltro", "Seleccione")
        } else {
            //$("#cboMedidaItem").html('<option value="0">SELECCIONE</option>')
        }

        //let obra = JSON.parse(data);
        //llenarComboObra(obra, "IdObra", "Seleccione")
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
    $("#cboObraFiltro").prop("selectedIndex", 1);
    ObtenerAlmacenxIdObra()
}
function ObtenerAlmacenxIdObra() {
    console.log(3);
    let IdObra = $("#cboObraFiltro").val();
    console.log(IdObra);
    $.ajaxSetup({ async: false });
    $.post("/Almacen/ObtenerAlmacenxIdObra", { 'IdObra': IdObra }, function (data, status) {
        let almacen = JSON.parse(data);
        llenarComboAlmacen(almacen, "cboAlmacenFiltro", "Seleccione")
        //llenarComboAlmacen(almacen, "cboAlmacenItem", "Seleccione")
    });
    console.log(4);
}



window.onload = function () {
    CargarBaseFiltro();

    $(".ocultartabla").hide();
    //setTimeout(function () {
    //    ObtenerStockxAlmacen();
    //}, 2000);
   
};



function formatNumber(num) {
    if (!num || num == 'NaN') return '-';
    if (num == 'Infinity') return '&#x221e;';
    num = num.toString().replace(/\$|\,/g, '');
    if (isNaN(num))
        num = "0";
    sign = (num == (num = Math.abs(num)));
    num = Math.floor(num * 100 + 0.50000000001);
    cents = num % 100;
    num = Math.floor(num / 100).toString();
    if (cents < 10)
        cents = "0" + cents;
    for (var i = 0; i < Math.floor((num.length - (1 + i)) / 3); i++)
        num = num.substring(0, num.length - (4 * i + 3)) + ',' + num.substring(num.length - (4 * i + 3));
    return (((sign) ? '' : '-') + num + '.' + cents);
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
    if (cbo != null) cbo.innerHTML = contenido;
    $("#cboAlmacenFiltro").prop("selectedIndex", 1);
}


function ObtenerStockxAlmacen() {

    $(".ocultartabla").show();

    table = $('#table_id').dataTable({
        language: lenguaje_data,
        responsive: true,
        ajax: {
            url: 'ListarInventarioxAlmacen',
            type: 'POST',
            data: {
                'IdAlmacen': $("#cboAlmacenFiltro").val(),
                pagination: {
                    perpage: 50,
                },
            },
        },

        columnDefs: [
            // {"className": "text-center", "targets": "_all"},
            //{
            //    targets: -1,
            //    orderable: false,
            //    render: function (data, type, full, meta) {

            //        return 1
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
                    return full.Codigo.toUpperCase()
                },
            },
            {
                data: null,
                targets: 2,
                orderable: false,
                render: function (data, type, full, meta) {
                    return full.NombArticulo.toUpperCase()
                },
            },
            {
                data: null,
                targets: 3,
                orderable: false,
                render: function (data, type, full, meta) {
                    return formatNumber(full.Stock)
                },
            },
            {
                data: null,
                targets: 4,
                orderable: false,
                render: function (data, type, full, meta) {
                    return formatNumber(full.PrecioPromedio)
                },
            },
            {
                data: null,
                targets: 5,
                orderable: false,
                render: function (data, type, full, meta) {
                    return formatNumber(parseFloat(parseFloat(full.PrecioPromedio) * parseFloat(full.Stock)))
                },
            }

        ],
        "bDestroy": true
    }).DataTable();
}

function validadJson(json) {
    try {
        object = JSON.parse(json);
        return true;
    } catch (error) {
        return false;
    }
}

