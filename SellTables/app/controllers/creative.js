
angular.module('creative', ['ngRoute'])
  .controller('CreativeController',
  ['$scope',
      '$http',
      '$window',
      '$route',

      function ($scope, $http, $window) {
          $scope.creatives = [];
          $scope.popular = [];
          $scope.shownCreatives = [];
          var sortType = 1;
          $scope.loading = true;
          var current = 1;
          var count = 4;
          var haveMore = true;
          var isBusy = false;
          $scope.orderByField = 'firstName';
          $scope.reverseSort = false;

          $scope.getCreatives = function () {
              $scope.load();
          }



          $scope.load = function () {

              if (haveMore && !isBusy) {
                  isBusy = true;
                  $http.get('/Home/GetCreativesRange?start=' + current + '&count=' + count + '&sortType=' + sortType).success(function (result) {
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

                  $scope.loading = false;
                  current += count;
              }
          }

          $scope.vote = function (rate, creativeObj) {
              $http.post('/Creative/GetRatingFromView', { rating: rate, creative: creativeObj }).success(function (result) {
              })
        .error(function (data) {
            console.log(data);
        });
              $window.location.href = ''; //test variant to reload the page 
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
          }

          $scope.getPopular = function () {
              $http.get('/Home/GetPopular').success(function (result) {
                  $scope.popular = result;
              })
           .error(function (data) {
               console.log(data);
           });
          }

          $scope.getCreativesByUser = function (name) {
              $http.post('/Creative/GetCreativesByUser', { userName: name }).success(function (result) {
                  $scope.creatives = result;
              })
          .error(function (data) {
              console.log(data);
          });
          }


      }]);
