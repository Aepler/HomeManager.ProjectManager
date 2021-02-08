$(document).ready(function () {
    List();
    CreatePayment()
});

//==============================================================================
// DataTables ==================================================================
//==============================================================================

function List() {
    var overview = $("#tblIndexOverview").DataTable({
        "dom": 'Pfrtip',
        "processing": true,
        "serverSide": true,
        "filter": true,
        "orderMulti": false,
        "destroy": true,
        "ordering": true,
        "order": [[1, "ASC"]],
        "ajax": {
            "url": '/Payments/GetTableData',
            "type": "POST",
            "datatype": "json"
        },
        "columns": [
            {
                "className": 'details-control',
                "orderable": false,
                "data": null,
                "defaultContent": '<button class="btn btn-primary" type="button">+</button>'
            }
            , { "data": "date" }
            , { "data": "description" }
            , { "data": "type" }
            , { "data": "amount" }
        ]
    });

    ListExtra(overview);
}

//==============================================================================
// List Child row ==============================================================
//==============================================================================

function ListExtra(overview) {
    $('#tblIndexOverview tbody').on('click', 'td.details-control', function () {
        var tr = $(this).closest('tr');
        var row = overview.row(tr);

        if (row.child.isShown()) {
            // This row is already open - close it
            row.child.hide();
            tr.removeClass('shown');
        }
        else {
            // Open this row
            row.child(ListFormat(row.data())).show();
            tr.addClass('shown');
        }
    });
}

//==============================================================================
// List child row content ======================================================
//==============================================================================

function ListFormat(d) {
    var data = '<table cellpadding="5" cellspacing="0" border="0" style="padding-left:50px;">';

    var date = '<tr>' +
        '<td>Date:</td>' +
        '<td>' + d.date + '</td>' +
        '</tr>';

    var description = '<tr>' +
        '<td>Description:</td>' +
        '<td>' + d.description + '</td>' +
        '</tr>';

    var description_extra = '<tr>' +
        '<td>Description_Extra:</td>' +
        '<td>' + d.description_Extra + '</td>' +
        '</tr>';

    var description_tax = '<tr>' +
        '<td>Description_Tax:</td>' +
        '<td>' + d.description_Tax + '</td>' +
        '</tr>';

    var tax = '<tr>' +
        '<td>Tax:</td>' +
        '<td>' + d.tax + '</td>' +
        '</tr>';

    var amount_taxList = '<tr>' +
        '<td>Tax_Extra:</td>' +
        '<td>' + d.amount_TaxList + '</td>' +
        '</tr>';

    var amount = '<tr>' +
        '<td>Amount:</td>' +
        '<td style="color:green">+' + d.amount + ' €</td>' +
        '</tr>';

    var amount_tax = '<tr>' +
        '<td>Amount_Tax:</td>' +
        '<td>' + d.amount_Tax + '</td>' +
        '</tr>';

    var amount_gross = '<tr>' +
        '<td>Amount_Gross:</td>' +
        '<td>' + d.amount_Gross + '</td>' +
        '</tr>';

    var amount_net = '<tr>' +
        '<td>Amount_Net:</td>' +
        '<td>' + d.amount_Net + '</td>' +
        '</tr>';

    var amount_extra = '<tr>' +
        '<td>Amount_Extra:</td>' +
        '<td>' + d.amount_Extra + '</td>' +
        '</tr>';

    var category = '<tr>' +
        '<td>Category:</td>' +
        '<td>' + d.category + '</td>' +
        '</tr>';

    var status = '<tr>' +
        '<td>Status:</td>' +
        '<td>' + d.status + '</td>' +
        '</tr>';

    data = data + date + description + amount + amount_net + amount_gross + tax + amount_tax + category + status;

    data = data + '</table>';
    return data
}


//==============================================================================
// Edit Entry ==================================================================
//==============================================================================

function EditPayment(id) {
    $('#formEditPayment').attr('action', "/Payments/Edit/" + id);
};

//==============================================================================
// Delete Entry ================================================================
//==============================================================================

function DeletePayment(id) {
    $('#formDeletePayment').attr('action', "/Payments/Delete/" + id);
};

//==============================================================================
// Create Entry ================================================================
//==============================================================================

function CreatePayment() {
    var init = '<div class="form-group form-floating">' +
        '<select name="fk_TypeId" class ="form-control" id="dropdownTypeCreatePayment"></select>' +
        '<label for="fk_TypeId" class="control-label">Type</label>' +
        '</div >';

    $("#modalBodyCreatePayment").append(init);

    var dropdownCategory = $('#dropdownTypeCreatePayment');

    GetTypeList(dropdownCategory);

    dropdownCategory.change(function () {
        var type = $("#dropdownTypeCreatePayment option:selected").val;
        var statusId = $("#dropdownTypeCreatePayment option:selected").attr("id");
        var taxType = $("#dropdownTypeCreatePayment option:selected").attr("tax");
        var inputFields = $("#dropdownTypeCreatePayment option:selected").attr("inputfields");

        var date = '<br class="created" />' +
            '<div class="form-group form-floating  created">' +
            '<input name="Date" placeholder="Date" class="form-control" type="date"  id="datepickerDateCreatePayment" />' +
            '<label for="Date" class="control-label">Date</label>' +
            '</div>';
        var description = '<br class="created" />' +
            '<div class="form-group form-floating created">' +
            '<input name="Description" placeholder="Description" class="form-control" id="inputDescriptionCreatePayment" />' +
            '<label for="Description">Description</label>' +
            '</div>';
        var description_extra = '<br class="created" />' +
            '<div class="form-group form-floating created">' +
            '<input name="Description_Extra" placeholder="Description_Extra" class="form-control" id="inputDescriptionExtraCreatePayment" />' +
            '<label for="Description_Extra" class="control-label">Description Extra</label>' +
            '</div>';
        var tax = '<br class="created" />' +
            '<div class="row g-3 form-group created">' +
            '<div class="col form-floating created">' +
            '<input name="Tax" placeholder="Tax" class="form-control" id="inputTaxCreatePayment" />' +
            '<label for="Tax" class="control-label">Tax</label>' +
            '</div>' +
            '<div class="col form-floating created">' +
            '<input name="Amount_Tax" placeholder="Amount_Tax" class="form-control" id="inputAmountTaxCreatePayment" />' +
            '<label for="Amount_Tax" class="control-label">Amount Tax</label>' +
            '</div>' +
            '</div>';
        var amount = '<br class="created" />' +
            '<div class="form-group form-floating created">' +
            '<input name="Amount" placeholder="Amount" class="form-control" id="inputAmountCreatePayment" />' +
            '<label for="Amount" class="control-label">Total Amount</label>' +
            '</div>';
        var amount_gross = '<div class="form-group form-floating created">' +
            '<input name="Amount_Gross" placeholder="Amount_Gross" class="form-control" id="inputAmountGrossCreatePayment" />' +
            '<label for="Amount_Gross" class="control-label">Amount Gross</label>' +
            '</div>';
        var amount_net = '<div class="form-group form-floating created">' +
            '<input name="Amount_Net" placeholder="Amount_Net" class="form-control" id="inputAmountNetCreatePayment" />' +
            '<label for="Amount_Net" class="control-label">Amount Net</label>' +
            '</div>';
        var amount_extra = '<br class="created" />' +
            '<div class="form-group form-floating created">' +
            '<input name="Amount_Extra" placeholder="Amount_Extra" class="form-control" id="inputAmountExtraCreatePayment" />' +
            '<label for="Amount_Extra" class="control-label">Amount Extra</label>' +
            '</div>';
        var files = '<br class="created" />' +
            '<div class="form-group created">' +
            '<label class="control-label" for="files">Upload Invoice</label>' +
            '<input class="form-control" name="files" type="file" id="uploadFilesCreatePayment" />' +
            '</div>';
        var category = '<br class="created" />' +
            '<div class="form-group form-floating created">' +
            '<select name="fk_CategoryId" class ="form-control" id="dropdownCategoryCreatePayment"></select>' +
            '<label for="fk_CategoryId" class="control-label">Category</label>' +
            '</div>';
        var status = '<br class="created" />' +
            '<div class="form-group form-floating created">' +
            '<select name="fk_StatusId" class ="form-control" id="dropdownStatusCreatePayment"></select>' +
            '<label for="fk_StatusId" class="control-label">Status</label>' +
            '</div>';

        var advancedAmount = '<a href="#" class="link-dark created" id="linkAdvancedAmountCreatePayment" onclick="AdvancedAmount()" value="0" >Advanced</a>'
        var advancedTax = '<a href="#" class="link-dark created" id="linkAdvancedTaxCreatePayment" onclick="AdvancedTax()" value="0">Advanced</a>'
        var extraAmount = '<a href="#" class="link-dark created" id="linkExtraAmountCreatePayment" onclick="ExtraAmount()" value="0">Extra Cost</a>'

        var data = date + description + amount;

        if (inputFields.includes("Extra_Amount")) {
            data = data + extraAmount;
        }

        data = data + advancedAmount;
        if (taxType == "Gross") {
            data = data + '<div class="created" id="divAdvancedCreatePayment" style="display: none">' + amount_gross + '<br class="created" />' + amount_net + '</div>';
        } else if (taxType == "Net") {
            data = data + '<div class="created" id="divAdvancedCreatePayment" style="display: none">' + amount_net + '<br class="created" />' + amount_gross + '</div>';
        }

        data = data + tax;

        if (inputFields.includes("TaxList")) {
            data = data + advancedTax;
        }

        data = data + category + status + files;

        $(".created").remove();
        $("#modalBodyCreatePayment").append(data);

        var dropdownCategory = $('#dropdownCategoryCreatePayment');
        var dropdownStatus = $('#dropdownStatusCreatePayment');
        GetCategoryList(dropdownCategory);
        GetStatusListByType(dropdownStatus, statusId);
    });

    $('#inputAmountGrossCreatePayment').change(function () {
        var grossField = $('#inputAmountGrossCreatePayment');
        var netField = $('#inputAmountNetCreatePayment');
        var taxField = $('#inputAmountTaxCreatePayment');

        CalcNet(grossField, netField, taxField);
    });
}


function AdvancedTax() {
    var countString = $('#linkAdvancedTaxCreatePayment').attr("value");
    var count = parseInt(countString);
    if (count == 0) {
        var advancedTax = '<a href="#" class="link-dark created advancedTax" id="linkAddTaxCreatePayment" onclick="AddTax()" value="1">Add Tax</a>'
        $('#linkAdvancedTaxCreatePayment').after(advancedTax);
        var data = '<br class="advancedTax created" />' +
            '<div class="row g-3 form-group advancedTax created">' +
            '<div class="col form-floating">' +
            '<input name="Description_Tax" placeholder="Description_Tax" class="form-control" id="inputDescriptionTaxCreatePayment" />' +
            '<label for="Description_Tax" class="control-label">Description</label>' +
            '</div>' +
            '<div class="col form-floating">' +
            '<input name="Amount_TaxList" placeholder="Amount_TaxList" class="form-control" id="inputAmountTaxListCreatePayment" />' +
            '<label for="Amount_TaxList" class="control-label">Amount</label>' +
            '</div>' +
            '</div>';
        $('#linkAdvancedTaxCreatePayment').after(data);

        $('#linkAdvancedTaxCreatePayment').attr("value", 1);
    }
    if (count == 1) {
        $('.advancedTax').remove();
        $('#linkAdvancedTaxCreatePayment').attr("value", 0);
    }
}

function AddTax() {
    var countString = $('#linkAddTaxCreatePayment').attr("value");
    var count = parseInt(countString);
    count += 1;
    $('#linkAddTaxCreatePayment').attr("value", count);

    var data = '<br class="created advancedTax" />' +
        '<div class="row g-3 form-group created advancedTax">' +
        '<div class="col form-floating">' +
        '<input name="Description_Tax" placeholder="Description_Tax" class="form-control" id="inputDescriptionTaxCreatePayment" />' +
        '<label for="Description_Tax" class="control-label">Description</label>' +
        '</div>' +
        '<div class="col form-floating">' +
        '<input name="Amount_TaxList" placeholder="Amount_TaxList" class="form-control" id="inputAmountTaxListCreatePayment" />' +
        '<label for="Amount_TaxList" class="control-label">Amount</label>' +
        '</div>' +
        '</div>';

    $('#linkAddTaxCreatePayment').before(data);
}


function AdvancedAmount() {
    $("#divAdvancedCreatePayment").toggle();
}

//==============================================================================
// Edit Entry ==================================================================
//==============================================================================



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

function GetTypeList(dropDown) {
    $.ajax({
        cache: false,
        type: "GET",
        url: "Payments/GetTypeList",
        success: function (data) {
            var s = '<option value="-1" disabled selected>Select a Type</option>';
            for (var i = 0; i < data.length; i++) {
                s += '<option value="' + data[i].id + '" id="' + data[i].fk_StatusId + '" tax="' + data[i].endTaxType + '" inputFields="' + data[i].extraInput + '">' + data[i].name + '</option>';
            }
            dropDown.html(s);
        },
        error: function (xhr, ajaxOptions, thrownError) {
            alert('Failed to retrieve types.');
        }
    });
};

function GetCategoryList(dropDown) {
    $.ajax({
        cache: false,
        type: "GET",
        url: "Payments/GetCategoryList",
        success: function (data) {
            var s = '<option value="-1" disabled selected>Select a Category</option>';
            for (var i = 0; i < data.length; i++) {
                s += '<option value="' + data[i].id + '">' + data[i].name + '</option>';
            }
            dropDown.html(s);
        },
        error: function (xhr, ajaxOptions, thrownError) {
            alert('Failed to retrieve categories.');
        }
    });
};

function GetStatusListByType(dropDown, id) {
    $.ajax({
        cache: false,
        type: "GET",
        url: "Payments/GetStatusListByType/" + id,
        success: function (data) {
            var s = '<option value="-1" disabled selected>Select a Status</option>';
            for (var i = 0; i < data.length; i++) {
                s += '<option value="' + data[i].id + '">' + data[i].name + '</option>';
            }
            dropDown.html(s);
        },
        error: function (xhr, ajaxOptions, thrownError) {
            alert('Failed to retrieve status.');
        }
    });
};