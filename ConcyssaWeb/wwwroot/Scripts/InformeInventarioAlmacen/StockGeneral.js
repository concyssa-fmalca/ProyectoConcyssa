
window.onload = function () {
    BuscarItemsExp()


};

function BuscarItemsExp() {


    $("#cboAgregarArticulo").select2({
        language: "es",
        width: '100%',

        //theme: "classic",
        async: false,
        ajax: {
            url: "/Articulo/ListarArticulosActivosSelect2",
            type: "post",
            dataType: 'json',
            delay: 250,
            data: function (params) {

                return {
                    searchTerm: params.term, // search term            
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
function Buscar() {

    let IdArticulo = $("#cboAgregarArticulo").val()

    if ($("#cboClaseArticulo").val() != 2) {
        $.post("/Articulo/ObtenerArticulosConStockObras", { 'IdArticulo': IdArticulo }, function (data, status) {


            try {
                let tr = '';

                let area = JSON.parse(data);

                for (var i = 0; i < area.length; i++) {


                    tr += '<tr>' +
                        '<td>' + area[i].Obra + '</td>' +
                        '<td>' + area[i].Almacen + '</td>' +
                        '<td>' + area[i].Stock + '</td>' +
                        '</tr>';
                }

                $("#tbody_stockOtrosAlmacenes").html(tr);

            } catch (e) {
                $("#tbody_stockOtrosAlmacenes").html("");
            }



        });
    } else {
    }
}