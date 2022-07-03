"use strict"
var table;
function init() {
    listarCodigoUbso();
    //listartipoplanillaselect2();
    //listarsucursalselect2();
    //listarempleadosselect2();
    //$("#form_planilla").on("submit", function (e) {
    //    form_planilla(e)
    //});
}

//function listarempleadosselect2() {
//    $.ajax({
//        url: "employeeselect2",
//        type: "GET",
//        contentType: "application/json",
//        dataType: "json",
//        cache: false,
//        contentType: false,
//        success: function (datos) {
//            $("#select2empleados").select2({
//                dropdownParent: $("#modalempleados"),
//                data: (datos)
//            });
//        }
//    });
//}

//function addempleadoplanilla() {
//    let payroll_id = $("#idmodalempleado").val();
//    let valores = $("#select2empleados").val();
//    let formData = new FormData();
//    formData.append('employees_id', valores.toString());
//    formData.append('payroll_id', payroll_id);
//    formData.append('csrf_token', getCookie('csrftoken'));
//    $.ajax({
//        headers: { 'X-CSRFToken': getCookie('csrftoken') },
//        url: "storepayrollemployees",
//        type: "post",
//        data: formData,
//        cache: false,
//        processData: false,
//        contentType: false,
//        success: function (datos) {
//            $("#select2empleados").val('').trigger('change');
//            Swal.fire({
//                icon: 'success',
//                title: 'Se registro correctamente',
//                showConfirmButton: false,
//                timer: 1500
//            });
//            listarempleadostablexplanilla(payroll_id);

//        }
//    });


//}

//function listartipoplanillaselect2() {
//    $.ajax({
//        url: "listartypepayrollsselect2",
//        type: "GET",
//        contentType: "application/json",
//        dataType: "json",
//        cache: false,
//        contentType: false,
//        success: function (datos) {
//            $("#typepayroll_id").select2({
//                dropdownParent: $("#modalplanillas"),
//                data: (datos)
//            });
//        }
//    });
//}

//function listarsucursalselect2() {
//    $.ajax({
//        url: "listarsucursalselect2",
//        type: "GET",
//        contentType: "application/json",
//        dataType: "json",
//        cache: false,
//        contentType: false,
//        success: function (datos) {
//            $("#sucursal_id").select2({
//                dropdownParent: $("#modalplanillas"),
//                data: (datos)
//            });
//        }
//    });
//}

//function obtenersedesxsucursal() {
//    let sucursal_id = $("#sucursal_id").val();
//    $("#sede_id").html('');
//    $("#sede_id").val('').change();
//    $.ajax({
//        url: "sedexsucursalselect2",
//        type: "GET",
//        contentType: "application/json",
//        dataType: "json",
//        data: { 'sucursal_id': sucursal_id },
//        cache: false,
//        contentType: false,
//        success: function (datos) {
//            $("#sede_id").select2({
//                dropdownParent: $("#modalplanillas"),
//                data: (datos)
//            });

//        }
//    });
//}




//function form_planilla(e) {
//    e.preventDefault(); //No se activará la acción predeterminada del evento
//    var formData = new FormData($("#form_planilla")[0]);
//    $.ajax({
//        headers: { 'X-CSRFToken': '{{ csrf_token }}' },
//        url: "storepayroll",
//        type: "post",
//        data: formData,
//        cache: false,
//        processData: false,
//        contentType: false,
//        success: function (datos) {
//            limpiardataform();
//            table.ajax.reload();
//            $("#modalplanillas").modal("hide");
//            Swal.fire({
//                icon: 'success',
//                title: 'Se registro correctamente',
//                showConfirmButton: false,
//                timer: 1500
//            });

//        }
//    });

//}

//function limpiardataform() {
//    $("#id").val('')
//    $("#code").val('')
//    $("#description").val('')
//    $("#typepayroll_id").val('').change();
//    $("#sucursal_id").val('').change();
//    $("#sede_id").val('').change();
//    $("#year").val('');
//    $("#month").val('');
//    $("#semana").val('');
//}

function listarCodigoUbso() {
    alert('ddd');
    $.ajax({
        url: "listadocodigoubsodt",
        type: "GET",
        contentType: "application/json",
        dataType: "json",
        //data: { 'sucursal_id': sucursal_id },
        cache: false,
        contentType: false,
        success: function (datos) {
            $("#sede_id").select2({
                dropdownParent: $("#modalplanillas"),
                data: (datos)
            });

        }
    });


    //table = $('#dt_codigoubso').dataTable({
    //    ajax: {
    //        url: 'listadocodigoubsodt',
    //        type: 'GET',
    //        data: {
    //            pagination: {
    //                perpage: 50,
    //            },
    //        },
    //    },
    //    columnDefs: [
    //        {
    //            targets: 0,
    //            render: function (data, type, full, meta) {
    //                return full.name_typepayroll
    //            },
    //        },
    //        {
    //            targets: 1,
    //            render: function (data, type, full, meta) {
    //                return full.code
    //            },
    //        },
    //        {
    //            targets: 2,
    //            render: function (data, type, full, meta) {
    //                return full.description
    //            },
    //        },
    //        {
    //            targets: 3,
    //            render: function (data, type, full, meta) {
    //                return full.sucursal_name
    //            },
    //        },
    //        {
    //            targets: 4,
    //            render: function (data, type, full, meta) {
    //                return full.sede_name
    //            },
    //        },
    //        {
    //            targets: 5,
    //            render: function (data, type, full, meta) {
    //                return full.year
    //            },
    //        },
    //        {
    //            targets: 6,
    //            render: function (data, type, full, meta) {
    //                return full.month
    //            },
    //        },
    //        {
    //            targets: 7,
    //            render: function (data, type, full, meta) {
    //                return full.semana
    //            },
    //        },
    //        // {
    //        //     targets: 8,
    //        //     render: function(data, type, full, meta) {
    //        //         return full.code
    //        //     },
    //        // },
    //        // {
    //        //     targets: 9,
    //        //     render: function(data, type, full, meta) {
    //        //         return full.code
    //        //     },
    //        // },
    //        {
    //            targets: -1,
    //            render: function (data, type, full, meta) {
    //                return `<button class="btn btn-sm btn-icon btn-warning" onclick="editartipoplanilla(` + full.id + `)"> <i class="fas fa-pen-nib"></i> </button>
    //                <button class="btn btn-sm btn-icon btn-success" onclick="mostrarempleados(`+ full.id + `)"> E </button>
    //                <a href="report_excel_payrollemployee/`+ full.id + `/" target="_blank" class="btn btn-sm btn-info" download>R</a>
                    
    //                <button class="btn btn-sm btn-icon btn-danger" onclick="deletepayroll(`+ full.id + `)"> <i class="fas fa-trash-alt"></i> </button>`
    //            },
    //        }
    //    ],
    //    "bDestroy": true
    //}).DataTable();
}

//function mostrarempleados(id) {
//    $("#idmodalempleado").val('');
//    $("#modalempleados").modal("show");
//    $("#idmodalempleado").val(id);
//    listarempleadostablexplanilla(id)
//}

//function listarempleadostablexplanilla(idplanilla) {
//    $.ajax({
//        url: "employeesxpayrolldt",
//        type: "GET",
//        contentType: "application/json",
//        dataType: "json",
//        data: { 'payroll_id': idplanilla },
//        cache: false,
//        contentType: false,
//        success: function (datos) {
//            let tr = ""
//            $("#tbody_empleadosmodal").html('');
//            for (let index = 0; index < datos.aaData.length; index++) {
//                tr += `<tr>
//                <td>`+ datos.aaData[index].nombresyapellidos + `</td>
//                <td> <button class="btn btn-danger btn-sm" onclick="deletepayrollemployee(`+ datos.aaData[index].payrollemployee_id + `,` + datos.aaData[index].payroll_id + `)">-</button></td>
//              </tr>`

//            }
//            $("#tbody_empleadosmodal").html(tr);
//        }
//    });
//}

//function deletepayrollemployee(id, payroll_id) {
//    Swal.fire({
//        title: 'Desea eliminar?',
//        text: "Esta seguro de eliminar registro!",
//        icon: 'warning',
//        showCancelButton: true,
//        confirmButtonColor: '#3085d6',
//        cancelButtonColor: '#d33',
//        confirmButtonText: 'Si, eliminar registro!'
//    }).then((result) => {
//        if (result.isConfirmed) {
//            $.ajax({
//                headers: { 'X-CSRFToken': getCookie('csrftoken') },
//                url: "deletepayrollemployee",
//                type: "delete",
//                data: { 'id': id, 'csrf_token': getCookie('csrftoken') },
//                cache: false,
//                contentType: false,
//                success: function (datos) {
//                    Swal.fire(
//                        'Eliminado!',
//                        'Se ha eliminado correctamente el registro.',
//                        'success'
//                    )
//                    listarempleadostablexplanilla(payroll_id);
//                }
//            });


//        }
//    })
//}

//function editartipoplanilla(id) {
//    $("#modalplanillas").modal("show");
//    $.ajax({
//        url: "showpayroll",
//        type: "GET",
//        contentType: "application/json",
//        dataType: "json",
//        data: { 'id': id },
//        cache: false,
//        contentType: false,
//        success: function (datos) {
//            limpiardataform()
//            $("#id").val(datos.id)
//            $("#code").val(datos.code)
//            $("#description").val(datos.description)
//            $("#typepayroll_id").val(datos.typepayroll_id).change();
//            $("#sucursal_id").val(datos.sucursal_id).change();
//            $("#sede_id").val(datos.sede_id).change();
//            $("#year").val(datos.year);
//            $("#month").val(datos.month);
//            $("#semana").val(datos.semana);

//        }
//    });
//}


//function deletepayroll(id) {
//    Swal.fire({
//        title: 'Desea eliminar?',
//        text: "Esta seguro de eliminar registro!",
//        icon: 'warning',
//        showCancelButton: true,
//        confirmButtonColor: '#3085d6',
//        cancelButtonColor: '#d33',
//        confirmButtonText: 'Si, eliminar registro!'
//    }).then((result) => {
//        if (result.isConfirmed) {
//            $.ajax({
//                headers: { 'X-CSRFToken': getCookie('csrftoken') },
//                url: "deletepayroll",
//                type: "delete",
//                data: { 'id': id, 'csrf_token': getCookie('csrftoken') },
//                cache: false,
//                contentType: false,
//                success: function (datos) {
//                    limpiardataform()
//                    Swal.fire(
//                        'Eliminado!',
//                        'Se ha eliminado correctamente el registro.',
//                        'success'
//                    )
//                    table.ajax.reload();
//                }
//            });


//        }
//    })



//}

//function getCookie(name) {
//    let cookieValue = null;
//    if (document.cookie && document.cookie !== '') {
//        const cookies = document.cookie.split(';');
//        for (let i = 0; i < cookies.length; i++) {
//            const cookie = cookies[i].trim();
//            // Does this cookie string begin with the name we want?
//            if (cookie.substring(0, name.length + 1) === (name + '=')) {
//                cookieValue = decodeURIComponent(cookie.substring(name.length + 1));
//                break;
//            }
//        }
//    }
//    return cookieValue;
//}


//function generarcodigoplanilla() {
//    let codigo_generado = "";
//    let code = $("#code").val();
//    let typepayroll_id = $("#typepayroll_id").val();
//    let sucursal_id = $("#sucursal_id").val();
//    let sede_id = $("#sede_id").val();
//    let year = $("#year").val();
//    let month = $("#month").val();
//    let semana = $("#semana").val();

//    codigo_generado = showtipoplanilla(typepayroll_id);
//    codigo_generado += showsucursal(sucursal_id);
//    codigo_generado += showsede(sede_id);
//    codigo_generado += year;
//    codigo_generado += month;
//    $("#code").val(codigo_generado);
//}

//function showtipoplanilla(id) {
//    let code = "";
//    $.ajax({
//        url: "showtypepayroll",
//        type: "GET",
//        contentType: "application/json",
//        dataType: "json",
//        data: { 'id': id },
//        cache: false,
//        contentType: false,
//        async: false,
//        success: function (datos) {
//            code = datos.code
//        }
//    });
//    return code;
//}

//function showsucursal(id) {
//    let code = "";
//    $.ajax({
//        url: "showsucursal",
//        type: "GET",
//        contentType: "application/json",
//        dataType: "json",
//        data: { 'id': id },
//        cache: false,
//        contentType: false,
//        async: false,
//        success: function (datos) {
//            code = datos.id;

//        }
//    });
//    return code;
//}

//function showsede(id) {
//    let code = "";
//    $.ajax({
//        url: "showsede",
//        type: "GET",
//        contentType: "application/json",
//        dataType: "json",
//        data: { 'id': id },
//        cache: false,
//        contentType: false,
//        success: function (datos) {

//            code = datos.id
//        }
//    });
//    return code;
//}



jQuery(document).ready(function () {
    init()
})