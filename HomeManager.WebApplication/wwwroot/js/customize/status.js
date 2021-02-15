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
                "orderable": false,
                "data": null,
                "defaultContent": "<button class='buttonEditStatusCustomize btn btn-outline-secondary' data-bs-toggle='modal' data-bs-target='#modalEditStatusCustomize'>Edit</button>" +
                    " | " +
                    "<button class='buttonDeleteStatusCustomize btn btn-outline-danger' data-bs-toggle='modal' data-bs-target='#modalDeleteStatusCustomize'>Delete</button>",
                "width": "150px"
            }
        ]
    });

    $('#tblStatusCustomize tbody').on('click', '.buttonDeleteStatusCustomize', function () {
        var id = $(this).parent().parent().attr("id");
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
        var id = $(this).parent().parent().attr("id");
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

function GetStatusEdit(id) {
    $.ajax({
        cache: false,
        type: "GET",
        url: "/Finance/Customize/GetStatus/" + id,
        success: function (data) {
            $('#inputNameEditStatusCustomize').val(data.name);
            $('#inputEndPointEditStatusCustomize').attr("checked", data.endPoint);
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
        url: "/Finance/Customize/CreateStatus/",
        data: $('#formCreateStatusCustomize').serialize(),
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
        url: "/Finance/Customize/EditStatus/" + id,
        data: $('#formEditStatusCustomize').serialize(),
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
        url: "/Finance/Customize/DeleteStatus/" + id,
        data: $('#formDeleteStatusCustomize').serialize(),
        success: function () {
            table.draw(false);
        },
        error: function (xhr, ajaxOptions, thrownError) {
            alert('Failed to delete Status.');
        }
    });
};