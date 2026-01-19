var grid, dialog;

function FillGoodsDdl() {
    var sendUrl = 'http://localhost:5188/Good/GetAll';
    $.ajax(
        {
            contentType: 'application/json',
            url: sendUrl,
            method: 'GET'
        }
    )
        .done(function (data) {
            data.unshift({ id: '', name: '' });

            $ddl = $('#GoodsDdl');
            $ddl.children('option').remove();

            $.each(data, function (i, item) {
                $ddl.append($('<option>', {
                    value: item.id,
                    text: item.name
                }));
            });
        })
        .fail(function () {
            alert('Невозможно загрузить данные по товарам');
        });
}


function Save() {
    var record = {
        Number: $('#Number').val(),
        Company_Id: $('#companyDdl').find(":selected").val(),
        Good_Id: $('#GoodsDdl').find(":selected").val(),
        Good_Count: $('#goodCount').val(),
        Total_Sum: $('#contractSum').val()
    };

    var sendUrl = 'http://localhost:5188/Contract/Create';
    var sendMethod = 'POST';

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
            alert('Ошибка при сохранении.');
            dialog.close();
        });
}

function CancelContract(e) {
    if (e.data.record.status === 'Canceled') {
        alert('Контракт уже отменен');
        return;
    }

    if (e.data.record.status === 'Finished') {
        alert('Контракт уже завершен его нельзя отменить');
        return;
    }

    if (confirm('Вы уверены?')) {
        var sendUrl = 'http://localhost:5188/Contract/Cancel?Id=' + e.data.record.id;

        $.ajax(
            {
                contentType: 'application/json',
                url: sendUrl,
                method: 'PUT'
            }
        )
            .done(function () {
                grid.reload();
            })
            .fail(function () {
                alert('Не удалось удалить запись.');
            });
    }
}

function Delete(e) {
    if (confirm('Вы уверены?')) {
        var sendUrl = 'http://localhost:5188/Contract/Delete?id=' + e.data.id;

        $.ajax(
            {
                contentType: 'application/json',
                url: sendUrl,
                method: 'DELETE'
            }
        )
            .done(function () {
                grid.reload();
            })
            .fail(function () {
                alert('Не удалось удалить запись..');
            });
    }
}

function FillCompanyDdl(goodId) {
    if (!goodId)
        return;

    var sendUrl = 'http://localhost:5188/CompanyGoodPrice/GetByGoodId?goodId=' + goodId;
    $.ajax(
        {
            contentType: 'application/json',
            url: sendUrl,
            method: 'GET'
        }
    )
        .done(function (data) {
            if (data.length === 0) {
                alert('Нет данных по компаниям с выбранным товаром');
                return;
            }

            data.unshift({ company_Id: '', company_Name: '', price: '' });

            $ddl = $('#companyDdl');
            $ddl.children('option').remove();

            $.each(data, function (i, item) {
                var itemText = '';
                if (item.company_Name) {
                    itemText = item.company_Name + ' - стоимость: ' + item.price;
                }

                $ddl.append(
                    $('<option>',
                        {
                            value: item.company_Id,
                            text: itemText
                        }
                    ).data('price', item.price)
                );
            });
        })
        .fail(function () {
            alert('Невозможно загрузить данные по компаниям');
        });
}

$(document).ready(function () {

    grid = $('#grid').grid({
        primaryKey: 'id',
        dataSource: 'http://localhost:5188/Contract/GetAll',
        uiLibrary: 'bootstrap',
        columns: [
            { field: 'id', title: 'ID', width: 45 },
            { field: 'integrationId', hidden: true },
            { field: 'number', title: 'Номер' },
            { field: 'good_Id', hidden: true },
            { field: 'good_Name', title: 'Наименование товара' },
            { field: 'good_Count', title: 'Кол-во товара' },
            { field: 'company_Id', hidden: true },
            { field: 'company_Name', title: 'Компания' },
            { field: 'total_Sum', title: 'Сумма' },
            { field: 'done_Sum', title: 'Сумма исполнения' },
            { field: 'status', hidden: true },
            { field: 'statusName', title: 'Статус' },
            { title: '', field: '', width: 34, 
                tmpl: '<a style="color: inherit;" href="/Contract/DoneList?contractId={id}"><span class="glyphicon-calendar glyphicon" style="cursor: pointer;"></span></a>',
                tooltip: 'Посмотреть исполнения'
            },
            { title: '', field: 'Edit', width: 34, type: 'icon', icon: 'glyphicon-ban-circle', tooltip: 'Отменить контракт', events: { 'click': CancelContract } },
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

    $('#GoodsDdl').on('change', function () {
        var goodId = $(this).find(":selected").val();
        FillCompanyDdl(goodId);
    });

    $('#companyDdl').on('change', function () {
        var price = $(this).find(":selected").data('price');
        var goodCount = $('#goodCount').val();

        if (price && goodCount) {
            $('#contractSum').val(price * goodCount);
        }
    });


    $('#btnAdd').on('click', function () {
        FillGoodsDdl();
        $('#Number').val('');
        $('#companyDdl').val('').change();
        $('#goodCount').val('');
        $('#contractSum').val('');

        dialog.open('Добавить товар с ценой');
    });
        

    $('#contractForm').validate({
        rules: {
            NumberField: { required: true },
            GoodsDdlField: { required: true },
            goodCountField: { required: true },
            companyDdlField: { required: true }
        },
        messages: {
            NumberField: 'Введите номер контракта',
            GoodsDdlField: 'Выберите товар',
            goodCountField: 'Укажите кол-во товара',
            companyDdlField: 'Укажите компанию'
        }
    });


    $('#btnSave').on('click', function () {
        var validateRes = $('#contractForm').valid();
        if (!validateRes) { // Not Valid
            return false;
        }
        else {
            Save();
        }
    });

    $('#btnCancel').on('click', function () {
        dialog.close();

        //var $validationForm = $('#contractForm');
        //var $errLabels = $validationForm.find("label.error");
        //$errLabels.remove();

        //var $errItems = $validationForm.find(".error");
        //$errItems.removeClass("error");
    });
});