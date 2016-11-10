angular.module('app', ['user', 'creative', 'tag', 'ngRoute', 'chapter'])
  .controller('MainController', [
      '$scope',
      '$http',
      '$window',
      function ($scope, $http, $window) {

      $scope.redirect = function (Id) {
              $window.location.href = '/Chapter/Details/'+Id;
      }


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
