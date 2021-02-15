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
                "orderable": false,
                "data": null,
                "defaultContent": "<button class='buttonEditStatusAdmin btn btn-outline-secondary' data-bs-toggle='modal' data-bs-target='#modalEditStatusAdmin'>Edit</button>" +
                    " | " +
                    "<button class='buttonDeleteStatusAdmin btn btn-outline-danger' data-bs-toggle='modal' data-bs-target='#modalDeleteStatusAdmin'>Delete</button>",
                "width": "150px"
            }
        ]
    });

    $('#tblStatusAdmin tbody').on('click', '.buttonDeleteStatusAdmin', function () {
        var id = $(this).parent().parent().attr("id");
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
        var id = $(this).parent().parent().attr("id");
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

function GetStatusEdit(id) {
    $.ajax({
        cache: false,
        type: "GET",
        url: "/Admin/GetStatus/" + id,
        success: function (data) {
            $('#inputNameEditStatusAdmin').val(data.name);
            $('#inputEndPointEditStatusAdmin').attr("checked", data.endPoint);
        },
        error: function (xhr, ajaxOptions, thrownError) {
            alert('Failed to retrieve status.');
        }
    });
};

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