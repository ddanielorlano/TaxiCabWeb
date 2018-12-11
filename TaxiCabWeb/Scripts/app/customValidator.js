(function (ng) {

    var app = ng.module('appModule');
    if (!app) return;

    app.directive('customValidator', customValidatorFunc);

    function customValidatorFunc() {
        return {
            restrict: 'A',
            require: 'ngModel',
            link: function (scope, element, attr, ngModel) {
                // validation callback registration to ngModel
                ngModel.$validators.checkHour = function (hour) {
                    return hour >= 3;
                }
            }
        }
    }
})(this.angular);