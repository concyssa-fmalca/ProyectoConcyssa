let table = '';


window.onload = function () {

    ConsultaServidor();

    listarObra()
    listarMarca()
    listarModelo()
    listarArea()

    $("#txtFechaMantenimiento").val(getFechaHoy())

    CargarProveedor() 
    $("#IdProveedor").select2()

    $("#SubirAnexos").on("submit", function (e) {
        e.preventDefault();
        var formData = new FormData($("#SubirAnexos")[0]);
        $.ajax({
            url: "GuardarFile",
            type: "POST",
            data: formData,
            cache: false,
            contentType: false,
            processData: false,
            success: function (datos) {
                let data = JSON.parse(datos);
                console.log(data);
                if (data.length > 0) {
               
                    AgregarLineaAnexo(data[0],'tabla_files');
                }

            }
        });
    });

    //$("#SubirAnexosDetalle").on("submit", function (e) {
    //    e.preventDefault();
    //    var formData = new FormData($("#SubirAnexosDetalle")[0]);
    //    $.ajax({
    //        url: "GuardarFile",
    //        type: "POST",
    //        data: formData,
    //        cache: false,
    //        contentType: false,
    //        processData: false,
    //        success: function (datos) {
    //            let data = JSON.parse(datos);
    //            console.log(data);
    //            if (data.length > 0) {
    //                AgregarLineaAnexo(data[0],'tabla_filesDetalle');
    //            }

    //        }
    //    });
    //});
};


function SubirAnexosLinea() {

    Swal.fire({
        title: "Cargando...",
        text: "Por favor espere",
        showConfirmButton: false,
        allowOutsideClick: false
    });

    setTimeout(() => {
        const fileInput = $('#file')[0];


        let fileName = "";
        const folderPath = "portalprov";


        const file = fileInput.files[0];
        if (!file) {
            Swal.fire('Por favor selecciona un archivo');
            return;
        }

        // Validar tamaño máximo: 10MB (10 * 1024 * 1024 bytes)
        const maxSizeMB = 5;
        const maxSizeBytes = maxSizeMB * 1000 * 1000;
        if (file.size > maxSizeBytes) {
            Swal.fire(`El archivo no debe superar los ${maxSizeMB} MB`);
            return;
        }

        console.log(file)

        fileName = file.name; // Nombre de archivo por defecto
        let ultimoPuntoIndex = fileName.lastIndexOf('.');
        let parteAntesDelUltimoPunto = fileName.substring(0, ultimoPuntoIndex);
        let fileExtension = fileName.substring(ultimoPuntoIndex + 1);

        // Agregar la fecha al nombre
        let fecha = new Date();
        let fechaString = fecha.toLocaleDateString('es-ES').replace(/\//g, '-') + ' ' +
            fecha.toLocaleTimeString('es-ES').replace(/:/g, '_');

        fileName = parteAntesDelUltimoPunto + ' ' + fechaString;

        // Reemplazar caracteres no permitidos
        fileName = fileName.replace(/[+%?@&#=;:'"`]/g, ' ');
        fileName += "." + fileExtension;



        try {
            // Preparar la URL con los parámetros
            let url = 'https://pzmnyh8wx3.execute-api.us-east-1.amazonaws.com/upload';
            const params = [];

            params.push(`filename=${encodeURIComponent(fileName)}`);

            params.push(`folder=${encodeURIComponent(folderPath)}`);


            // Añadir los parámetros a la URL
            if (params.length > 0) {
                url += `?${params.join('&')}`;
            }

            // Crear un objeto XMLHttpRequest para poder monitorear el progreso
            const xhr = new XMLHttpRequest();

            // Configurar el evento de progreso
            xhr.upload.addEventListener('progress', function (event) {
                if (event.lengthComputable) {
                    const percentComplete = Math.round((event.loaded / event.total) * 100);
                }
            });

            // Configurar el evento de completado
            xhr.addEventListener('load', function () {
                if (xhr.status >= 200 && xhr.status < 300) {
                    const response = JSON.parse(xhr.responseText);

                    let ObjetoAnexo = {
                        'Nombre': fileName,
                        'url': response.fileUrl
                    }

                    Swal.close()
                    AgregarLineaAnexo(ObjetoAnexo);


                } else {
                    Swal.fire("Error!", 'Error al subir el archivo: ' + xhr.status, "error");
                }
            });

            // Configurar el evento de error
            xhr.addEventListener('error', function () {
                Swal.fire("Error!", 'Error de red al intentar subir el archivo', "error");
            });

            // Abrir y enviar la solicitud
            xhr.open('POST', url, true);
            xhr.send(file);

        } catch (message) {
            Swal.fire("Error!", 'Error de red al intentar subir el archivo: ' + message, "error");
        }

    }, 150)



}



function ConsultaServidor() {
    var url = "ObtenerInstrumentoMedicion";
    $.post(url, function (data, status) {

        //console.log(data);
        if (data == "error") {
            table = $("#table_id").DataTable(lenguaje);
            return;
        }

        let tr = '';

        let InstMed = JSON.parse(data);
        let total_InstMed = InstMed.length;

        for (var i = 0; i < InstMed.length; i++) {

            let PeriodoCalibracion = "ANUAL"
            if (InstMed[i].PeriodoCalibracion == 2) PeriodoCalibracion = "6 MESES"
            if (InstMed[i].PeriodoCalibracion == 3) PeriodoCalibracion = "NO DEFINIDO"

            let EstadoCalibracion = "CALIBRADO"
            if (InstMed[i].EstadoCalibracion == 2) EstadoCalibracion = "EN COTIZACION"
            if (InstMed[i].EstadoCalibracion == 3) EstadoCalibracion = "VENCIDO"
            if (InstMed[i].EstadoCalibracion == 4) EstadoCalibracion = "NO DEFINIDO"
            if (InstMed[i].EstadoCalibracion == 5) EstadoCalibracion = "VERIFICADO"

            let TipoCalibracion = "EXTERNA"
            if (InstMed[i].TipoCalibracion == 2) TipoCalibracion = "INTERNA"

            let EstadoInstrumento = "NUEVO"
            if (InstMed[i].TipoCalibracion == 2) TipoCalibracion = "OPERATIVO"
            if (InstMed[i].TipoCalibracion == 3) TipoCalibracion = "NO DEFINIDO"
            if (InstMed[i].TipoCalibracion == 4) TipoCalibracion = "INOPERATIVO"

            let ProximaCalibracion = InstMed[i].ProxCalib.split('T')[0]
            ProximaCalibracion = ProximaCalibracion.split('-')[2] + '-' + ProximaCalibracion.split('-')[1] + '-' + ProximaCalibracion.split('-')[0]
            if (ProximaCalibracion == '01-01-1999') ProximaCalibracion = '-'

            let anexo = "-"
            if (InstMed[i].NombreArchivo != "-") anexo = '<a target="_blank" href="/Anexos/' + InstMed[i].NombreArchivo + '"> Ver Documento </a>'


            let estado = '<i style="font-size:20px;color:green" class="fa fa-smile-o" aria-hidden="true"></i>'
            if (InstMed[i].EstadoCalib < 0) estado = '<i style="font-size:20px;color:red" class="fa fa-frown-o" aria-hidden="true"></i>'
            if (InstMed[i].EstadoCalib > 0 && InstMed[i].EstadoCalib < 30) estado = '<i style="font-size:20px;color:darkgoldenrod" class="fa fa-meh-o" aria-hidden="true"></i>'

            tr += '<tr>' +
                '<td>' + (i + 1) + '</td>' +
                '<td>' + InstMed[i].NombObra.toUpperCase() + '</td>' +
                '<td>' + estado + '</td>' +
                '<td>' + InstMed[i].Nombre.toUpperCase() + '</td>' +
                '<td>' + InstMed[i].NumeroSerie.toUpperCase() + '</td>' +
                '<td>' + anexo + '</td>' +
                '<td>' + ProximaCalibracion + '</td>' +
                '<td>' + InstMed[i].NombMarca.toUpperCase() + '</td>' +
                '<td>' + InstMed[i].NombModelo.toUpperCase() + '</td>' +
                '<td>' + PeriodoCalibracion + '</td>' +
                '<td>' + InstMed[i].ParametroMedicion.toUpperCase() + '</td>' +
                '<td>' + InstMed[i].PatronMedicion.toUpperCase() + '</td>' +
                '<td>' + InstMed[i].NombArea.toUpperCase() + '</td>' +
                '<td>' + InstMed[i].Responsable.toUpperCase() + '</td>' +
                '<td>' + EstadoCalibracion + '</td>' +
                '<td>' + InstMed[i].Ubicacion.toUpperCase() + '</td>' +
                '<td>' + TipoCalibracion + '</td>' +
                '<td>' + EstadoInstrumento + '</td>' +
                '<td><button class="btn btn-primary fa fa-pencil btn-xs" onclick="ObtenerDatosxID(' + InstMed[i].IdInstrumentoMedicion + ')"></button>' +
                '<button class="btn btn-primary btn-xs  fa fa-eye" onclick="VerDetalles(' + InstMed[i].IdInstrumentoMedicion + ',`' + PeriodoCalibracion + '`,`' + InstMed[i].Nombre.toUpperCase() +'`)"></button>' +
                '<button class="btn btn-danger btn-xs  fa fa-trash" onclick="eliminar(' + InstMed[i].IdInstrumentoMedicion + ')"></button></td >' +
                '</tr>';
        }

        $("#tbody_area").html(tr);
        $("#spnTotalRegistros").html(total_InstMed);

        table = $("#table_id").DataTable(lenguaje);

    });

}




function ModalNuevo() {
    $("#chkActivo").prop('checked', true);
    $("#lblTituloModal").html("Nuevo Registro");
    AbrirModal("modal-form");
    $("#tabla_files").find('tbody').empty();
}




function GuardarRegistro() {
    let varIdInstrumentoMedicion = $("#txtId").val();
    let varIdObra = $("#IdObra").val()
    let varNombre = $("#txtNombre").val()
    let varIdMarca = $("#IdMarca").val()
    let varIdModelo = $("#IdModelo").val()
    let varNumeroSerie = $("#txtNumSerie").val()
    let varPeriodoCalibracion = $("#cboPeriodoCalibracion").val()
    let varParametroMedicion = $("#ParamMedicion").val()
    let varPatronMedicion = $("#PatronMedicion").val()
    let varIdArea = $("#IdArea").val()
    let varResponsable = $("#txtResponsable").val()
    let varEstadoCalibracion = $("#EstadoCalibracion").val()
    let varUbicacion = $("#txtUbiacion").val()
    let varTipoCalibracion = $("#TipoCalibracion").val()
    let varEstadoInstrumento = $("#EstadoInstrumento").val()
    let varObservaciones = $("#txtObservacion").val()




    let varEstado = false;

    if ($('#chkActivo')[0].checked) {
        varEstado = true;
    }

    if (varIdObra == 0 || varIdObra == undefined || varIdObra == null) {
        Swal.fire("Error", "El Campo Obra es Obligatorio", "info")
        return
    }

    if (varNombre == "" || varNombre == undefined || varNombre == null) {
        Swal.fire("Error", "El Campo Nombre es Obligatorio", "info")
        return
    }
    if (varIdMarca == 0 || varIdMarca == undefined || varIdMarca == null) {
        Swal.fire("Error", "El Campo Marca es Obligatorio", "info")
        return
    }
    if (varIdModelo == 0 || varIdModelo == undefined || varIdModelo == null) {
        Swal.fire("Error", "El Campo Modelo es Obligatorio", "info")
        return
    }
    if (varNumeroSerie == "" || varNumeroSerie == undefined || varNumeroSerie == null) {
        Swal.fire("Error", "El Campo Número de Serie es Obligatorio", "info")
        return
    }
    if (varPeriodoCalibracion == 0 || varPeriodoCalibracion == undefined || varPeriodoCalibracion == null) {
        Swal.fire("Error", "El Campo Periodo Calibracion es Obligatorio", "info")
        return
    }
    if (varIdArea == 0 || varIdArea == undefined || varIdArea == null) {
        Swal.fire("Error", "El Campo Area es Obligatorio", "info")
        return
    }
    //if (varResponsable == "" || varResponsable == undefined || varResponsable == null) {
    //    Swal.fire("Error", "El Campo Responsable es Obligatorio", "info")
    //    return
    //}
    if (varEstadoCalibracion == 0 || varEstadoCalibracion == undefined || varEstadoCalibracion == null) {
        Swal.fire("Error", "El Campo Estado Calibracion es Obligatorio", "info")
        return
    }
    if (varUbicacion == "" || varUbicacion == undefined || varUbicacion == null) {
        Swal.fire("Error", "El Campo Ubicacion es Obligatorio", "info")
        return
    }
    if (varTipoCalibracion == 0 || varTipoCalibracion == undefined || varTipoCalibracion == null) {
        Swal.fire("Error", "El Campo Estado Tipo Calibracion es Obligatorio", "info")
        return
    }
    if (varEstadoInstrumento == 0 || varEstadoInstrumento == undefined || varEstadoInstrumento == null) {
        Swal.fire("Error", "El Campo Estado Instrumento es Obligatorio", "info")
        return
    }

    //AnexoDetalle
    let arrayTxtNombreAnexo = new Array();
    $("input[name='txtNombreAnexo[]']").each(function (indice, elemento) {
        arrayTxtNombreAnexo.push($(elemento).val());
    });
    let arrayTxtUrlAnexo = new Array();
    $("input[name='txtUrlAnexo[]']").each(function (indice, elemento) {
        arrayTxtUrlAnexo.push($(elemento).val());
    });


    let AnexoDetalle = [];
    for (var i = 0; i < arrayTxtNombreAnexo.length; i++) {
        AnexoDetalle.push({
            'NombreArchivo': arrayTxtNombreAnexo[i],
            'ruta': arrayTxtUrlAnexo[i],
            'web': true
        });
    }
    //let arrayTxtNombreAnexo = new Array();
    //$("input[name='txtNombreAnexo[]']").each(function (indice, elemento) {
    //    arrayTxtNombreAnexo.push($(elemento).val());
    //});

    //let AnexoDetalle = [];
    //for (var i = 0; i < arrayTxtNombreAnexo.length; i++) {
    //    AnexoDetalle.push({
    //        'NombreArchivo': arrayTxtNombreAnexo[i]
    //    });
    //}


    $.post('UpdateInsertInstrumentoMedicion', {
        'IdInstrumentoMedicion' : varIdInstrumentoMedicion ,
        'IdObra' : varIdObra ,
        'Nombre' : varNombre ,
        'IdMarca' : varIdMarca, 
        'IdModelo' : varIdModelo ,
        'NumeroSerie' : varNumeroSerie ,
        'PeriodoCalibracion' : varPeriodoCalibracion ,
        'ParametroMedicion' : varParametroMedicion, 
        'PatronMedicion' : varPatronMedicion,
        'IdArea' : varIdArea,
        'Responsable' : varResponsable,
        'EstadoCalibracion' : varEstadoCalibracion,
        'Ubicacion' : varUbicacion,
        'TipoCalibracion' : varTipoCalibracion,
        'EstadoInstrumento' : varEstadoInstrumento,
        'Observaciones' : varObservaciones,
        'Estado': varEstado,
        AnexoDetalle
    }, function (data, status) {

        if (data == 1) {
            swal("Exito!", "Proceso Realizado Correctamente", "success")
            table.destroy();
            ConsultaServidor();
            limpiarDatos();
            closePopup()
        } else {
            swal("Error!", "Ocurrio un Error")
        }

    });
}

function ObtenerDatosxID(varIdInstrumentoMedicion) {
    $("#lblTituloModal").html("Editar Registro");
    AbrirModal("modal-form");
    $("#tabla_files").find('tbody').empty();
    //console.log(varIdUsuario);

    $.post('ObtenerDatosxID', {
        'IdInstrumentoMedicion': varIdInstrumentoMedicion,
    }, function (data, status) {

        if (data == "Error") {
            swal("Error!", "Ocurrio un error")
            limpiarDatos();
        } else {
            let datos = JSON.parse(data);
            console.log(datos);
            $("#txtId").val(datos[0].IdInstrumentoMedicion);
            $("#IdObra").val(datos[0].IdObra)
            $("#txtNombre").val(datos[0].Nombre.toUpperCase())
            $("#IdMarca").val(datos[0].IdMarca)
            $("#IdModelo").val(datos[0].IdModelo)
            $("#txtNumSerie").val(datos[0].NumeroSerie.toUpperCase())
            $("#cboPeriodoCalibracion").val(datos[0].PeriodoCalibracion)
            $("#ParamMedicion").val(datos[0].ParametroMedicion.toUpperCase())
            $("#PatronMedicion").val(datos[0].PatronMedicion.toUpperCase())
            $("#IdArea").val(datos[0].IdArea)
            $("#txtResponsable").val(datos[0].Responsable.toUpperCase())
            $("#EstadoCalibracion").val(datos[0].EstadoCalibracion)
            $("#txtUbiacion").val(datos[0].Ubicacion.toUpperCase())
            $("#TipoCalibracion").val(datos[0].TipoCalibracion)
            $("#EstadoInstrumento").val(datos[0].EstadoInstrumento)
            $("#txtObservacion").val(datos[0].Observaciones)

            if (datos[0].Estado) {
                $("#chkActivo").prop('checked', true);
            }

            //AnxoDetalle
            let AnexoDetalle = datos[0].AnexoDetalle;
            let trAnexo = '';
            for (var k = 0; k < AnexoDetalle.length; k++) {
                trAnexo += `
                <tr id="`+ AnexoDetalle[k].IdAnexo + `">
                    <td>
                       `+ AnexoDetalle[k].NombreArchivo + `
                    </td>
                    <td>
                       <a target="_blank" href="`+ AnexoDetalle[k].ruta + `"> Descargar </a>
                    </td>
                    <td><button type="button" class="btn btn-xs btn-danger borrar fa fa-trash" onclick="EliminarAnexo(`+ AnexoDetalle[k].IdAnexo + `)"></button></td>
                </tr>`;
            }
            $("#tabla_files").find('tbody').append(trAnexo);



        }

    });

}

function EliminarAnexo(idRow) {
    alertify.confirm('Confirmar', '¿Desea eliminar este Anexo?', function () {
        $.post("/Proveedor/EliminarAnexo", { 'IdAnexo': idRow }, function (data) {

            if (data == 0) {
                swal("Error!", "Ocurrio un Error")
                limpiarDatos();
            } else {
                swal("Exito!", "Anexo Eliminado", "success")
                $("#" + idRow).remove();
            }

        });

    }, function () { });
}

function eliminar(ID) {


    alertify.confirm('Confirmar', '¿Desea eliminar este registro?', function () {
        $.post("Eliminar", { 'IdInstrumentoMedicion': ID }, function (data) {

            if (data == 0) {
                swal("Error!", "Ocurrio un Error")
                limpiarDatos();
            } else {
                swal("Exito!", "Registro Eliminado", "success")
                table.destroy();
                ConsultaServidor();
                limpiarDatos();
            }

        });

    }, function () { });

}


function limpiarDatos() {
    $("#txtId").val('');
    $("#IdObra").val(0)
    $("#txtNombre").val('')
    $("#IdMarca").val(0)
    $("#IdModelo").val(0)
    $("#txtNumSerie").val('')
    $("#cboPeriodoCalibracion").val(0)
    $("#ParamMedicion").val('')
    $("#PatronMedicion").val('')
    $("#IdArea").val(0)
    $("#txtResponsable").val('')
    $("#EstadoCalibracion").val(0)
    $("#txtUbiacion").val('')
    $("#TipoCalibracion").val(0)
    $("#EstadoInstrumento").val(0)
    $("#txtObservacion").val('')
    $("#chkActivo").prop('checked', true);
}


function listarObra() {
    $.ajax({
        url: "/Obra/ObtenerObraxIdUsuarioSessionFiltro",
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

function listarMarca() {
    $.ajax({
        url: "/Marca/ObtenerMarca",
        type: "GET",
        contentType: "application/json",
        dataType: "json",
        data: {
            'estado': 1
        },
        cache: false,
        contentType: false,
        success: function (datos) {
            $("#IdMarca").html('');
            let options = `<option value="0">Seleccione</option>`;
            if (datos.length > 0) {

                for (var i = 0; i < datos.length; i++) {
                    options += `<option value="` + datos[i].IdMarca + `">` + datos[i].Descripcion + `</option>`;
                }
                $("#IdMarca").html(options);
            }
        }
    });
}

function listarModelo() {
    $.ajax({
        url: "/Modelo/ObtenerModelo",
        type: "GET",
        contentType: "application/json",
        dataType: "json",
        data: {
            'estado': 1
        },
        cache: false,
        contentType: false,
        success: function (datos) {
            $("#IdModelo").html('');
            let options = `<option value="0">Seleccione</option>`;
            if (datos.length > 0) {

                for (var i = 0; i < datos.length; i++) {
                    options += `<option value="` + datos[i].IdModelo + `">` + datos[i].Descripcion + `</option>`;
                }
                $("#IdModelo").html(options);
            }
        }
    });
}
let contadorAnexo = 0;
//function AgregarLineaAnexo(Nombre,NombreTabla) {
   
//    contadorAnexo++
//    let tr = '';
//    tr += `<tr id="filaAnexo` + contadorAnexo + `">
//            <td style="display:none"><input  class="form-control" type="text" value="0" id="txtIdSolicitudRQAnexo" name="txtIdSolicitudRQAnexo[]"/></td>
//            <td>
//               `+ Nombre + `
//               <input  class="form-control" type="hidden" value="`+ Nombre + `" id="txtNombreAnexo" name="txtNombreAnexo[]"/>
//            </td>
//            <td>
//               <a href="/Anexos/`+ Nombre + `" target="_blank" >Descargar</a>
//            </td>
//            <td><button type="button" class="btn  btn-danger btn-xs borrar fa fa-trash" onclick="EliminarAnexoEnMemoria(`+ contadorAnexo + `)"></button></td>
//            </tr>`;

//    $("#" + NombreTabla).find('tbody').append(tr);
//    //$("#tabla_files").find('tbody').append(tr);

//}

function AgregarLineaAnexo(ObjetoArchivo) {
    contadorAnexo++
    let tr = '';
    tr += `<tr id="filaAnexo` + contadorAnexo + `">
            <td style="display:none"><input  class="form-control" type="text" value="0" id="txtIdSolicitudRQAnexo" name="txtIdSolicitudRQAnexo[]"/></td>
            <td>
               `+ ObjetoArchivo.Nombre + `
               <input  class="form-control" type="hidden" value="`+ ObjetoArchivo.Nombre + `" id="txtNombreAnexo" name="txtNombreAnexo[]"/>
               <input  class="form-control" type="hidden" value="`+ ObjetoArchivo.url + `" id="txtUrlAnexo" name="txtUrlAnexo[]"/>
            </td>
            <td>
               <a href="`+ ObjetoArchivo.url + `" target="_blank" >Descargar</a>
            </td>
            <td><button type="button" class="btn btn-xs btn-danger borrar  fa fa-trash " onclick="EliminarAnexoEnMemoria(`+ contadorAnexo + `)"></button></td>
            </tr>`;

    $("#tabla_files").find('tbody').append(tr);

}


function EliminarAnexoEnMemoria(contAnexo) {
    $("#filaAnexo" + contAnexo).remove();
}

function listarArea() {
    $.ajax({
        url: "/Area/ObtenerArea",
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
function openContenido(evt, Name) {
    var i, tabcontent, tablinks;
    tabcontent = document.getElementsByClassName("tabcontent");
    for (i = 0; i < tabcontent.length; i++) {
        tabcontent[i].style.display = "none";
    }
    tablinks = document.getElementsByClassName("tablinks");
    for (i = 0; i < tablinks.length; i++) {
        tablinks[i].className = tablinks[i].className.replace(" active", "");
    }
    document.getElementById(Name).style.display = "block";
    evt.currentTarget.className += " active";
}

function VerDetalles(Id,varPeriodo,varNombre) {
    AbrirModal("modal-detalles");
    $("#txtIdCabecera").val(Id)
    $("#txtNombreCabecera").val(varNombre)
    $("#txtPeriodoCalibCabecera").val(varPeriodo)


    $("#txtInstrumentoDatoDetalle").val(varNombre)
    CalcularFechaCalibracion()
    CargarDetalles(Id)
}

function ModalDetalle() {
    $("#ModalDatoDetalle").modal('show')
    $("#btn_AgregarDocDetalle").prop("disabled", true)
    $("#lblModalDatoDetalle").html('Agregar Calibracion')
    $("#tbody_detalleDocs").empty()

}
function getFechaHoy() {
    var fechaHoy = new Date();
    var dia = String(fechaHoy.getDate()).padStart(2, "0");
    var mes = String(fechaHoy.getMonth() + 1).padStart(2, "0");
    var anio = fechaHoy.getFullYear();
    let FechaSalida = anio + "-" + mes + "-" + dia;
    return FechaSalida
}

function CalcularFechaCalibracion() {

    let FechaMantemiento = $("#txtFechaMantenimiento").val()
    let FechaProx = "";

    if ($("#txtPeriodoCalibCabecera").val() == "ANUAL") {
        FechaProx = (+FechaMantemiento.split('-')[0] + 1) + '-' + FechaMantemiento.split('-')[1] + '-' + FechaMantemiento.split('-')[2]
        $("#txtFechaProxMant").val(FechaProx)
        $("#txtFechaProxMant").prop('disabled', true)
    } else if ($("#txtPeriodoCalibCabecera").val() == "6 MESES") {
        var fecha = new Date(FechaMantemiento);

        var dia = fecha.getDate();


        fecha.setMonth(fecha.getMonth() + 6);


        if (fecha.getDate() !== dia) {
            fecha.setDate(0);
        }

        fecha.setDate(fecha.getDate() + 1);

        var year = fecha.getFullYear();
        var month = ('0' + (fecha.getMonth() + 1)).slice(-2);
        var day = ('0' + fecha.getDate()).slice(-2);

        var nuevaFechaString = year + '-' + month + '-' + day;

        $("#txtFechaProxMant").val(nuevaFechaString)
        $("#txtFechaProxMant").prop('disabled', true)
    } else {
        $("#txtFechaProxMant").val(FechaMantemiento)
    }

}

function GuardarDetalle() {
    $.post('UpdateInsertInstrumentoMedicionDetalle', {
        'IdInstrumentoMedicionDetalle': $("#txtIdDetalle").val(),
        'IdInstrumentoMedicion': $("#txtIdCabecera").val(),
        'Fecha': $("#txtFechaMantenimiento").val(),
        'ProximaFecha': $("#txtFechaProxMant").val(),
        'Observaciones': $("#txtObservacionDatoDetalle").val(),
    }, function (data, status) {

        if (data == 1) {
            swal("Exito!", "Proceso Realizado Correctamente", "success")
            CargarDetalles($("#txtIdCabecera").val())
            CerrarModalDatoDetalle()
            ConsultaServidor()
        } else {
            swal("Error!", "Ocurrio un Error")
        }

    });
}

function CargarDetalles(Id) {
    $.post('ObtenerInstrumentoMedicionDetalle', { 'IdInstrumentoMedicion': Id }, function (data, status) {
        $("#tbody_detalle").empty()
        //console.log(data);
        if (data == "error") {
           
            return;
        }

        let tr = '';

        let InstMed = JSON.parse(data);
        let total_InstMed = InstMed.length;

        for (var i = 0; i < InstMed.length; i++) {

            InstMed[i].Fecha = InstMed[i].Fecha.split('T')[0]
            let Fecha = InstMed[i].Fecha.split('-')[2] + '-' + InstMed[i].Fecha.split('-')[1] + '-' + InstMed[i].Fecha.split('-')[0]

            InstMed[i].ProximaFecha = InstMed[i].ProximaFecha.split('T')[0]
            let ProximaFecha = InstMed[i].ProximaFecha.split('-')[2] + '-' + InstMed[i].ProximaFecha.split('-')[1] + '-' + InstMed[i].ProximaFecha.split('-')[0]

            tr += '<tr>' +
                '<td>' + (i + 1) + '</td>' +
                '<td>' + Fecha + '</td>' +
                '<td>' + ProximaFecha + '</td>' +
                '<td>' + InstMed[i].Observaciones + '</td>' +             
                '<td><button class="btn btn-primary fa fa-pencil btn-xs" onclick="ObtenerDatosDetallexID(' + InstMed[i].IdInstrumentoMedicionDetalle + ')"></button>' +
                '<button class="btn btn-danger btn-xs fa fa-trash" style="background-color:red"  onclick="eliminarDetalle(' + InstMed[i].IdInstrumentoMedicionDetalle + ')"></button></td >' +
                '</tr>';
        }

        $("#tbody_detalle").html(tr);

    });
}
function CerrarModalDatoDetalle() {
    $("#ModalDatoDetalle").modal('hide')
    $("#txtObservacionDatoDetalle").val('')
    $("#txtIdDetalle").val('')
}

function ObtenerDatosDetallexID(Id) {

    $("#lblModalDatoDetalle").html('Editar Calibracion')

    $("#ModalDatoDetalle").modal('show')
    $.post('ObtenerDatosDetallexID', { 'IdInstrumentoMedicionDetalle': Id }, function (data, status) {

        if (data == "error") {

            Swal.fire("Error", "Ocurrió un error", "error");
            return
        }
        let dato = JSON.parse(data)
        console.log(dato[0].ProximaFecha)
        let fecha = (dato[0].Fecha).toString().split('T')[0]
        let fechaProxima = (dato[0].ProximaFecha).toString().split('T')[0]
        $("#txtInstrumentoDatoDetalle").val($("#txtNombreCabecera").val())
        $("#txtFechaMantenimiento").val(fecha)
        $("#txtFechaProxMant").val(fechaProxima)
        $("#txtObservacionDatoDetalle").val(dato[0].Observaciones) 
        $("#txtIdDetalle").val(dato[0].IdInstrumentoMedicionDetalle)
        $("#btn_AgregarDocDetalle").prop("disabled",false)
        
    });

    CargarDetallesDocs(Id)
}
function eliminarDetalle(ID) {
    alertify.confirm('Confirmar', '¿Desea eliminar este registro?', function () {
        $.post("EliminarDetalle", { 'IdInstrumentoMedicionDetalle': ID }, function (data) {

            if (data == 0) {
                swal("Error!", "Ocurrio un Error")
            } else {
                swal("Exito!", "Registro Eliminado", "success")
                CargarDetalles($("#txtIdCabecera").val())
                ConsultaServidor()
            }
        });
    }, function () { });
}

function ModalAgregarDocDetalle() {
    $("#ModalDetalleDocs").modal('show')
    $("#lblModalDetalleDocs").html('Agregar Documento Anexo')
}

function CargarProveedor() {
    $.ajaxSetup({ async: false });
    $.post("/Proveedor/ObtenerProveedores", { estado: 1 }, function (data, status) {
        let proveedores = JSON.parse(data);
        llenarComboProveedor(proveedores, "IdProveedor", "Seleccione")
    });
}

function llenarComboProveedor(lista, idCombo, primerItem) {
    var contenido = "";
    if (primerItem != null) contenido = "<option value='0'>" + primerItem + "</option>";
    var nRegistros = lista.length;
    var nCampos;
    var campos;
    for (var i = 0; i < nRegistros; i++) {

        if (lista.length > 0) { contenido += "<option value='" + lista[i].IdProveedor + "'>" + lista[i].NumeroDocumento + " - " + lista[i].RazonSocial + "</option>"; }
        else { }
    }
    var cbo = document.getElementById(idCombo);
    if (cbo != null) cbo.innerHTML = contenido;
}

function GuardarDetalleDoc() {

    //AnexoDetalle
    let arrayTxtNombreAnexo = new Array();
    $("input[name='txtNombreAnexo[]']").each(function (indice, elemento) {
        arrayTxtNombreAnexo.push($(elemento).val());
    });

    let AnexoDetalle = new Array;
    for (var i = 0; i < arrayTxtNombreAnexo.length; i++) {
        AnexoDetalle.push({
            'NombreArchivo': arrayTxtNombreAnexo[i]
        });
    }

    
    


    $.post('UpdateInsertInstrumentoMedicionDetalleDoc', {
        'IdInstrumentoMedicionDetalleDocs': $("#txtIdDetalleDoc").val(),
        'IdInstrumentoMedicionDetalle': $("#txtIdDetalle").val(),
        'NumeroDoc': $("#txtNumeroDoc").val(),
        'IdProveedor': $("#IdProveedor").val(),
        'Observaciones': $("#txtObservacionDetalleDoc").val(),
        AnexoDetalle
        
    }, function (data, status) {

        if (data == 1) {
            swal("Exito!", "Proceso Realizado Correctamente", "success")
            CerrarModalDatoDetalleDocs()
            CargarDetallesDocs($("#txtIdDetalle").val())
            ConsultaServidor()
        } else {
            swal("Error!", "Ocurrio un Error")
        }

    });
}

function CerrarModalDatoDetalleDocs() {
    $("#ModalDetalleDocs").modal('hide')
    $("#txtNumeroDoc").val('')
    $("#IdProveedor").val(0).change()
    $("#txtObservacionDetalleDoc").val('')
    $("#tabla_filesDetalle").empty()

}


function CargarDetallesDocs(Id) {
    $("#lblModalDetalleDocs").html('Editar Documento Anexo')

    $.post('ObtenerInstrumentoMedicionDetalleDoc', { 'IdInstrumentoMedicionDetalle': Id }, function (data, status) {
        $("#tbody_detalleDocs").empty()
        //console.log(data);
        if (data == "error") {

            return;
        }

        let tr = '';

        let InstMed = JSON.parse(data);
        let total_InstMed = InstMed.length;

        for (var i = 0; i < InstMed.length; i++) {

            let anexo = "-";
            console.log(InstMed[i].NombreAnexo)
            if (InstMed[i].NombreAnexo != "-") {
                anexo = '<a target="_blank" href="/Anexos/' + InstMed[i].NombreAnexo + '"> ' + InstMed[i].NombreAnexo + ' </a>'
            }


            tr += '<tr>' +
                '<td>' + (i + 1) + '</td>' +
                '<td>' + InstMed[i].NumeroDoc + '</td>' +
                '<td>' + InstMed[i].Observaciones + '</td>' +
                '<td>' + anexo +'</td>' +
                '<td><button class="btn btn-primary fa fa-pencil btn-xs" onclick="ObtenerDatosDetalleDocxID(' + InstMed[i].IdInstrumentoMedicionDetalleDocs + ')"></button>' +
                '<button class="btn btn-danger btn-xs  fa fa-trash" style="background-color:red"  onclick="eliminarDetalleDoc(' + InstMed[i].IdInstrumentoMedicionDetalleDocs + ')"></button></td >' +
                '</tr>';
        }

        $("#tbody_detalleDocs").html(tr);

    });
}


function ObtenerDatosDetalleDocxID(Id) {
  
    $("#ModalDetalleDocs").modal('show')
  
    $("#tabla_filesDetalle").find('tbody').empty();
        //console.log(varIdUsuario);

    $.post('ObtenerDatosDetalleDocxID', {
            'IdInstrumentoMedicionDetalleDoc': Id,
        }, function (data, status) {

            if (data == "Error") {
                swal("Error!", "Ocurrio un error")
                limpiarDatos();
            } else {
                let datos = JSON.parse(data);
                console.log(datos);
                $("#txtIdDetalleDoc").val(datos[0].IdInstrumentoMedicionDetalleDocs);
                $("#txtNumeroDoc").val(datos[0].NumeroDoc)
                $("#IdProveedor").val(datos[0].IdProveedor).change()
                $("#txtObservacionDetalleDoc").val(datos[0].Observaciones)

                if (datos[0].Estado) {
                    $("#chkActivo").prop('checked', true);
                }

                //AnxoDetalle
                let AnexoDetalle = datos[0].AnexoDetalle;
                let trAnexo = '';
                for (var k = 0; k < AnexoDetalle.length; k++) {
                    trAnexo += `
                <tr id="`+ AnexoDetalle[k].IdAnexo + `">
                    <td>
                       `+ AnexoDetalle[k].NombreArchivo + `
                    </td>
                    <td>
                       <a target="_blank" href="`+ AnexoDetalle[k].ruta + `"> Descargar </a>
                    </td>
                    <td><button type="button" class="btn btn-xs btn-danger borrar fa fa-trash" onclick="EliminarAnexo(`+ AnexoDetalle[k].IdAnexo + `)"></button></td>
                </tr>`;
                }
                $("#tabla_filesDetalle").find('tbody').append(trAnexo);



            }

        });

    
}

function eliminarDetalleDoc(ID) {
    alertify.confirm('Confirmar', '¿Desea eliminar este registro?', function () {
        $.post("EliminarDetalleDoc", { 'IdInstrumentoMedicionDetalleDoc': ID }, function (data) {

            if (data == 0) {
                swal("Error!", "Ocurrio un Error")
            } else {
                swal("Exito!", "Registro Eliminado", "success")
                CargarDetallesDocs($("#txtIdDetalle").val())
                ConsultaServidor()
            }
        });
    }, function () { });
}