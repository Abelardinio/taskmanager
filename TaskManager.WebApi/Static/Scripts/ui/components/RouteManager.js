ui.components.RouteManager = (function () {
    function constructor(location) {
        var that = this;

        that.location = location;
    }

    constructor.prototype.location = null;

    constructor.prototype.GoToTasksRoute = function (taskId) {
        var that = this;

        debugger;
        that.location.skipReload().path('/tasks/' + taskId).replace();
    }

    return constructor;
})();