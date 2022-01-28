$(function() {
    $('#datepickerFrom').datepicker({
        inline: true
    });
});

$(function () {
    $('#datepickerTo').datepicker({
        inline: true
    });
});

$(function () {
    let minPrice = parseInt($('#MinPrice').val());
    let maxPrice = parseInt($('#MaxPrice').val());
    let priceFrom = $('#PriceFrom').val();
    let priceTo = $('#PriceTo').val();

    $("#sliderPrice").slider({
        range: true,
        values: [priceFrom, priceTo],
        min: minPrice,
        max: maxPrice,
        //values: [priceFrom, priceTo],
        slide: function (event, ui) {
            $("#amountPrice").val("от " + ui.values[0] + " - до " + ui.values[1]);
            //$("#PriceFrom").val($("#sliderPrice").slider("values", 0));
            //$("#PriceTo").val($("#sliderPrice").slider("values", 1));
        },
        change: function() {
            $("#PriceFrom").val($("#sliderPrice").slider("values", 0));
            $("#PriceTo").val($("#sliderPrice").slider("values", 1));
        }
    });
    //$slide.slider('option', 'min', $('#MinPrice').val());
    //$slide.slider('option', 'max', $('#MaxPrice').val());
    //$("#sliderPrice").slider("option", "min", minPrice);
    //$("#sliderPrice").slider("values", "1", maxPrice);
    //$("#sliderPrice").slider("option", "values", [priceFrom, priceTo]);
    $("#amountPrice").val("от " + $("#sliderPrice").slider("values", 0) +
        " - до " + $("#sliderPrice").slider("values", 1));
});
