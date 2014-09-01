/// <reference path="repository.ts" />

module Billboard.Repository {

	export class NoteRepository {

		private repository: Repository<Models.NoteModel, Models.IRawNoteModel>;

		constructor() {
			this.repository = new Repository<Models.NoteModel, Models.IRawNoteModel>("Note");
		}

		/**
		 * Get the Note by the Id
		 * @return		A promise that will return the note
		 */
		public getById(id: Guid): JQueryPromise<Models.NoteModel> {
			var deferred: JQueryDeferred<Models.NoteModel> = $.Deferred();

			// call actual generic repository to retrieve the data
			this.repository.getById(id)
			// when data arrived, convert raw data to model
				.done(
				(raw: Models.IRawNoteModel) => {
					var model = new Models.NoteModel(raw);
					deferred.resolve(model);
				})
			// when failed, pass the failure on.
				.fail(deferred.reject);

			return deferred.promise();
		}

		public persist(model: Models.NoteModel): JQueryPromise<Guid> {
			return this.repository.persist(model);
		}

		/**
		 * remove the given note from the repository
		 */
		public remove(model: Models.NoteModel): JQueryPromise<boolean> {
			return this.repository.remove(model);
		}

		/**
		 * Gets all the notes in the given category, in a raw format
		 */
		public getNotesInCategoryRaw(categoryModel: Models.CategoryModel): JQueryPromise<Models.IRawNoteModel[]> {
			var deferred: JQueryDeferred<Models.IRawNoteModel[]> = $.Deferred();

			categories.getByIdRaw(categoryModel.id, true)
				.done(rawCat=> deferred.resolve(rawCat.notes))
				.fail(error=> deferred.reject(error));

			return deferred.promise();
		}

	}

	export var notes: NoteRepository = new NoteRepository();
}