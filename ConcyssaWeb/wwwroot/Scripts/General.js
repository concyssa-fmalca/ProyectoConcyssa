
let GDecimalesCantidades;
let GDecimalesImportes;
let GDecimalesPrecios;
let GDecimalesPorcentajes;

function ObtenerConfiguracionDecimales() {
    //$.post("/ConfiguracionDecimales/ObtenerConfiguracionDecimales", function (data, status) {

    //    let datos = JSON.parse(data);
    //    GDecimalesCantidades = datos[0].Cantidades;
    //    GDecimalesImportes = datos[0].Importes;
    //    GDecimalesPrecios = datos[0].Precios;
    //    GDecimalesPorcentajes = datos[0].Porcentajes;
    //});

    var xhr = new XMLHttpRequest();
    xhr.withCredentials = true;
    xhr.addEventListener("readystatechange", function () {
        if (this.readyState === 4) {
            let datos = JSON.parse(this.responseText);
            GDecimalesCantidades = datos[0].Cantidades;
            GDecimalesImportes = datos[0].Importes;
            GDecimalesPrecios = datos[0].Precios;
            GDecimalesPorcentajes = datos[0].Porcentajes;
        }
    });
    xhr.open("GET", "/ConfiguracionDecimales/ObtenerConfiguracionDecimales");
    xhr.send();

}

let lenguaje = {
    "language": {
        "decimal": ",",
        "thousands": ".",
        "info": "Mostrando registros del _START_ al _END_ de un total de _TOTAL_ registros",
        "infoEmpty": "Mostrando registros del 0 al 0 de un total de 0 registros",
        "infoPostFix": "",
        "infoFiltered": "(filtrado de un total de _MAX_ registros)",
        "loadingRecords": "Cargando...",
        "lengthMenu": "Mostrar _MENU_ registros",
        "paginate": {
            "first": "Primero",
            "last": "Último",
            "next": "Siguiente",
            "previous": "Anterior"
        },
        "processing": "Procesando...",
        "search": "Buscar:",
        "searchPlaceholder": "",
        "zeroRecords": "No se encontraron resultados",
        "emptyTable": "Ningún dato disponible en esta tabla",
        "aria": {
            "sortAscending": ": Activar para ordenar la columna de manera ascendente",
            "sortDescending": ": Activar para ordenar la columna de manera descendente"
        },

    },
    //scrollX: true,
    responsive: true
};

let lenguaje_data = {
        "decimal": ",",
        "thousands": ".",
        "info": "Mostrando registros del _START_ al _END_ de un total de _TOTAL_ registros",
        "infoEmpty": "Mostrando registros del 0 al 0 de un total de 0 registros",
        "infoPostFix": "",
        "infoFiltered": "(filtrado de un total de _MAX_ registros)",
        "loadingRecords": "Cargando...",
        "lengthMenu": "Mostrar _MENU_ registros",
        "paginate": {
            "first": "Primero",
            "last": "Último",
            "next": "Siguiente",
            "previous": "Anterior"
        },
        "processing": "Procesando...",
        "search": "Buscar:",
        "searchPlaceholder": "",
        "zeroRecords": "No se encontraron resultados",
        "emptyTable": "Ningún dato disponible en esta tabla",
        "aria": {
            "sortAscending": ": Activar para ordenar la columna de manera ascendente",
            "sortDescending": ": Activar para ordenar la columna de manera descendente"
        },

   
};

function AbrirModal(idModal) {
    $.magnificPopup.open({
        //removalDelay: 100,
        items: {
            src: $('#' + idModal)
        },
        callbacks: {
            beforeOpen: function (e) {
                var Animation = 'mfp-slideDown';
                this.st.mainClass = Animation;

            }
        },
        midClick: true,
    });
    setTimeout(function () {
        var elems = document.getElementsByClassName('input-sm');
        for (var i = elems.length; i--;) {
            var tidx2 = elems[i].getAttribute('tabindex');
            if (tidx2 == 1) elems[i].focus();
        }
        $(".mfp-wrap").eq(0).removeAttr("tabindex");
    }, 100);
}

function closePopup() {
    $.magnificPopup.close();
    limpiarDatos();
    //console.log("hola11");
    //if (scrollHeight > 0) {
    //    setTimeout(function () {
    //        var html = document.getElementsByTagName("HTML")[0];
    //        html.style.height = scrollHeight + "px";
    //    }, 500);
    //}
}

function formatNumberDecimales(num, tipodecimal) {
    //GDecimalesCantidades
    //GDecimalesImportes
    //GDecimalesPrecios
    //GDecimalesPorcentajes
    let numerooriginal = 0;
    let partedecimal = 0;
    numerooriginal = num;
    switch (tipodecimal) {
        case 1: //CANTIDADES
            if (GDecimalesCantidades == 2) {
                if (!num || num == 'NaN') return '-';
                if (num == 'Infinity') return '&#x221e;';
                num = num.toString().replace(/\$|\,/g, '');
                if (isNaN(num))
                    num = "0";
                sign = (num == (num = Math.abs(num)));
                num = Math.floor(num * 100 + 0.50000000001);
                cents = num % 100;
                console.log(num)
                console.log(cents)
                num = Math.floor(num / 100).toString();
                if (cents < 10)
                    cents = "0" + cents;
                for (var i = 0; i < Math.floor((num.length - (1 + i)) / 3); i++)
                    num = num.substring(0, num.length - (4 * i + 3)) + ',' + num.substring(num.length - (4 * i + 3));
                return (((sign) ? '' : '-') + num + '.' + cents);
            } else {
                
                if (!num || num == 'NaN') return '-';
                if (num == 'Infinity') return '&#x221e;';
                num = num.toString().replace(/\$|\,/g, '');
                if (isNaN(num))
                    num = "0";
                sign = (num == (num = Math.abs(num)));
                num = Math.floor(num * 100 + 0.50000000001);


                if (!isNaN(num)) {
                  
                    if (!(numerooriginal % 1 == 0)) {
                        
                        partedecimal = numerooriginal.toString().split(".")[1];
                        //5-3
                        alert(partedecimal.length)
                        alert(GDecimalesCantidades)
                        if (partedecimal.length < GDecimalesCantidades) {
                            let textoconcatenar = "";
                            for (var i = 0; i < (GDecimalesCantidades - partedecimal.length); i++) {
                                textoconcatenar +="0"
                            }
                           
                          
                            cents = partedecimal + textoconcatenar
                            console.log('*******')
                            console.log(cents)
                            console.log('*******')
                        } else {
                            cents = partedecimal.substring(0, GDecimalesCantidades)
                          
                        }
                    } else {
                        cents = 00
                    }
                } else {
                    cents=00
                }
                console.log(cents);
                //cents = num % 100;
                num = Math.floor(num / 100).toString();
                //if (cents < 10)
                //    cents = "0" + cents;
                for (var i = 0; i < Math.floor((num.length - (1 + i)) / 3); i++)
                    num = num.substring(0, num.length - (4 * i + 3)) + ',' + num.substring(num.length - (4 * i + 3));
                return (((sign) ? '' : '-') + num + '.' + cents);
            }
            break;
        case 2: //importes
            if (GDecimalesImportes == 2) {
                if (!num || num == 'NaN') return '-';
                if (num == 'Infinity') return '&#x221e;';
                num = num.toString().replace(/\$|\,/g, '');
                if (isNaN(num))
                    num = "0";
                sign = (num == (num = Math.abs(num)));
                num = Math.floor(num * 100 + 0.50000000001);
                cents = num % 100;
                console.log(num)
                console.log(cents)
                num = Math.floor(num / 100).toString();
                if (cents < 10)
                    cents = "0" + cents;
                for (var i = 0; i < Math.floor((num.length - (1 + i)) / 3); i++)
                    num = num.substring(0, num.length - (4 * i + 3)) + ',' + num.substring(num.length - (4 * i + 3));
                return (((sign) ? '' : '-') + num + '.' + cents);
            } else {

                if (!num || num == 'NaN') return '-';
                if (num == 'Infinity') return '&#x221e;';
                num = num.toString().replace(/\$|\,/g, '');
                if (isNaN(num))
                    num = "0";
                sign = (num == (num = Math.abs(num)));
                num = Math.floor(num * 100 + 0.50000000001);


                if (!isNaN(num)) {
                    alert(numerooriginal % 1);
                    if (!(numerooriginal % 1 == 0)) {
                        console.log(numerooriginal.toString())
                        partedecimal = numerooriginal.toString().split(".")[1];
                        console.log(partedecimal.length)
                        //5-3
                        if (partedecimal.length < GDecimalesImportes) {
                            let textoconcatenar = "";
                            for (var i = 0; i < (GDecimalesImportes - partedecimal.length); i++) {
                                textoconcatenar += "0"
                            }

                            cents = partedecimal + textoconcatenar
                        } else {
                            cents = partedecimal.substring(0, GDecimalesImportes)

                        }
                    } else {
                        cents = 00
                    }
                } else {
                    cents = 00
                }
                //cents = num % 100;
                num = Math.floor(num / 100).toString();
                //if (cents < 10)
                //    cents = "0" + cents;
                for (var i = 0; i < Math.floor((num.length - (1 + i)) / 3); i++)
                    num = num.substring(0, num.length - (4 * i + 3)) + ',' + num.substring(num.length - (4 * i + 3));
                return (((sign) ? '' : '-') + num + '.' + cents);
            }
            break;
        case 3: //precios
            // code block
            if (GDecimalesPrecios == 2) {
                if (!num || num == 'NaN') return '-';
                if (num == 'Infinity') return '&#x221e;';
                num = num.toString().replace(/\$|\,/g, '');
                if (isNaN(num))
                    num = "0";
                sign = (num == (num = Math.abs(num)));
                num = Math.floor(num * 100 + 0.50000000001);
                cents = num % 100;
                console.log(num)
                console.log(cents)
                num = Math.floor(num / 100).toString();
                if (cents < 10)
                    cents = "0" + cents;
                for (var i = 0; i < Math.floor((num.length - (1 + i)) / 3); i++)
                    num = num.substring(0, num.length - (4 * i + 3)) + ',' + num.substring(num.length - (4 * i + 3));
                return (((sign) ? '' : '-') + num + '.' + cents);
            } else {

                if (!num || num == 'NaN') return '-';
                if (num == 'Infinity') return '&#x221e;';
                num = num.toString().replace(/\$|\,/g, '');
                if (isNaN(num))
                    num = "0";
                sign = (num == (num = Math.abs(num)));
                num = Math.floor(num * 100 + 0.50000000001);


                if (!isNaN(num)) {
                    alert(numerooriginal % 1);
                    if (!(numerooriginal % 1 == 0)) {
                        console.log(numerooriginal.toString())
                        partedecimal = numerooriginal.toString().split(".")[1];
                        console.log(partedecimal.length)
                        //5-3
                        if (partedecimal.length < GDecimalesPrecios) {
                            let textoconcatenar = "";
                            for (var i = 0; i < (GDecimalesPrecios - partedecimal.length); i++) {
                                textoconcatenar += "0"
                            }

                            cents = partedecimal + textoconcatenar
                        } else {
                            cents = partedecimal.substring(0, GDecimalesPrecios)

                        }
                    } else {
                        cents = 00
                    }
                } else {
                    cents = 00
                }
                //cents = num % 100;
                num = Math.floor(num / 100).toString();
                //if (cents < 10)
                //    cents = "0" + cents;
                for (var i = 0; i < Math.floor((num.length - (1 + i)) / 3); i++)
                    num = num.substring(0, num.length - (4 * i + 3)) + ',' + num.substring(num.length - (4 * i + 3));
                return (((sign) ? '' : '-') + num + '.' + cents);
            }
            break;
        case 4: //porcentajes
            // code block
            if (GDecimalesPorcentajes == 2) {
                if (!num || num == 'NaN') return '-';
                if (num == 'Infinity') return '&#x221e;';
                num = num.toString().replace(/\$|\,/g, '');
                if (isNaN(num))
                    num = "0";
                sign = (num == (num = Math.abs(num)));
                num = Math.floor(num * 100 + 0.50000000001);
                cents = num % 100;
                console.log(num)
                console.log(cents)
                num = Math.floor(num / 100).toString();
                if (cents < 10)
                    cents = "0" + cents;
                for (var i = 0; i < Math.floor((num.length - (1 + i)) / 3); i++)
                    num = num.substring(0, num.length - (4 * i + 3)) + ',' + num.substring(num.length - (4 * i + 3));
                return (((sign) ? '' : '-') + num + '.' + cents);
            } else {

                if (!num || num == 'NaN') return '-';
                if (num == 'Infinity') return '&#x221e;';
                num = num.toString().replace(/\$|\,/g, '');
                if (isNaN(num))
                    num = "0";
                sign = (num == (num = Math.abs(num)));
                num = Math.floor(num * 100 + 0.50000000001);


                if (!isNaN(num)) {
                    alert(numerooriginal % 1);
                    if (!(numerooriginal % 1 == 0)) {
                        console.log(numerooriginal.toString())
                        partedecimal = numerooriginal.toString().split(".")[1];
                        console.log(partedecimal.length)
                        //5-3
                        if (partedecimal.length < GDecimalesPorcentajes) {
                            let textoconcatenar = "";
                            for (var i = 0; i < (GDecimalesPorcentajes - partedecimal.length); i++) {
                                textoconcatenar += "0"
                            }

                            cents = partedecimal + textoconcatenar
                        } else {
                            cents = partedecimal.substring(0, GDecimalesPorcentajes)

                        }
                    } else {
                        cents = 00
                    }
                } else {
                    cents = 00
                }
                //cents = num % 100;
                num = Math.floor(num / 100).toString();
                //if (cents < 10)
                //    cents = "0" + cents;
                for (var i = 0; i < Math.floor((num.length - (1 + i)) / 3); i++)
                    num = num.substring(0, num.length - (4 * i + 3)) + ',' + num.substring(num.length - (4 * i + 3));
                return (((sign) ? '' : '-') + num + '.' + cents);
            }
            break;
        default:
        // code block
    }

    
}

// function ValidarFechaContabilizacionxDocumento(IdSerie, IdDocumento, Fecha, Orden) {
//    let respuesta;
//    var xhr = new XMLHttpRequest();
//    xhr.withCredentials = true;
//    //$.ajaxSetup({ async: false });
//    xhr.addEventListener("readystatechange", function () {
//        if (this.readyState === 4) {
//            if (validadJsonGeneral(this.responseText)) {
//                return respuesta = JSON.parse(this.responseText);

//            } else {
//                return respuesta
//            } 
    
//        }
//    });
//    xhr.open("GET", "/Serie/ObtenerDatosSerieValidacion?IdSerie=" + IdSerie + "&IdDocumento=" + IdDocumento + "&Orden=" + Orden + "&fecha=" + Fecha);
//    xhr.send();

//}





function validadJsonGeneral(json) {
    try {
        object = JSON.parse(json);
        return true;
    } catch (error) {
        return false;
    }
}


ObtenerConfiguracionDecimales();