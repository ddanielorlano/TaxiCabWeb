describe("test the taxi ctrl", function () {
    var $rootScope;
    var $controller;
    beforeEach(module("appModule"));
    beforeEach(inject(function ($injector) {

        $rootScope = $injector.get('$rootScope');
        $controller = $injector.get('$controller');
        $scope = $rootScope.$new();

    }));

    beforeEach(inject(function ($controller) {
        taxiFareRateController = $controller("taxiFareRateController", { $scope: $scope });
    }));

    it("dayClicked func should change the taxiRateObj.rideDate properly", function () {

        var testDate = new Date(2018, 11, 11, 6, 30, 0, 0);//Tue 12-11-18 6:30 AM
        $scope.taxiRateObj = taxiFareRateController.getTaxiRateObj(testDate);

        expect($scope.taxiRateObj.dayChosen).toBe(2);//Tue
        expect($scope.taxiRateObj.rideDate.getDate()).toBe(11);

        taxiFareRateController.changeRideDate(3);//This should increase date by 1 day
        $scope.$digest();
        expect($scope.taxiRateObj.dayChosen).toBe(3);//Wed
        expect($scope.taxiRateObj.rideDate.getDate()).toBe(12);

    });

    it("should fail so you know tests are actually running", function () {
        expect("this to fail").toBe("see, it fails");
    });

});