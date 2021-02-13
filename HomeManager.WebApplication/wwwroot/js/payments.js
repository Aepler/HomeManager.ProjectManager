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

    $('#tblIndexPayments tbody').on('click', '.buttonDeletePayment', function () {
        var id = $(this).val();
        if (id != null) {
            $('#buttonModalDeletePayment').val(id);
        }
    });

    $('#modalFooterDeletePayment').on('click', '#buttonModalDeletePayment', function () {
        var id = $(this).val();
        if (id != null) {
            DeletePaymentPost(id, overview);
        }
    });

    $('#modalFooterEditPayment').on('click', '#buttonModalEditPayment', function () {
        var id = $(this).val();
        if (id != null) {
            EditPaymentPost(id, overview);
        }
    });

    $('#modalFooterCreatePayment').on('click', '#buttonModalCreatePayment', function () {
        CreatePaymentPost(overview);
    });

    $('#tblIndexPayments tbody').on('click', '.buttonEditPayment', function () {
        var id = $(this).val();
        if (id != "") {
            $('#buttonModalEditPayment').val(id);
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
        '<button class="buttonEditPayment btn btn-outline-secondary" value="' + d.id + '" data-bs-toggle="modal" data-bs-target="#modalEditPayment">Edit</button>' +
        ' | ' +
        '<button class="buttonDeletePayment btn btn-outline-danger" value="' + d.id + '" data-bs-toggle="modal" data-bs-target="#modalDeletePayment">Delete</button>' +
        '</div>';

    data = data + date + description + amount + amount_net + amount_gross + tax + amount_tax + category + status + button;

    data = data + '</div>';
    return data
}


//==============================================================================
// Create Entry ================================================================
//==============================================================================

function CreatePayment() {
    var init = '<div class="form-group form-floating">' +
        '<select name="fk_TypeId" class ="form-control" id="dropdownTypeCreatePayment"></select>' +
        '<label for="fk_TypeId" class="control-label">Type</label>' +
        '</div >';

    $("#modalBodyCreatePayment").append(init);

    var dropdownType = $('#dropdownTypeCreatePayment');

    GetTypeList(dropdownType);

    dropdownType.change(function () {
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

        var advancedAmount = '<a href="#" class="link-dark created" id="linkAdvancedAmountCreatePayment" value="0" >Advanced</a>'
        var advancedTax = '<a href="#" class="link-dark created" id="linkAdvancedTaxCreatePayment" value="0">Advanced</a>'
        var extraAmount = '<a href="#" class="link-dark created" id="linkExtraAmountCreatePayment" value="0">Extra Cost</a>'


        var type = $("#dropdownTypeCreatePayment option:selected").val;
        var statusId = $("#dropdownTypeCreatePayment option:selected").attr("id");
        var taxType = $("#dropdownTypeCreatePayment option:selected").attr("tax");
        var inputFields = $("#dropdownTypeCreatePayment option:selected").attr("inputfields");

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

    $('#modalBodyCreatePayment').on('change', '#inputAmountGrossCreatePayment', function () {
        var grossField = $('#inputAmountGrossCreatePayment');
        var netField = $('#inputAmountNetCreatePayment');
        var taxField = $('#inputAmountTaxCreatePayment');

        CalcNet(grossField, netField, taxField);
    });

    $('#modalBodyCreatePayment').on('click', '#linkAdvancedTaxCreatePayment', function () {
        AdvancedTax("Create");
    });

    $('#modalBodyCreatePayment').on('click', '#linkAdvancedAmountCreatePayment', function () {
        AdvancedAmount("Create");
    });

    $('#modalBodyCreatePayment').on('click', '#linkAddTaxCreatePayment', function () {
        AddTax("Create");
    });
}





//==============================================================================
// Edit Entry ==================================================================
//==============================================================================

function EditPayment(data) {
    var type = '<div class="form-group form-floating created">' +
        '<select name="fk_TypeId" class ="form-control" id="dropdownTypeEditPayment"></select>' +
        '<label for="fk_TypeId" class="control-label">Type</label>' +
        '</div >';

    var date = '<br class="created" />' +
        '<div class="form-group form-floating  created">' +
        '<input name="Date" placeholder="Date" class="form-control" type="date"  id="datepickerDateEditPayment" value="' + data.date.substring(0, 10) + '" />' +
        '<label for="Date" class="control-label">Date</label>' +
        '</div>';
    var description = '<br class="created" />' +
        '<div class="form-group form-floating created">' +
        '<input name="Description" placeholder="Description" class="form-control" id="inputDescriptionEditPayment" value="' + data.description + '" />' +
        '<label for="Description">Description</label>' +
        '</div>';
    var description_extra = '<br class="created" />' +
        '<div class="form-group form-floating created">' +
        '<input name="Description_Extra" placeholder="Description_Extra" class="form-control" id="inputDescriptionExtraEditPayment" value="' + data.description_Extra + '" />' +
        '<label for="Description_Extra" class="control-label">Description Extra</label>' +
        '</div>';
    var tax = '<br class="created" />' +
        '<div class="row g-3 form-group created">' +
        '<div class="col form-floating created">' +
        '<input name="Tax" placeholder="Tax" class="form-control" id="inputTaxEditPayment" value="' + data.tax + '" />' +
        '<label for="Tax" class="control-label">Tax</label>' +
        '</div>' +
        '<div class="col form-floating created">' +
        '<input name="Amount_Tax" placeholder="Amount_Tax" class="form-control" id="inputAmountTaxEditPayment" value="' + data.amount_Tax + '" />' +
        '<label for="Amount_Tax" class="control-label">Amount Tax</label>' +
        '</div>' +
        '</div>';
    var amount = '<br class="created" />' +
        '<div class="form-group form-floating created">' +
        '<input name="Amount" placeholder="Amount" class="form-control" id="inputAmountEditPayment" value="' + data.amount + '" />' +
        '<label for="Amount" class="control-label">Total Amount</label>' +
        '</div>';
    var amount_gross = '<div class="form-group form-floating created">' +
        '<input name="Amount_Gross" placeholder="Amount_Gross" class="form-control" id="inputAmountGrossEditPayment" value="' + data.amount_Gross + '" />' +
        '<label for="Amount_Gross" class="control-label">Amount Gross</label>' +
        '</div>';
    var amount_net = '<div class="form-group form-floating created">' +
        '<input name="Amount_Net" placeholder="Amount_Net" class="form-control" id="inputAmountNetEditPayment" value="' + data.amount_Net + '" />' +
        '<label for="Amount_Net" class="control-label">Amount Net</label>' +
        '</div>';
    var amount_extra = '<br class="created" />' +
        '<div class="form-group form-floating created">' +
        '<input name="Amount_Extra" placeholder="Amount_Extra" class="form-control" id="inputAmountExtraEditPayment" value="' + data.amount_Extra + '" />' +
        '<label for="Amount_Extra" class="control-label">Amount Extra</label>' +
        '</div>';
    var files = '<br class="created" />' +
        '<div class="form-group created">' +
        '<label class="control-label" for="files">Upload Invoice</label>' +
        '<input class="form-control" name="files" type="file" id="uploadFilesEditPayment" />' +
        '</div>';
    var category = '<br class="created" />' +
        '<div class="form-group form-floating created">' +
        '<select name="fk_CategoryId" class ="form-control" id="dropdownCategoryEditPayment"></select>' +
        '<label for="fk_CategoryId" class="control-label">Category</label>' +
        '</div>';
    var status = '<br class="created" />' +
        '<div class="form-group form-floating created">' +
        '<select name="fk_StatusId" class ="form-control" id="dropdownStatusEditPayment"></select>' +
        '<label for="fk_StatusId" class="control-label">Status</label>' +
        '</div>';

    var advancedAmount = '<a href="#" class="link-dark created" id="linkAdvancedAmountEditPayment" value="0" >Advanced</a>'
    var advancedTax = '<a href="#" class="link-dark created" id="linkAdvancedTaxEditPayment" value="0">Advanced</a>'
    var extraAmount = '<a href="#" class="link-dark created" id="linkExtraAmountEditPayment" value="0">Extra Cost</a>'

    var html = type + date + description + amount;

    if (data.type.extraInput.includes("Extra_Amount")) {
        html += extraAmount;
    }

    html += advancedAmount;
    if (data.type.endTaxType == "Gross") {
        html += '<div class="created" id="divAdvancedEditPayment" style="display: none">' + amount_gross + '<br class="created" />' + amount_net + '</div>';
    } else if (data.type.endTaxType == "Net") {
        html += '<div class="created" id="divAdvancedEditPayment" style="display: none">' + amount_net + '<br class="created" />' + amount_gross + '</div>';
    }

    html += tax;

    if (data.type.extraInput.includes("TaxList")) {
        html += advancedTax;
    }

    html += category + status + files;

    $(".created").remove();
    $("#modalBodyEditPayment").append(html);

    var dropdownType = $('#dropdownTypeEditPayment');
    var dropdownCategory = $('#dropdownCategoryEditPayment');
    var dropdownStatus = $('#dropdownStatusEditPayment'); 

    GetTypeList(dropdownType, data.fk_TypeId);
    GetCategoryList(dropdownCategory, data.fk_CategoryId);
    GetStatusListByType(dropdownStatus, data.fk_StatusId, data.fk_StatusId);
};

function EditLoader() {
    $('#modalBodyEditPayment').on('change', '#inputAmountGrossEditPayment', function () {
        var grossField = $('#inputAmountGrossEditPayment');
        var netField = $('#inputAmountNetEditPayment');
        var taxField = $('#inputAmountTaxEditPayment');

        CalcNet(grossField, netField, taxField);
    });

    $('#modalBodyEditPayment').on('click', '#linkAdvancedTaxEditPayment', function () {
        AdvancedTax("Edit");
    });

    $('#modalBodyEditPayment').on('click', '#linkAdvancedAmountEditPayment', function () {
        AdvancedAmount("Edit");
    });

    $('#modalBodyEditPayment').on('click', '#linkAddTaxEditPayment', function () {
        AddTax("Edit");
    });
}

//==============================================================================
// Show more ===================================================================
//==============================================================================

function AdvancedTax(type) {
    var countString = $('#linkAdvancedTax' + type + 'Payment').attr("value");
    var count = parseInt(countString);
    if (count == 0) {
        var addTax = '<a href="#" class="link-dark created advancedTax" id="linkAddTax' + type + 'Payment" value="1">Add Tax</a>'
        $('#linkAdvancedTax' + type + 'Payment').after(addTax);
        var data = '<br class="advancedTax created" />' +
            '<div class="row g-3 form-group advancedTax created">' +
            '<div class="col form-floating">' +
            '<input name="Description_Tax" placeholder="Description_Tax" class="form-control" id="inputDescriptionTax' + type + 'Payment" />' +
            '<label for="Description_Tax" class="control-label">Description</label>' +
            '</div>' +
            '<div class="col form-floating">' +
            '<input name="Amount_TaxList" placeholder="Amount_TaxList" class="form-control" id="inputAmountTaxList' + type + 'Payment" />' +
            '<label for="Amount_TaxList" class="control-label">Amount</label>' +
            '</div>' +
            '</div>';
        $('#linkAdvancedTax' + type + 'Payment').after(data);

        $('#linkAdvancedTax' + type + 'Payment').attr("value", 1);
    }
    if (count == 1) {
        $('.advancedTax').remove();
        $('#linkAdvancedTax' + type + 'Payment').attr("value", 0);
    }
}

function AddTax(type) {
    var countString = $('#linkAddTax' + type + 'Payment').attr("value");
    var count = parseInt(countString);
    count += 1;
    $('#linkAddTax' + type + 'Payment').attr("value", count);

    var data = '<br class="created advancedTax" />' +
        '<div class="row g-3 form-group created advancedTax">' +
        '<div class="col form-floating">' +
        '<input name="Description_Tax" placeholder="Description_Tax" class="form-control" id="inputDescriptionTax' + type + 'Payment" />' +
        '<label for="Description_Tax" class="control-label">Description</label>' +
        '</div>' +
        '<div class="col form-floating">' +
        '<input name="Amount_TaxList" placeholder="Amount_TaxList" class="form-control" id="inputAmountTaxList' + type + 'Payment" />' +
        '<label for="Amount_TaxList" class="control-label">Amount</label>' +
        '</div>' +
        '</div>';

    $('#linkAddTax' + type + 'Payment').before(data);
}


function AdvancedAmount(type) {
    $('#divAdvanced' + type + 'Payment').toggle();
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

function GetPaymentEdit(id) {
    $.ajax({
        cache: false,
        type: "GET",
        url: "Payments/GetPayment/" + id,
        success: function (data) {
            EditPayment(data);
            //$('#modalEditPayment').toggle();
        },
        error: function (xhr, ajaxOptions, thrownError) {
            alert('Failed to retrieve payment.');
        }
    });
};

function GetTypeList(dropDown, selected) {
    $.ajax({
        cache: false,
        type: "GET",
        url: "Payments/GetTypeList",
        success: function (data) {
            var s = "";
            if (selected == null) {
                s = '<option value="-1" disabled selected>Select a Type</option>';
            }
            for (var i = 0; i < data.length; i++) {
                s += '<option value="' + data[i].id + '" id="' + data[i].fk_StatusId + '" tax="' + data[i].endTaxType + '" inputFields="' + data[i].extraInput + '"';
                if (selected == data[i].id) {
                    s += 'selected';
                }
                s += '>' + data[i].name + '</option>';
            }
            dropDown.html(s);
        },
        error: function (xhr, ajaxOptions, thrownError) {
            alert('Failed to retrieve types.');
        }
    });
};

function GetCategoryList(dropDown, selected) {
    $.ajax({
        cache: false,
        type: "GET",
        url: "Payments/GetCategoryList",
        success: function (data) {
            var s = "";
            if (selected == null) {
                s = '<option value="-1" disabled selected>Select a Category</option>';
            }
            for (var i = 0; i < data.length; i++) {
                s += '<option value="' + data[i].id + '"';
                if (selected == data[i].id) {
                    s += 'selected';
                }
                s += '>' + data[i].name + '</option>';
            }
            dropDown.html(s);
        },
        error: function (xhr, ajaxOptions, thrownError) {
            alert('Failed to retrieve categories.');
        }
    });
};

function GetStatusListByType(dropDown, id, selected) {
    $.ajax({
        cache: false,
        type: "GET",
        url: "Payments/GetStatusListByType/" + id,
        success: function (data) {
            var s = "";
            if (selected == null) {
                s = '<option value="-1" disabled selected>Select a Status</option>';
            }
            for (var i = 0; i < data.length; i++) {
                s += '<option value="' + data[i].id + '"';
                if (selected == data[i].id) {
                    s += 'selected';
                }
                s += '>' + data[i].name + '</option>';
            }
            dropDown.html(s);
        },
        error: function (xhr, ajaxOptions, thrownError) {
            alert('Failed to retrieve status.');
        }
    });
};

function CreatePaymentPost(table) {
    var form = $('#formCreatePayment').serialize();
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

    var fileUpload = $("#uploadFilesCreatePayment").get(0);
    var files = fileUpload.files;

    for (var i = 0; i < files.length; i++) {

        data.append("files", files[i]);

    } 

    $.ajax({
        cache: false,
        type: "Post",
        url: "Payments/Create/",
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
        url: "Payments/Edit/" + id,
        data: $('#formEditPayment').serialize(),
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
        url: "Payments/Delete/" + id,
        data: $('#formDeletePayment').serialize(),
        success: function () {
            table.draw(false);
        },
        error: function (xhr, ajaxOptions, thrownError) {
            alert('Failed to delete payment.');
        }
    });
};