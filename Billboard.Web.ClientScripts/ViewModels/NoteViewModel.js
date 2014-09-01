var __extends = this.__extends || function (d, b) {
    for (var p in b) if (b.hasOwnProperty(p)) d[p] = b[p];
    function __() { this.constructor = d; }
    __.prototype = b.prototype;
    d.prototype = new __();
};
var Billboard;
(function (Billboard) {
    (function (ViewModels) {
        var NoteViewModel = (function (_super) {
            __extends(NoteViewModel, _super);
            function NoteViewModel(raw) {
                var _this = this;
                _super.call(this);

                this.model = new Billboard.Models.NoteModel(raw);

                this.formattedPrice = ko.computed({
                    read: function () {
                        return $isUndefined(_this.model.price()) ? "" : "€ " + Math.round(_this.model.price());
                    },
                    write: function (value) {
                        var result = parseInt(value.replace(/[^\d]*/g, ""), 10);
                        if (!isNaN(result)) {
                            _this.model.price(result);
                        }
                    },
                    owner: this
                });

                this.canSave = ko.computed(function () {
                    return $hasValue(_this.model.title) && $hasValue(_this.model.message) && $hasValue(_this.model.email);
                });

                this.errorText = ko.observable(null);
                this.hasError = ko.computed(function () {
                    return _this.errorText() != null;
                });
            }
            NoteViewModel.prototype.save = function () {
                this.errorText(null);

                Billboard.Repository.notes.persist(this.model).done(this.onSaved).fail(this.onSaveFailed);
            };

            NoteViewModel.prototype.cancel = function () {
                window.history.back();
            };

            NoteViewModel.prototype.onSaved = function () {
                this.goBack();
            };

            NoteViewModel.prototype.onSaveFailed = function (error) {
                this.errorText("Opslaan notitie mislukt. " + JSON.stringify(error));
            };

            NoteViewModel.prototype.canGoBack = function () {
                return true;
            };

            NoteViewModel.prototype.goBack = function () {
                Billboard.Repository.categories.getById(this.model.categoryId).done(function (category) {
                    return document.location.href = "/Category/" + category.name();
                }).fail(window.history.back);
            };

            NoteViewModel.prototype.askDelete = function () {
                this.isBarVisible(true);
            };

            NoteViewModel.prototype.confirmDelete = function () {
                var _this = this;
                this.isBarVisible(false);
                Billboard.Repository.notes.remove(this.model).done(this.goBack).fail(function () {
                    return _this.isBarVisible(true);
                });
            };

            NoteViewModel.prototype.abortDelete = function () {
                this.isBarVisible(false);
            };
            return NoteViewModel;
        })(ViewModels.ViewModel);
        ViewModels.NoteViewModel = NoteViewModel;
    })(Billboard.ViewModels || (Billboard.ViewModels = {}));
    var ViewModels = Billboard.ViewModels;
})(Billboard || (Billboard = {}));
