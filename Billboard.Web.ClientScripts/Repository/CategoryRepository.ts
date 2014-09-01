/// <reference path="repository.ts" />

module Billboard.Repository {

	export class CategoryRepository {

		private repository: Repository<Models.CategoryModel, Models.IRawCategoryModel>;

		constructor() {
			this.repository = new Repository<Models.CategoryModel, Models.IRawCategoryModel>("Category");
		}

		/**
		 * Create a new Category with the given name
		 * @param	categoryName	The name of the new category
		 */
		public create(categoryName: string): JQueryPromise<Models.CategoryModel> {
			var deferred = $.Deferred();

			var model =
				new Models.CategoryModel(
				{
					id: null,
					name: categoryName,
					notes: [],
					noteCount: 0
				});

			this.repository.persist(model)
				.done(id=> deferred.resolve(model))
				.fail(deferred.reject);

			return deferred.promise();
		}

		/**
		 * Persist the changes to the Category
		 */
		public persist(model: Models.CategoryModel): JQueryPromise<Guid> {
			return this.repository.persist(model);
		}

		public remove(model: Models.CategoryModel): JQueryPromise<boolean> {
			return this.repository.remove(model);
		}

		/**
		 * Get the Category by the Id
		 * @return		A promise that will return the category
		 */
		public getById(id: Guid): JQueryPromise<Models.CategoryModel> {

			var deferred: JQueryDeferred<Models.CategoryModel> = $.Deferred();

			// call actual generic repository to retrieve the data
			this.repository.getById(id)
				// when data arrived, convert raw data to model
				.done(
					(raw: Models.IRawCategoryModel) => {
						var model = new Models.CategoryModel(raw);
						deferred.resolve(model);
					})
				// when failed, pass the failure on.
				.fail(deferred.reject);

			return deferred.promise();
		}

		public getByIdRaw(id: Guid, withNotes: boolean): JQueryPromise<Models.IRawCategoryModel> {
			return this.repository.getById(id, { withNotes: withNotes });
		}

		/**
		 * Get all the categories in a RAW format including a note count
		 */
		public getAllRaw(): JQueryPromise<Models.IRawCategoryModel[]> {
			return this.repository.getRaw();
		}

	}

	export var categories: CategoryRepository = new CategoryRepository();
}