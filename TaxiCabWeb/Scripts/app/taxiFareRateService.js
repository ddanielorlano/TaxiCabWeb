(function (ng) {

    var app = ng.module('appModule');
    if (!app) return;

    app.factory('taxiFareRateService', function ($resource) {

        var self = this;
        var RateResource = $resource('Home/CalculateRate');

        self.getFare = function (taxiCabRateRequestModel) {
            return RateResource.get(taxiCabRateRequestModel);
        }
        return self;
    });

})(this.angular);
