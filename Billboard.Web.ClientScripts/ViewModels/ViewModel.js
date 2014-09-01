var Billboard;
(function (Billboard) {
    (function (ViewModels) {
        var ViewModel = (function () {
            function ViewModel() {
                this.isBarVisible = ko.observable(false);

                Billboard.Helpers.rebind(this);
            }
            ViewModel.prototype.canGoBack = function () {
                return window.history.length > 1;
            };

            ViewModel.prototype.goBack = function () {
                window.history.back();
            };
            return ViewModel;
        })();
        ViewModels.ViewModel = ViewModel;
    })(Billboard.ViewModels || (Billboard.ViewModels = {}));
    var ViewModels = Billboard.ViewModels;
})(Billboard || (Billboard = {}));
