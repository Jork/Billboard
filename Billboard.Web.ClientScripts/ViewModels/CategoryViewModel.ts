/// <reference path="../helpers/linq.ts" />
/// <reference path="viewmodel.ts" />

module Billboard.ViewModels {

	/** ViewModel for a note on the Category List */
	export class NoteTileViewModel extends ViewModel {

		public model: Models.NoteModel;
		
		/** Formatted Price of the item on the Note */
		public formattedPrice: KnockoutComputed<string>;

		/** Create a new ViewModel for a Note on the Category List */
		constructor(model: Models.NoteModel) {
			super();

			this.model = model;

			this.formattedPrice = ko.computed(
				() =>
					isNaN(this.model.price()) || this.model.price() === null || this.model.price() === void 0
						? ""
						: "€ " + Math.round(this.model.price()).toString());
		}

		public select(): void {
			// navigate to the actual note.
			document.location.href = "/Note/" + this.model.id;
		}

	}

	export class CategoryViewModel extends ViewModel {

		public category: Models.CategoryModel;
		public notes: KnockoutObservableArray<NoteTileViewModel>;

		constructor(raw: Models.IRawCategoryModel) {
			super();

			this.category = new Models.CategoryModel(raw);
			this.notes = ko.observableArray([]);

			$.each(this.category.notes(), (ix, note) => {
				var noteViewModel = new NoteTileViewModel(note);
				window.setTimeout(() => this.notes.push(noteViewModel), 100 * ix);
			});

			window.setInterval(this.updateNotes, 5000);
		}

		public addNew(): void {
			document.location.assign("/Note?catId=" + this.category.id);
		}

		public canGoBack(): boolean {
			return true;
		}

		public goBack(): void {
			document.location.assign("/Category");
		}

		/**
		 * Connect to the server and request an update of all the notes
		 */
		private updateNotes(): void {
			Repository.notes.getNotesInCategoryRaw(this.category)
				.done(this.receivedCategoryNotesUpdate)
				.fail(error=> { debugger; });
		}

		/**
		 * Called when an tile update call finished.
		 * We should now check if there are any differences and update them.
		 */
		private receivedCategoryNotesUpdate(rawNotes: Models.IRawNoteModel[]): void {
			// find updated or new tiles
			rawNotes.forEach(raw=> {
				var match = this.notes().filter(vm=> vm.model.id == raw.id);
				if (match.length) {
					// found so  update
					match[0].model.title(raw.title);
					match[0].model.price(raw.price);
				} else {
					// not found, so create new tile
					this.notes.push(new NoteTileViewModel(new Models.NoteModel(raw)));
				}
			});

			// find deleted tiles and remove them from notes
			this.notes()
				.filter(vm=> rawNotes.every(raw=> raw.id != vm.model.id))
				.forEach(vm=> this.notes.remove(vm));
		}
	}

} 