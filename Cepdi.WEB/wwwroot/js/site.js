const tablaUsuarios = null;
const tablaMedicamentos = null;

//URL Usuarios
const urlBase = "https://localhost:7058/api";
const urlEliminarUsuario = `${urlBase}/Usuario/eliminar?id=`;

//URL Medicamentos
const urlMedicamentosTdos = `${urlBase}/Medicamento/todos`;
const urlObtenerMedicamentoPorId = `${urlBase}/Medicamento/porId?id=`;
const urlMedicamentosAgregar = `${urlBase}/Medicamento/agregar`;
const urlMedicamentosActualizar = `${urlBase}/Medicamento/actualizar`;
const urlMedicamentosEliminar = `${urlBase}/Medicamento/eliminar?id=`;

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


  //Boton para guardar los datos de usuarios
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
      CEPDI.app.HideDialogModal("exampleModal");
      if (response.isSuccess) {
        this.tablaUsuarios.ajax.reload();
        CEPDI.app.limpiarFormulario("form-usuario");
        CEPDI.app.SuccessAlert('Bien hecho!', response.message );
      } else {
        CEPDI.app.ErrorAlert('¡Alto!', response.message );
      }
    });
  });

  //Botón para guardar los datos de Medicamentos
  $("#btn-guardar-medicamento").click(function () {
    var idMedicamento = $("#txt-id-medicamento").val();
    var medicamento = {
      idmedicamento: CEPDI.app.isStringNullOrEmpty(idMedicamento) ? null : idMedicamento,
      nombre: $("#txt-nombre-medicamento").val(),
      concentracion: $("#txt-concentracion-medicamento").val(),
      precio: $("#txt-precio-medicamento").val(),
      presentacion: $("#txt-presentacion-medicamento").val(),
      acciones: $("#txt-acciones-medicamento").val(),
    };

    console.log('medicamento => ', medicamento);

    var tipo = medicamento.idmedicamento !== null ? "PUT" : "POST";
    var url = medicamento.idmedicamento !== null ? urlMedicamentosActualizar : urlMedicamentosAgregar;

    //if (usuario.id !== null) usuario.status = true;

    CEPDI.app.CallApiAjax(url, medicamento, tipo, function (response) {
      CEPDI.app.HideDialogModal("exampleModal");
      if (response.isSuccess) {
        this.tablaMedicamentos.ajax.reload();
        CEPDI.app.limpiarFormulario("form-usuario");
        CEPDI.app.SuccessAlert('Bien hecho!', response.message );
      } else {
        CEPDI.app.ErrorAlert('¡Alto!', response.message );
      }
    });

  });

  //Editar y eliminar usuario
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

  //Editar y eliminar medicamentos
  $("#tabla-reporte-medicamentos tbody").on("click", ".btn-edit-medicamento", function () {
    var $row = $(this).closest("tr");
    var data = $("#tabla-reporte-medicamentos").DataTable().row($row).data();

    // console.log("data", data);
    // console.log("Record ID is", data["idmedicamento"]);

    getMedicamentoById(data.idmedicamento, function (response) {
      //console.log('response => ', response.data.precio);
      $("#txt-id-medicamento").val(response.data.idmedicamento);
      $("#txt-nombre-medicamento").val(response.data.nombre);
      $("#txt-concentracion-medicamento").val(response.data.concentracion);
      $("#txt-precio-medicamento").val(response.data.precio);
      $("#txt-presentacion-medicamento").val(response.data.presentacion);
      $("#txt-acciones-medicamento").val(response.data.acciones);
      //$("exampleModal").
      CEPDI.app.OpenDialogModal("exampleModal");
    });
  });
  $("#tabla-reporte-medicamentos tbody").on("click", ".btn-delete-medicamento", function () {
    var $row = $(this).closest("tr");
    var data = $("#tabla-reporte-medicamentos").DataTable().row($row).data();

    console.log("data", data);
    // console.log("Record ID is", data["id"]);

    CEPDI.app.QuestionModal(
      "¿Estas seguro de eliminar este registro?",
      function (isConfirmed) {
        if (isConfirmed) {
          var url = urlMedicamentosEliminar + data.idmedicamento;
          console.log("Eliminar URL =>", url);

          CEPDI.app.CallApiAjax(url, null, "DELETE", function (response) {
            console.log("eliminar => ", response);
            if (response.isSuccess) {
              this.tablaMedicamentos.ajax.reload();
              Swal.fire({
                title: "Operación exitosa!",
                text: response.message,
                icon: "success",
              });
            }
          });
        }
      }
    );
  });
});

//Manejo de componponentes de usuarios
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

//Manejo de componponentes de Medicamento
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
    "<button type='button' class='btn btn-light btn-edit-medicamento'><i class='glyphicon glyphicon-pencil'></i></button>";
  botonesAcciones +=
    "<button type='button' class='btn btn-light btn-delete-medicamento'><i class='glyphicon glyphicon-trash'></i></button>";
  botonesAcciones += "</div>";

  $("#tltle-element").text("MEDICAMENTO");
  $("#card-body-usaurio").hide();
  $("#card-body-medicamento").show();

  this.tablaMedicamentos = CEPDI.app.startDatatable(
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


//Operaciones con el local storage
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

//Obtiene los datos de un usuario, llamada API
function getUserById(id, functionBack) {
  var url = CEPDI.app.urlAppHost() + `api/Usuario/porId?id=${id}`;
  CEPDI.app.CallApiAjax(url, null, "GET", function (response) {
    if (response != null) {
      if (response.data !== null) {
        functionBack(response);
      } else {
        CEPDI.app.WarningAlert(response.message);
      }
    }
  });
}

function getMedicamentoById(id, functionBack) {
  var url = urlObtenerMedicamentoPorId + id;
  CEPDI.app.CallApiAjax(url, null, "GET", function (response) {
    if (response != null) {
      if (response.data !== null) {
        functionBack(response);
      } else {
        CEPDI.app.WarningAlert(response.message);
      }
    }
  });
}