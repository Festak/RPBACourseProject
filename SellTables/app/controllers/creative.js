
angular.module('creative', [])
  .controller('CreativeController', ['$scope', '$http', function ($scope, $http) {
      $scope.creatives = [];
      $scope.shownCreatives = [];
      $scope.loading = true;
      var current = 1;
      var count = 4;
      $scope.getCreatives = function () {
          $http.get('/Home/GetCreativesRange?start=' + current + '&count=' + count).success(function (result) {
              result.forEach(function (item, i, arr) {
                  $scope.creatives.push(item);
              });
          })
          .error(function (data) {
              console.log(data);
          });
          $scope.loading = false;
          current += count;
      }



      $scope.save = function (noteText) {
          console.log(noteText);
      }

      $scope.load = function () {

          $scope.getCreatives();

      }
    

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