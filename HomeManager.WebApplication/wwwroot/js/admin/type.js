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
            "url": '/Admin/GetTypeTableData',
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
                "className": 'details-control',
                "orderable": false,
                "data": "buttons",
                "width": "150px"
            }
        ]
    });

    $('#tblTypeAdmin tbody').on('click', '.buttonDeleteTypeAdmin', function () {
        var id = $(this).val();
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
        var id = $(this).val();
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

function CreateTypePost(table) {
    $.ajax({
        cache: false,
        type: "Post",
        url: "/Admin/CreateType",
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
        url: "/Admin/EditType/" + id,
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
        url: "/Admin/DeleteType/" + id,
        data: $('#formDeleteTypeAdmin').serialize(),
        success: function () {
            table.draw(false);
        },
        error: function (xhr, ajaxOptions, thrownError) {
            alert('Failed to delete Type.');
        }
    });
};