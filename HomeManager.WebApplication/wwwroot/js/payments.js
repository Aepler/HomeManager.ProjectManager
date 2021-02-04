$(document).ready(function () {
    List();
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
            , { "data": "Date" }
            , { "data": "Description" }
            , { "data": "Type" }
            , { "data": "Amount" }
        ]
    });

    var container = $('<div/>').insertBefore(overview.table().container());

    var chart = Highcharts.chart(container[0], {
        chart: {
            type: 'pie',
        },
        title: {
            text: 'Staff Count Per Position',
        },
        series: [
            {
                data: chartData(overview),
            },
        ],
    });

    // On each draw, update the data in the chart
    table.on('draw', function () {
        chart.series[0].setData(chartData(table));
    });

    var overviewPending = $("#tblIndexOverviewPending").DataTable({
        "processing": true,
        "serverSide": true,
        "filter": true,
        "orderMulti": false,
        "destroy": true,
        "ordering": true,
        "order": [[1, "ASC"]],
        "ajax": {
            "url": '/Payments/GetTableDataPending',
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
            , { "data": "Date" }
            , { "data": "Description" }
            , { "data": "Type" }
            , { "data": "Amount" }
        ]
    });

    var overviewSalary = $("#tblIndexSalary").DataTable({
        "processing": true,
        "serverSide": true,
        "filter": true,
        "orderMulti": false,
        "destroy": true,
        "ordering": true,
        "order": [[1, "ASC"]],
        "ajax": {
            "url": '/Payments/GetTableDataSalary',
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
            , { "data": "Date" }
            , { "data": "Description" }
            , { "data": "Amount" }
        ]
    });

    var overviewMonthlyExpenses = $("#tblIndexMonthlyExpenses").DataTable({
        "processing": true,
        "serverSide": true,
        "filter": true,
        "orderMulti": false,
        "destroy": true,
        "ordering": true,
        "order": [[1, "ASC"]],
        "ajax": {
            "url": '/Payments/GetTableDataMonthlyExpenses',
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
            , { "data": "Date" }
            , { "data": "Description" }
            , { "data": "Amount" }
        ]
    });

    var overviewExpenditure = $("#tblIndexExpenditure").DataTable({
        "processing": true,
        "serverSide": true,
        "filter": true,
        "orderMulti": false,
        "destroy": true,
        "ordering": true,
        "order": [[1, "ASC"]],
        "ajax": {
            "url": '/Payments/GetTableDataExpenditure',
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
            , { "data": "Date" }
            , { "data": "Description" }
            , { "data": "Amount" }
        ]
    });

    var overviewEarnings = $("#tblIndexEarnings").DataTable({
        "processing": true,
        "serverSide": true,
        "filter": true,
        "orderMulti": false,
        "destroy": true,
        "ordering": true,
        "order": [[1, "ASC"]],
        "ajax": {
            "url": '/Payments/GetTableDataEarnings',
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
            , { "data": "Date" }
            , { "data": "Description" }
            , { "data": "Amount" }
        ]
    });

    ListExtra(overview, overviewPending, overviewSalary, overviewMonthlyExpenses, overviewExpenditure, overviewEarnings);
}

function chartData(table) {
    var counts = {};

    // Count the number of entries for each position
    table
        .column(1, { search: 'applied' })
        .data()
        .each(function (val) {
            if (counts[val]) {
                counts[val] += 1;
            } else {
                counts[val] = 1;
            }
        });

    // And map it to the format highcharts uses
    return $.map(counts, function (val, key) {
        return {
            name: key,
            y: val,
        };
    });
}

//==============================================================================
// List Child row ==============================================================
//==============================================================================

function ListExtra(overview, overviewPending, overviewSalary, overviewMonthlyExpenses, overviewExpenditure, overviewEarnings) {
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

    $('#tblIndexOverviewPending tbody').on('click', 'td.details-control', function () {
        var tr = $(this).closest('tr');
        var row = overviewPending.row(tr);

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

    $('#tblIndexSalary tbody').on('click', 'td.details-control', function () {
        var tr = $(this).closest('tr');
        var row = overviewSalary.row(tr);

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

    $('#tblIndexMonthlyExpenses tbody').on('click', 'td.details-control', function () {
        var tr = $(this).closest('tr');
        var row = overviewMonthlyExpenses.row(tr);

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

    $('#tblIndexExpenditure tbody').on('click', 'td.details-control', function () {
        var tr = $(this).closest('tr');
        var row = overviewExpenditure.row(tr);

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

    $('#tblIndexEarnings tbody').on('click', 'td.details-control', function () {
        var tr = $(this).closest('tr');
        var row = overviewEarnings.row(tr);

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
    if (d.Type == "Salary") {
        data = data + '<tr>' +
            '<td>Date:</td>' +
            '<td>' + d.Date + '</td>' +
            '</tr>';
        data = data + '<tr>' +
            '<td>Description:</td>' +
            '<td>' + d.Description + '</td>' +
            '</tr>';
        if (d.Description_Extra != null) {
            data = data + '<tr>' +
                '<td>Description_Extra:</td>' +
                '<td>' + d.Description_Extra + '</td>' +
                '</tr>';
        }
        if (d.Description_Tax_Extra != null) {
            data = data + '<tr>' +
                '<td>Description_Tax_Extra:</td>' +
                '<td>' + d.Description_Tax_Extra + '</td>' +
                '</tr>';
        }
        if (d.Tax != "0") {
            data = data + '<tr>' +
                '<td>Tax:</td>' +
                '<td>' + d.Tax + '</td>' +
                '</tr>';
        }
        if (d.Tax_Extra != null) {
            data = data + '<tr>' +
                '<td>Tax_Extra:</td>' +
                '<td>' + d.Tax_Extra + '</td>' +
                '</tr>';
        }
        data = data + '<tr>' +
            '<td>Amount:</td>' +
            '<td style="color:green">+' + d.Amount + ' €</td>' +
            '</tr>';
        if (d.Tax != "0") {
            data = data + '<tr>' +
                '<td>Amount_Tax:</td>' +
                '<td>' + d.Amount_Tax + '</td>' +
                '</tr>';

            data = data + '<tr>' +
                '<td>Amount_Gross:</td>' +
                '<td>' + d.Amount_Gross + '</td>' +
                '</tr>';

            if (d.Amount_Net != d.Amount) {
                data = data + '<tr>' +
                    '<td>Amount_Net:</td>' +
                    '<td>' + d.Amount_Net + '</td>' +
                    '</tr>';
            }
        }
        if (d.Amount_Extra != null) {
            data = data + '<tr>' +
                '<td>Amount_Extra:</td>' +
                '<td>' + d.Amount_Extra + '</td>' +
                '</tr>';
        }
        if (d.Amount_Tax_Extra != null) {
            data = data + '<tr>' +
                '<td>Amount_Tax_Extra:</td>' +
                '<td>' + d.Amount_Tax_Extra + '</td>' +
                '</tr>';
        }
        if (d.Invoice != null) {
            data = data + '<tr>' +
                '<td>Invoice:</td>' +
                '<td>' + d.Invoice + '</td>' +
                '</tr>';
        }
        data = data + '<tr>' +
            '<td>Status:</td>' +
            '<td>' + d.Status + '</td>' +
            '</tr>';
    }
    if (d.Type == "Monthly Expens") {
        data = data + '<tr>' +
            '<td>Date:</td>' +
            '<td>' + d.Date + '</td>' +
            '</tr>';
        data = data + '<tr>' +
            '<td>Description:</td>' +
            '<td>' + d.Description + '</td>' +
            '</tr>';
        if (d.Description_Extra != null) {
            data = data + '<tr>' +
                '<td>Description_Extra:</td>' +
                '<td>' + d.Description_Extra + '</td>' +
                '</tr>';
        }
        if (d.Tax != "0") {
            data = data + '<tr>' +
                '<td>Tax:</td>' +
                '<td>' + d.Tax + ' €</td>' +
                '</tr>';
        }
        data = data + '<tr>' +
            '<td>Amount:</td>' +
            '<td style="color:red">-' + d.Amount + ' €</td>' +
            '</tr>';
        if (d.Tax != "0") {
            data = data + '<tr>' +
                '<td>Amount_Tax:</td>' +
                '<td>' + d.Amount_Tax + '</td>' +
                '</tr>';
            if (d.Amount_Gross != d.Amount) {
                data = data + '<tr>' +
                    '<td>Amount_Gross:</td>' +
                    '<td>' + d.Amount_Gross + '</td>' +
                    '</tr>';
            }

            data = data + '<tr>' +
                '<td>Amount_Net:</td>' +
                '<td>' + d.Amount_Net + '</td>' +
                '</tr>';
        }
        if (d.Amount_Extra != null) {
            data = data + '<tr>' +
                '<td>Amount_Extra:</td>' +
                '<td>' + d.Amount_Extra + '</td>' +
                '</tr>';
        }
        if (d.Invoice != null) {
            data = data + '<tr>' +
                '<td>Invoice:</td>' +
                '<td>' + d.Invoice + '</td>' +
                '</tr>';
        }
        data = data + '<tr>' +
            '<td>Status:</td>' +
            '<td>' + d.Status + '</td>' +
            '</tr>';
    }
    if (d.Type == "Expenditures") {
        data = data + '<tr>' +
            '<td>Date:</td>' +
            '<td>' + d.Date + '</td>' +
            '</tr>';
        data = data + '<tr>' +
            '<td>Description:</td>' +
            '<td>' + d.Description + '</td>' +
            '</tr>';
        if (d.Description_Extra != null) {
            data = data + '<tr>' +
                '<td>Description_Extra:</td>' +
                '<td>' + d.Description_Extra + '</td>' +
                '</tr>';
        }
        if (d.Tax != "0") {
            data = data + '<tr>' +
                '<td>Tax:</td>' +
                '<td>' + d.Tax + '</td>' +
                '</tr>';
        }
        data = data + '<tr>' +
            '<td>Amount:</td>' +
            '<td style="color:red">-' + d.Amount + ' €</td>' +
            '</tr>';
        if (d.Tax != "0") {
            data = data + '<tr>' +
                '<td>Amount_Tax:</td>' +
                '<td>' + d.Amount_Tax + '</td>' +
                '</tr>';

            if (d.Amount_Gross != d.Amount) {
                data = data + '<tr>' +
                    '<td>Amount_Gross:</td>' +
                    '<td>' + d.Amount_Gross + '</td>' +
                    '</tr>';
            }

            data = data + '<tr>' +
                '<td>Amount_Net:</td>' +
                '<td>' + d.Amount_Net + '</td>' +
                '</tr>';
        }
        if (d.Amount_Extra != null) {
            data = data + '<tr>' +
                '<td>Amount_Extra:</td>' +
                '<td>' + d.Amount_Extra + '</td>' +
                '</tr>';
        }
        if (d.Invoice != null) {
            data = data + '<tr>' +
                '<td>Invoice:</td>' +
                '<td>' + d.Invoice + '</td>' +
                '</tr>';
        }
        data = data + '<tr>' +
            '<td>Status:</td>' +
            '<td>' + d.Status + '</td>' +
            '</tr>';
    }
    if (d.Type == "Earnings") {
        data = data + '<tr>' +
            '<td>Date:</td>' +
            '<td>' + d.Date + '</td>' +
            '</tr>';
        data = data + '<tr>' +
            '<td>Description:</td>' +
            '<td>' + d.Description + '</td>' +
            '</tr>';
        if (d.Tax != "0") {
            data = data + '<tr>' +
                '<td>Tax:</td>' +
                '<td>' + d.Tax + '</td>' +
                '</tr>';
        }
        data = data + '<tr>' +
            '<td>Amount:</td>' +
            '<td style="color:green">+' + d.Amount + ' €</td>' +
            '</tr>';
        if (d.Tax != "0") {
            data = data + '<tr>' +
                '<td>Amount_Tax:</td>' +
                '<td>' + d.Amount_Tax + '</td>' +
                '</tr>';

            data = data + '<tr>' +
                '<td>Amount_Gross:</td>' +
                '<td>' + d.Amount_Gross + '</td>' +
                '</tr>';
            if (d.Amount_Net != d.Amount) {
                data = data + '<tr>' +
                    '<td>Amount_Net:</td>' +
                    '<td>' + d.Amount_Net + '</td>' +
                    '</tr>';
            }
        }
        if (d.Invoice != null) {
            data = data + '<tr>' +
                '<td>Invoice:</td>' +
                '<td>' + d.Invoice + '</td>' +
                '</tr>';
        }
        data = data + '<tr>' +
            '<td>Status:</td>' +
            '<td>' + d.Status + '</td>' +
            '</tr>';
    }
    data = data + '</table>';
    return data
}


//==============================================================================
// Edit Entry ==================================================================
//==============================================================================

function EditSalary(id) {
    $('#formEditSalary').attr('action', "/Payments/EditSalary/" + id);
    $('#editIDSalary').val(id);
};

function EditMonthlyExpenses(id) {
    $('#formEditSalary').attr('action', "/Payments/EditMonthlyExpenses/" + id);
    $('#editIDSalary').val(id);
};

function EditExpenditure(id) {
    $('#formEditSalary').attr('action', "/Payments/EditExpenditure/" + id);
    $('#editIDSalary').val(id);
};

function EditEarnings(id) {
    $('#formEditSalary').attr('action', "/Payments/EditEarnings/" + id);
    $('#editIDSalary').val(id);
};

//==============================================================================
// Delete Entry ================================================================
//==============================================================================

function DeleteSalary(id) {
    $('#formDeleteSalary').attr('action', "/Payments/DeleteSalary/" + id);
};

function DeleteMonthlyExpenses(id) {
    $('#formDeleteSalary').attr('action', "/Payments/DeleteMonthlyExpenses/" + id);
};

function DeleteExpenditure(id) {
    $('#formDeleteSalary').attr('action', "/Payments/DeleteExpenditure/" + id);
};

function DeleteEarnings(id) {
    $('#formDeleteSalary').attr('action', "/Payments/DeleteEarnings/" + id);
};

//==============================================================================
// Create Entry ================================================================
//==============================================================================

function AmountCreateSalary() {
    var gross = $('#Amount_GrossCreateSalary').val().replace(',', '.');
    var net = $('#Amount_NetCreateSalary').val().replace(',', '.');

    if (gross != "" && net != "") {
        var resultTax = gross - net;
        resultTax = resultTax.toFixed(2);
        $('#Amount_TaxCreateSalary').val(resultTax.replace('.', ','));

        var resultPercentage = (100 / (net / gross)) - 100;
        resultPercentage = resultPercentage.toFixed(0);
        $('#TaxCreateSalary').val(resultPercentage);
    }

};

function AmountCreateMonthlyExpenses() {
    var gross = $('#Amount_GrossCreateMonthlyExpenses').val().replace(',', '.');
    var tax = $('#TaxCreateMonthlyExpenses').val();

    if (gross != "" && tax != "") {
        var resultNet = gross / ((tax / 100) + 1);
        var resultTax = gross - resultNet;
        resultNet = resultNet.toFixed(2);
        resultTax = resultTax.toFixed(2);
        $('#Amount_NetCreateMonthlyExpenses').val(resultNet.replace('.', ','));
        $('#Amount_TaxCreateMonthlyExpenses').val(resultTax.replace('.', ','));
    }

};

function AmountCreateExpenditure() {
    var gross = $('#Amount_GrossCreateExpenditure').val().replace(',', '.');
    var tax = $('#TaxCreateExpenditure').val();

    if (gross != "" && tax != "") {
        var resultNet = gross / ((tax / 100) + 1);
        var resultTax = gross - resultNet;
        resultNet = resultNet.toFixed(2);
        resultTax = resultTax.toFixed(2);
        $('#Amount_NetCreateExpenditure').val(resultNet.replace('.', ','));
        $('#Amount_TaxCreateExpenditure').val(resultTax.replace('.', ','));
    }

};

function AmountCreateEarnings() {
    var gross = $('#Amount_GrossCreateEarnings').val().replace(',', '.');
    var net = $('#Amount_NetCreateEarnings').val().replace(',', '.');

    if (gross != "" && net != "") {
        var resultTax = gross - net;
        resultTax = resultTax.toFixed(2);
        $('#Amount_TaxCreateEarnings').val(resultTax.replace('.', ','));

        var resultPercentage = (100 / (net / gross)) - 100;
        resultPercentage = resultPercentage.toFixed(0);
        $('#TaxCreateEarnings').val(resultPercentage);
    }
};

//==============================================================================
// Edit Entry ==================================================================
//==============================================================================

function AmountEditSalary() {
    var gross = $('#Amount_GrossEditSalary').val().replace(',', '.');
    var net = $('#Amount_NetEditSalary').val().replace(',', '.');

    if (gross != "" && net != "") {
        var resultTax = gross - net;
        resultTax = resultTax.toFixed(2);
        $('#Amount_TaxEditSalary').val(resultTax.replace('.', ','));

        var resultPercentage = (100 / (net / gross)) - 100
        resultPercentage = resultPercentage.toFixed(0);
        $('#TaxEditSalary').val(resultPercentage);
    }

};

function AmountEditMonthlyExpenses() {
    var gross = $('#Amount_GrossEditMonthlyExpenses').val().replace(',', '.');
    var tax = $('#TaxEditMonthlyExpenses').val();

    if (gross != "" && tax != "") {
        var resultNet = gross / ((tax / 100) + 1);
        var resultTax = gross - resultNet;
        resultNet = resultNet.toFixed(2);
        resultTax = resultTax.toFixed(2);
        $('#Amount_NetEditMonthlyExpenses').val(resultNet.replace('.', ','));
        $('#Amount_TaxEditMonthlyExpenses').val(resultTax.replace('.', ','));
    }

};

function AmountEditExpenditure() {
    var gross = $('#Amount_GrossEditExpenditure').val().replace(',', '.');
    var tax = $('#TaxEditExpenditure').val();

    if (gross != "" && tax != "") {
        var resultNet = gross / ((tax / 100) + 1);
        var resultTax = gross - resultNet;
        resultNet = resultNet.toFixed(2);
        resultTax = resultTax.toFixed(2);
        $('#Amount_NetEditExpenditure').val(resultNet.replace('.', ','));
        $('#Amount_TaxEditExpenditure').val(resultTax.replace('.', ','));
    }

};

function AmountEditEarnings() {
    var gross = $('#Amount_GrossEditEarnings').val().replace(',', '.');
    var net = $('#Amount_NetEditEarnings').val().replace(',', '.');

    if (gross != "" && net != "") {
        var resultTax = gross - net;
        resultTax = resultTax.toFixed(2);
        $('#Amount_TaxEditEarnings').val(resultTax.replace('.', ','));

        var resultPercentage = (100 / (net / gross)) - 100;
        resultPercentage = resultPercentage.toFixed(0);
        $('#TaxEditEarnings').val(resultPercentage);
    }
};