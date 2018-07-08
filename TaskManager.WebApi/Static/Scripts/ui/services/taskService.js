ui.services.TaskService = (function (angular) {
    function constructor($http, notifyManager) {
        var that = this;

        that._$http = $http;
        that._notifyManager = notifyManager;
    }

    constructor.prototype._$http = null;
    constructor.prototype._notifyManager = null;

    constructor.prototype.add = function (taskInfo) {
        var that = this;

        return  that._$http.post("api/task", angular.toJson(taskInfo))
            .then(function () {
                that._notifyManager.notifySuccess("Task has been successfuly added.");
            })
            .catch(function () {
                that._notifyManager.notifyError("Task has not been added. Something went wrong.");
            });
    };

    constructor.prototype.get = function () {
        var that = this;

        return that._$http.get("api/task").catch(function () {
            that._notifyManager.notifyError("Something went wrong.");
        });
    };

    constructor.prototype.getTask = function (taskId) {
        var that = this;

        return that._$http.get("api/task/" + taskId).catch(function() {
            that._notifyManager.notifyError("Something went wrong.");
        });
    };

    return constructor;
})(angular);