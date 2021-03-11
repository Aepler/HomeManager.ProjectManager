$(document).ready(function () {
    List();
});

//==============================================================================
// DataTables ==================================================================
//==============================================================================

function List() {
    var table = $("#tblWalletCustomize").DataTable({
        "processing": true,
        "serverSide": true,
        "filter": true,
        "orderMulti": true,
        "destroy": true,
        "order": [[0, "ASC"]],
        "ajax": {
            "url": '/Customize/Finance/GetWalletTableData',
            "type": "POST",
            "datatype": "json"
        },
        "rowId": 'id',
        "columns": [
            { "data": "name" }
            , { "data": "description" }
            , { "data": "startBalance" }
            , { "data": "currentBalance" }
            , {
                "orderable": false,
                "data": null,
                "defaultContent": "<button class='buttonEditWalletCustomize btn btn-outline-secondary' data-bs-toggle='modal' data-bs-target='#modalEditWalletCustomize'>Edit</button>" +
                    " | " +
                    "<button class='buttonDeleteWalletCustomize btn btn-outline-danger' data-bs-toggle='modal' data-bs-target='#modalDeleteWalletCustomize'>Delete</button>",
                "width": "150px"
            }
        ]
    });

    $('#tblWalletCustomize tbody').on('click', '.buttonDeleteWalletCustomize', function () {
        var id = $(this).parent().parent().attr("id");
        if (id != null) {
            $('#buttonModalDeleteWalletCustomize').val(id);
        }
    });

    $('#modalFooterDeleteWalletCustomize').on('click', '#buttonModalDeleteWalletCustomize', function () {
        var id = $(this).val();
        if (id != null) {
            DeleteWalletPost(id, table);
        }
    });

    $('#modalFooterEditWalletCustomize').on('click', '#buttonModalEditWalletCustomize', function () {
        var id = $(this).val();
        if (id != null) {
            EditWalletPost(id, table);
        }
    });

    $('#modalFooterCreateWalletCustomize').on('click', '#buttonModalCreateWalletCustomize', function () {
        CreateWalletPost(table);
    });

    $('#tblWalletCustomize tbody').on('click', '.buttonEditWalletCustomize', function () {
        var id = $(this).parent().parent().attr("id");
        if (id != "") {
            $('#buttonModalEditWalletCustomize').val(id);
            GetWalletEdit(id);
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

function GetWalletEdit(id) {
    $.ajax({
        cache: false,
        type: "GET",
        url: "/Customize/Finance/GetWallet/" + id,
        success: function (data) {
            $('#inputNameEditWalletCustomize').val(data.name);
            $('#inputEndTaxWalletEditWalletCustomize').val(data.endTaxWallet);
            $('#inputDebitEditWalletCustomize').attr("checked", data.debit);
            $('#inputExtraInputEditWalletCustomize').val(data.extraInput);
            $('#dropdownStatusEditWalletCustomize').val(data.fk_StatusId);
        },
        error: function (xhr, ajaxOptions, thrownError) {
            alert('Failed to retrieve type.');
        }
    });
};

function CreateWalletPost(table) {
    $.ajax({
        cache: false,
        type: "Post",
        url: "/Customize/Finance/CreateWallet/",
        data: $('#formCreateWalletCustomize').serialize(),
        success: function () {
            table.draw(false);
        },
        error: function (xhr, ajaxOptions, thrownError) {
            alert('Failed to edit Wallet.');
        }
    });
};

function EditWalletPost(id, table) {
    $.ajax({
        cache: false,
        type: "Post",
        url: "/Customize/Finance/EditWallet/" + id,
        data: $('#formEditWalletCustomize').serialize(),
        success: function () {
            table.draw(false);
        },
        error: function (xhr, ajaxOptions, thrownError) {
            alert('Failed to edit Wallet.');
        }
    });
};

function DeleteWalletPost(id, table) {
    $.ajax({
        cache: false,
        type: "Post",
        url: "/Customize/Finance/DeleteWallet/" + id,
        data: $('#formDeleteWalletCustomize').serialize(),
        success: function () {
            table.draw(false);
        },
        error: function (xhr, ajaxOptions, thrownError) {
            alert('Failed to delete Wallet.');
        }
    });
};