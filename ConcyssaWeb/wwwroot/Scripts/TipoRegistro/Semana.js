let table = '';
let contador = 0;
let table_series;

window.onload = function () {
    CargarBaseFiltro()
    CargarTipoRegistro();
    CargarObra();
    ObtenerTipoRegistroFiltro()
    KeyPressNumber($("#txtFondo"));
    $("#cboObraFiltro").prop("selectedIndex", 1);
    $("#cboPeriodo").prop("selectedIndex", 3);
    setTimeout(() => {
        ObtenerSemanas()
    },200)
};

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
    let IdBase = $("#cboBaseFiltro").val();

    console.log(IdBase);
    $.ajaxSetup({ async: false });
    $.post("/Obra/ObtenerObraxIdUsuarioSession", { 'IdBase': IdBase }, function (data, status) {

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
    $("#cboObraFiltro").prop("selectedIndex", 0);
}
function ObtenerTipoRegistroFiltro() {

    $.ajaxSetup({ async: false });
    $.post("ObtenerTipoRegistrosAjax",  function (data, status) {
        if (validadJson(data)) {
            let TipoRegistroData = JSON.parse(data);
            llenarComboTipoRegistroFiltro(TipoRegistroData, "cboTipoRegistroFiltro", "Seleccione")
        } else {}
    });
}

function llenarComboTipoRegistroFiltro(lista, idCombo, primerItem) {

    var contenido = "";
    if (primerItem != null) contenido = "<option value='0'>" + primerItem + "</option>";
    var nRegistros = lista.length;
    var nCampos;
    var campos;
   
    for (var i = 0; i < nRegistros; i++) {
       
        if (lista.length > 0) { contenido += "<option value='" + lista[i].IdTipoRegistro + "'>" + lista[i].NombTipoRegistro.toUpperCase() + "</option>"; }
        else { }
    }
    var cbo = document.getElementById(idCombo);
    if (cbo != null) cbo.innerHTML = contenido;
    $("#cboTipoRegistroFiltro").prop("selectedIndex", 1);
}
function ModalNuevo() {
    $("#lblTituloModal").html("Semana Nueva");
    $("#chkActivo").prop("checked",true)
    AbrirModal("modal-form");
   
}

function Guardar() {
    let Anio = $("#Anio").val();
    let NroSemana = $("#NroSemana").val();
    let DateFechaInicial = $("#DateFechaInicial").val();
    let DateFechaFinal = $("#DateFechaFinal").val();
    let Fondo = quitarFormato( $("#txtFondo").val());
    let Estado = false;
    if ($('#chkActivo')[0].checked) {
        Estado = true;
    }
    $.post('UpdateInsertSemana', {
        'IdSemana': +$("#txtId").val(),
        'Anio': +Anio,
        'NumSemana': +NroSemana,
        'FechaI': DateFechaInicial,
        'FechaF': DateFechaFinal,
        'Estado': Estado,
        'IdTipoRegistro': +$("#cboTipoRegistro").val(),
        'IdObra': +$("#cboObra").val(),
        'Fondo': Fondo,
     
    }, function (data, status) {
        if (data > 0) {
            swal("Exito!", "Proceso Realizado Correctamente", "success")
            limpiarDatos();
        } else {
            swal("Error!", "Ocurrio un Error")
            limpiarDatos();
        }
        ObtenerSemanas();
        closePopup();
    });
}




function eliminar(IdSemana) {


    alertify.confirm('Confirmar', '¿Desea eliminar esta Semana?', function () {
        $.post("EliminarSemana", { 'IdSemana': IdSemana }, function (data) {

            var errorEmpresa = validarEmpresaUpdateInsert(data);
            if (errorEmpresa) {
                return;
            }


            if (data == 0) {
                swal("Error!", "Ocurrio un Error")
                limpiarDatos();
            } else {
                swal("Exito!", "Semana Eliminada", "success")

            }
            ObtenerSemanas()
            closePopup();
        });

    }, function () { });

}


function limpiarDatos() {
    $("#txtId").val(""); 
   $("#Anio").val("");
    $("#NroSemana").val("");
   $("#DateFechaInicial").val("");
    $("#DateFechaFinal").val("");
    $("#cboTipoRegistro").val(0);
    $("#cboObra").val(0);
    $("#txtFondo").val("");
}





function ObtenerSemanas() {
    let varIdObraFiltro = $("#cboObraFiltro").val()
    let varAnioFiltro = $("#cboPeriodo").val()
    let varIdTipoRegistro = $("#cboTipoRegistroFiltro").val()
    table = $('#table_id').dataTable({
        language: lenguaje_data,
        responsive: true,
        ajax: {
            url: 'ObtenerSemanas',
            type: 'POST',
            data: {
                'IdObra': varIdObraFiltro,
                'Anio': varAnioFiltro,
                'IdTipoRegistro': varIdTipoRegistro,
                pagination: {
                    perpage: 50,
                },
            },
        },

        columnDefs: [
            { "className": "text-center", "targets": "_all" },
            {
                targets: -1,
                orderable: false,
                render: function (data, type, full, meta) {
                    let button = `<button class="btn btn-danger editar  fa fa-edit btn-xs " onclick="ObtenerDatosxID(` + full.IdSemana + `)"></button><button class="btn btn-danger borrar btn-xs fa fa-trash " onclick="eliminar(` + full.IdSemana + `)"></button>`;
                    //`<button class="btn btn-danger btn-xs fa fa-trash" onclick="eliminar(` + full[i].IdSerie + `)"></button>`;
                    return button;

                },
            },
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
                    return full.TipoRegistro
                },
            },
            {
                targets: 2,
                orderable: false,
                render: function (data, type, full, meta) {
                    return full.Obra
                },
            },
            {
                targets: 3,
                orderable: false,
                render: function (data, type, full, meta) {
                    return full.Anio
                },
            },
            {
                targets: 4,
                orderable: false,
                render: function (data, type, full, meta) {
                    return full.NumSemana
                },
            },
            {
                targets: 5,
                orderable: false,
                render: function (data, type, full, meta) {
                   
                    return full.FechaI.split('T')[0]
                },
            },
            {
                targets: 6,
                orderable: false,
                render: function (data, type, full, meta) {
                    let dateI = new Date(full.FechaF);
                    return full.FechaF.split('T')[0]
                },
            },
            {
                targets: 7,
                orderable: false,
                render: function (data, type, full, meta) {
                   
                    return FormatMiles(full.Fondo)
                },
            },
            {
                targets:8,
                orderable: false,
                render: function (data, type, full, meta) {
                    if (full.Estado) {
                        return 'Activo'
                    } else {
                        return 'Desactivado'
                    }

                },
            },
           

        ],
        "bDestroy": true
    }).DataTable();
}


const formatear = f => {
    const año = f.getFullYear();
    const mes = ("0" + (f.getMonth() + 1)).substr(-2);
    const dia = ("0" + f.getDate()).substr(-2);
    return `${año}-${mes}-${dia}`
}
function ObtenerDatosxID(Id) {
  

    $("#lblTituloModal").html("Editar Semana");
    AbrirModal("modal-form");



    $.post('ObtenerSemanaDatosxID', {
        'IdSemana': Id,
    }, function (data, status) {

        if (data == "Error") {
            swal("Error!", "Ocurrio un error")
            limpiarDatos();
        } else {
            let dato = JSON.parse(data);
            let dateI = new Date(dato.FechaI);
            let dateF = new Date(dato.FechaF);
            //console.log(usuarios);
            $("#txtId").val(Id);
            let dos = $("#txtId").val();
            $("#Anio").val(dato.Anio);
            $("#NroSemana").val(dato.NumSemana);
            $("#cboTipoRegistro").val(dato.IdTipoRegistro);
            $("#cboObra").val(dato.IdObra);
            $("#DateFechaInicial").val(formatear(dateI));
            $("#DateFechaFinal").val(formatear(dateF));
            $("#txtFondo").val(FormatMiles( dato.Fondo));
            if (dato.Estado) {
                $("#chkActivo").prop('checked', true);  
            } else {
                $("#chkActivo").prop('checked', false);
            }

        }

    });

}

function CargarTipoRegistro() {
    $.ajaxSetup({ async: false });
    $.post("ObtenerTipoRegistrosAjax", { 'estado': 1 }, function (data, status) {
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

function CargarObra() {
    $.ajaxSetup({ async: false });
    $.post("/obra/ObtenerObraxIdUsuarioSessionSinBase", function (data, status) {
        let tipoRegistros = JSON.parse(data);
        llenarComboObra(tipoRegistros, "cboObra", "Seleccione")
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
}

function validadJson(json) {
    try {
        object = JSON.parse(json);
        return true;
    } catch (error) {
        return false;
    }
}

