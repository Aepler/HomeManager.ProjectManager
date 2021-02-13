$(document).ready(function () {
    List();
});

//==============================================================================
// DataTables ==================================================================
//==============================================================================

function List() {
    var table = $("#tblUserRoleAdmin").DataTable({
        "processing": true,
        "serverSide": true,
        "filter": true,
        "orderMulti": true,
        "destroy": true,
        "order": [[0, "ASC"]],
        "ajax": {
            "url": '/Admin/GetUserRoleTableData',
            "type": "POST",
            "datatype": "json"
        },
        "columns": [
            { "data": "user" }
            , { "data": "role" }
            , {
                "className": 'details-control',
                "orderable": false,
                "data": "buttons",
                "width": "75px"
            }
        ]
    });

    $('#tblUserRoleAdmin tbody').on('click', '.buttonDeleteUserRoleAdmin', function () {
        var id = $(this).val();
        var role = $(this).attr("role");
        if (id != null) {
            $('#inputUserIdDeleteRoleAdmin').val(id);
            $('#inputRoleDeleteRoleAdmin').val(role);
        }
    });

    $('#modalFooterDeleteUserRoleAdmin').on('click', '#buttonModalDeleteUserRoleAdmin', function () {
        DeleteUserRolePost(table);
    });
    $('#modalFooterCreateUserRoleAdmin').on('click', '#buttonModalCreateUserRoleAdmin', function () {
        CreateUserRolePost(table);
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

function CreateUserRolePost(table) {
    $.ajax({
        cache: false,
        type: "Post",
        url: "/Admin/CreateUserRole",
        data: $('#formCreateUserRoleAdmin').serialize(),
        success: function () {
            table.draw(false);
        },
        error: function (xhr, ajaxOptions, thrownError) {
            alert('Failed to edit UserRole.');
        }
    });
};

function DeleteUserRolePost(table) {
    $.ajax({
        cache: false,
        type: "Post",
        url: "/Admin/DeleteUserRole",
        data: $('#formDeleteUserRoleAdmin').serialize(),
        success: function () {
            table.draw(false);
        },
        error: function (xhr, ajaxOptions, thrownError) {
            alert('Failed to delete UserRole.');
        }
    });
};