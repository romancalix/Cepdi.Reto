const tablaUsuarios = null;

//URL Usuarios
const urlBase = "https://localhost:7058/api";
const urlEliminarUsuario = `${urlBase}/Usuario/eliminar?id=`;

//URL Medicamentos
const urlMedicamentosTdos = `${urlBase}/Medicamento/todos`;

$(document).ready(function () {
  showHome();
  $("#btn-openModal-usuario").hide();
  $("#btn-openModal-medicamento").hide();

  $("#card-body-usaurio").hide();
  $("#card-body-medicamento").hide();

  $("#btn-login-aceptar").click(function () {
    var user = $("#txt-usuario").val();
    var pass = $("#txt-pass").val();
    var url = `${urlBase}/Usuario/Login?usuario=${user}&contrasena=${pass}`;

    CEPDI.app.CallApiAjax(url, null, "GET", function (response) {
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

  $("#btn-openModal").click(function () {
    CEPDI.app.limpiarFormulario("form-usuario");
    $("#exampleModal").modal("show");
  });

  $("#btn-guardar-usuario").click(function () {
    var idUsuario = $("#txt-id-usuario").val();
    var usuario = {
      id: CEPDI.app.isStringNullOrEmpty(idUsuario) ? null : idUsuario,
      nombre: $("#txt-nombre-usuario").val(),
      usuario: $("#txt-nombreusuario-usuario").val(),
      password: $("#txt-password-usuario").val(),
      acciones: $("#txt-acciones-usuario").val(),
    };

    var tipo = usuario.id !== null ? "PUT" : "POST";
    var metodo = usuario.id !== null ? "actualizar" : "agregar";

    if (usuario.id !== null) usuario.status = true;

    var apiUrl = `${urlBase}/Usuario/${metodo}`;

    CEPDI.app.CallApiAjax(apiUrl, usuario, tipo, function (response) {
      alert(response.message);
      console.log("response => ", response);
      CEPDI.app.HideDialogModal("exampleModal");
      if (response.isSuccess) {
        this.tablaUsuarios.ajax.reload();
        CEPDI.app.limpiarFormulario("form-usuario");
      } else {
        alert(response.message);
      }
    });
  });

  $("#tabla-reporte-usuarios tbody").on("click", ".btn-edit", function () {
    var $row = $(this).closest("tr");
    var data = $("#tabla-reporte-usuarios").DataTable().row($row).data();

    console.log("data", data);
    console.log("Record ID is", data["id"]);

    // Add your code here
    getUserById(data.id, function (response) {
      $("#txt-id-usuario").val(response.data.id);
      $("#txt-nombre-usuario").val(response.data.nombre);
      $("#txt-nombreusuario-usuario").val(response.data.usuario);
      $("#txt-acciones-usuario").val(response.data.acciones);
      $("#txt-password-usuario").val(response.data.password);
      //$("exampleModal").
      CEPDI.app.OpenDialogModal("exampleModal");
    });
  });
  $("#tabla-reporte-usuarios tbody").on("click", ".btn-delete", function () {
    var $row = $(this).closest("tr");
    var data = $("#tabla-reporte-usuarios").DataTable().row($row).data();

    console.log("data", data);
    console.log("Record ID is", data["id"]);

    CEPDI.app.QuestionModal(
      "¿Estas seguro de eliminar este registro?",
      function (isConfirmed) {
        if (isConfirmed) {
          var url = urlEliminarUsuario + data["id"];

          CEPDI.app.CallApiAjax(url, null, "DELETE", function (response) {
            console.log("eliminar => ", response);
            if (response.isSuccess) {
              this.tablaUsuarios.ajax.reload();
              Swal.fire({
                title: "Operación exitosa!",
                text: response.data.message,
                icon: "success",
              });
            }
          });
        }
      }
    );
  });
});

function actividadesUsuario() {
  $(".modal-title").text("Nuevo Usuario");
  $("#btn-openModal-usuario").show();
  $("#btn-guardar-usuario").show();
  $("#form-usuario").show();

  
  $("#form-medicamentos").hide();
  $("#btn-openModal-medicamento").hide();
  $("#btn-guardar-medicamento").hide();

  $("#tltle-element").text("USUARIO");
  $("#card-body-medicamento").hide();

  $("#card-body-usaurio").show();

  var botonesAcciones =
    "<div class='btn-group' role='group' aria-label='Basic example'>";
  botonesAcciones +=
    "<button type='button' class='btn btn-light btn-edit'><i class='glyphicon glyphicon-pencil'></i></button>";
  botonesAcciones +=
    "<button type='button' class='btn btn-light btn-delete'><i class='glyphicon glyphicon-trash'></i></button>";
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
    `${urlBase}/Usuario/todos`
  );
}

function editarUsuario(id) {
  alert("Editando usuario");
}

function actividadesMedicamento() {
  $("#btn-openModal-usuario").hide();  
  $("#form-usuario").hide();
  $("#btn-guardar-usuario").hide();

  $(".modal-title").text("Nuevo Medicamento");

  
  $("#card-body-usaurio").hide();
  $("#form-medicamentos").show();
  $("#btn-guardar-medicamento").show();
  $("#btn-openModal-medicamento").show();

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
    urlMedicamentosTdos
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
  $("#li-username").text(user.data.nombre);
}

function getUserById(id, functionBack) {
  var url = CEPDI.app.urlAppHost() + `api/Usuario/porId?id=${id}`;
  console.log("CLX => ", url);
  CEPDI.app.CallApiAjax(url, null, "GET", function (response) {
    if (response != null) {
      if (response.data !== null) {
        functionBack(response);
      } else {
        CEPDI.app.MessageBox(user.message);
      }
    }
  });
}
