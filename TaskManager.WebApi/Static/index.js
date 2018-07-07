(function (angular) {

    var taskManager = angular.module('TaskManager', ['ngRoute']);

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

    taskManager.controller('addTaskController', function ($scope) {
        $scope.message = "Add task form";
    });

    taskManager.controller('taskListController', function ($scope) {
        $scope.message = "Task list form";
    });

})(angular);