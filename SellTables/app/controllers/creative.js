
angular.module('creative', [])
  .controller('CreativeController', ['$scope', '$http', function ($scope, $http) {
      $scope.creatives = [];
      $scope.shownCreatives = [];
      var current = 0;

      $scope.getCreatives = function () {
          $http.get('/home/getcreatives').success(function (result) {
              $scope.creatives = result;
              for (var i = 0; i < 4; i++){
                  $scope.shownCreatives.push($scope.creatives[current]);
                  current++;
              }
          })
          .error(function (data) {
              console.log(data);
          });
         
          
      }
      $scope.save = function (noteText) {
          console.log(noteText);
      }

      $scope.load = function () {
          
          for (var i = 0; i < 4 && current<$scope.creatives.length; i++) {
              $scope.shownCreatives.push($scope.creatives[current]);
              current++;
          }
      }
      $scope.load();     
  }])
    .directive("whenScrolled", function () {
      return {

          restrict: 'A',
          link: function (scope, elem, attrs) {

              // we get a list of elements of size 1 and need the first element
              raw = elem[0];

              // we load more elements when scrolled past a limit
              elem.bind("scroll", function () {
                  if (raw.scrollTop + raw.offsetHeight + 5 >= raw.scrollHeight) {
                      scope.loading = true;

                      // we can give any function which loads more elements into the list
                      scope.$apply(attrs.whenScrolled);
                  }
              });
          }
      }
  });