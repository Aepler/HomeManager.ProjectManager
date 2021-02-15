$(document).ready(function () {
    List();
});

//==============================================================================
// DataTables ==================================================================
//==============================================================================

function List() {
    var table = $("#tblCategoryAdmin").DataTable({
        "processing": true,
        "serverSide": true,
        "filter": true,
        "orderMulti": true,
        "destroy": true,
        "order": [[0, "ASC"]],
        "ajax": {
            "url": '/Admin/GetCategoryTableData',
            "type": "POST",
            "datatype": "json"
        },
        "rowId": 'id',
        "columns": [
            { "data": "name" }
            , {
                "orderable": false,
                "data": null,
                "defaultContent": "<button class='buttonEditCategoryAdmin btn btn-outline-secondary' data-bs-toggle='modal' data-bs-target='#modalEditCategoryAdmin'>Edit</button>" +
                    " | " +
                    "<button class='buttonDeleteCategoryAdmin btn btn-outline-danger' data-bs-toggle='modal' data-bs-target='#modalDeleteCategoryAdmin'>Delete</button>",
                "width": "150px"
            }
        ]
    });

    $('#tblCategoryAdmin tbody').on('click', '.buttonDeleteCategoryAdmin', function () {
        var id = $(this).parent().parent().attr("id");
        if (id != null) {
            $('#buttonModalDeleteCategoryAdmin').val(id);
        }
    });

    $('#modalFooterDeleteCategoryAdmin').on('click', '#buttonModalDeleteCategoryAdmin', function () {
        var id = $(this).val();
        if (id != null) {
            DeleteCategoryPost(id, table);
        }
    });

    $('#modalFooterEditCategoryAdmin').on('click', '#buttonModalEditCategoryAdmin', function () {
        var id = $(this).val();
        if (id != null) {
            EditCategoryPost(id, table);
        }
    });

    $('#modalFooterCreateCategoryAdmin').on('click', '#buttonModalCreateCategoryAdmin', function () {
        CreateCategoryPost(table);
    });

    $('#tblCategoryAdmin tbody').on('click', '.buttonEditCategoryAdmin', function () {
        var id = $(this).parent().parent().attr("id");
        if (id != "") {
            $('#buttonModalEditCategoryAdmin').val(id);
            GetCategoryEdit(id);
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

function GetCategoryEdit(id) {
    $.ajax({
        cache: false,
        type: "GET",
        url: "/Admin/GetCategory/" + id,
        success: function (data) {
            $('#inputNameEditCategoryAdmin').val(data.name);
        },
        error: function (xhr, ajaxOptions, thrownError) {
            alert('Failed to retrieve payment.');
        }
    });
};


function CreateCategoryPost(table) {
    $.ajax({
        cache: false,
        type: "Post",
        url: "/Admin/CreateCategory/",
        data: $('#formCreateCategoryAdmin').serialize(),
        success: function () {
            table.draw(false);
        },
        error: function (xhr, ajaxOptions, thrownError) {
            alert('Failed to edit Category.');
        }
    });
};

function EditCategoryPost(id, table) {
    $.ajax({
        cache: false,
        type: "Post",
        url: "/Admin/EditCategory/" + id,
        data: $('#formEditCategoryAdmin').serialize(),
        success: function () {
            table.draw(false);
        },
        error: function (xhr, ajaxOptions, thrownError) {
            alert('Failed to edit Category.');
        }
    });
};

function DeleteCategoryPost(id, table) {
    $.ajax({
        cache: false,
        type: "Post",
        url: "/Admin/DeleteCategory/" + id,
        data: $('#formDeleteCategoryAdmin').serialize(),
        success: function () {
            table.draw(false);
        },
        error: function (xhr, ajaxOptions, thrownError) {
            alert('Failed to delete Category.');
        }
    });
};