$(document).ready(function () {
    List();
});

//==============================================================================
// DataTables ==================================================================
//==============================================================================

function List() {
    var table = $("#tblRoleAdmin").DataTable({
        "processing": true,
        "serverSide": true,
        "filter": true,
        "orderMulti": true,
        "destroy": true,
        "order": [[0, "ASC"]],
        "ajax": {
            "url": '/Admin/Manage/GetRoleTableData',
            "type": "POST",
            "datatype": "json"
        },
        "rowId": 'id',
        "columns": [
            { "data": "name" }
            , {
                "orderable": false,
                "data": null,
                "defaultContent": "<button class='buttonEditRoleAdmin btn btn-outline-secondary' data-bs-toggle='modal' data-bs-target='#modalEditRoleAdmin'>Edit</button>" +
                    " | " +
                    "<button class='buttonDeleteRoleAdmin btn btn-outline-danger' data-bs-toggle='modal' data-bs-target='#modalDeleteRoleAdmin'>Delete</button>",
                "width": "150px"
            }
        ]
    });

    $('#tblRoleAdmin tbody').on('click', '.buttonDeleteRoleAdmin', function () {
        var id = $(this).parent().parent().attr("id");
        if (id != null) {
            $('#buttonModalDeleteRoleAdmin').val(id);
        }
    });

    $('#modalFooterDeleteRoleAdmin').on('click', '#buttonModalDeleteRoleAdmin', function () {
        var id = $(this).val();
        if (id != null) {
            DeleteRolePost(id, table);
        }
    });

    $('#modalFooterEditRoleAdmin').on('click', '#buttonModalEditRoleAdmin', function () {
        var id = $(this).val();
        if (id != null) {
            EditRolePost(id, table);
        }
    });

    $('#modalFooterCreateRoleAdmin').on('click', '#buttonModalCreateRoleAdmin', function () {
        CreateRolePost(table);
    });

    $('#tblRoleAdmin tbody').on('click', '.buttonEditRoleAdmin', function () {
        var id = $(this).parent().parent().attr("id");
        if (id != "") {
            $('#buttonModalEditRoleAdmin').val(id);
            GetRoleEdit(id);
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

function GetRoleEdit(id) {
    $.ajax({
        cache: false,
        type: "GET",
        url: "/Admin/Manage/GetRole/" + id,
        success: function (data) {
            $('#inputNameEditRoleAdmin').val(data.name);
        },
        error: function (xhr, ajaxOptions, thrownError) {
            alert('Failed to retrieve payment.');
        }
    });
};

function CreateRolePost(table) {
    $.ajax({
        cache: false,
        type: "Post",
        url: "/Admin/Manage/CreateRole",
        data: $('#formCreateRoleAdmin').serialize(),
        success: function () {
            table.draw(false);
        },
        error: function (xhr, ajaxOptions, thrownError) {
            alert('Failed to edit Role.');
        }
    });
};

function EditRolePost(id, table) {
    $.ajax({
        cache: false,
        type: "Post",
        url: "/Admin/Manage/EditRole/" + id,
        data: $('#formEditRoleAdmin').serialize(),
        success: function () {
            table.draw(false);
        },
        error: function (xhr, ajaxOptions, thrownError) {
            alert('Failed to edit Role.');
        }
    });
};

function DeleteRolePost(id, table) {
    console.log($('#formDeleteRoleAdmin').serialize())
    $.ajax({
        cache: false,
        type: "Post",
        url: "/Admin/Manage/DeleteRole/" + id,
        data: $('#formDeleteRoleAdmin').serialize(),
        success: function () {
            table.draw(false);
        },
        error: function (xhr, ajaxOptions, thrownError) {
            alert('Failed to delete Role.');
        }
    });
};