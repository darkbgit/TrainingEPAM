successRequest = function (data, textStatus, XHR) {
    $('#filters').html(data);
    bindButton();
}

failRequest = function () {
    jQuery("#filtercontent").html().val('');
}


window.onload = function () {
    loadFilters();
};

bindButton = function() {
    let clearButton = document.querySelector('#clearFilters');
    let applyButton = document.querySelector('#applyFilters');
    clearButton.onclick = clearFilters;
    applyButton.onclick = applyFilters;
}


function loadFilters() {
    let form = $('#__AjaxAntiForgeryForm');
    let token = $('input[name="__RequestVerificationToken"]', form).val();
    $.ajax({
        type: 'GET',
        url: '/OrdersFilter/Index',
        data: {
            __RequestVerificationToken: token
        },
        //dataType: 'json',
        success: function (response) {
            console.log('success!');
            $('#filtersContainer').html(response);
            bindButton();
        },
        error: function() {
            $('#filtersContainer').html('');
        }
    });
}

function clearFilters() {
    let form = $('#__AjaxAntiForgeryForm');
    let token = $('input[name="__RequestVerificationToken"]', form).val();

    $.ajax({
        type: 'POST',
        url: '/OrdersFilter/Index',
        data: {
            __RequestVerificationToken: token,
            IsClear: true
        },
        //dataType: 'json',
        success: function () {
            loadOrders();
            loadFilters();
            bindButton();
        }
    });
};

function applyFilters() {
    let form = $('#__AjaxAntiForgeryForm');
    let token = $('input[name="__RequestVerificationToken"]', form).val();

    $.ajax({
        type: 'POST',
        url: '/OrdersFilter/Index',
        data: $('#filterForm').serialize() + "&__RequestVerificationToken=" + token,
        //},
        //dataType: 'json',
        success: function (response) {
            $('#filtersContainer').html(response);
            bindButton();
            loadOrders();
        }
        //error: function(response) {
        //    $('#filtersContainer').html(response);
        //    bindButton();
        //}
    })
    .fail(function(response) {
        $('#filtersContainer').html(response.responseText);
        bindButton();
    });
};

function loadOrders() {
    let form = $('#__AjaxAntiForgeryForm');
    let token = $('input[name="__RequestVerificationToken"]', form).val();
    $.ajax({
        type: 'POST',
        url: '/Orders/Index',
        data: {
            __RequestVerificationToken: token
        },
        //dataType: 'json',
        success: function (response) {
            console.log('success!');
            $('#ordersContainer').html(response);
        }
    });
}