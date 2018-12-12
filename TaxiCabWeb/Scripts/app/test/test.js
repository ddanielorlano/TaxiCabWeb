describe("taxi fare test", function () {
    var $rootScope;
    var $controller;
    beforeEach(module("appModule"));
    beforeEach(inject(function ($injector) {

        $rootScope = $injector.get('$rootScope');
        $controller = $injector.get('$controller');
        $scope = $rootScope.$new();

    }));
    beforeEach(inject(function ($controller) {
        taxiFareRateController = $controller("taxiFareRateController", {$scope: $rootScope});

    }));

    it("Should say hello", function () {
        $scope.rideMinute = 1;
        $scope.getFare();
        expect($scope.taxiCabRateResponseModel.Success).toBe(false);
    });

});