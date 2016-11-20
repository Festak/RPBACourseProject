angular.module('widthChange', [])
  .controller('WidthChangeController',['$scope', '$http', function ($scope, $http) {
      var currentClasses = "col-md-12 col-sm-12 col-xs-12";
      var currentFontClass = "font100";

      $scope.changeTo40 = function () {
          var element = angular.element(document.getElementsByName("content"));
          console.log(element);
          element.removeClass(currentClasses);
          currentClasses = "col-md-6 col-sm-6 col-xs-6 col-md-offset-3 col-sm-offset-3 col-xs-offset-3";
          element.addClass(currentClasses);
   
      }

      $scope.changeTo60 = function () {
          var element = angular.element(document.getElementsByName('content'));
          element.removeClass(currentClasses);
          currentClasses = "col-md-8 col-sm-8 col-xs-8 col-md-offset-2 col-sm-offset-2 col-xs-offset-2";
          element.addClass(currentClasses);  
      }

      $scope.changeTo80 = function () {
          var element = angular.element(document.getElementsByName('content'));
          element.removeClass(currentClasses);
          currentClasses = "col-md-10 col-sm-10 col-xs-10 col-md-offset-1 col-sm-offset-1 col-xs-offset-1";
          element.addClass(currentClasses);   
      }

      $scope.changeTo100 = function () {
          var element = angular.element(document.getElementsByName('content'));
          element.removeClass(currentClasses);
          currentClasses = "col-md-12 col-sm-12 col-xs-12";
          element.addClass(currentClasses);
      }

      $scope.changeFontTo70 = function () {
          var element = angular.element(document.getElementsByName('content'));
          element.removeClass(currentFontClass);
          currentFontClass = "font70";
          element.addClass(currentFontClass);
      }

      $scope.changeFontTo100 = function () {
          var element = angular.element(document.getElementsByName('content'));
          element.removeClass(currentFontClass);
          currentFontClass = "font100";
          element.addClass(currentFontClass);
      }

      $scope.changeFontTo200 = function () {
          var element = angular.element(document.getElementsByName('content'));
          element.removeClass(currentFontClass);
          currentFontClass = "font200";
          element.addClass(currentFontClass);
      }

      $scope.changeFontTo300 = function () {
          var element = angular.element(document.getElementsByName('content'));
          element.removeClass(currentFontClass);
          currentFontClass = "font300";
          element.addClass(currentFontClass);
      }

  }]);