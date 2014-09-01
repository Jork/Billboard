/// <reference path="Scripts/typings/jquery/jquery.d.ts"/>
/// <reference path="Scripts/typings/signalr/signalr.d.ts"/>
/// <reference path="Scripts/typings/jquery.transit/jquery.transit.d.ts" />
/// <reference path="Scripts/typings/jqueryui/jqueryui.d.ts" />
/// <reference path="Scripts/typings/knockout/knockout.d.ts" />

module Billboard {

	interface IStartupArguments {
		viewModelName: string;
		model: any;
	}

	export function startup() {

		// get element with startup json
		var startupJsonElement = document.getElementById("startupJson");
		var startupJson = startupJsonElement.innerText;
		var startupArgs: IStartupArguments = JSON.parse(startupJson);

		// create viewmodel
		var viewModel = new Billboard.ViewModels[startupArgs.viewModelName](startupArgs.model);

		// and apply viewmodel using knockout
		ko.applyBindings(viewModel);
	}
}

$(document).ready(Billboard.startup);