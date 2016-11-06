angular.module('app', ['user','appTest','creative'])
  .controller('testController', ['$scope', '$http', function ($scope, $http) {

      $scope.savetest = function (noteText) {
          console.log(noteText+" test");
      }

      $scope.items = ["1. Scroll the list to load more"];
      $scope.loading = true;

      // this function fetches a random text and adds it to array
      $scope.more = function () {
          //$http({
          //    method: "GET",
          //    url: "/Home/GetCreatives"
          //}).success(function (data, status, header, config) {
          //    console.log(data);
          //    // returned data contains an array of 2 sentences
          //    for (line in data) {
                 
          //        $scope.items.push(line);
          //    }
          //    $scope.loading = false;
          //});
      };

      // we call the function twice to populate the list
      $scope.more();
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
  
