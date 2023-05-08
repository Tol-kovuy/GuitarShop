
$(function () {
    $.ajax({
        url: '@Url.Action("GetCartCount", "Cart")',
        success: function (count) {
            $('#cart-count').text(count);
        }
    });
});