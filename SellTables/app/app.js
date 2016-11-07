angular.module('app', ['user','appTest','creative'])
  .controller('testController', ['$scope', '$http', function ($scope, $http) {

      $scope.savetest = function (noteText) {
          console.log(noteText+" test");
      }

      $scope.items = {};
      $scope.loading = true;


      $scope.more = function () {
          $http.get('/Home/GetCreatives').success(function (result) {

              $scope.items.push(result);
              console.log(result);
              $scope.loading = false;
          })
          .error(function (data) {
              console.log(data);
          });
      }

      //$scope.more = function () {
      //    $http({
      //      method: "GET",
      //   url: "/Home/GetCreatives"
      //    }).success(function (result) {
      //        console.log(data);
      //        for (line in data) { 
      //            $scope.items.push(line);
      //       }
      //        $scope.loading = false;
      //    }) ;
      //};

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
  
