var CEPDI = window.CEPDI || {};

CEPDI.app = (function ($, window, document, undefined) {
  var MessageBox = function (texto) {
    alert(texto);
  };

  // var InitDateTimeID = function (id) {
  //     var existe = $("#" + id).attr("class").indexOf("Fechax");

  //     if (parseInt(existe) < 0) {
  //         $("#" + id).inputmask("dd/mm/yyyy", { "placeholder": "dd/mm/yyyy" });
  //         $('#' + id).datepicker({

  //             todayBtn: "linked",
  //             language: "es",
  //             autoclose: true,
  //             todayHighlight: true,
  //             format: 'dd/mm/yyyy'
  //         });
  //         $(".CampoFecha").datepicker("setDate", new Date());
  //         $(".CampoFecha").addClass("Fechax");
  //     }
  // }

  var InitDialogModal = function (idModal) {
    $("#" + idModal).modal("hide");
  };

  var OpenDialogModal = function (idModal) {
    $("#" + idModal).modal("show");
  };

  var HideDialogModal = function (idModal) {
    $("#" + idModal).modal("hide");
  };

  var InitDataTable = function (idDataTable) {
    return $("#" + idDataTable).DataTable({
      scrollY: "300px",
      scrollX: true,
      scrollCollapse: true,
      language: {
        lengthMenu: "Muestra _MENU_ registros por pagina",
        zeroRecords: "No se encontraron registros",
        info: "Mostrando pagina _PAGE_ de _PAGES_",
        infoEmpty: "No hay registros disponibles",
        infoFiltered: "(filtrado de un total de _MAX_ registros)",
        processing: "Procesando...",
        loadingRecords: "Cargando...",
        search: "Buscar:",
        paginate: {
          first: "Primera",
          last: "Ultima",
          next: '<i class="fa fa-fw fa-angle-right fa-3">',
          previous: '<i class="fa fa-fw fa-angle-left fa-3">',
        },
      },
    });
  };

  var startDatatable = function (idDataTable, columns, apiUrl) {
    return $("#" + idDataTable).DataTable({
      retrieve: true,
      processing: true,
      responsive: true,
      scrollY: "300px",
      scrollX: true,
      scrollCollapse: true,
      language: {
        lengthMenu: "Muestra _MENU_ registros por pagina",
        zeroRecords: "No se encontraron registros",
        info: "Mostrando pagina _PAGE_ de _PAGES_",
        infoEmpty: "No hay registros disponibles",
        infoFiltered: "(filtrado de un total de _MAX_ registros)",
        processing: "Procesando...",
        loadingRecords: "Cargando...",
        search: "Buscar:",
        paginate: {
          first: "Primera",
          last: "Ultima",
          next: '<i class="fa fa-fw fa-angle-right fa-3">',
          previous: '<i class="fa fa-fw fa-angle-left fa-3">',
        },
      },
      ajax: {
        url: apiUrl,
        type: "GET",
        datatype: "json",
      },
      columns: columns,
      columnDefs: [
        {
          target: 0,
          visible: false,
        },
      ],
      initComplete: function () {
        this.api()
          .columns()
          .every(function () {
            let column = this;
            let title = column.footer().textContent;

            // Create input element
            let input = document.createElement("input");
            input.placeholder = title;
            column.footer().replaceChildren(input);

            // Event listener for user input
            input.addEventListener("keyup", () => {
              if (column.search() !== this.value) {
                column.search(input.value).draw();
              }
            });
          });
      },
      fixedHeader: {
        footer: true,
      },
    });
  };

  var CallApiAjax = function (apiUrl, data, type, functionBack) {
    $.ajax({
      url: apiUrl,
      type: type,
      data: JSON.stringify(data),
      async: false,
      contentType: "application/json",
      datatype: "JSON",
      success: function (response) {
        functionBack(response);
      },
      error: function (xhr, ajaxOptions, thrownError) {
        alert(
          "Ocurrio un error  generado " + thrownError + " - " + xhr.responseText
        );
        console.log("ajaxOptions => ", ajaxOptions);
        console.log("xhr.responseText => ", xhr.responseText);
        console.log("thrownError => ", thrownError);
        //UnBlockUI();
      },
    });
  };

  var GoToMethod = function (action, data) {
    $.ajax({
      url: action,
      type: "POST",
      data: data,
      async: false,
      contentType: "application/json",
      datatype: "JSON",
      error: function (xhr, ajaxOptions, thrownError) {
        alert("Ocurrio un error  generado " + action);
      },
    });
  };

  /**
   * Descripcion: Metodo que regresa el url del host.
   */
  var urlAppHost = function (numero) {
    url_site = document.location.href;

    url_site = url_site.replace("5255", "7058");
    url_site = url_site.replace("http", "https");
    url_site = url_site.replace("#", "");

    console.log("url_site => ", url_site);

    return url_site;
  };

  var BlockUI = function () {
    $.blockUI({
      message: 'Espere un momento.. <img src="../../images/loading.gif" />',
    });
  };

  var CleanString = function (entrada) {
    var limpia;
    limpia = entrada.replace(/\s+/, "");

    return limpia;
  };

  var getObjectsInJson = function (obj, key, val) {
    var newObj = false;
    $.each(obj, function () {
      var testObject = this;
      $.each(testObject, function (k, v) {
        //alert(k);
        if (val == v && k == key) {
          newObj = testObject;
        }
      });
    });

    return newObj;
  };

  var isStringNullOrEmpty = function (val) {
    switch (val) {
      case "":
      case 0:
      case "0":
      case null:
      case false:
      case undefined:
      case typeof this === "undefined":
        return true;
      default:
        return false;
    }
  };

  var TextBoxOnlyNumbers = function () {
    $(".OnlyNumber").keydown(function (e) {
      // Permite: backspace, delete, tab, escape, enter and .
      if (
        $.inArray(e.keyCode, [46, 8, 9, 27, 13, 110]) !== -1 ||
        // PErmite: Ctrl+A, Command+A
        (e.keyCode === 65 && (e.ctrlKey === true || e.metaKey === true)) ||
        // PErmite: home, end, left, right, down, up
        (e.keyCode >= 35 && e.keyCode <= 40)
      ) {
        // no hace nada
        return;
      }
      // si es un numero detiene el key press
      if (
        (e.shiftKey || e.keyCode < 48 || e.keyCode > 57) &&
        (e.keyCode < 96 || e.keyCode > 105)
      ) {
        e.preventDefault();
      }
    });
  };

  var limpiarFormulario = function (formId) {
    $("div." + formId + " div div input[type='text']").each(function () {
      $(this).val("");
    });
    $("div." + formId + " div div input[type='email']").each(function () {
      $(this).val("");
    });
    $("div." + formId + " div div input[type='tel']").each(function () {
      $(this).val("");
    });
    $("div." + formId + " div div textarea").each(function () {
      $(this).val("");
    });
    $("div." + formId + " div div select").each(function () {
      $("div." + formId + " div div #" + this.id).val("-1");
    });
    $("div." + formId + " div div input[type='checkbox']").each(function () {
      $(this).prop("checked", false);
    });
    $("div." + formId + " div div input[type='radio']").each(function () {
      var name = $(this).prop("name");
      $("#" + name + "No").prop("checked", true);
    });
  };

  var QuestionModal = function (title, functionback) {
    Swal.fire({
      title: title,
      showCancelButton: true,
      confirmButtonText: "Si, eliminar",
      cancelButtonText: `Cancelar`,
      icon: "warning",
    }).then((result) => {
      /* Read more about isConfirmed, isDenied below */
      functionback(result.isConfirmed);
    });
   
  };

  return {
    QuestionModal: QuestionModal,
    MessageBox: MessageBox,
    InitDialogModal: InitDialogModal,
    OpenDialogModal: OpenDialogModal,
    HideDialogModal: HideDialogModal,
    InitDataTable: InitDataTable,
    CallApiAjax: CallApiAjax,
    urlAppHost: urlAppHost,
    GoToMethod: GoToMethod,
    BlockUI: BlockUI,
    CleanString: CleanString,
    getObjectsInJson: getObjectsInJson,
    isStringNullOrEmpty: isStringNullOrEmpty,
    TextBoxOnlyNumbers: TextBoxOnlyNumbers,
    startDatatable: startDatatable,
    limpiarFormulario: limpiarFormulario,
  };
})($, window, document, undefined);
