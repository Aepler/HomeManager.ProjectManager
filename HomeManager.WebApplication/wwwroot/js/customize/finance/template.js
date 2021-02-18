$(document).ready(function () {
    List();
});

//==============================================================================
// DataTables ==================================================================
//==============================================================================

function List() {
    var table = $("#tblTemplateCustomize").DataTable({
        "processing": true,
        "serverSide": true,
        "filter": true,
        "orderMulti": true,
        "destroy": true,
        "order": [[0, "ASC"]],
        "ajax": {
            "url": '/Customize/Finance/GetTemplateTableData',
            "type": "POST",
            "datatype": "json"
        },
        "rowId": 'id',
        "columns": [
            { "data": "description" }
            , { "data": "type" }
            , { "data": "category" }
            , { "data": "amount" }
            , {
                "orderable": false,
                "data": null,
                "defaultContent": "<button class='buttonEditTemplateCustomize btn btn-outline-secondary' data-bs-toggle='modal' data-bs-target='#modalEditTemplateCustomize'>Edit</button>" +
                    " | " +
                    "<button class='buttonDeleteTemplateCustomize btn btn-outline-danger' data-bs-toggle='modal' data-bs-target='#modalDeleteTemplateCustomize'>Delete</button>",
                "width": "150px"
            }
        ]
    });

    $('#tblTemplateCustomize tbody').on('click', '.buttonDeleteTemplateCustomize', function () {
        var id = $(this).parent().parent().attr("id");
        if (id != null) {
            $('#buttonModalDeleteTemplateCustomize').val(id);
        }
    });

    $('#modalFooterDeleteTemplateCustomize').on('click', '#buttonModalDeleteTemplateCustomize', function () {
        var id = $(this).val();
        if (id != null) {
            DeleteTemplatePost(id, table);
        }
    });

    $('#modalFooterEditTemplateCustomize').on('click', '#buttonModalEditTemplateCustomize', function () {
        var id = $(this).val();
        if (id != null) {
            EditTemplatePost(id, table);
        }
    });

    $('#modalFooterCreateTemplateCustomize').on('click', '#buttonModalCreateTemplateCustomize', function () {
        CreateTemplatePost(table);
    });

    $('#tblTemplateCustomize tbody').on('click', '.buttonEditTemplateCustomize', function () {
        var id = $(this).parent().parent().attr("id");
        if (id != "") {
            $('#buttonModalEditTemplateCustomize').val(id);
            GetTemplateEdit(id);
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

function GetTemplateEdit(id) {
    $.ajax({
        cache: false,
        type: "GET",
        url: "/Customize/Finance/GetTemplate/" + id,
        success: function (data) {

        },
        error: function (xhr, ajaxOptions, thrownError) {
            alert('Failed to retrieve payment.');
        }
    });
};

function CreateTemplatePost(table) {
    $.ajax({
        cache: false,
        type: "Post",
        url: "/Customize/Finance/CreateTemplate/",
        data: $('#formCreateTemplateCustomize').serialize(),
        success: function () {
            table.draw(false);
        },
        error: function (xhr, ajaxOptions, thrownError) {
            alert('Failed to edit Template.');
        }
    });
};

function EditTemplatePost(id, table) {
    $.ajax({
        cache: false,
        type: "Post",
        url: "/Customize/Finance/EditTemplate/" + id,
        data: $('#formEditTemplateCustomize').serialize(),
        success: function () {
            table.draw(false);
        },
        error: function (xhr, ajaxOptions, thrownError) {
            alert('Failed to edit Template.');
        }
    });
};

function DeleteTemplatePost(id, table) {
    $.ajax({
        cache: false,
        type: "Post",
        url: "/Customize/Finance/DeleteTemplate/" + id,
        data: $('#formDeleteTemplateCustomize').serialize(),
        success: function () {
            table.draw(false);
        },
        error: function (xhr, ajaxOptions, thrownError) {
            alert('Failed to delete Template.');
        }
    });
};