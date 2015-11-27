
app.factory("storeFactory", function ($http, $q) {

    var serviceBase = '/api/Store/', factory = {};

    factory.getProducts = function () {
        var deferred = $q.defer();

        $http.get(serviceBase + "GetProducts").
        success(function (data) {
            deferred.resolve(data);
        }).error(function (msg, code) {
            deferred.reject(msg);
        });

        return deferred.promise;
    };

    factory.getDiscountSchemes = function () {
        var deferred = $q.defer();

        $http.get(serviceBase + "GetDiscountSchemes").
        success(function (data) {
            deferred.resolve(data);
        }).error(function (msg, code) {
            deferred.reject(msg);
        });

        return deferred.promise;
    };

    factory.calculateOrder = function (orderWithDiscountScheme) {
        var deferred = $q.defer();

        $http.post(serviceBase + "CalculateOrder", orderWithDiscountScheme).
        success(function (data) {
            deferred.resolve(data);
        }).error(function (msg, code) {
            deferred.reject(msg);
        });

        return deferred.promise;
    };

    return factory;
});