
angular.module('tag', [])
  .controller('TagController', ['$scope', '$http', function ($scope, $http) {
      $scope.tags = {};

      $scope.getTags = function () {
          $http.get('/Home/GetTags').success(function (result) {
              $scope.tags = result;
              console.log(result);
          })
              .error(function (data) {
                  console.log(data);
              });
      }
  

  }]);