interface Guid extends String {
}

module Guid {
	export var Zero: Guid = "00000000-0000-0000-0000-000000000000";
}

module Billboard.Helpers {

	export function IsNullOrUndefinedOrEmpty(obj: any): boolean {
		if (isNullOrUndefined(obj) === true || obj === "")
			return true;

		return false;
	}

	/** Check if any of the given object references is null or undefined */
	export function isNullOrUndefined(...obj: any[]): boolean;
	/** Check if the given object reference is null or undefined */
	export function isNullOrUndefined(obj: any): boolean {
		if (arguments.length === 0)
			return false;

		if (arguments.length > 1) {
			for (var i = 0; i < arguments.length; i++) {
				var value = arguments[i];

				// check if it is an observable, yes, then first get value from the observable
				if (ko.isObservable(value))
					value = value();

				if (arguments[i] === null || arguments[i] === void 0)
					return true;
			}
		}
		else {
			var value = obj;

			// check if it is an observable, yes, then first get value from the observable
			if (ko.isObservable(value))
				value = value();

			if (value === null || value === void 0)
				return true;
		}

		return false;
	}

	/** Rebind the this of all methods on the given object, to itself 	*/
	export function rebind(obj: any) {
		var prototype = <Object>obj.constructor.prototype;

		// loop over all items on the object prototype
		for (var key in prototype) {
			var name: string = key;
			// if the field is a custom field and if it is a function
			if (!obj.hasOwnProperty(name) && $.isFunction(prototype[name])) {
				var method = <Function>prototype[name];

				// check if it a function and not an observable property
				if (name !== "constructor" && !ko.isObservable(<any>method)) {
					// 'replace' the method with a method where the this pointer has been rebound to the object itself
					if ($isUndefined(method, method.bind))
						obj[name] = $.proxy(method, obj);
					else
						obj[name] = method.bind(obj);
				}
			}
		}
	}
}

/**	Check if the given object reference is null or undefined
*/
var $isUndefined = Billboard.Helpers.isNullOrUndefined;

/**	Check if the given object reference is not null and not undefined and not an empty string, i.e. if it has an actual value.
*/
var $hasValue = obj => !Billboard.Helpers.IsNullOrUndefinedOrEmpty(obj);