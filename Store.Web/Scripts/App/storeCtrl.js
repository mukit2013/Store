
app.controller("StoreController", ["$scope", "$http", "storeFactory", function ($scope, $http, storeFactory) {
    $scope.Products = [];
    $scope.Order = {};
    $scope.Order.OrderDetails = [];
    $scope.DiscountSchemes = [];
    $scope.CurrentDiscountScheme = '';
    $scope.CustomerBirthday = '';

    $scope.GetProducts = function () {
        var promise = storeFactory.getProducts();
        promise.then(function (data) {
            angular.forEach(data, function (value) {
                value.IsSelected = false;
                value.Quantity = 1;
            });
            $scope.Products = data;
        });
    };
    $scope.GetProducts();
    
    $scope.GetDiscountSchemes = function () {
        var promise = storeFactory.getDiscountSchemes();
        promise.then(function (data) {
            $scope.DiscountSchemes = data;
            $scope.CurrentDiscountScheme = $scope.DiscountSchemes[0].Name;
        });
    };
    $scope.GetDiscountSchemes();

    $scope.CalculateOrder = function () {
        var orderDetails = [];
        angular.forEach($scope.Products, function (value) {
            if (value.IsSelected) {
                var product = { Name: value.Name, Price: value.Price, UniqueCode: value.UniqueCode };
                var tmpOrderDetails = { Product: product, Quantity: value.Quantity };
                orderDetails.push(tmpOrderDetails);
            }
        });

        var birthday = moment($scope.CustomerBirthday).format("MM-DD-YYYY");
        var order = { OrderDetails: orderDetails, CustomerBirthday: birthday };
        var orderWithDiscountScheme = { Order: order, DiscountSchemeName: $scope.CurrentDiscountScheme };

        var promise = storeFactory.calculateOrder(orderWithDiscountScheme);
        promise.then(function (data) {
            $scope.Order = data;
        });
    };


}]);

