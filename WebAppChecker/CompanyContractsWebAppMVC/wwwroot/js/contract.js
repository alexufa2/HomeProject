var grid, dialog;


$(document).ready(function () {

    grid = $('#grid').grid({
        primaryKey: 'id',
        dataSource: 'http://localhost:6188/Contract/GetAll',
        uiLibrary: 'bootstrap',
        columns: [
            { field: 'id', title: 'ID', width: 45 },
            { field: 'integrationId', hidden: true },
            { field: 'number', title: 'Номер' },
            { field: 'good_Name', title: 'Наименование товара' },
            { field: 'good_Count', title: 'Кол-во товара' },
            { field: 'company_Name', title: 'Компания' },
            { field: 'total_Sum', title: 'Сумма' },
            { field: 'done_Sum', title: 'Сумма исполнения' },
            { field: 'statusName', title: 'Статус' },
            { title: '', field: '', width: 34, 
                tmpl: '<a style="color: inherit;" href="/Contract/DoneList?contractId={id}"><span class="glyphicon-calendar glyphicon" style="cursor: pointer;"></span></a>',
                tooltip: 'Посмотреть исполнения'
            }
        ],
        pager: { limit: 5, sizes: [2, 5, 10, 20] }
    });
 });