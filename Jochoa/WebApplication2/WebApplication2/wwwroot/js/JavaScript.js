
$(function () {

    var producto = {
        "id": 80,
        "nombre": "durazno",
        "descripcion": "pequeño",
        "precio": 0.45
    }

    $.ajax({
        type: "Get",
        url: "Home/ObtenerProductos",
        success: function (response) {
            var Tabla = "";
            response.forEach(element => {
                Tabla += `<tr>
                            <th>${element.id}</th>
                            <td>${element.nombre}</td>
                            <td>${element.descripcion}</td>
                            <td>${element.precio}</td>
                        </tr>`;

            });
            $("#cuerpoTabla").append(Tabla);
        }
    });

    $.ajax({
        type: "Post",
        url: "Home/GuardarProducto",
        data: producto,
        dataType: "json",
        success: function (response) {
            alert(response);
        }
    });

});