
angular.module('creative', [])
  .controller('CreativeController', ['$scope', '$http', function ($scope, $http) {
      $scope.creatives = [];
      $scope.popular = [];
      $scope.shownCreatives = [];
      var sortType = 1;
      $scope.loading = true;
      var current = 1;
      var count = 4;
      var haveMore = true;
      var isBusy = false;

      $scope.getCreatives = function () {
          $scope.load();
      }
      $scope.save = function (noteText) {
          console.log(noteText);
      }
      $scope.load = function () {
          if (haveMore && !isBusy) {
              isBusy = true;
              $http.get('Home/GetCreativesRange?start=' + current + '&count=' + count + '&sortType=' + sortType).success(function (result) {
                  if (result == false) {
                      haveMore = false;
                  }
                  else {
                      result.forEach(function (item, i, arr) {
                          $scope.creatives.push(item);
                      });
                      angular.element(document).ready(function () {
                          for (var i = current - 2; i > current - count - 2; i--) {
                              $scope.setRating(i, $scope.creatives[i].Rating);
                          }
                      });
                  }
                  isBusy = false;
              })
              .error(function (data) {
                  console.log(data);
              });


              current += count;



          }
      }

      $scope.changeSortType = function (i) {
          $scope.creatives = [];
          sortType = i;
          isBusy = false;
          current = 1;
          haveMore = true;
          $scope.load();
      }

      $scope.show = function (id, i) {
          for (var j = 1; j <= i; j++) {
              var element = angular.element(document.getElementById('' + id + j));
              element.css({
                  'color': 'yellow',
              });
          }

      }

      $scope.hide = function (id) {
          for (var j = 1; j <= 5; j++) {
              var element = angular.element(document.getElementById('' + id + j));
              element.css({
                  'color': 'black',
              });
          }
      }

      $scope.setRating = function (id, i) {
          for (var j = 1; j <= i; j++) {
              var element = angular.element(document.getElementById('' + id + j));
              element.removeClass('glyphicon-star-empty');
              element.addClass('glyphicon-star');
          }

      $scope.getPopular = function () {
          $http.get('/Home/GetPopular').success(function (result) {
              console.log(result);
              $scope.popular = result;
          })
       .error(function (data) {
           console.log(data);
       });
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