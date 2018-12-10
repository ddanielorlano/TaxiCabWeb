(function (ng) {
    var app = ng.module('appModule');
    if (!app) return;

    app.controller('taxiFareRateController', ['$scope', 'taxiFareRateService', function ($scope, taxiFareRateService) {


        function getRate() {

            var requestModel = getRequestModel();

            taxiFareRateService.getFare(requestModel).$promise.then(function (result) {
                $scope.taxiCabRateResponseModel = {
                    entryFare: !result.EntryFare ? 0 : result.EntryFare,
                    nyStateTaxSurcharge: !result.NyStateTaxSurcharge ? 0 : result.NyStateTaxSurcharge,
                    fareMilesTraveledBelow6mph: !result.FareMilesTraveledBelow6mph ? 0 : result.FareMilesTraveledBelow6mph,
                    fareMinutesTraveledAbove6mph: !result.FareMinutesTraveledAbove6mph ? 0 : result.FareMinutesTraveledAbove6mph,
                    nightSurcharge: !result.NightSurcharge ? 0 : result.NightSurcharge,
                    peakWeakdaySurcharge: !result.PeakWeakdaySurcharge ? 0 : result.PeakWeakdaySurcharge,
                    totalFare: !result.TotalFare ? 0 : result.TotalFare,
                }
                $scope.showResults = true;
            });
        }
        function getRequestModel() {
            var taxiCabRateRequestModel = {
                minutesTravelingAbove6pmh: $scope.taxiRateObj.minutesTravelingAbove6mph,
                milesTraveledBelow6mph: $scope.taxiRateObj.milesTraveledBelow6mph,
                rideBeginDateTime: $scope.taxiRateObj.minutesTravelingAbove6mph
            };
            var rideDate = $scope.taxiRateObj.rideDate;
            var newDate = new Date(rideDate.getFullYear(), rideDate.getMonth() + 1, rideDate.getDate(),
                $scope.taxiRateObj.rideHour, $scope.taxiRateObj.rideMinute, 0, 0);

            taxiCabRateRequestModel.rideBeginDateTime = newDate;

            return taxiCabRateRequestModel;
        }

        var daysInWeekStrs = ["Sun", "Mon", "Tue", "Wed", "Thur", "Fri"];
        $scope.daysInWeek = daysInWeekStrs;

        $scope.dayClicked = function (index) {

            var newDate = new Date($scope.beginDate);
            var diff = index - $scope.beginDate.getDay();
            newDate.setDate(newDate.getDate() + diff);

            $scope.beginDate = newDate;
            $scope.dayChosen = $scope.beginDate.getDay();

            $scope.taxiRateObj.rideDate = $scope.beginDate;
        };

        $scope.datePickerChanged = function () {
            $scope.dayChosen = $scope.beginDate.getDay();
            $scope.taxiRateObj.rideDate = $scope.beginDate;
            alert($scope.taxiRateObj.rideDate);
        };


        $scope.beginDate = new Date();
        $scope.dayChosen = $scope.beginDate.getDay();

        $scope.rate = "0.0";
        $scope.getRate = getRate;

        var taxiRateObj = {
            minutesTravelingAbove6mph: 0,
            milesTraveledBelow6mph: 0,
            rideDate: $scope.beginDate,
            rideTime: '8:00',
            rideHour: 11,
            rideMinute: 30
        };

        $scope.taxiRateObj = taxiRateObj;
        $scope.taxiCabRateResponseModel;
        $scope.showResults = false;

    }]);
})(this.angular);
