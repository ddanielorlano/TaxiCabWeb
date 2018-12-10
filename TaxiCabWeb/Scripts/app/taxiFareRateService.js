(function (ng) {

    var app = ng.module('appModule');
    if (!app) return;

    app.factory('taxiFareRateService', function ($resource) {

        var self = this;
        var RateResource = $resource('http://google.com');
        self.GetFare = function (taxiRateFareObj) {
            return RateResource.get(taxiRateFareObj);
        }
    });

})(this.angular);
