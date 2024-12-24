var grid, dialog, isEdit, company;

function FillGoodsDdl() {
    var companyId = $('#CompanyId').val();
    FillGoodsDdlByCompanyId(companyId);
}

function FillGoodsDdlByCompanyId(companyId) {
    var sendUrl = 'http://localhost:5188/CompanyGoodPrice/GetNotExistGoodsByCompanyId?companyId=' + companyId;
    $.ajax(
        {
            contentType: 'application/json',
            url: sendUrl,
            method: 'GET'
        }
    )
        .done(function (data) {
            if (data.length === 0) {
                $('#btnAdd').prop('disabled', 'disabled');
            }
            else {
                $('#btnAdd').prop('disabled', false);
            }

            data.unshift({ id: '', name: '' });

            $ddl = $('#GoodsDdl');
            $ddl.children('option').remove();
            $ddl.prop('disabled', false);

            $.each(data, function (i, item) {
                $ddl.append($('<option>', {
                    value: item.id,
                    text: item.name
                }));
            });
        })
        .fail(function () {
            alert('Невозможно загрузить данные по товарам компании');
        });
}
function Edit(e) {
    isEdit = true;
    var $ddl = $('#GoodsDdl');

    $ddl.append($('<option>', {
        value: e.data.record.good_Id,
        text: e.data.record.good_Name
    }));

    $ddl.val(e.data.record.good_Id).change();
    $ddl.prop('disabled', 'disabled');

    $('#Price').val(e.data.record.price);
    dialog.open('Редактировать компанию');
}

function Save() {
    var record = {
        Company_Id: $('#CompanyId').val(),
        Good_Id: $('#GoodsDdl').find(":selected").val(),
        Price: $('#Price').val()
    };
    var sendUrl = 'http://localhost:5188/CompanyGoodPrice/Create';
    var sendMethod = 'POST';

    if (isEdit) {
        sendUrl = 'http://localhost:5188/CompanyGoodPrice/Update';
        sendMethod = 'PUT';
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
            FillGoodsDdl();
        })
        .fail(function () {
            alert('Ошибка при сохранении.');
            dialog.close();
        });
}

function Delete(e) {
    if (confirm('Вы уверены?')) {
        var companyId = $('#CompanyId').val();
        var sendUrl = 'http://localhost:5188/CompanyGoodPrice/Delete?companyId=' + companyId + '&goodId=' + e.data.record.good_Id;

        $.ajax(
            {
                contentType: 'application/json',
                url: sendUrl,
                method: 'DELETE'
            }
        )
            .done(function () {
                grid.reload();
                FillGoodsDdl();
            })
            .fail(function () {
                alert('Не удалось удалить запись.');
            });
    }
}


$(document).ready(function () {
    // данные по Id компании
    var urlParams = new URLSearchParams(window.location.search);
    var companyId = urlParams.get('companyId');
    $('#CompanyId').val(companyId);

    var sendUrl = 'http://localhost:5188/Company/GetById?id=' + companyId;
    $.ajax(
        {
            contentType: 'application/json',
            url: sendUrl,
            method: 'GET'
        }
    )
        .done(function (data) {
            company = data;
            $('#h3').text('Товары компании ' + company.name + ' ' + company.address);
            var newTitile = 'Товары компании ' + company.name;
            $(document).prop('title', newTitile);
        })
        .fail(function () {
            alert('Невозможно загрузить данные по компании');
        });

    FillGoodsDdlByCompanyId(companyId);

    grid = $('#grid').grid({
        //primaryKey: 'id',
        dataSource: 'http://localhost:5188/CompanyGoodPrice/GetByCompanyId?companyId=' + companyId,
        uiLibrary: 'bootstrap',
        columns: [
            { field: 'company_Id', title: 'Company_Id', hidden: true },
            { field: 'good_Id', title: 'Good_Id', hidden: true },
            { field: 'good_Name', title: 'Наименование товара', sortable: true },
            { field: 'price', title: 'Стоимость за единицу', sortable: true },
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
        $('#GoodsDdl').val('').change();
        $('#Price').val('');
        dialog.open('Добавить товар с ценой');
    });

    jQuery.validator.addMethod(
        "money",
        function (value, element) {
            var isValidMoney = /^\d{0,5}(\.\d{0,2})?$/.test(value);
            var isNotEmpty = (value !== "");
            return isNotEmpty && isValidMoney;
        },
        "Введиту стоимость товара через '.'"
    );

    $('#companyGoodsForm').validate({
        rules: {
            priceField: { money: true },
            goodsDdlField: { required: true }
        },
        messages: {
            priceField: "Введиту стоимость товара через '.'",
            goodsDdlField: "Выберите товар из списка"
        }
    });


    $('#btnSave').on('click', function () {
        var validateRes = $('#companyGoodsForm').valid();
        if (!validateRes) { // Not Valid
            return false;
        }
        else {
            Save();
        }
    });

    $('#btnCancel').on('click', function () {
        dialog.close();

        var $validationForm = $('#companyGoodsForm');
        var $errLabels = $validationForm.find("label.error");
        $errLabels.remove();

        var $errItems = $validationForm.find(".error");
        $errItems.removeClass("error");
        FillGoodsDdl();
    });
});