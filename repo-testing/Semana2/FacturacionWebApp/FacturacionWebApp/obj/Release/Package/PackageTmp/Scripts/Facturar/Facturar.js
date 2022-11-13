$(function () {
    $("#buscar").click(function (e) {
        e.preventDefault();
        if ($("#producto").val()!="") {
            $.ajax({
                url: urlBuscarProducto,
                method: "POST",
                data: $("#formFactura").serialize(),
                success: function (data) {
                    $("#modal").find(".modal-body").html(data);
                    $("#modal").modal('show');
                }
            });
        } else {
            Swal.fire({
                title: 'Error!',
                text: "LLene el campo producto",
                icon: 'error',
                confirmButtonText: 'OK'
            });
        }
    });

    $("#consultar").click(function (e) {
        e.preventDefault();
        if ($("#numero").val()!="") {
            $.ajax({
                url: urlConsultarFactura,
                method: "GET",
                data: { _nfactura: $("#numero").val() },
                success: function (data) {
                    if (data != "") {
                        $("#modal").find(".modal-body").html(data);
                        $("#modal").modal('show');
                    } else {
                        Swal.fire({
                            title: 'Error!',
                            text: "Esa factura no existe",
                            icon: 'error',
                            confirmButtonText: 'OK'
                        });
                    }
                }
            });
        } else {
            Swal.fire({
                title: 'Error!',
                text: "LLene el campo numero de factura",
                icon: 'error',
                confirmButtonText: 'OK'
            });
        }
    });

    $("#facturar").click(function (e) {
        e.preventDefault();
        $.ajax({
            url: urlIngresarCab,
            method: "POST",
            //data: $("#formFactura").serialize(),
            success: function (data) {
                if (data.IdFactura!=0) {
                    $.ajax({
                        url: urlIngresarDet,
                        method: "POST",
                        data: { _nfactura: data.IdFactura },
                        success: function (data) {
                            Swal.fire({
                                title: 'Error!',
                                text: data.Mensaje,
                                icon: 'success',
                                confirmButtonText: 'OK'
                            });
                            location.reload();
                        }
                    });
                    Swal.fire({
                        title: 'Realizado con exito!',
                        text: data.Mensaje,
                        icon: 'success',
                        confirmButtonText: 'OK'
                    });
                }
                else {
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

    $(".eliminar").click(function () {
        var productIndex = $(this).attr('id');
        $.ajax({
            url: urlEliminarDet,
            method: "POST",
            data: { _indiceDetalle: productIndex },
            success: function () {
                window.location.href = urlIndexFacturar;
            }
        });
    });
});