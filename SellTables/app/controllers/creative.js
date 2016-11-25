
angular.module('creative', ['ngRoute', 'as.sortable'])
  .controller('CreativeController',
  ['$scope',
      '$http',
      '$window',
      '$route',

      function ($scope, $http, $window) {
          $scope.creatives = [];
          $scope.popular = [];
          $scope.lastEdited = [];
          $scope.shownCreatives = [];
          $scope.sortedCreatives = [];
          var sortType = 1;
          $scope.loading = true;
          var current = 1;
          var count = 4;
          var haveMore = true;
          var isBusy = false;
          $scope.orderByField = 'firstName';
          $scope.reverseSort = false;

          $scope.creativeId;


          $scope.getCreatives = function () {
              $scope.load();
          }



          $scope.load = function () {

              if (haveMore && !isBusy) {
                  isBusy = true;
                  $http.get('/Home/GetCreativesRange?start=' + current + '&count=' + count + '&sortType=' + sortType).success(function (result) {
                      if (result == false) {
                          haveMore = false;
                      }
                      else {
                          result.forEach(function (item, i, arr) {
                              $scope.creatives.push(item);
                          });
                          angular.element(document).ready(function () {
                              for (var i = current - 2; i > current - count - 2; i--) {
                                  $scope.setRating(i, $scope.creatives[i].Rating);
                              }
                          });
                      }
                      isBusy = false;
                  })
                  .error(function (data) {
                      console.log(data);
                  });

                  $scope.loading = false;
                  current += count;
              }
          }

          $scope.dragControlListeners = {
              accept: function (sourceItemHandleScope, destSortableScope) {
                  return sourceItemHandleScope.itemScope.sortableScope.$id === destSortableScope.$id;

              },
              itemMoved: function (eventObj) {
              },
              orderChanged: function (eventObj) {
                  containment: '#board'//optional param.
                  clone: true //optional param for clone feature.
                  allowDuplicates: false //optional param allows duplicates to be dropped.
                  // from this index eventObj.source.index
                  // to this index eventObj.dest.index
                  $scope.from = angular.element(eventObj)[0].dest
                      .sortableScope.modelValue[eventObj.source.index].Id;
                  $scope.to = angular.element(eventObj)[0].dest
                  .sortableScope.modelValue[eventObj.dest.index].Id;
          
                 $scope.SendLastAndNewChapterPos(eventObj.source.index, eventObj.dest.index, $scope.from, $scope.to);
              }
          };

          $scope.SendLastAndNewChapterPos = function (oldPos, newPos, from, to) {
              $http.post('/Creative/UpdateChapterPos', { oldPosition: oldPos, newPosition: newPos, fromChapterId: from, toChapterId: to }).success(function (result) {
              })
                  .error(function (data) {
                      console.log(data);
                  });
          }

          $scope.vote = function (rate, creativeObj) {            
              $http.post('/Creative/GetRatingFromView', { rating: rate, creative: creativeObj }).success(function (result) {
              })
        .error(function (data) {
            console.log(data);
        });
              $window.location.href = '';
          }

          $scope.changeSortType = function (i) {
              $scope.creatives = [];
              sortType = i;
              isBusy = false;
              current = 1;
              haveMore = true;
              $scope.load();
          }

          $scope.show = function (id, i) {
              for (var j = 1; j <= i; j++) {
                  var element = angular.element(document.getElementById('' + id + j));
                  element.css({
                      'color': 'yellow',
                  });
              }
          }

          $scope.hide = function (id) {
              for (var j = 1; j <= 5; j++) {
                  var element = angular.element(document.getElementById('' + id + j));
                  element.css({
                      'color': 'black',
                  });
              }
          }

          $scope.updateCreativeName = function (creativeId, name, user, index) {              
              $http.post('/Creative/UpdateCreativeName', { id: creativeId, newName: name }).success(function (result) {
                  $scope.creatives[index].Name = name;
              });
              
              
          }

          $scope.setRating = function (id, i) {
              for (var j = 1; j <= i; j++) {
                  var element = angular.element(document.getElementById('' + id + j));
                  element.removeClass('glyphicon-star-empty');
                  element.addClass('glyphicon-star');
              }
          }

          $scope.getPopular = function () {
              $http.get('/Home/GetPopular').success(function (result) {
                  $scope.popular = result;
              })
           .error(function (data) {
               console.log(data);
           });
          }

          $scope.getLastEditedCreatives = function () {
              console.log("here");
              $http.get('/Home/GetLastEdited').success(function (result) {
                  $scope.lastEdited = result;
              })
         .error(function (data) {
             console.log(data);
         });
          }

          $scope.deleteCreativeById = function (creativeId, user) {
              $http.post('/Creative/DeleteCreativeById', { id: creativeId, userName: user }).success(function (result) {
                  $scope.getCreativesByUser(user);
              })
        .error(function (data) {
            console.log(data);
        });

          }

          $scope.deleteChapterById = function (chapterId, user) {
              $http.post('/Creative/DeleteChapterById', { id: chapterId, userName: user }).success(function (result) {
                  $scope.getCreativesByUser(user);
              })
  .error(function (data) {
      console.log(data);
  });
          }


          $scope.getCreativesByUser = function (name) {
              $http.post('/Creative/GetCreativesByUser', { userName: name }).success(function (result) {
                  $scope.creatives = result;
              })
          .error(function (data) {
              console.log(data);
          });
          }

          $scope.startChanging = function (id) {
              clearInput();
              $scope.creativeId = id;
          }

          $scope.acceptImageChange = function (user) {
         
              img = angular.element(document.getElementById('imageUploadInput')).attr('value');
              $http.post('/Creative/UpdateCreativeImage', { id: $scope.creativeId, image: img })
                  .success(function (result) {
                      $scope.getCreativesByUser(user);
                  }).error(function (data) {
                      console.log(data);
                  });
              $scope.creativeId = null;
          }


      }]).directive("directive", function () {
          return {
              restrict: "A",
              require: "ngModel",
              link: function (scope, element, attrs, ngModel) {

                  function read() {
                      // view -> model
                      var html = element.html();
                      console.log(html);
                      html = html.replace(/&nbsp;/g, "\u00a0");
                      ngModel.$setViewValue(html);
                  }
                  // model -> view
                  ngModel.$render = function () {
                      element.html(ngModel.$viewValue || "");
                  };

                  element.bind("text-change blur", function () {
                      scope.$apply(read);
                  });
                  element.bind("keydown keypress", function (event) {
                      if (event.which === 13) {
                          this.blur();
                          event.preventDefault();
                      }
                  });
              }
          };
      });