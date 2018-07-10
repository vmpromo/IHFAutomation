
var virtualConsole = function () {
    if (typeof console === 'undefined') {
        return {
            log: function () { },
            error: function (message) { alert(message); }
        }
    } else {
        return {
            log: function (message) { console.log(message); },
            error: function (message) { console.error(message); }
        }
    }
} ();