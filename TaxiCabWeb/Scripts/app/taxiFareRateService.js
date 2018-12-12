(function (ng) {

    var app = ng.module('appModule');
    if (!app) return;

    app.factory('taxiFareRateService', function ($resource) {

        var self = this;
        var ctrlName = '/Home/CalculateRate';
        var href = window.location.href;
        var url = href.endsWith('/Home/Index') ? href.replace('/Home/Index', ctrlName) : ctrlName;
        var RateResource = $resource(url);

        self.getFare = function (taxiCabRateRequestModel) {
            return RateResource.get(taxiCabRateRequestModel);
        }
        return self;
    });

})(this.angular);
