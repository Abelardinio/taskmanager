(function (ui, angular) {
    ui.taskManager.controller("menuController", function ($scope, $location) {
        $scope.menuClass = function (page) {
            var current = $location.path().substring(1);
            return page === current ? "active" : "";
        };
    });

    ui.taskManager.controller('addTaskController', ['$scope', '$http', 'notify', function ($scope, $http, $notify) {
        var manager = new ui.components.NotifyManager($notify);
        var service = new ui.services.TaskService($http, manager);
        var controller = new ui.controllers.AddTaskController($scope, service);
    }]);

    ui.taskManager.controller('taskListController', ['$scope', '$http', 'notify', 'location', '$routeParams', function ($scope, $http, $notify, location, $routeParams) {
        var manager = new ui.components.NotifyManager($notify);
        var routeManager = new ui.components.RouteManager(location);
        var service = new ui.services.TaskService($http, manager);
        var controller = new ui.controllers.TasksController($scope, service, parseInt($routeParams.Id, 10), routeManager);
    }]);
})(ui, angular);