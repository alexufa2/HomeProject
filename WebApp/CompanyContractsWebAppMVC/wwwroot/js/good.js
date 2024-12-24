var grid, dialog, isEdit;

function Edit(e) {
    isEdit = true;
    $('#ID').val(e.data.id);
    $('#Name').val(e.data.record.name);
    $('#Description').val(e.data.record.description);
    $('#Measurement_Unit').val(e.data.record.measurement_Unit);
    dialog.open('Редактировать товар');
}

function Save() {
    var record = {
        ID: 0,
        Name: $('#Name').val(),
        Description: $('#Description').val(),
        Measurement_Unit: $('#Measurement_Unit').val()
    };
    var sendUrl = 'http://localhost:5188/Good/Create';
    var sendMethod = 'POST';

    if (isEdit) {
        sendUrl = 'http://localhost:5188/Good/Update';
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
        var sendUrl = 'http://localhost:5188/Good/Delete?id=' + e.data.id;

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
                alert('Не удалось удалить запись.');
            });
    }
}
$(document).ready(function () {
    grid = $('#grid').grid({
        primaryKey: 'id',
        dataSource: 'http://localhost:5188/Good/GetAll',
        uiLibrary: 'bootstrap',
        columns: [
            { field: 'id', title: 'ID', width: 45 },
            { field: 'name', title: 'Наименование', sortable: true },
            { field: 'description', title: 'Описание', sortable: true },
            { field: 'measurement_Unit', title: 'Единица измерения', sortable: true },
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
        $('#Description').val('');
        $('#Measurement_Unit').val('');
        dialog.open('Добавить товар');
    });

    $('#goodForm').validate({
        rules: {
            nameField: { required: true },
            measureUnitField: { required: true }
        },
        messages: {
            nameField: { required: "Поле является обязательным" },
            measureUnitField: { required: "Поле является обязательным" }
        }
    });


    $('#btnSave').on('click', function () {
        var validateRes = $('#goodForm').valid();
        if (!validateRes) { // Not Valid
            return false;
        } else {
            Save();
        }
    });

    $('#btnCancel').on('click', function () {
        dialog.close();

        var $validationForm = $('#goodForm');
        var $errLabels = $validationForm.find("label.error");
        $errLabels.remove();

        var $errItems = $validationForm.find(".error");
        $errItems.removeClass("error");
    });
});