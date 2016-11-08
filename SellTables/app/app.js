angular.module('app', ['user', 'appTest', 'creative', 'tag', 'ngRoute','chapter'])
  .controller('MainController', [
      '$scope',
      '$http',
      '$window',
      function ($scope, $http, $window) {

      $scope.savetest = function (noteText) {
          console.log(noteText + " test");
      }

      $scope.items = [];
      $scope.loading = true;

      $scope.redirect = function (Id) {
          alert(Id);
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
