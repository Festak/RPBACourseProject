
angular.module('user', [])
  .controller('UserController', ['$scope', '$http', function ($scope, $http) {
      $scope.users = [];
      $scope.user = {};
      $scope.isAdmin = false;

      $scope.getUsers = function () {
          $http.get('/Home/GetUsers').success(function (result) {
              $scope.users = result;
          })
              .error(function (data) {
                  console.log(data);
              });
      }


      $scope.getCurrentUser = function () {
          $http.get('/User/GetCurrentUser').success(function (result) {
              $scope.user = result;
          })
             .error(function (data) {
                 console.log(data);
             });
      }


      $scope.isCurrentUserIsAnAdmin = function () {
          $http.post('/User/IsCurrentUserIsAnAdmin').success(function (result) {
             $scope.isAdmin = true;
              return result;
          });
      }

  }]);