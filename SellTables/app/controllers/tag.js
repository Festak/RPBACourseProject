
angular.module('tag', [])
  .controller('TagController', ['$scope', '$http', function ($scope, $http) {
      $scope.tags = {};
      $scope.colors = ("red","yellow","blue");
      $scope.color = "red";

      $scope.getTags = function () {
          $http.get('/Home/GetTags').success(function (result) {
              $scope.tags = result;
          })
              .error(function (data) {
                  console.log(data);
              });
      }

      $scope.getRandomColor = function () {
          var item = colors[Math.floor(Math.random() * colors.length)];
          return item;
      }
  

  }]);