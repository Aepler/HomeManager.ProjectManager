$(document).ready(function () {
    List();
});

//==============================================================================
// DataTables ==================================================================
//==============================================================================

function List() {
    var table = $("#tblTypeAdmin").DataTable({
        "processing": true,
        "serverSide": true,
        "filter": true,
        "orderMulti": true,
        "destroy": true,
        "order": [[0, "ASC"]],
        "ajax": {
            "url": '/Admin/Finance/GetTypeTableData',
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
                "defaultContent": "<button class='buttonEditTypeAdmin btn btn-outline-secondary' data-bs-toggle='modal' data-bs-target='#modalEditTypeAdmin'>Edit</button>" +
                    " | " +
                    "<button class='buttonDeleteTypeAdmin btn btn-outline-danger' data-bs-toggle='modal' data-bs-target='#modalLabelDeleteTypeAdmin'>Delete</button>",
                "width": "150px"
            }
        ]
    });

    $('#tblTypeAdmin tbody').on('click', '.buttonDeleteTypeAdmin', function () {
        var id = $(this).parent().parent().attr("id");
        if (id != null) {
            $('#buttonModalDeleteTypeAdmin').val(id);
        }
    });

    $('#modalFooterDeleteTypeAdmin').on('click', '#buttonModalDeleteTypeAdmin', function () {
        var id = $(this).val();
        if (id != null) {
            DeleteTypePost(id, table);
        }
    });

    $('#modalFooterEditTypeAdmin').on('click', '#buttonModalEditTypeAdmin', function () {
        var id = $(this).val();
        if (id != null) {
            EditTypePost(id, table);
        }
    });

    $('#modalFooterCreateTypeAdmin').on('click', '#buttonModalCreateTypeAdmin', function () {
        CreateTypePost(table);
    });

    $('#tblTypeAdmin tbody').on('click', '.buttonEditTypeAdmin', function () {
        var id = $(this).parent().parent().attr("id");
        if (id != "") {
            $('#buttonModalEditTypeAdmin').val(id);
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
        url: "/Admin/Finance/GetType/" + id,
        success: function (data) {
            $('#inputNameEditTypeAdmin').val(data.name);
            $('#inputEndTaxTypeEditTypeAdmin').val(data.endTaxType);
            $('#inputDebitEditTypeAdmin').attr("checked", data.debit);
            $('#inputExtraInputEditTypeAdmin').val(data.extraInput);
            $('#dropdownStatusEditTypeAdmin').val(data.fk_StatusId);
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
        url: "/Admin/Finance/CreateType",
        data: $('#formCreateTypeAdmin').serialize(),
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
        url: "/Admin/Finance/EditType/" + id,
        data: $('#formEditTypeAdmin').serialize(),
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
        url: "/Admin/Finance/DeleteType/" + id,
        data: $('#formDeleteTypeAdmin').serialize(),
        success: function () {
            table.draw(false);
        },
        error: function (xhr, ajaxOptions, thrownError) {
            alert('Failed to delete Type.');
        }
    });
};