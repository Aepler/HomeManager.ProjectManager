$(document).ready(function () {

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
            "url": '/Customize/GetPaymentTemplateTableData',
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

    $('#tblPaymentTemplateCustomize tbody').on('click', '.buttonDeletePaymentTemplateCustomize', function () {
        var id = $(this).val();
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
        var id = $(this).val();
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

function CreatePaymentTemplatePost(id, table) {
    $.ajax({
        cache: false,
        type: "Post",
        url: "Customize/CreatePaymentTemplate/" + id,
        data: $('#formCreatePaymentTemplate').serialize(),
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
        url: "Customize/EditPaymentTemplate/" + id,
        data: $('#formEditPaymentTemplate').serialize(),
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
        url: "Customize/DeletePaymentTemplate/" + id,
        data: $('#formDeletePaymentTemplate').serialize(),
        success: function () {
            table.draw(false);
        },
        error: function (xhr, ajaxOptions, thrownError) {
            alert('Failed to delete PaymentTemplate.');
        }
    });
};