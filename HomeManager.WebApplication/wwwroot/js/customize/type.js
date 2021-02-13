$(document).ready(function () {

});

//==============================================================================
// DataTables ==================================================================
//==============================================================================

function List() {
    var table = $("#tblTypeCustomize").DataTable({
        "processing": true,
        "serverSide": true,
        "filter": true,
        "orderMulti": true,
        "destroy": true,
        "order": [[0, "ASC"]],
        "ajax": {
            "url": '/Customize/GetTypeTableData',
            "type": "POST",
            "datatype": "json"
        },
        "rowId": 'id',
        "columns": [
            { "data": "date" }
            , { "data": "description" }
            , { "data": "type" }
            , { "data": "category" }
            , { "data": "amount" }
        ]
    });

    $('#tblTypeCustomize tbody').on('click', '.buttonDeleteTypeCustomize', function () {
        var id = $(this).val();
        if (id != null) {
            $('#buttonModalDeleteTypeCustomize').val(id);
        }
    });

    $('#modalFooterDeleteTypeCustomize').on('click', '#buttonModalDeleteTypeCustomize', function () {
        var id = $(this).val();
        if (id != null) {
            DeleteTypePost(id, table);
        }
    });

    $('#modalFooterEditTypeCustomize').on('click', '#buttonModalEditTypeCustomize', function () {
        var id = $(this).val();
        if (id != null) {
            EditTypePost(id, table);
        }
    });

    $('#modalFooterCreateTypeCustomize').on('click', '#buttonModalCreateTypeCustomize', function () {
        CreateTypePost(table);
    });

    $('#tblTypeCustomize tbody').on('click', '.buttonEditTypeCustomize', function () {
        var id = $(this).val();
        if (id != "") {
            $('#buttonModalEditTypeCustomize').val(id);
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

function CreateTypePost(id, table) {
    $.ajax({
        cache: false,
        type: "Post",
        url: "Customize/CreateType/" + id,
        data: $('#formCreateType').serialize(),
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
        url: "Customize/EditType/" + id,
        data: $('#formEditType').serialize(),
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
        url: "Customize/DeleteType/" + id,
        data: $('#formDeleteType').serialize(),
        success: function () {
            table.draw(false);
        },
        error: function (xhr, ajaxOptions, thrownError) {
            alert('Failed to delete Type.');
        }
    });
};