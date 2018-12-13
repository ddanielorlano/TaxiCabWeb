(function (ng) {

    var app = ng.module('appModule');
    if (!app) return;

    app.directive('customValidator', customValidatorFunc);

    function customValidatorFunc() {
        return {
            restrict: 'A',
            require: 'ngModel',
            link: function (scope, element, attr, ngModel) {

                ngModel.$validators.checkHour = function (hour) {
                    return !(hour >= 3 && hour <= 4);//false indicates error
                };

                ngModel.$validators.checkMinutesAbove6 = function (minutes) {
                    return minutes < 15;//> 15 indicates error
                };

                ngModel.$validators.checkMilesBelow6 = function (miles) {
                    return miles < 6;//> 6 indicates error
                };

                //When the date is null will trigger $error
                ngModel.$validators.checkRideBeginDate = function (rideDate) {
                    if (!rideDate) return false;
                };
            }
        };
    }
})(this.angular);