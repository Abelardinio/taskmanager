ui.components.NotifyManager = (function() {
    function constructor($notify) {
        var that = this;

        that.$notify = $notify;
    }

    constructor.prototype.$notify = null;

    constructor.prototype.notifySuccess = function(message) {
        var that = this;

        that._notify(message, "success-message");
    };

    constructor.prototype.notifyError = function(message) {
        var that = this;

        that._notify(message, "fail-message");
    };

    constructor.prototype._notify = function(message, className) {
        var that = this;

        that.$notify({
            message: message,
            classes: className,
            templateUrl: "static/templates/notification.html",
            position: "right",
            duration: 1500
        });
    };

    return constructor;
})();