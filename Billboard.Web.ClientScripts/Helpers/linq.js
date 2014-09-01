Array.prototype.select = function select(convert) {
    var result = [];

    for (var index = 0; index < this.length; index++)
        result[index] = convert(this[index]);

    return result;
};
