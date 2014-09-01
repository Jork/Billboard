var Billboard;
(function (Billboard) {
    (function (Models) {
        var CategoryModel = (function () {
            function CategoryModel(raw) {
                this.id = raw.id;
                this.name = ko.observable(raw.name);
                this.notes = ko.observableArray($isUndefined(raw.notes) ? [] : raw.notes.select(function (r) {
                    return new NoteModel(r);
                }));
                this.noteCount = ko.observable(raw.noteCount);
            }
            return CategoryModel;
        })();
        Models.CategoryModel = CategoryModel;

        var NoteModel = (function () {
            function NoteModel(raw) {
                this.categoryId = raw.categoryId;
                this.id = raw.id;
                this.title = ko.observable(raw.title);
                this.message = ko.observable(raw.message);
                this.price = ko.observable(raw.price);
                this.email = ko.observable(raw.email);
            }
            return NoteModel;
        })();
        Models.NoteModel = NoteModel;
    })(Billboard.Models || (Billboard.Models = {}));
    var Models = Billboard.Models;
})(Billboard || (Billboard = {}));
