$(document).ready(function () {
    List();
    CreatePayment();
    EditLoader()
});

//==============================================================================
// DataTables ==================================================================
//==============================================================================

function List() {
    var overview = $("#tblIndexPayments").DataTable({
        "processing": true,
        "serverSide": true,
        "filter": true,
        "orderMulti": true,
        "destroy": true,
        "order": [[0, "ASC"]],
        "ajax": {
            "url": '/Finance/Payments/GetTableData',
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

    $('#tblIndexPayments tbody').on('click', '.buttonDeletePaymentFinance', function () {
        var id = $(this).val();
        if (id != null) {
            $('#buttonModalDeletePaymentFinance').val(id);
        }
    });

    $('#modalFooterDeletePaymentFinance').on('click', '#buttonModalDeletePaymentFinance', function () {
        var id = $(this).val();
        if (id != null) {
            DeletePaymentPost(id, overview);
        }
    });

    $('#modalFooterEditPaymentFinance').on('click', '#buttonModalEditPaymentFinance', function () {
        var id = $(this).val();
        if (id != null) {
            EditPaymentPost(id, overview);
        }
    });

    $('#modalFooterCreatePaymentFinance').on('click', '#buttonModalCreatePaymentFinance', function () {
        CreatePaymentPost(overview);
    });

    $('#tblIndexPayments tbody').on('click', '.buttonEditPaymentFinance', function () {
        var id = $(this).val();
        if (id != "") {
            $('#buttonModalEditPaymentFinance').val(id);
            GetPaymentEdit(id);
        }
    });

    ListExtra(overview);
}

//==============================================================================
// List Child row ==============================================================
//==============================================================================

function ListExtra(overview) {
    $('#tblIndexPayments tbody').on('click', 'tr', function () {
        var row = overview.row(this);
        if (row.data() != null) {
            var id = row.data().id;
            if (row.child.isShown()) {
                row.child.hide();
            }
            else {
                row.child(ListFormat(row.data())).show();
                var details = $("#detailsIndexPayments" + id);
                details.parent().parent().addClass("no-hover");
            }
        }
    });
}

//==============================================================================
// List child row content ======================================================
//==============================================================================

function ListFormat(d) {
    var data = '<div id="detailsIndexPayments' + d.id + '" class="container-fluid">';

    var date = '<div class="row">' +
        '<div class="col">Date</div>' +
        '<div class="col">' + d.date + '</div>' +
        '</div>';

    var description = '<div class="row">' +
        '<div class="col">Description</div>' +
        '<div class="col">' + d.description + '</div>' +
        '</div>';

    var description_extra = '<div class="row">' +
        '<div class="col">Description_Extra</div>' +
        '<div class="col">' + d.description_Extra + '</div>' +
        '</div>';

    var description_tax = '<div class="row">' +
        '<div class="col">Description_Tax</div>' +
        '<div class="col">' + d.description_Tax + '</div>' +
        '</div>';

    var tax = '<div class="row">' +
        '<div class="col">Tax</div>' +
        '<div class="col">' + d.tax + '</div>' +
        '</div>';

    var amount_taxList = '<div class="row">' +
        '<div class="col">Tax_Extra</div>' +
        '<div class="col">' + d.amount_TaxList + '</div>' +
        '</div>';

    var amount = '<div class="row">' +
        '<div class="col">Amount</div>' +
        '<div class="col">' + d.amount + '</div>' +
        '</div>';

    var amount_tax = '<div class="row">' +
        '<div class="col">Amount_Tax</div>' +
        '<div class="col">' + d.amount_Tax + '</div>' +
        '</div>';

    var amount_gross = '<div class="row">' +
        '<div class="col">Amount_Gross</div>' +
        '<div class="col">' + d.amount_Gross + '</div>' +
        '</div>';

    var amount_net = '<div class="row">' +
        '<div class="col">Amount_Net</div>' +
        '<div class="col">' + d.amount_Net + '</div>' +
        '</div>';

    var amount_extra = '<div class="row">' +
        '<div class="col">Amount_Extra</div>' +
        '<div class="col">' + d.amount_Extra + '</div>' +
        '</div>';

    var category = '<div class="row">' +
        '<div class="col">Category</div>' +
        '<div class="col">' + d.category + '</div>' +
        '</div>';

    var status = '<div class="row">' +
        '<div class="col">Status</div>' +
        '<div class="col">' + d.status + '</div>' +
        '</div>';

    var button = '<div style="float:right">' +
        '<button class="buttonEditPaymentFinance btn btn-outline-secondary" value="' + d.id + '" data-bs-toggle="modal" data-bs-target="#modalEditPaymentFinance">Edit</button>' +
        ' | ' +
        '<button class="buttonDeletePaymentFinance btn btn-outline-danger" value="' + d.id + '" data-bs-toggle="modal" data-bs-target="#modalDeletePaymentFinance">Delete</button>' +
        '</div>';

    data = data + date + description + amount + amount_net + amount_gross + tax + amount_tax + category + status + button;

    data = data + '</div>';
    return data
}


//==============================================================================
// Create Entry ================================================================
//==============================================================================

function CreatePayment() {
    $('#dropdownTypeCreatePaymentFinance').change(function () {
        var id = $(this).val();
        if (id != "") {
            GetPaymentCreate(id);
        }
    });

    $('#modalBodyCreatePaymentFinance').on('change', '#inputAmountGrossCreatePaymentFinance', function () {
        var grossField = $('#inputAmountGrossCreatePaymentFinance');
        var netField = $('#inputAmountNetCreatePaymentFinance');
        var taxField = $('#inputAmountTaxCreatePaymentFinance');

        CalcNet(grossField, netField, taxField);
    });

    $('#modalBodyCreatePaymentFinance').on('click', '#linkAdvancedAmountCreatePaymentFinance', function () {
        AdvancedAmount("Create");
    });

    $('#modalBodyCreatePaymentFinance').on('click', '#linkAdvancedTaxListCreatePaymentFinance', function () {
        AdvancedTax("Create");
    });

    $('#modalBodyCreatePaymentFinance').on('click', '#linkAddTaxCreatePaymentFinance', function () {
        AddTax("Create");
    });

    $('#modalBodyCreatePaymentFinance').on('click', '#linkAdvancedExtraCostsCreatePaymentFinance', function () {
        AdvancedExtraCosts("Create");
    });

    $('#modalBodyCreatePaymentFinance').on('click', '#linkAddExtraCostCreatePaymentFinance', function () {
        AddExtraCost("Create");
    });
}

function EditLoader() {
    $('#modalBodyEditPaymentFinance').on('change', '#inputAmountGrossEditPaymentFinance', function () {
        var grossField = $('#inputAmountGrossEditPaymentFinance');
        var netField = $('#inputAmountNetEditPaymentFinance');
        var taxField = $('#inputAmountTaxEditPaymentFinance');

        CalcNet(grossField, netField, taxField);
    });

    $('#modalBodyEditPaymentFinance').on('click', '#linkAdvancedAmountEditPaymentFinance', function () {
        AdvancedAmount("Edit");
    });

    $('#modalBodyEditPaymentFinance').on('click', '#linkAdvancedTaxListEditPaymentFinance', function () {
        AdvancedTax("Edit");
    });

    $('#modalBodyEditPaymentFinance').on('click', '#linkAddTaxEditPaymentFinance', function () {
        AddTax("Edit");
    });

    $('#modalBodyEditPaymentFinance').on('click', '#linkAdvancedExtraCostsEditPaymentFinance', function () {
        AdvancedExtraCosts("Edit");
    });

    $('#modalBodyEditPaymentFinance').on('click', '#linkAddExtraCostEditPaymentFinance', function () {
        AddExtraCost("Edit");
    });
}

//==============================================================================
// Show more ===================================================================
//==============================================================================

function AdvancedAmount(type) {
    $('#divAdvancedAmount' + type + 'PaymentFinance').toggle();
}

function AdvancedTax(type) {
    $('#divAdvancedTax' + type + 'PaymentFinance').toggle();
}

function AddTax(type) {
    var data = '<br class="created" />' +
        '<div class="row g-3 form-group advancedTax created">' +
        '<div class="col-6 form-floating">' +
        '<input name="Description_TaxList" placeholder="Description_TaxList" class="form-control" id="inputDescription_TaxList' + type + 'PaymentFinance" />' +
        '<label for="Description_TaxList" class="control-label">Description</label>' +
        '</div>' +
        '<div class="col form-floating">' +
        '<input name="TaxList" placeholder="TaxList" class="form-control" id="inputTaxList' + type + 'PaymentFinance" />' +
        '<label for="TaxList" class="control-label">Tax %</label>' +
        '</div>' +
        '<div class="col form-floating">' +
        '<input name="Amount_TaxList" placeholder="Amount_TaxList" class="form-control" id="inputAmountTaxList' + type + 'PaymentFinance" />' +
        '<label for="Amount_TaxList" class="control-label">Amount</label>' +
        '</div>' +
        '</div>';

    $('#linkAddTax' + type + 'PaymentFinance').before(data);
}

function AdvancedExtraCosts(type) {
    $('#divExtraCosts' + type + 'PaymentFinance').toggle();
}

function AddExtraCost(type) {
    var data = '<br class="created" />' +
        '<div class="row g-3 form-group advancedTax created">' +
        '<div class="col form-floating">' +
        '<input name="Description_ExtraCosts" placeholder="Description_ExtraCosts" class="form-control" id="inputDescription_ExtraCosts' + type + 'PaymentFinance" />' +
        '<label for="Description_ExtraCosts" class="control-label">Description</label>' +
        '</div>' +
        '<div class="col form-floating">' +
        '<input name="Amount_ExtraCosts" placeholder="Amount_ExtraCosts" class="form-control" id="inputAmount_ExtraCosts' + type + 'PaymentFinance" />' +
        '<label for="Amount_ExtraCosts" class="control-label">Amount</label>' +
        '</div>' +
        '</div>';

    $('#linkAddExtraCost' + type + 'PaymentFinance').before(data);
}

//==============================================================================
// Calc ========================================================================
//==============================================================================

function CalcTax(grossField, netField, taxField, taxPrecentField) {
    var gross = grossField.val().replace(',', '.');
    var net = netField.val().replace(',', '.');

    if (gross != "" && net != "") {
        var resultTax = gross - net;
        resultTax = resultTax.toFixed(2);
        taxField.val(resultTax.replace('.', ','));

        var resultPercentage = (100 / (net / gross)) - 100;
        resultPercentage = resultPercentage.toFixed(0);
        taxPrecentField.val(resultPercentage);
    }

};

function CalcNet(grossField, netField, taxField) {
    var gross = grossField.val().replace(',', '.');
    var tax = taxField.val();

    if (gross != "" && tax != "") {
        var resultNet = gross / ((tax / 100) + 1);
        var resultTax = gross - resultNet;
        resultNet = resultNet.toFixed(2);
        resultTax = resultTax.toFixed(2);
        netField.val(resultNet.replace('.', ','));
        taxField.val(resultTax.replace('.', ','));
    }
};

//==============================================================================
// Ajax ========================================================================
//==============================================================================

function GetPaymentCreate(id) {
    $.ajax({
        cache: false,
        type: "GET",
        url: "/Finance/Payments/GetPaymentCreate/" + id,
        success: function (data) {
            $(".created").remove();
            $("#modalBodyCreatePaymentFinance").append(data);
        },
        error: function (xhr, ajaxOptions, thrownError) {
            alert('Failed to retrieve payment.');
        }
    });
};

function GetPaymentEdit(id) {
    $.ajax({
        cache: false,
        type: "GET",
        url: "/Finance/Payments/GetPaymentEdit/" + id,
        success: function (data) {
            $(".created").remove();
            $("#modalBodyEditPaymentFinance").append(data);
        },
        error: function (xhr, ajaxOptions, thrownError) {
            alert('Failed to retrieve payment.');
        }
    });
};

function CreatePaymentPost(table) {
    var form = $('#formCreatePaymentFinance').serialize();
    var data = new FormData();
    var formArray = form.split('&');

    var elementArray = [];
    $.each(formArray, function (index, value) {
        elementArray[index] = value;
    });

    $.each(elementArray, function (index, value) {
        var itemArray = value.split('=');
        data.append(itemArray[0], itemArray[1]);
    });

    var fileUpload = $("#uploadFilesCreatePaymentFinance").get(0);
    var files = fileUpload.files;

    for (var i = 0; i < files.length; i++) {

        data.append("files", files[i]);

    } 

    $.ajax({
        cache: false,
        type: "Post",
        url: "/Finance/Payments/Create/",
        processData: false,
        contentType: false,
        data: data,
        success: function () {
            table.draw(false);
        },
        error: function (xhr, ajaxOptions, thrownError) {
            alert('Failed to create payment.');
        }
    });
};

function EditPaymentPost(id, table) {
    $.ajax({
        cache: false,
        type: "Post",
        url: "/Finance/Payments/Edit/" + id,
        data: $('#formEditPaymentFinance').serialize(),
        success: function () {
            table.draw(false);
        },
        error: function (xhr, ajaxOptions, thrownError) {
            alert('Failed to edit payment.');
        }
    });
};

function DeletePaymentPost(id, table) {
    $.ajax({
        cache: false,
        type: "Post",
        url: "/Finance/Payments/Delete/" + id,
        data: $('#formDeletePaymentFinance').serialize(),
        success: function () {
            table.draw(false);
        },
        error: function (xhr, ajaxOptions, thrownError) {
            alert('Failed to delete payment.');
        }
    });
};