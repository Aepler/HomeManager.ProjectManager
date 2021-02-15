$(document).ready(function () {
    List();
});

//==============================================================================
// DataTables ==================================================================
//==============================================================================

function List() {
    var table = $("#tblCategoryCustomize").DataTable({
        "processing": true,
        "serverSide": true,
        "filter": true,
        "orderMulti": true,
        "destroy": true,
        "order": [[0, "ASC"]],
        "ajax": {
            "url": '/Finance/Customize/GetCategoryTableData',
            "type": "POST",
            "datatype": "json"
        },
        "rowId": 'id',
        "columns": [
            { "data": "name" }
            , {
                "orderable": false,
                "data": null,
                "defaultContent": "<button class='buttonEditCategoryCustomize btn btn-outline-secondary' data-bs-toggle='modal' data-bs-target='#modalEditCategoryCustomize'>Edit</button>" +
                    " | " +
                    "<button class='buttonDeleteCategoryCustomize btn btn-outline-danger' data-bs-toggle='modal' data-bs-target='#modalDeleteCategoryCustomize'>Delete</button>",
                "width": "150px"
            }
        ]
    });

    $('#tblCategoryCustomize tbody').on('click', '.buttonDeleteCategoryCustomize', function () {
        var id = $(this).parent().parent().attr("id");
        if (id != null) {
            $('#buttonModalDeleteCategoryCustomize').val(id);
        }
    });

    $('#modalFooterDeleteCategoryCustomize').on('click', '#buttonModalDeleteCategoryCustomize', function () {
        var id = $(this).val();
        if (id != null) {
            DeleteCategoryPost(id, table);
        }
    });

    $('#modalFooterEditCategoryCustomize').on('click', '#buttonModalEditCategoryCustomize', function () {
        var id = $(this).val();
        if (id != null) {
            EditCategoryPost(id, table);
        }
    });

    $('#modalFooterCreateCategoryCustomize').on('click', '#buttonModalCreateCategoryCustomize', function () {
        CreateCategoryPost(table);
    });

    $('#tblCategoryCustomize tbody').on('click', '.buttonEditCategoryCustomize', function () {
        var id = $(this).parent().parent().attr("id");
        if (id != "") {
            $('#buttonModalEditCategoryCustomize').val(id);
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
        url: "/Finance/Customize/GetCategory/" + id,
        success: function (data) {
            $('#inputNameEditCategoryCustomize').val(data.name);
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
        url: "/Finance/Customize/CreateCategory/",
        data: $('#formCreateCategoryCustomize').serialize(),
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
        url: "/Finance/Customize/EditCategory/" + id,
        data: $('#formEditCategoryCustomize').serialize(),
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
        url: "/Finance/Customize/DeleteCategory/" + id,
        data: $('#formDeleteCategoryCustomize').serialize(),
        success: function () {
            table.draw(false);
        },
        error: function (xhr, ajaxOptions, thrownError) {
            alert('Failed to delete Category.');
        }
    });
};