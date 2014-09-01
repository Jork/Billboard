interface Array<T> {
	select<TResult>(convert: (element: T) => TResult): Array<TResult>;
}

Array.prototype.select =
	function select<TResult>(convert: (element: Text) => TResult): Array<TResult> {

		var result: Array<TResult> = [];

		for (var index = 0; index < this.length; index++)
			result[index] = convert(this[index]);

		return result;
	};