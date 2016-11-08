
angular.module('chapter', [])
  .controller('ChapterController', ['$scope', '$http', function ($scope, $http) {
      $scope.tags = {};

      $scope.getChapters = function () {
          $http.get('/Home/GetTags').success(function (result) {
              $scope.tags = result;
              console.log(result);
          })
              .error(function (data) {
                  console.log(data);
              });
      }


  

  }]);