$(function () {

    var idProducto = 0;
    var coincidencias;

    $("#btn_Nuevo").click(function (e) {
        e.preventDefault();
        $.ajax({
            url: urlIngresarProducto,
            method: "POST",
            data: $("#formProducto").serialize(),
            success: function (data) {

                if (data.Codigo == "000") {
                    Swal.fire({
                        title: 'Realizado Exitosamente!',
                        text: data.Mensaje,
                        icon: 'success',
                        confirmButtonText: 'OK'
                    });
                    location.reload();
                } else {
                    Swal.fire({
                        title: 'Error!',
                        text: data.Mensaje,
                        icon: 'error',
                        confirmButtonText: 'OK'
                    });
                }
            }
        });
    });

    $(document).on('click', '.editar', function () {
    //$(".editar").click(function () {
        $("#Code").prop('disabled', true);
        $("#btn_Nuevo").prop('disabled', true);
        $("#btn_Buscar").prop('disabled', true);
        $("#btn_Encontrado").prop('disabled', true);
        $("#editar").show();
        $("#cancelar").show();
        var cod, nom, desc, pr, stk;
        if ($(this).attr('idB') == undefined) {
            var productIndex = $(this).attr('id');
            cod = arreglo[productIndex].Codigo;
            nom = arreglo[productIndex].Nombre;
            desc = arreglo[productIndex].Descripcion;
            pr = arreglo[productIndex].Precio;
            stk = arreglo[productIndex].Stock;
        }
        else {
            var productIndex = $(this).attr('idB');
            cod = coincidencias[productIndex].Codigo;
            nom = coincidencias[productIndex].Nombre;
            desc = coincidencias[productIndex].Descripcion;
            pr = coincidencias[productIndex].Precio;
            stk = coincidencias[productIndex].Stock;
        }
        idProducto = cod;
        $("#Code").val(cod);
        $("#Name").val(nom);
        $("#Descripcion").val(desc);
        $("#Precio").val(pr);
        $("#Stock").val(stk);
        
    });

    $("#btn_Actualizar").click(function (e) {
        e.preventDefault();
        $("#Code").val(idProducto);
        $("#Code").prop('disabled', false);
        $.ajax({
            url: urlActualizarProducto,
            method: "POST",
            data: $("#formProducto").serialize(),
            success: function (data) {
                if (data.Codigo == "000") {
                    Swal.fire({
                        title: 'Realizado Exitosamente!',
                        text: data.Mensaje,
                        icon: 'success',
                        confirmButtonText: 'OK'
                    });
                    location.reload();
                } else {
                    $("#Code").prop('disabled', true);
                    Swal.fire({
                        title: 'Error!',
                        text: data.Mensaje,
                        icon: 'error',
                        confirmButtonText: 'OK'
                    });
                }
            }
        });
    });

    $("#btn_Cancelar").click(function (e) {
        e.preventDefault();
        $("#Code").prop('disabled', false);
        $("#btn_Nuevo").prop('disabled', false);
        $("#btn_Buscar").prop('disabled', false);
        $("#btn_Encontrado").prop('disabled', false);
        $("#Code").val("");
        $("#Name").val("");
        $("#Descripcion").val("");
        $("#Precio").val("");
        $("#Stock").val("");
        $("#editar").hide();
        $("#cancelar").hide();
    });

    $("#btn_Buscar").click(function (e) {
        e.preventDefault();
        $("#tabla2 *").remove();
        var i = 0;
        if ($("#Name").val()!="") {
            var tabla = "";
            $("#tabla1").hide();
            $("#tabla2").show();
            $("#Encontrado").show();
            coincidencias = arreglo.filter(producto => producto.Nombre.includes($("#Name").val()));
            coincidencias.forEach(element => {
                tabla += `<tr>
                            <td>${element.Codigo}</td>
                            <td>${element.Nombre}</td>
                            <td>${element.Descripcion}</td>
                            <td>${element.Precio}</td>
                            <td>${element.Stock}</td>
                            <td><i class="fa-solid fa-pen-to-square editar" style="cursor:pointer" idB=${i}></i></td>
                          </tr>`
                i++;
            });
            $(tabla).appendTo("#tabla2");
        } else {
            Swal.fire({
                title: 'Error!',
                text: "Llene el campo Nombre",
                icon: 'error',
                confirmButtonText: 'OK'
            });
        }
    });

    $("#btn_Encontrado").click(function (e) {
        e.preventDefault();
        $("#tabla1").show();
        $("#tabla2").hide();
        $("#Encontrado").hide();
        $("#btn_Nuevo").prop('disabled', false);
        $("#btn_Buscar").prop('disabled', false);
    });

});