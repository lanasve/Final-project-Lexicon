var app = angular.module("app", []);
app.controller("SearchController", function ($scope, $http) {

    $scope.keyword = [];

    $scope.SearchData = function () {
        if ($scope.keyword.length > 2) {

            $('#myModal').modal('show');


            $scope.showTable = true;

            var parameters =
                {
                    keyword: encodeURI($scope.keyword)

                };
            var config =
                {
                    params: parameters
                };

            if ($scope.keyword == "") {
                $scope.ResponseDetails = "Type in a new search";
                $scope.showTable = false;
            }
            else {
                $http.get('/Search/GetData', config)

                .success(function (data, config) {
                    $scope.DbData = data;
                    //debugger;
                })
                .error(function (data, config) {
                    $scope.ResponseDetails = "Fel Medelande (status): " + status;
                });

                //-------------------------------------------------------------------------

                //Sök på tag

                $http.get('/Search/GetTagData', config)
                .success(function (tagdata, config) {
                    $scope.TagData = tagdata;
                    //debugger;
                })
                .error(function (tagdata, config) {
                    $scope.ResponseDetails = "Fel Medelande (status): " + status;
                });

                //-------------------------------------------------------------------------
               
                //Sök på Post
                $http.get('/Search/GetPostData', config)
                     .success(function (postdata, config) {
                         $scope.PostData = postdata;
                         //debugger;
                     })
                     .error(function (postdata, config) {
                         $scope.ResponseDetails = "Fel Medelande (status): " + status;
                     });
            }
        }
        else {
            $scope.DbData == "";           
            $scope.showTable = false;
        }
    };
});
