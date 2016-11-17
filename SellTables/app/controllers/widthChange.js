angular.module('widthChange', [])
  .controller('WidthChangeController', function ($scope, $http) {
      var currentClasses = "col-md-11 col-sm-11 col-xs-10";




      $scope.changeTo40 = function () {
          var element = angular.element(document.getElementById('content'));
          element.removeClass(currentClasses);
          currentClasses = "col-md-5 col-sm-5 col-xs-4 col-md-offset-4 col-sm-offset-4 col-xs-offset-4";
          element.addClass(currentClasses);
   
      }

      $scope.changeTo60 = function () {
          var element = angular.element(document.getElementById('content'));
          element.removeClass(currentClasses);
          currentClasses = "col-md-7 col-sm-7 col-xs-6 col-md-offset-3 col-sm-offset-3 col-xs-offset-3";
          element.addClass(currentClasses);  
      }

      $scope.changeTo80 = function () {
          var element = angular.element(document.getElementById('content'));
          element.removeClass(currentClasses);
          currentClasses = "col-md-9 col-sm-9 col-xs-8 col-md-offset-2 col-sm-offset-2 col-xs-offset-2";
          element.addClass(currentClasses);   
      }

      $scope.changeTo100 = function () {
          var element = angular.element(document.getElementById('content'));
          element.removeClass(currentClasses);
          currentClasses = "col-md-11 col-sm-11 col-xs-10";
          element.addClass(currentClasses);
      }

  });