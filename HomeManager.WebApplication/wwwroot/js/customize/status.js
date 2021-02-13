$(document).ready(function () {
    List();
});

//==============================================================================
// DataTables ==================================================================
//==============================================================================

function List() {
    var table = $("#tblStatusCustomize").DataTable({
        "processing": true,
        "serverSide": true,
        "filter": true,
        "orderMulti": true,
        "destroy": true,
        "order": [[0, "ASC"]],
        "ajax": {
            "url": '/Finance/Customize/GetStatusTableData',
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

    $('#tblStatusCustomize tbody').on('click', '.buttonDeleteStatusCustomize', function () {
        var id = $(this).val();
        if (id != null) {
            $('#buttonModalDeleteStatusCustomize').val(id);
        }
    });

    $('#modalFooterDeleteStatusCustomize').on('click', '#buttonModalDeleteStatusCustomize', function () {
        var id = $(this).val();
        if (id != null) {
            DeleteStatusPost(id, table);
        }
    });

    $('#modalFooterEditStatusCustomize').on('click', '#buttonModalEditStatusCustomize', function () {
        var id = $(this).val();
        if (id != null) {
            EditStatusPost(id, table);
        }
    });

    $('#modalFooterCreateStatusCustomize').on('click', '#buttonModalCreateStatusCustomize', function () {
        CreateStatusPost(table);
    });

    $('#tblStatusCustomize tbody').on('click', '.buttonEditStatusCustomize', function () {
        var id = $(this).val();
        if (id != "") {
            $('#buttonModalEditStatusCustomize').val(id);
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

function CreateStatusPost(id, table) {
    $.ajax({
        cache: false,
        type: "Post",
        url: "Customize/CreateStatus/" + id,
        data: $('#formCreateStatus').serialize(),
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
        url: "Customize/EditStatus/" + id,
        data: $('#formEditStatus').serialize(),
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
        url: "Customize/DeleteStatus/" + id,
        data: $('#formDeleteStatus').serialize(),
        success: function () {
            table.draw(false);
        },
        error: function (xhr, ajaxOptions, thrownError) {
            alert('Failed to delete Status.');
        }
    });
};