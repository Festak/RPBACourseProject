angular.module('admin', [])
  .controller('AdminController', ['$scope', '$http', '$window', function ($scope, $http, $window) {

      $scope.DeleteUser = function (name) {
          $http.post('/Admin/DeleteUser', { userName: name })
              .success(function (data) {
                  $window.location.href = '/Admin/Index/';
              });
      };

      $scope.BanUser = function (user) {
          $http.post('/Admin/BanUser', { userName: user }).success(function (result) {
              $window.location.href = '/Admin/Index/';
          });

      }

      $scope.UnbanUser = function (user) {
          $http.post('/Admin/UnbanUser', { userName: user }).success(function (result) {
              $window.location.href = '/Admin/Index/';
          });
      }

  }]);