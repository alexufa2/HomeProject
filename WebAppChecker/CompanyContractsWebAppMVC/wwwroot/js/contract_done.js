var grid, dialog, isEdit, contract;

function HideCols() {
    if (grid && contract) {
        if (contract.status === 'Canceled' || contract.status === 'Finished') {
            grid.hideColumn('Edit');
            grid.hideColumn('Delete');
        }
    }
}

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

            if (contract.status === 'Canceled' || contract.status === 'Finished') {
                $('#btnAdd').prop('disabled', 'disabled');
            }

            HideCols();
        })
        .fail(function () {
            alert('Невозможно загрузить данные по контракту');
        });
}

function Edit(e) {
    isEdit = true;
    $('#ID').val(e.data.id);
    $('#amount').val(e.data.record.done_Amount);
    $('#oldAmount').val(e.data.record.done_Amount);
    dialog.open('Редактировать исполнение');
}

function Save() {
    var newAmount = 0;
    var newAmountVal = $('#amount').val();
    if (newAmountVal !== '') {
        newAmount = parseFloat(newAmountVal).toFixed(2);
    }
   
    var oldAmount = 0;
    var oldAmountVal = $('#oldAmount').val();
    if (oldAmountVal !== '') {
        oldAmount = parseFloat(oldAmountVal).toFixed(2);
    }

    var contractRemind = contract.total_Sum - contract.done_Sum;
    var amountDelta = newAmount - oldAmount

    if (contractRemind < amountDelta) {
        alert('Введена слишком большая сумма. Остаток по контракту: ' + contractRemind);
        return;
    }

    var record = {
        Id: 0,
        Contract_Id: contract.id,
        Done_Amount: $('#amount').val()
    };
    var sendUrl = 'http://localhost:6188/ContractDone/Create';
    var sendMethod = 'POST';

    if (isEdit) {
        sendUrl = 'http://localhost:6188/ContractDone/Update';
        sendMethod = 'PUT';
        record.Id = parseInt($('#ID').val());
    }

    $.ajax(
        {
            contentType: 'application/json',
            url: sendUrl,
            data: JSON.stringify(record),
            method: sendMethod
        })
        .done(function () {
            dialog.close();
            grid.reload();
            LoadContractData(contract.id);
        })
        .fail(function () {
            alert('Ошибка при сохранении.');
            dialog.close();
        });
}

function Delete(e) {
    if (confirm('Вы уверены?')) {
        var sendUrl = 'http://localhost:6188/ContractDone/Delete?Id=' + e.data.id;

        $.ajax(
            {
                contentType: 'application/json',
                url: sendUrl,
                method: 'DELETE'
            }
        )
            .done(function () {
                grid.reload();
                LoadContractData(contract.id);
            })
            .fail(function () {
                alert('Не удалось удалить запись.');
            });
    }
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
            { field: 'contract_Id', title: 'Contract_Id', hidden: true },
            { field: 'done_Amount', title: 'Сумма исполнения', width: 450 },
            { field: 'statusName', title: 'Статус' },
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
        "Введиту размер суммы исполнения через '.'"
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