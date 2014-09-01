/// <reference path="../models/iidentifier.ts" />

module Billboard.Repository {

	/** Generic interface a repository should implement */
	export interface IRepository<TModel extends Models.IModel, TRawModel extends Models.IIdentifier> {

		/** Get the model by it's id
		 * @param	id	The id of the moel to get
		 * @return		The promise that will return the requested model, or null when it could not be found.
		 */
		getById(id: Guid): JQueryPromise<TRawModel>;

		/** Persist the changes or new model 
		 * @param	model	The model to persist
		 */
		persist(model: TModel): JQueryPromise<Guid>;

		/** Remove the given model *
		 * @param	model	The model to remove
		 */
		remove(model: TModel): JQueryPromise<boolean>;
	}

}