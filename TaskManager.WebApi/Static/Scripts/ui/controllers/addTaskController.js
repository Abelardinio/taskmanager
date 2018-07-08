ui.controllers.AddTaskController = (function() {
    function constructor($scope, taskService) {
        var that = this;

        that._$scope = $scope;
        that._taskService = taskService;
        that._$scope.onSubmitButtonClick = that.onSubmitButtonClick;
    }

    constructor.prototype._$scope = null;
    constructor.prototype._taskService = null;

    constructor.prototype.onSubmitButtonClick = function () {
        var that = this;

        that._taskService.add(that._$scope.taskInfo);
    };

    return constructor;
})();