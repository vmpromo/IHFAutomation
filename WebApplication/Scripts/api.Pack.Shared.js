(function ($) {
    $.apiCall = function (options) {
        var config = $.extend({
            type: 'POST',
            data: {},
            contentType: 'application/json; charset=utf-8',
            dataType: "json",
            success: function () { },
            error: function () { }
        }, options);

        $.ajax({
            type: config.type,
            url: config.url,
            contentType: config.contentType,
            data: config.data,
            success: function (result) {
                config.success(result);
            },
            error: function (result) {
                config.error(result);
               // $('#errorDisplay').show().html('<p>' + result.responseText + '</p>');
               
            }
        });
    }
})(jQuery);