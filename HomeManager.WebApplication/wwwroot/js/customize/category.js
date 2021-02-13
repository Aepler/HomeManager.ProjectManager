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
                "className": 'details-control',
                "orderable": false,
                "data": "buttons",
                "width": "150px"
            }
        ]
    });

    $('#tblCategoryCustomize tbody').on('click', '.buttonDeleteCategoryCustomize', function () {
        var id = $(this).val();
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
        var id = $(this).val();
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

function CreateCategoryPost(id, table) {
    $.ajax({
        cache: false,
        type: "Post",
        url: "Customize/CreateCategory/" + id,
        data: $('#formCreateCategory').serialize(),
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
        url: "Customize/EditCategory/" + id,
        data: $('#formEditCategory').serialize(),
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
        url: "Customize/DeleteCategory/" + id,
        data: $('#formDeleteCategory').serialize(),
        success: function () {
            table.draw(false);
        },
        error: function (xhr, ajaxOptions, thrownError) {
            alert('Failed to delete Category.');
        }
    });
};