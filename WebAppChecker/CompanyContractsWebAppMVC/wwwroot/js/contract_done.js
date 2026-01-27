var grid, contractId, contract;

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
    contractId = urlParams.get('contractId');

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

    var gridReload = grid.reload;
    grid.reload = new function customReload() {
        gridReload();
        LoadContractData(contractId);
    }
});