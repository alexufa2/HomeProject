var grid, dialog, isEdit;

function Edit(e) {
    isEdit = true;
    $('#ID').val(e.data.id);
    $('#Name').val(e.data.record.name);
    $('#Inn').val(e.data.record.inn);
    $('#Address').val(e.data.record.address);
    dialog.open('Редактировать компанию');
}

function Save() {
    var record = {
        ID: 0,
        Name: $('#Name').val(),
        Inn: $('#Inn').val(),
        Address: $('#Address').val()
    };
    var sendUrl = 'http://localhost:5188/Company/Create';
    var sendMethod = 'POST';

    if (isEdit) {
        sendUrl = 'http://localhost:5188/Company/Update';
        sendMethod = 'PUT';
        record.ID = parseInt($('#ID').val());
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
        })
        .fail(function () {
            alert('Failed to save.');
            dialog.close();
        });
}

function Delete(e) {
    if (confirm('Вы уверены?')) {
        varSendUrl = 'http://localhost:5188/Company/Delete?id=' + e.data.id;

        $.ajax(
            {
                contentType: 'application/json',
                url: varSendUrl,
                method: 'DELETE'
            }
        )
            .done(function () {
                grid.reload();
            })
            .fail(function () {
                alert('Failed to delete.');
            });
    }
}

function RedirectToCompanyGoods(e) {
    window.location.replace("/Company/Goods?companyId=" + e.data.id);
}

$(document).ready(function () {
    grid = $('#grid').grid({
        primaryKey: 'id',
        dataSource: 'http://localhost:5188/Company/GetAll',
        uiLibrary: 'bootstrap',
        columns: [
            { field: 'id', title: 'ID', width: 45 },
            { field: 'name', title: 'Наименование', sortable: true },
            { field: 'inn', title: 'ИНН', sortable: true },
            { field: 'address', title: 'Адрес', sortable: true },
            { title: '', field: '', width: 34, type: 'icon', icon: 'glyphicon-list-alt', tooltip: 'Посмотреть товары', events: { 'click': RedirectToCompanyGoods } },
        { title: '', field: 'Edit', width: 34, type: 'icon', icon: 'glyphicon-pencil', tooltip: 'Редактировать', events: { 'click': Edit } },
        { title: '', field: 'Delete', width: 34, type: 'icon', icon: 'glyphicon-remove', tooltip: 'Удалить', events: { 'click': Delete } }
        ],
        pager: { limit: 5, sizes: [2, 5, 10, 20] }
    });

dialog = $('#dialog').dialog({
    uiLibrary: 'bootstrap',
    autoOpen: false,
    resizable: false,
    modal: true
});
$('#btnAdd').on('click', function () {
    isEdit = false;
    $('#ID').val('');
    $('#Name').val('');
    $('#Inn').val('');
    $('#Address').val('');
    dialog.open('Добавить компанию');
});

$('#btnSave').on('click', Save);

$('#btnCancel').on('click', function () {
    dialog.close();
});

    //var urlParams = new URLSearchParams(window.location.search);
    //var myParam = urlParams.get('id');

    //if (myParam)
    //    alert(myParam);
});