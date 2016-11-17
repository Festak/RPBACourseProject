angular.module('cookies', [])
  .controller('CookiesController', function ($scope) {
      $scope.Thememes = [
          {
              Type: "light",
              Uri: "~/Content/bootstrapCerulean.min.css"
          },
      {
          Type: "dark",
          Uri: ""
      }
      ]
      $scope.Theme.Uri = $scope.Themes[0].Uri;
      $scope.test = function () {
          console.log("test");
      };


  });



