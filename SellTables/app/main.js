angular.module('main', ['user', 'creative', 'tag', 'ngRoute', 'chapter'])
  .controller('MainController', [
      '$scope',
      '$http',
      '$window',
      function ($scope, $http, $window) {

          $scope.redirect = function (Id) {
              $window.location.href = '/Chapter/Details/' + Id;
          }
          $scope.redirectToAddCreative = function (Id) {
              $window.location.href = '/Creative/Details/' + Id;
          }

          $scope.redirectToEditCreative = function (Id) {
              $window.location.href = '/Creative/Edit/' + Id;
          }

          //$scope.loadTags = function () {
          //    alert("HERE");
          //    return $http.get('/Home/GetTags');
          //};

      }]).config(function ($routeProvider) {
          $routeProvider.
          when('/firstPage', {
              templateUrl: 'creative/create',
              controller: 'CreativeController'
          }).
          when('/secondPage', {
              templateUrl: 'routedemo/second',
              controller: 'routeDemoSecondController'
          })
      });