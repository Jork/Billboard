var Billboard;
(function (Billboard) {
    function startup() {
        var startupJsonElement = document.getElementById("startupJson");
        var startupJson = startupJsonElement.innerText;
        var startupArgs = JSON.parse(startupJson);

        var viewModel = new Billboard.ViewModels[startupArgs.viewModelName](startupArgs.model);

        ko.applyBindings(viewModel);
    }
    Billboard.startup = startup;
})(Billboard || (Billboard = {}));

$(document).ready(Billboard.startup);
