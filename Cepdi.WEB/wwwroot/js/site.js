
$(document).ready(function () {
   
    showHome();

    $("#card-body-usaurio").hide();
    $("#card-body-medicamento").hide();

    $("#btn-login-aceptar").click(function () {

        
        var user = $("#txt-usuario").val();
        var pass = $("#txt-pass").val();
        var url = CEPDI.app.urlAppHost() + `api/Usuario/Login?usuario=${user}&contrasena=${pass}`;

        CEPDI.app.GetJsonObject(
            url,
            null,
            'GET',
            function (response) {
                if (response != null) {
                    
                    if (response.data !== null) {
                        setUsuarioStorage('usuario', response);
                        showHome();
                    } else {
                        CEPDI.app.MessageBox(user.message);
                    }
                }
            });
    });

    $("#item-cerrar-sesion").click(function () {
        removeUserSession();
        showHome();
    });
    $("#item-usuario").click(function () {
        actividadesUsuario();
    });
    $("#item-medicamento").click(function () {
        actividadesMedicamento();

    });
    $("#nav-item-usuario").click(function () {
        actividadesUsuario();
    });
    $("#nav-item-medicamento").click(function () {
        actividadesMedicamento();
    });
});

function actividadesUsuario() { 
    $("#tltle-element").text("USUARIO");
    $("#card-body-medicamento").hide();
    $("#card-body-usaurio").show();

    var botonesAcciones = "<div class='btn-group' role='group' aria-label='Basic example'>";
    botonesAcciones += "<button type='button' class='btn btn-info' onclick='editarUsuario();'>Editar</button>"
    botonesAcciones += "<button type='button' class='btn btn-danger' onclick='eliminarUsuario();'>Eliminar</button>"
    botonesAcciones += "</div>"
   
    CEPDI.app.startDatatable("tabla-reporte-usuarios", [
        { "data": "id" },
        { "data": "nombre" },
        { "data": "creacion" },
        { "data": "usuario" },
        { "data": "acciones" },
        { "data": "", "defaultContent": botonesAcciones }
    ], "https://localhost:7058/api/Usuario/todos");

    
}
function actividadesMedicamento() { 

    var botonesAcciones = "<div class='btn-group' role='group' aria-label='Basic example'>";
    botonesAcciones += "<button type='button' class='btn btn-info' onclick='editarMedicamento();'>Editar</button>"
    botonesAcciones += "<button type='button' class='btn btn-danger' onclick='eliminarMedicamento();'>Eliminar</button>"
    botonesAcciones += "</div>"

    $("#tltle-element").text("MEDICAMENTO");
    $("#card-body-usaurio").hide();
    $("#card-body-medicamento").show();
    CEPDI.app.startDatatable("tabla-reporte-medicamentos", [
        { "data": "idmedicamento" },
        { "data": "nombre" },
        { "data": "concentracion" },
        { "data": "precio" },
        { "data": "presentacion" },
        { "data": "acciones" },
        { "data": "", "defaultContent": botonesAcciones }
    ], "https://localhost:7058/api/Medicamento/todos");
    
}

function setUsuarioStorage(key, value) {
    

    localStorage.setItem(key, JSON.stringify(value));
}
function getUsuarioStorage() {
    return localStorage.getItem("usuario");
}

function removeUserSession() {
    localStorage.removeItem("usuario");
}

function isUserLogin() {
    var user = getUsuarioStorage();
    console.log(user);

    return getUsuarioStorage() === null ? false : true;
}

function showHome() {
    if (isUserLogin()) {
        $("#login-content").hide();
        $("#home-content").show();
        loadUser();
    } else {
        $("#login-content").show();
        $("#home-content").hide();
    }
}

function loadUser() {

    var user = JSON.parse( getUsuarioStorage() );
    //alert(getUsuarioStorage());
    //alert(user.data.nombre)
    $("#li-username").text(user.data.nombre);
   
}
