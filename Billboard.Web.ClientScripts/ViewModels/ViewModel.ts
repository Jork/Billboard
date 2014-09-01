/// <reference path="../helpers/misc.ts" />

module Billboard.ViewModels {
	
	export class ViewModel {
		
		public isBarVisible: KnockoutObservable<boolean>;

		constructor() {
			this.isBarVisible = ko.observable(false);

			// rebind the this pointer so viewmodel can be used with knockout.
			Billboard.Helpers.rebind(this);
		}

		public canGoBack(): boolean {
			return window.history.length > 1;
		}

		public goBack(): void {
			window.history.back();
		}

	}

} 