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
                    totalFare: !result.TotalFare ? 0 : result.TotalFare
                };

                $scope.showResults = true;

            });
        }

        function getRequestModel() {
            return taxiCabRateRequestModel = {
                minutesTraveledAbove6pmh: $scope.taxiRateObj.minutesTravelingAbove6mph,
                milesTraveledBelow6mph: $scope.taxiRateObj.milesTraveledBelow6mph,
                rideBeginDateTime: $scope.taxiRateObj.rideDate
            };
        }

        function getRideBeginDateTime() {

            var rideDate = $scope.taxiRateObj.rideDate;
            var newDate = new Date(rideDate.getFullYear(), rideDate.getMonth() + 1, rideDate.getDate(),
                $scope.taxiRateObj.rideHour, $scope.taxiRateObj.rideMinute, 0, 0);
            return newDate;
        }

        var daysInWeekStrs = ["Sun", "Mon", "Tue", "Wed", "Thur", "Fri"];
        $scope.daysInWeek = daysInWeekStrs;

        $scope.dayClicked = function (index) {

            var newDate = new Date($scope.taxiRateObj.rideDate);
            var diff = index - $scope.taxiRateObj.rideDate.getDay();
            newDate.setDate(newDate.getDate() + diff);
            $scope.taxiRateObj.rideDate = newDate;
            $scope.taxiRateObj.dayChosen = $scope.taxiRateObj.rideDate.getDay();

        };

        $scope.datePickerChanged = function () {
            $scope.taxiRateObj.dayChosen = $scope.taxiRateObj.rideDate.getDay();
        };

        $scope.minuteRangeChange = function () {
            $scope.taxiRateObj.rideDate.setMinutes($scope.taxiRateObj.rideMinute);
        };

        $scope.hourRangeChange = function () {
            $scope.taxiRateObj.rideDate.setHours($scope.taxiRateObj.rideHour);
        }

        var taxiRateObj = {
            minutesTravelingAbove6mph: 5,
            milesTraveledBelow6mph: 2,
            rideDate: new Date(),
            dayChosen: new Date().getDay(),
            rideHour: 11,
            rideMinute: 30
        };

        taxiRateObj.rideHour = taxiRateObj.rideDate.getHours();
        taxiRateObj.rideMinute = taxiRateObj.rideDate.getMinutes();

        $scope.taxiRateObj = taxiRateObj;
        $scope.taxiCabRateResponseModel;
        $scope.showResults = false;
        $scope.rate = "0.0";
        $scope.getRate = getRate;

    }]);
})(this.angular);
