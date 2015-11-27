
app.controller("StoreController", ["$scope", "$http", "storeFactory", function ($scope, $http, storeFactory) {
    $scope.Products = [];
    $scope.CartProducts = [];
    $scope.Order = {};
    $scope.Order.OrderDetails = [];
    $scope.DiscountSchemes = [];
    $scope.CurrentDiscountScheme = '';
    $scope.CustomerBirthday = '';
    $scope.InvalidForm = false;

    $scope.GetProducts = function () {
        var promise = storeFactory.getProducts();
        promise.then(function (data) {
            angular.forEach(data, function (value) {
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

        var birthday = moment($scope.CustomerBirthday).format("MM-DD-YYYY");
        if (birthday == "Invalid date") {
            $scope.CustomerBirthday = '';
        }

        var orderDetails = [];
        angular.forEach($scope.CartProducts, function (value) {
            if (value.Quantity > 0) {
                var product = { Name: value.Name, Price: value.Price, UniqueCode: value.UniqueCode };
                var tmpOrderDetails = { Product: product, Quantity: value.Quantity };
                orderDetails.push(tmpOrderDetails);
            }
        });

        if (!orderDetails.length) {
            $scope.InvalidForm = true;
            return;
        };
        
        var order = { OrderDetails: orderDetails, CustomerBirthday: birthday };
        var orderWithDiscountScheme = { Order: order, DiscountSchemeName: $scope.CurrentDiscountScheme };

        var promise = storeFactory.calculateOrder(orderWithDiscountScheme);
        promise.then(function (data) {
            $scope.Order = data;
        });
    };

    $scope.AddToCart = function (product) {

        var index = $.inArray(product, $scope.CartProducts);
        if (index > -1) {
            var tmpProduct = $scope.CartProducts[index];
            tmpProduct.Quantity = tmpProduct.Quantity + 1;
        }
        else {
            $scope.CartProducts.push(product);
        }
    };

    $scope.RemoveFromCart = function (product) {

        var index = $.inArray(product, $scope.CartProducts);
        if (index > -1) {
            product.Quantity = 1;
            $scope.CartProducts.splice(index, 1);
        }
    };

    $scope.onBirthdaySet = function (newDate, oldDate) {
        $scope.CustomerBirthday = moment(newDate).format("MM-DD-YYYY");
    }

}]);

