/// <reference path="../helpers/linq.ts" />
/// <reference path="viewmodel.ts" />

module Billboard.ViewModels {

	export class CategoryListViewModel extends ViewModel {

		public categories: KnockoutObservableArray<Models.CategoryModel>;
		public categoryName: KnockoutObservable<string>;

		constructor(rawModels: Models.IRawCategoryModel[]) {
			super();
			this.categories = ko.observableArray([]);

			$.each(rawModels, (ix, raw) => {
				var category = new Models.CategoryModel(raw);
				window.setTimeout(() => this.categories.push(category), 100 * ix);
			});

			this.categoryName = ko.observable("");

			// update the categories every 5 seconds. (todo: switch to SignalR)
			window.setInterval(this.updateCategories, 5000);
		}

		public select(item: Models.CategoryModel): void {
			document.location.href = "/Category/" + item.name();
		}

		public addNew(): void {
			this.isBarVisible(true);
		}

		public addNewOk(): void {
			this.isBarVisible(false);

			Repository.categories.create(this.categoryName())
				.done(category=> this.categories.push(category))
				.fail(error=> this.isBarVisible(true));
		}

		public addNewCancel(): void {
			this.isBarVisible(false);
		}

		public canGoBack(): boolean {
			return false;
		}

		/**
		 * Connect to the server and request an update of all the category information
		 */
		private updateCategories(): void {
			Repository.categories.getAllRaw()
				.done(this.receivedCategoryUpdates);
		}

		/**
		 * Called when an tile update call finished.
		 * We should now check if there are any differences and update them.
		 */
		private receivedCategoryUpdates(rawCategories: Models.IRawCategoryModel[]): void {
			// find updated or new tiles
			rawCategories.forEach(raw=> {
				var match = this.categories().filter(c=> c.id == raw.id);
				if (match.length) {
					// found so  update
					match[0].name(raw.name);
					match[0].noteCount(raw.noteCount);
				} else {
					// not found, so create new tile
					this.categories.push(new Models.CategoryModel(raw));
				}
			});

			// find deleted tiles and remove them from categories
			this.categories()
				.filter(c=> rawCategories.every(raw=> raw.id != c.id))
				.forEach(c=> this.categories.remove(c));
		}
	}

} 