angular.module('app', ['user','appTest'])
  .controller('testController', ['$scope', '$http', function ($scope, $http) {

      $scope.savetest = function (noteText) {
          console.log(noteText+" test");
      }


  }]);
