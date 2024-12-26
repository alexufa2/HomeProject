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
            alert('Ошибка при сохранении.');
            dialog.close();
        });
}

function Delete(e) {
    if (confirm('Вы уверены?')) {
        var sendUrl = 'http://localhost:5188/Company/Delete?id=' + e.data.id;

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

$(document).ready(function () {
    grid = $('#grid').grid({
        primaryKey: 'id',
        dataSource: 'http://localhost:5188/Company/GetAll',
        uiLibrary: 'bootstrap',
        columns: [
            { field: 'id', title: 'ID', width: 45 },
            { field: 'name', title: 'Наименование' },
            { field: 'inn', title: 'ИНН'},
            { field: 'address', title: 'Адрес'},
            {
                title: '', field: '', width: 34,
                tmpl: '<a style="color: inherit;" href="/Company/Goods?companyId={id}"><span class="glyphicon-list-alt glyphicon" style="cursor: pointer;"></span></a>',
                tooltip: 'Посмотреть товары'
            },
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

    $('#companyForm').validate({
        rules: {
            nameField: { required: true },
            innField: {
                required: true,
                digits: true,
                minlength: 12,
                maxlength: 12
            },
            addressField: { required: true }
        },
        messages: {
            nameField: { required: "Поле является обязательным" },
            innField: {
                required: "Поле является обязательным",
                digits: "Допустимы только цифры",
                minlength: "Должно быть 12 цифр",
                maxlength: "Вы ввели больше 12 цифр" 
            },
            addressField: { required: "Поле является обязательным" }
        }
    });


    $('#btnSave').on('click', function () {
        var validateRes = $('#companyForm').valid();
        if (!validateRes) { // Not Valid
            return false;
        } else {
            Save();
        }
    });

    $('#btnCancel').on('click', function () {
        dialog.close();

        var $validationForm = $('#companyForm');
        var $errLabels = $validationForm.find("label.error");
        $errLabels.remove();

        var $errItems = $validationForm.find(".error");
        $errItems.removeClass("error");
    });

});