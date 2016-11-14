angular.module('test', [])
  .controller('TesController', ['$scope', '$http', '$window', function ($scope, $http, $window) {

      $scope.DeleteUser = function (name) {
          $http.post('/Admin/DeleteUser', { userName: name })
              .success(function (data) {
                  $window.location.href = '/Admin/Index/';
              });
      };
 

  }]);