
angular.module('tag', [])
      .controller('TagController', function ($scope, $http, $rootScope) {
          $scope.tags = [];
          $rootScope.searchItems = [];
          $scope.colors = ("red", "yellow", "blue");
          $scope.color = "red";

          $scope.getTags = function () {

              $http.get('/Home/GetTags').success(function (result) {
                  $scope.tags = result;
                  $scope.tags.forEach(function (element) {
                      $scope.searchItems.push(element.Description);
                  });
              })
                  .error(function (data) {
                      console.log(data);
                  });
          }

          $scope.get = function () {
              $scope.tagsDescriptions.forEach(function (element) {
                  console.log(element);
              });

          }


          

          //Sort Array
          $rootScope.searchItems.sort();
          //Define Suggestions List
          $rootScope.suggestions = [];
          //Define Selected Suggestion Item
          $rootScope.selectedIndex = -1;

          //Function To Call On ng-change
          $rootScope.search = function () {
              $rootScope.suggestions = [];
              var myMaxSuggestionListLength = 0;
              for (var i = 0; i < $rootScope.searchItems.length; i++) {
                  var searchItemsSmallLetters = angular.lowercase($rootScope.searchItems[i]);
                  var searchTextSmallLetters = angular.lowercase($scope.searchText);
                  if (searchItemsSmallLetters.indexOf(searchTextSmallLetters) !== -1) {
                      $rootScope.suggestions.push(searchItemsSmallLetters);
                      myMaxSuggestionListLength += 1;
                      if (myMaxSuggestionListLength == 5) {
                          break;
                      }
                  }
              }
          }

          //Keep Track Of Search Text Value During The Selection From The Suggestions List  
          $rootScope.$watch('selectedIndex', function (val) {
              if (val !== -1) {
                  $scope.searchText = $rootScope.suggestions[$rootScope.selectedIndex];
              }
          });


          //Function To Call on ng-keyup
          $rootScope.checkKeyUp = function (event) {
              if (event.keyCode === 40) {//down key, increment selectedIndex
                  event.preventDefault();
                  if ($rootScope.selectedIndex + 1 !== $rootScope.suggestions.length) {
                      $rootScope.selectedIndex++;
                  }
              } else if (event.keyCode === 38) { //up key, decrement selectedIndex
                  event.preventDefault();
                  if ($rootScope.selectedIndex - 1 !== -1) {
                      $rootScope.selectedIndex--;
                  }
              } else if (event.keyCode === 13) { //enter key, empty suggestions array
                  event.preventDefault();
                  $rootScope.suggestions = [];
              } else if (event.keyCode !== 8 || event.keyCode !== 46) {//delete or backspace
                  if ($scope.searchText == "") {
                      $rootScope.suggestions = [];
                  }
              }
          }
          //======================================

          //List Item Events
          //Function To Call on ng-click
          $rootScope.AssignValueAndHide = function (index) {
              $scope.searchText = $rootScope.suggestions[index];
              $rootScope.suggestions = [];
          }

      });