/// <reference path="../helpers/misc.ts" />
/// <reference path="irepository.ts" />
/// <reference path="../models/iidentifier.ts" />

module Billboard.Repository {

	/** Generic implementation of a repository */
	export class Repository<TModel extends Models.IModel, TRawModel extends Models.IIdentifier> implements IRepository<TModel, TRawModel> {

		private controllerName: string;

		/** Create a new instance of a repository
		 * @param	controllerName	The name of the api-controller the repository will be using
		 */
		constructor(controllerName: string) {
			this.controllerName = controllerName;
		}

		public getById(id: Guid, args?: any): JQueryPromise<TRawModel> {

			// determine address
			var url = document.location.protocol + "//" + document.location.host + "/api/" + this.controllerName;
			url += "/" + id;

			if (args) {
				var isFirst: boolean = true;
				for (var key in args) {
					if (isFirst) {
						url += "?" + key + "=" + encodeURIComponent(args[key]);
						isFirst = false;
					} else {
						url += "&" + key + "=" + encodeURIComponent(args[key]);
					}
				}
			}

			return $.ajax({
				url: url,
				type: "GET",
				contentType: "application/json; charset=utf-8",
			});
		}

		public getRaw(uri?: string): JQueryPromise<any> {
			var url = document.location.protocol + "//" + document.location.host + "/api/" + this.controllerName;
			if (uri)
				url += "/" + uri;

			return $.ajax({
				url: url,
				type: "GET",
				contentType: "application/json; charset=utf-8",
			});
		}

		public persist(model: TModel): JQueryPromise<Guid> {

			// by default update
			var verb = "PUT";

			// if no id yet, then insert instead
			if ($isUndefined(model.id) || model.id === Guid.Zero) {
				verb = "POST";
			}

			// create json of model
			var jsonString: string = ko.toJSON(model);

			// determine address
			var url = document.location.protocol + "//" + document.location.host + "/api/" + this.controllerName;

			if (verb === "PUT") {
				url += "/" + model.id;
			}

			var deferred: JQueryDeferred<Guid> = $.Deferred();

			$.ajax({
				url: url,
				type: verb,
				contentType: "application/json; charset=utf-8",
				data: jsonString,
			})
				.done(id=> {
					if (verb === "POST")
						model.id = id;
					deferred.resolve(id);
				})
				.fail(deferred.reject);

			return deferred.promise();
		}

		public remove(model: TModel): JQueryPromise<boolean> {
			// determine address
			var url = document.location.protocol + "//" + document.location.host + "/api/" + this.controllerName + "/" + model.id;

			// call delete
			return $.ajax({
				url: url,
				type: "DELETE",
				contentType: "application/json; charset=utf-8"
			});
		}

	}


}