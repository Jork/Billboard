/// <reference path="../helpers/linq.ts" />
/// <reference path="viewmodel.ts" />
/// <reference path="../repository/noterepository.ts" />

module Billboard.ViewModels {

	/**
	 * The ViewModel for the Note view
	 */
	export class NoteViewModel extends ViewModel {

		/** The model of the note */
		public model: Models.NoteModel;
		/** The formatted price */
		public formattedPrice: KnockoutComputed<string>;
		/** If the viewmodel has any errors */
		public hasError: KnockoutComputed<boolean>;
		/** The text of the error to display */
		public errorText: KnockoutObservable<string>;
		/** If the note can be saved */
		public canSave: KnockoutComputed<boolean>;

		/**
		 * Create a new Note ViewModel
		 */
		constructor(raw: Models.IRawNoteModel) {
			super();

			// convert the raw model
			this.model = new Models.NoteModel(raw);

			// format the price
			this.formattedPrice = ko.computed({
				read: () => $isUndefined(this.model.price()) ? "" : "€ " + Math.round(this.model.price()),
				write: (value: string) => {
					var result = parseInt(value.replace(/[^\d]*/g, ""), 10);
					if (!isNaN(result)) {
						this.model.price(result);
					}
				},
				owner: this
			});

			this.canSave = ko.computed( () => $hasValue(this.model.title) && $hasValue(this.model.message) && $hasValue(this.model.email) );

			// keep track of errors
			this.errorText = ko.observable(null);
			this.hasError = ko.computed(() => this.errorText() != null);
		}

		/** 
		 * Call to Save the changes in the note 
		 */
		public save() {
			this.errorText(null);

			Repository.notes.persist(this.model)
				.done(this.onSaved)
				.fail(this.onSaveFailed);
		}

		/** 
		 * Call to cancel the changes in the note 
		 */
		public cancel() {
			// go back on a cancel
			window.history.back();
		}
		
		/** 
		 * Called when successfully saved 
		 */
		private onSaved() {
			this.goBack();
		}

		/** 
		 * Called when the save failed. 
		 */
		private onSaveFailed(error: any) {
			this.errorText("Opslaan notitie mislukt. " + JSON.stringify(error));
		}

		public canGoBack(): boolean {
			return true;
		}

		public goBack(): void {
			// query repository for category this note belongs to.
			// if found, go to that category, otherwise go to previous page.
			Repository.categories.getById(this.model.categoryId)
				.done(category=> document.location.href = "/Category/" + category.name())
				.fail(window.history.back);
		}

		/**
		 * Called when the user clicks the delete button to request deletion
		 */
		public askDelete(): void {
			this.isBarVisible(true);
		}

		/***
		 * Called when users confirms the deletion of the note
		 */
		public confirmDelete(): void {
			this.isBarVisible(false);
			Repository.notes.remove(this.model)
				.done(this.goBack)
				.fail(()=> this.isBarVisible(true));
		}

		/***
		 * Called when the user aborts the deletion of the note
		 */
		public abortDelete(): void {
			this.isBarVisible(false);
		}
	}

} 