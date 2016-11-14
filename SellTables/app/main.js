﻿angular.module('main', ['user', 'creative', 'tag', 'ngRoute', 'chapter', 'widthChange'])
  .controller('MainController', [
      '$scope',
      '$http',
      '$window',
      function ($scope, $http, $window) {

          $scope.redirect = function (Id) {
              $window.location.href = '/Chapter/Details/' + Id;
          }
          $scope.redirectToCreative = function (Id) {
              $window.location.href = '/Creative/Details/' + Id;
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