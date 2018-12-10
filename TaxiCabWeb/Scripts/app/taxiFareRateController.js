(function (ng) {
    var app = ng.module('appModule');
    if (!app) return;

    app.controller('taxiFareRateController', ['$scope', function ($scope) {


        function getRate() {
            //taxiFareRateService.GetRate($scope.taxiCabRateModel).$promise.then(function (rate) {
            //    $scope.rate = rate;
            //});
            return 1;
        }

        var daysInWeekStrs = ["Sun", "Mon", "Tue", "Wed", "Thur", "Fri"];
        $scope.daysInWeek = daysInWeekStrs;

        $scope.dayClicked = function (index) {

            var newDate = new Date($scope.beginDate);
            var diff = index - $scope.beginDate.getDay();
            newDate.setDate(newDate.getDate() + diff);

            $scope.beginDate = newDate;
            $scope.dayChosen = $scope.beginDate.getDay();

            $scope.taxiCabRateModel.rideDate = $scope.beginDate;
        };

        $scope.datePickerChanged = function () {
            $scope.dayChosen = $scope.beginDate.getDay();
            $scope.taxiCabRateModel.rideDate = $scope.beginDate;
            alert($scope.taxiCabRateModel.rideDate);
        }

       
        $scope.beginDate = new Date();
        $scope.dayChosen = $scope.beginDate.getDay();

        $scope.rate = "0.0";
        $scope.GetRate = getRate;

        var taxiCabRateModel = {
            minutesTravelingAbove6mph: 0,
            milesTraveledBelow6mph: 0,
            rideDate: $scope.beginDate,
            rideTime: '8:00',
            rideHour: 0,
            rideMinute: 59
        };

        $scope.taxiCabRateModel = taxiCabRateModel;
        
    }]);
})(this.angular);
