const tablaUsuarios = null;

$(document).ready(function () {
  showHome();

  $("#card-body-usaurio").hide();
  $("#card-body-medicamento").hide();

  $("#btn-login-aceptar").click(function () {
    var user = $("#txt-usuario").val();
    var pass = $("#txt-pass").val();
    var url =
      CEPDI.app.urlAppHost() +
      `api/Usuario/Login?usuario=${user}&contrasena=${pass}`;

    CEPDI.app.GetJsonObject(url, null, "GET", function (response) {
      if (response != null) {
        if (response.data !== null) {
          setUsuarioStorage("usuario", response);
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
  $("#btn-guardar-usuario").click(function () {
    var usuario = {
        nombre: $("#txt-nombre-usuario").val(),
        usuario: $("#txt-nombreusuario-usuario").val(),
        password: $("#txt-password-usuario").val(),
        acciones: $("#txt-acciones-usuario").val(),
    };

    var apiUrl = "https://localhost:7058/api/usuario/agregar";
    CEPDI.app.CallApiAjax(apiUrl, usuario, "POST", function (response) {
        alert(response.message);
        CEPDI.app.HideDialogModal('exampleModal');
        if (response.isSuccess) { 
            this.tablaUsuarios.ajax.reload();
            CEPDI.app.limpiarFormulario("form-usuario");
        }
    });
  });
});

function actividadesUsuario() {
  $("#tltle-element").text("USUARIO");
  $("#card-body-medicamento").hide();
  $("#card-body-usaurio").show();

  var botonesAcciones =
    "<div class='btn-group' role='group' aria-label='Basic example'>";
  botonesAcciones +=
    "<button type='button' class='btn btn-light' onclick='editarUsuario();'><i class='glyphicon glyphicon-pencil'></i></button>";
  botonesAcciones +=
    "<button type='button' class='btn btn-light' onclick='eliminarUsuario();'><i class='glyphicon glyphicon-trash'></i></button>";
  botonesAcciones += "</div>";

  this.tablaUsuarios = CEPDI.app.startDatatable(
    "tabla-reporte-usuarios",
    [
      { data: "id" },
      { data: "nombre" },
      { data: "creacion" },
      { data: "usuario" },
      { data: "acciones" },
      { data: "", defaultContent: botonesAcciones },
    ],
    "https://localhost:7058/api/Usuario/todos"
  );
}
function actividadesMedicamento() {
  var botonesAcciones =
    "<div class='btn-group' role='group' aria-label='Basic example'>";
  botonesAcciones +=
    "<button type='button' class='btn btn-light' onclick='editarMedicamento();'><i class='glyphicon glyphicon-pencil'></i></button>";
  botonesAcciones +=
    "<button type='button' class='btn btn-light' onclick='eliminarMedicamento();'><i class='glyphicon glyphicon-trash'></i></button>";
  botonesAcciones += "</div>";

  $("#tltle-element").text("MEDICAMENTO");
  $("#card-body-usaurio").hide();
  $("#card-body-medicamento").show();
  CEPDI.app.startDatatable(
    "tabla-reporte-medicamentos",
    [
      { data: "idmedicamento" },
      { data: "nombre" },
      { data: "concentracion" },
      { data: "precio" },
      { data: "presentacion" },
      { data: "acciones" },
      { data: "", defaultContent: botonesAcciones },
    ],
    "https://localhost:7058/api/Medicamento/todos"
  );
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
  var user = JSON.parse(getUsuarioStorage());
  //alert(getUsuarioStorage());
  //alert(user.data.nombre)
  $("#li-username").text(user.data.nombre);
}
