ui.controllers.TasksController = (function () {
    function constructor($scope, taskService, taskId, routeManager) {
        var that = this;

        that._$scope = $scope;
        that._taskService = taskService;
        that._routeManager = routeManager;
        that._$scope.tasks = [];
        that._$scope.onRowClick = that.onRowClick.bind(that);

        that._taskService.get().then(function(data) {
            that._$scope.tasks = data.data;
            that.onRowClick(taskId);
        });
    }

    constructor.prototype._taskService = null;
    constructor.prototype._routeManager = null;

    constructor.prototype.onRowClick = function(taskId) {
        var that = this;

        that._$scope.selectedTaskId = taskId;
        that._routeManager.GoToTasksRoute(taskId);
        that._taskService.getTask(taskId).then(function(data) {
            that._$scope.selectedTask = data.data;
        });
    };

    return constructor;
})();