
angular.module('creative', [])
  .controller('CreativeController', ['$scope', '$http', function ($scope, $http) {
      $scope.creatives = {};

      $scope.getCreatives = function () {
          $http.get('/Home/GetCreatives').success(function (result) {
              $scope.creatives = result;
              console.log(result);
          })
          .error(function (data) {
              console.log(data);
          });
      }



      $scope.save = function (noteText) {
          console.log(noteText);
      }

  }]);