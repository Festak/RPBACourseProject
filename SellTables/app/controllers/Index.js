
angular.module('appTest', [])
  .controller('MainController', ['$scope', '$http', function ($scope, $http) {

      $scope.save = function (noteText) {
          console.log(noteText);
      }


  }]);
