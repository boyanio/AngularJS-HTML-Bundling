(function () {
    angular.module('myapp', []).controller('HomeController', ['$scope', function ($scope) {

        $scope.welcomeText = 'Welcome to my app';

    }]).directive('hello', function () {
        return {
            templateUrl: '/Content/hello.html',
            link: function($scope, $element) {
                $scope.today = new Date();
            }
        };
    });
})();