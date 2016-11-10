
angular.module('user', [])
  .controller('UserController', ['$scope', '$http', function ($scope, $http) {
      $scope.users = [];
      $scope.user = {};

      $scope.getUsers = function () {
          $http.get('/Home/GetUsers').success(function (result) {
              $scope.users = result;
          })
              .error(function (data) {
                  console.log(data);
              });
      }


      $scope.getCurrentUser = function (noteText) {
          $http.get('/User/GetCurrentUser').success(function (result) {
              $scope.user = result;
          })
             .error(function (data) {
                 console.log(data);
             });
      }

  }]);