
angular.module('tag', [])
      .controller('TagController', function ($scope, $http) {
          $scope.tags = [];
          $scope.tagsDescriptions = [];
          $scope.colors = ("red", "yellow", "blue");
          $scope.color = "red";

          $scope.getTags = function () {

              $http.get('/Home/GetTags').success(function (result) {
                  $scope.tags = result;
                  $scope.tags.forEach(function (element) {
                      $scope.tagsDescriptions.push(element.Description);
                  });
              })
                  .error(function (data) {
                      console.log(data);
                  });
          }

          $scope.get = function () {
              $scope.tagsDescriptions.forEach(function (element) {
                  console.log(element);
              });

          }


      });