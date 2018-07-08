var ui = {
    components: {},
    controllers: {},
    services: {}
};

ui.taskManager = (function(angular) {
    return angular.module('TaskManager', ['ngRoute', 'cgNotify']);
})(angular);