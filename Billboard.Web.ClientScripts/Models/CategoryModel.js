var Billboard;
(function (Billboard) {
    (function (Models) {
        var CategoryModel = (function () {
            function CategoryModel(rawModel) {
                this.id = rawModel.id;
                this.name = ko.observable(rawModel.name);
                this.notes = ko.observableArray($isUndefined(rawModel.notes) ? null : rawModel.notes.select(function (n) {
                    return new Billboard.Models.NoteModel(n);
                }));
                this.noteCount = ko.observable(rawModel.noteCount);
            }
            return CategoryModel;
        })();
        Models.CategoryModel = CategoryModel;
    })(Billboard.Models || (Billboard.Models = {}));
    var Models = Billboard.Models;
})(Billboard || (Billboard = {}));
//# sourceMappingURL=CategoryModel.js.map
