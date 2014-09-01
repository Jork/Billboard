var __extends = this.__extends || function (d, b) {
    for (var p in b) if (b.hasOwnProperty(p)) d[p] = b[p];
    function __() { this.constructor = d; }
    __.prototype = b.prototype;
    d.prototype = new __();
};
var Billboard;
(function (Billboard) {
    (function (ViewModels) {
        var NoteTileViewModel = (function (_super) {
            __extends(NoteTileViewModel, _super);
            function NoteTileViewModel(model) {
                var _this = this;
                _super.call(this);

                this.model = model;

                this.formattedPrice = ko.computed(function () {
                    return isNaN(_this.model.price()) || _this.model.price() === null || _this.model.price() === void 0 ? "" : "€ " + Math.round(_this.model.price()).toString();
                });
            }
            NoteTileViewModel.prototype.select = function () {
                document.location.href = "/Note/" + this.model.id;
            };
            return NoteTileViewModel;
        })(ViewModels.ViewModel);
        ViewModels.NoteTileViewModel = NoteTileViewModel;

        var CategoryViewModel = (function (_super) {
            __extends(CategoryViewModel, _super);
            function CategoryViewModel(raw) {
                var _this = this;
                _super.call(this);

                this.category = new Billboard.Models.CategoryModel(raw);
                this.notes = ko.observableArray([]);

                $.each(this.category.notes(), function (ix, note) {
                    var noteViewModel = new NoteTileViewModel(note);
                    window.setTimeout(function () {
                        return _this.notes.push(noteViewModel);
                    }, 100 * ix);
                });

                window.setInterval(this.updateNotes, 5000);
            }
            CategoryViewModel.prototype.addNew = function () {
                document.location.assign("/Note?catId=" + this.category.id);
            };

            CategoryViewModel.prototype.canGoBack = function () {
                return true;
            };

            CategoryViewModel.prototype.goBack = function () {
                document.location.assign("/Category");
            };

            CategoryViewModel.prototype.updateNotes = function () {
                Billboard.Repository.notes.getNotesInCategoryRaw(this.category).done(this.receivedCategoryNotesUpdate).fail(function (error) {
                    debugger;
                });
            };

            CategoryViewModel.prototype.receivedCategoryNotesUpdate = function (rawNotes) {
                var _this = this;
                rawNotes.forEach(function (raw) {
                    var match = _this.notes().filter(function (vm) {
                        return vm.model.id == raw.id;
                    });
                    if (match.length) {
                        match[0].model.title(raw.title);
                        match[0].model.price(raw.price);
                    } else {
                        _this.notes.push(new NoteTileViewModel(new Billboard.Models.NoteModel(raw)));
                    }
                });

                this.notes().filter(function (vm) {
                    return rawNotes.every(function (raw) {
                        return raw.id != vm.model.id;
                    });
                }).forEach(function (vm) {
                    return _this.notes.remove(vm);
                });
            };
            return CategoryViewModel;
        })(ViewModels.ViewModel);
        ViewModels.CategoryViewModel = CategoryViewModel;
    })(Billboard.ViewModels || (Billboard.ViewModels = {}));
    var ViewModels = Billboard.ViewModels;
})(Billboard || (Billboard = {}));
