(function (ui) {
    ui.taskManager.config(function($routeProvider, $locationProvider) {
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
            })
            .when('/tasks/:Id',
                {
                    templateUrl: 'static/templates/taskList.html',
                    controller: 'taskListController'
                });

        $locationProvider.html5Mode(true);
    });

    ui.taskManager.factory('location', [
        '$location',
        '$route',
        '$rootScope',
        function ($location, $route, $rootScope) {
            $location.skipReload = function () {
                var lastRoute = $route.current;
                var un = $rootScope.$on('$locationChangeSuccess', function () {
                    $route.current = lastRoute;
                    un();
                });
                return $location;
            };
            return $location;
        }
    ]);
})(ui);