$(document).ready(function () {
    List();
});

//==============================================================================
// DataTables ==================================================================
//==============================================================================

function List() {
    var table = $("#tblStatusAdmin").DataTable({
        "processing": true,
        "serverSide": true,
        "filter": true,
        "orderMulti": true,
        "destroy": true,
        "order": [[0, "ASC"]],
        "ajax": {
            "url": '/Admin/GetStatusTableData',
            "type": "POST",
            "datatype": "json"
        },
        "rowId": 'id',
        "columns": [
            { "data": "name" }
            , { "data": "endPoint" }
            , {
                "className": 'details-control',
                "orderable": false,
                "data": "buttons",
                "width": "150px"
            }
        ]
    });

    $('#tblStatusAdmin tbody').on('click', '.buttonDeleteStatusAdmin', function () {
        var id = $(this).val();
        if (id != null) {
            $('#buttonModalDeleteStatusAdmin').val(id);
        }
    });

    $('#modalFooterDeleteStatusAdmin').on('click', '#buttonModalDeleteStatusAdmin', function () {
        var id = $(this).val();
        if (id != null) {
            DeleteStatusPost(id, table);
        }
    });

    $('#modalFooterEditStatusAdmin').on('click', '#buttonModalEditStatusAdmin', function () {
        var id = $(this).val();
        if (id != null) {
            EditStatusPost(id, table);
        }
    });

    $('#modalFooterCreateStatusAdmin').on('click', '#buttonModalCreateStatusAdmin', function () {
        CreateStatusPost(table);
    });

    $('#tblStatusAdmin tbody').on('click', '.buttonEditStatusAdmin', function () {
        var id = $(this).val();
        if (id != "") {
            $('#buttonModalEditStatusAdmin').val(id);
            GetStatusEdit(id);
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

function CreateStatusPost(table) {
    $.ajax({
        cache: false,
        type: "Post",
        url: "/Admin/CreateStatus",
        data: $('#formCreateStatusAdmin').serialize(),
        success: function () {
            table.draw(false);
        },
        error: function (xhr, ajaxOptions, thrownError) {
            alert('Failed to edit Status.');
        }
    });
};

function EditStatusPost(id, table) {
    $.ajax({
        cache: false,
        type: "Post",
        url: "/Admin/EditStatus/" + id,
        data: $('#formEditStatusAdmin').serialize(),
        success: function () {
            table.draw(false);
        },
        error: function (xhr, ajaxOptions, thrownError) {
            alert('Failed to edit Status.');
        }
    });
};

function DeleteStatusPost(id, table) {
    $.ajax({
        cache: false,
        type: "Post",
        url: "/Admin/DeleteStatus/" + id,
        data: $('#formDeleteStatusAdmin').serialize(),
        success: function () {
            table.draw(false);
        },
        error: function (xhr, ajaxOptions, thrownError) {
            alert('Failed to delete Status.');
        }
    });
};