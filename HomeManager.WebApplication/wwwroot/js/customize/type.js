$(document).ready(function () {
    List();
});

//==============================================================================
// DataTables ==================================================================
//==============================================================================

function List() {
    var table = $("#tblTypeCustomize").DataTable({
        "processing": true,
        "serverSide": true,
        "filter": true,
        "orderMulti": true,
        "destroy": true,
        "order": [[0, "ASC"]],
        "ajax": {
            "url": '/Finance/Customize/GetTypeTableData',
            "type": "POST",
            "datatype": "json"
        },
        "rowId": 'id',
        "columns": [
            { "data": "name" }
            , { "data": "endTaxType" }
            , { "data": "debit" }
            , { "data": "extraInput" }
            , { "data": "status" }
            , {
                "orderable": false,
                "data": null,
                "defaultContent": "<button class='buttonEditTypeCustomize btn btn-outline-secondary' data-bs-toggle='modal' data-bs-target='#modalEditTypeCustomize'>Edit</button>" +
                    " | " +
                    "<button class='buttonDeleteTypeCustomize btn btn-outline-danger' data-bs-toggle='modal' data-bs-target='#modalLabelDeleteTypeCustomize'>Delete</button>",
                "width": "150px"
            }
        ]
    });

    $('#tblTypeCustomize tbody').on('click', '.buttonDeleteTypeCustomize', function () {
        var id = $(this).parent().parent().attr("id");
        if (id != null) {
            $('#buttonModalDeleteTypeCustomize').val(id);
        }
    });

    $('#modalFooterDeleteTypeCustomize').on('click', '#buttonModalDeleteTypeCustomize', function () {
        var id = $(this).val();
        if (id != null) {
            DeleteTypePost(id, table);
        }
    });

    $('#modalFooterEditTypeCustomize').on('click', '#buttonModalEditTypeCustomize', function () {
        var id = $(this).val();
        if (id != null) {
            EditTypePost(id, table);
        }
    });

    $('#modalFooterCreateTypeCustomize').on('click', '#buttonModalCreateTypeCustomize', function () {
        CreateTypePost(table);
    });

    $('#tblTypeCustomize tbody').on('click', '.buttonEditTypeCustomize', function () {
        var id = $(this).parent().parent().attr("id");
        if (id != "") {
            $('#buttonModalEditTypeCustomize').val(id);
            GetTypeEdit(id);
        }
    });
}

//==============================================================================
// Create Entry ================================================================
//==============================================================================

//==============================================================================
// Edit Entry ==================================================================
//==============================================================================

//==============================================================================
// Ajax ========================================================================
//==============================================================================

function GetTypeEdit(id) {
    $.ajax({
        cache: false,
        type: "GET",
        url: "/Finance/Customize/GetType/" + id,
        success: function (data) {
            $('#inputNameEditTypeCustomize').val(data.name);
            $('#inputEndTaxTypeEditTypeCustomize').val(data.endTaxType);
            $('#inputDebitEditTypeCustomize').attr("checked", data.debit);
            $('#inputExtraInputEditTypeCustomize').val(data.extraInput);
            $('#dropdownStatusEditTypeCustomize').val(data.fk_StatusId);
        },
        error: function (xhr, ajaxOptions, thrownError) {
            alert('Failed to retrieve type.');
        }
    });
};

function CreateTypePost(table) {
    $.ajax({
        cache: false,
        type: "Post",
        url: "/Finance/Customize/CreateType/",
        data: $('#formCreateTypeCustomize').serialize(),
        success: function () {
            table.draw(false);
        },
        error: function (xhr, ajaxOptions, thrownError) {
            alert('Failed to edit Type.');
        }
    });
};

function EditTypePost(id, table) {
    $.ajax({
        cache: false,
        type: "Post",
        url: "/Finance/Customize/EditType/" + id,
        data: $('#formEditTypeCustomize').serialize(),
        success: function () {
            table.draw(false);
        },
        error: function (xhr, ajaxOptions, thrownError) {
            alert('Failed to edit Type.');
        }
    });
};

function DeleteTypePost(id, table) {
    $.ajax({
        cache: false,
        type: "Post",
        url: "/Finance/Customize/DeleteType/" + id,
        data: $('#formDeleteTypeCustomize').serialize(),
        success: function () {
            table.draw(false);
        },
        error: function (xhr, ajaxOptions, thrownError) {
            alert('Failed to delete Type.');
        }
    });
};