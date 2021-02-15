$(document).ready(function () {
    List();
});

//==============================================================================
// DataTables ==================================================================
//==============================================================================

function List() {
    var table = $("#tblPaymentTemplateCustomize").DataTable({
        "processing": true,
        "serverSide": true,
        "filter": true,
        "orderMulti": true,
        "destroy": true,
        "order": [[0, "ASC"]],
        "ajax": {
            "url": '/Finance/Customize/GetPaymentTemplateTableData',
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
                "defaultContent": "<button class='buttonEditPaymentTemplateCustomize btn btn-outline-secondary' data-bs-toggle='modal' data-bs-target='#modalEditPaymentTemplateCustomize'>Edit</button>" +
                    " | " +
                    "<button class='buttonDeletePaymentTemplateCustomize btn btn-outline-danger' data-bs-toggle='modal' data-bs-target='#modalDeletePaymentTemplateCustomize'>Delete</button>",
                "width": "150px"
            }
        ]
    });

    $('#tblPaymentTemplateCustomize tbody').on('click', '.buttonDeletePaymentTemplateCustomize', function () {
        var id = $(this).parent().parent().attr("id");
        if (id != null) {
            $('#buttonModalDeletePaymentTemplateCustomize').val(id);
        }
    });

    $('#modalFooterDeletePaymentTemplateCustomize').on('click', '#buttonModalDeletePaymentTemplateCustomize', function () {
        var id = $(this).val();
        if (id != null) {
            DeletePaymentTemplatePost(id, table);
        }
    });

    $('#modalFooterEditPaymentTemplateCustomize').on('click', '#buttonModalEditPaymentTemplateCustomize', function () {
        var id = $(this).val();
        if (id != null) {
            EditPaymentTemplatePost(id, table);
        }
    });

    $('#modalFooterCreatePaymentTemplateCustomize').on('click', '#buttonModalCreatePaymentTemplateCustomize', function () {
        CreatePaymentTemplatePost(table);
    });

    $('#tblPaymentTemplateCustomize tbody').on('click', '.buttonEditPaymentTemplateCustomize', function () {
        var id = $(this).parent().parent().attr("id");
        if (id != "") {
            $('#buttonModalEditPaymentTemplateCustomize').val(id);
            GetPaymentTemplateEdit(id);
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

function GetPaymentTemplateEdit(id) {
    $.ajax({
        cache: false,
        type: "GET",
        url: "/Finance/Customize/GetPaymentTemplate/" + id,
        success: function (data) {

        },
        error: function (xhr, ajaxOptions, thrownError) {
            alert('Failed to retrieve payment.');
        }
    });
};

function CreatePaymentTemplatePost(table) {
    $.ajax({
        cache: false,
        type: "Post",
        url: "/Finance/Customize/CreatePaymentTemplate/",
        data: $('#formCreatePaymentTemplateCustomize').serialize(),
        success: function () {
            table.draw(false);
        },
        error: function (xhr, ajaxOptions, thrownError) {
            alert('Failed to edit PaymentTemplate.');
        }
    });
};

function EditPaymentTemplatePost(id, table) {
    $.ajax({
        cache: false,
        type: "Post",
        url: "/Finance/Customize/EditPaymentTemplate/" + id,
        data: $('#formEditPaymentTemplateCustomize').serialize(),
        success: function () {
            table.draw(false);
        },
        error: function (xhr, ajaxOptions, thrownError) {
            alert('Failed to edit PaymentTemplate.');
        }
    });
};

function DeletePaymentTemplatePost(id, table) {
    $.ajax({
        cache: false,
        type: "Post",
        url: "/Finance/Customize/DeletePaymentTemplate/" + id,
        data: $('#formDeletePaymentTemplateCustomize').serialize(),
        success: function () {
            table.draw(false);
        },
        error: function (xhr, ajaxOptions, thrownError) {
            alert('Failed to delete PaymentTemplate.');
        }
    });
};