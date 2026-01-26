var grid, dialog, isEdit, contract;

function LoadContractData(contractId) {
    var sendUrl = 'http://localhost:6188/Contract/GetById?id=' + contractId;
    $.ajax(
        {
            contentType: 'application/json',
            url: sendUrl,
            method: 'GET'
        }
    )
        .done(function (data) {
            contract = data;

            var newTitile = 'Исполенения по контракту номер: ' + contract.number;
            $(document).prop('title', newTitile);

            var headerText = newTitile + '. Сумма контракта: ' + contract.total_Sum;
            $('#h3').text(headerText);

            var contractInfoText = 'Сумма исполнений: ' + contract.done_Sum + '; Статус контракта: ' + contract.statusName;
            $('#lblContract').text(contractInfoText);        
        })
        .fail(function () {
            alert('Невозможно загрузить данные по контракту');
        });
}

$(document).ready(function () {
    // данные по Id компании
    var urlParams = new URLSearchParams(window.location.search);
    var contractId = urlParams.get('contractId');

    $('#ContractId').val(contractId);
    LoadContractData(contractId);

    grid = $('#grid').grid({
        primaryKey: 'id',
        dataSource: 'http://localhost:6188/ContractDone/GetByContractId?contractId=' + contractId,
        uiLibrary: 'bootstrap',
        columns: [
            { field: 'id', title: 'ID', width: 45 },
            { field: 'integrationId', hidden: true },
            { field: 'contract_Id', title: 'Contract_Id', hidden: true },
            { field: 'done_Amount', title: 'Сумма исполнения', width: 250 },
            { field: 'statusName', title: 'Статус', width: 100 }
        ],
        pager: { limit: 5, sizes: [2, 5, 10, 20] }
    });

    HideCols();

    dialog = $('#dialog').dialog({
        uiLibrary: 'bootstrap',
        autoOpen: false,
        resizable: false,
        modal: true
    });
    $('#btnAdd').on('click', function () {
        isEdit = false;
        $('#ID').val('');
        $('#IntegrationId').val('00000000-0000-0000-0000-000000000000');
        $('#amount').val('');
        $('#oldAmount').val('');
        dialog.open('Добавить сумму исполнения');
    });

    jQuery.validator.addMethod(
        "money",
        function (value, element) {
            var isValidMoney = /^\d{0,8}(\.\d{0,2})?$/.test(value);
            var isNotEmpty = (value !== "");
            return isNotEmpty && isValidMoney;
        },
        "Введите размер суммы исполнения через '.'"
    );

    $('#contractDoneForm').validate({
        rules: {
            amountField: { money: true }
        },
        messages: {
            amountField: "Введиту размер суммы исполнения через '.'",
        }
    });


    $('#btnSave').on('click', function () {
        var validateRes = $('#contractDoneForm').valid();
        if (!validateRes) { // Not Valid
            return false;
        }
        else {
            Save();
        }
    });

    $('#btnCancel').on('click', function () {
        dialog.close();

        var $validationForm = $('#contractDoneForm');
        var $errLabels = $validationForm.find("label.error");
        $errLabels.remove();

        var $errItems = $validationForm.find(".error");
        $errItems.removeClass("error");
    });
});