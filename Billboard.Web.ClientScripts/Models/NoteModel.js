var Billboard;
(function (Billboard) {
    (function (Models) {
        var NoteModel = (function () {
            function NoteModel(rawModel) {
                this.categoryId = rawModel.categoryId;
                this.id = rawModel.id == "00000000-0000-0000-0000-000000000000" ? null : rawModel.id; // HACK: Special Case the Zero Guid.
                this.title = ko.observable(rawModel.title);
                this.message = ko.observable(rawModel.message);
                this.price = ko.observable(rawModel.price);
                this.email = ko.observable(rawModel.email);
            }
            return NoteModel;
        })();
        Models.NoteModel = NoteModel;
    })(Billboard.Models || (Billboard.Models = {}));
    var Models = Billboard.Models;
})(Billboard || (Billboard = {}));
//# sourceMappingURL=NoteModel.js.map
