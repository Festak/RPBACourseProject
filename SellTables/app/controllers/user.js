
angular.module('user', [])
  .controller('UserController', ['$scope', '$http', function ($scope, $http) {
      $scope.users = {};

      $scope.getUsers = function () {
          $http.get('/Home/GetUsers').success(function (result) {
              $scope.users = result;
          })
              .error(function (data) {
                  console.log(data);
              });
      }
      $scope.save = function (noteText) {
          console.log(noteText);
      }

  }]);