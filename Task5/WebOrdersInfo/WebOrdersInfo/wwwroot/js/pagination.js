$('body').on('click', '.page-item', function () {
    var page = $(this).children('a').attr('value');
    updatePageFromPagination(page);
});

function gettoken() {
    var token = '@Html.AntiForgeryToken()';
    token = $(token).val();
    return token;
}


function updatePageFromPagination(page) {
    let form = $('#__AjaxAntiForgeryForm');
    let token = $('input[name="__RequestVerificationToken"]', form).val();

    $.ajax({
        type: 'POST',
        url: '/Orders/Index',
        data: {
            __RequestVerificationToken: token,
            PageNumber: page
        },
        //dataType: 'json',
        success: function (response) {
            console.log('success!');
            $('#ordersContainer').html(response);
        },
        error: function (jqXHR, exception) {
            if (jqXHR.status === 404) {
                window.location.href = '/Order/Index';
            }
        }
    });
};
