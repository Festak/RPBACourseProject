angular.module('widthChange', [])
  .controller('WidthChangeController', function ($scope, $http) {
      var allClasses = "col-md-2 col-md-offset-5 col-md-4 col-md-offset-4 col-md-6 col-md-offset-3 col-md-12";



      $scope.changeTo40 = function () {
          var element = angular.element(document.getElementById('content'));
          element.removeClass(allClasses);
          element.addClass("col-md-2 col-md-offset-5");
   
      }

      $scope.changeTo60 = function () {
          var element = angular.element(document.getElementById('content'));
          element.removeClass(allClasses);
          element.addClass("col-md-4 col-md-offset-4");
  
      }

      $scope.changeTo80 = function () {
          var element = angular.element(document.getElementById('content'));
          element.removeClass(allClasses);
          element.addClass("col-md-6 col-md-offset-3");
   
      }

      $scope.changeTo100 = function () {
          var element = angular.element(document.getElementById('content'));
          element.removeClass(allClasses);
          element.addClass("col-md-12");
        
      }

  });