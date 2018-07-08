(function (angular) {

    var taskManager = angular.module('TaskManager', ['ngRoute', 'cgNotify']);

    taskManager.config(function ($routeProvider, $locationProvider) {
        $routeProvider
            .when('/',
                {
                    redirectTo: '/tasks'
                })
            .when('/tasks/add',
                {
                    templateUrl: 'static/templates/addTask.html',
                    controller: 'addTaskController'
                })
            .when('/tasks',
                {
                    templateUrl: 'static/templates/taskList.html',
                    controller: 'taskListController'
            });

        $locationProvider.html5Mode(true);
    });

    taskManager.controller("menuController", function ($scope, $location) {
        $scope.menuClass = function (page) {
            var current = $location.path().substring(1);
            return page === current ? "active" : "";
        };
    });

    taskManager.controller('addTaskController', ['$scope', '$http', 'notify', function ($scope, $http, $notify) {
        $scope.submit = function () {
            $http.post("api/task", angular.toJson($scope.taskInfo))
                .then(function (data, status, headers, config) {
                    $notify({
                        message: "Task has been successfuly added.",
                        classes: "success-message",
                        templateUrl: "static/templates/notification.html",
                        position: "right",
                        duration: 1500
                    });
                })
                .catch(function (data, status, header, config) {
                    $notify({
                        message: "Task has not been added. Something went wrong.",
                        classes: "fail-message",
                        templateUrl: "static/templates/notification.html",
                        position: "right",
                        duration: 1500
                    });
                });
        }
    }]);

    taskManager.controller('taskListController', function ($scope, $http) {
        $scope.tasks = [];
        $http.get("api/task").then(function (data, status, headers, config) {
            $scope.tasks = data.data;
        });
        $scope.message = "Task list form";
    });

})(angular);