(function (ui) {
    ui.taskManager.directive("onElementScroll", function () {
        return {
            link: function(scope, element, attrs) {
                element.bind("scroll",
                    function() {
                        if (element[0].scrollTop + element[0].offsetHeight >= element[0].scrollHeight) {
                            scope.$apply(attrs.onScrollToBottom);
                        }
                    });
            }
        };
    });
})(ui);