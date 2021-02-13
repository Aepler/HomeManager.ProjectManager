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
            "url": '/Admin/GetRoleTableData',
            "type": "POST",
            "datatype": "json"
        },
        "rowId": 'id',
        "columns": [
            { "data": "name" }
            , {
                "className": 'details-control',
                "orderable": false,
                "data": "buttons",
                "width": "150px"
            }
        ]
    });

    $('#tblRoleAdmin tbody').on('click', '.buttonDeleteRoleAdmin', function () {
        var id = $(this).val();
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
        var id = $(this).val();
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

function CreateRolePost(table) {
    $.ajax({
        cache: false,
        type: "Post",
        url: "/Admin/CreateRole",
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
        url: "/Admin/EditRole/" + id,
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
        url: "/Admin/DeleteRole/" + id,
        data: $('#formDeleteRoleAdmin').serialize(),
        success: function () {
            table.draw(false);
        },
        error: function (xhr, ajaxOptions, thrownError) {
            alert('Failed to delete Role.');
        }
    });
};