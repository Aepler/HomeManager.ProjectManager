$(document).ready(function () {
   List();
});

//==============================================================================
// DataTables ==================================================================
//==============================================================================

function List() {
    var table = $("#tblUserAdmin").DataTable({
        "processing": true,
        "serverSide": true,
        "filter": true,
        "orderMulti": true,
        "destroy": true,
        "order": [[0, "ASC"]],
        "ajax": {
            "url": '/Admin/GetUserTableData',
            "type": "POST",
            "datatype": "json"
        },
        "rowId": 'id',
        "columns": [
            { "data": "userName" }
            , { "data": "email" }
            , { "data": "name" }
            , { "data": "lastname" }
            , { "data": "phoneNumber" }
            , { "data": "twoFactorEnabled" }
            , {
                "className": 'details-control',
                "orderable": false,
                "data": "buttons",
                "width": "150px"
            }
        ]
    });

    $('#tblUserAdmin tbody').on('click', '.buttonDeleteUserAdmin', function () {
        var id = $(this).val();
        if (id != null) {
            $('#buttonModalDeleteUserAdmin').val(id);
        }
    });

    $('#modalFooterDeleteUserAdmin').on('click', '#buttonModalDeleteUserAdmin', function () {
        var id = $(this).val();
        if (id != null) {
            DeleteUserPost(id, table);
        }
    });

    $('#tblUserAdmin tbody').on('click', '.buttonEditUserAdmin', function () {
        var id = $(this).val();
        if (id != "") {
            $('#buttonModalEditUserAdmin').val(id);
            GetUserEdit(id);
        }
    });

    $('#modalFooterEditUserAdmin').on('click', '#buttonModalEditUserAdmin', function () {
        var id = $(this).val();
        if (id != null) {
            EditUserPost(id, table);
        }
    });

    $('#modalFooterCreateUserAdmin').on('click', '#buttonModalCreateUserAdmin', function () {
        CreateUserPost(table);
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

function CreateUserPost(table) {
    $.ajax({
        cache: false,
        type: "Post",
        url: "/Admin/CreateUser",
        data: $('#formCreateUserAdmin').serialize(),
        success: function () {
            table.draw(false);
        },
        error: function (xhr, ajaxOptions, thrownError) {
            alert('Failed to edit User.');
        }
    });
};

function EditUserPost(id, table) {
    $.ajax({
        cache: false,
        type: "Post",
        url: "/Admin/EditUser/" + id,
        data: $('#formEditUserAdmin').serialize(),
        success: function () {
            table.draw(false);
        },
        error: function (xhr, ajaxOptions, thrownError) {
            alert('Failed to edit User.');
        }
    });
};

function DeleteUserPost(id, table) {
    $.ajax({
        cache: false,
        type: "Post",
        url: "/Admin/DeleteUser/" + id,
        data: $('#formDeleteUserAdmin').serialize(),
        success: function () {
            table.draw(false);
        },
        error: function (xhr, ajaxOptions, thrownError) {
            alert('Failed to delete User.');
        }
    });
};