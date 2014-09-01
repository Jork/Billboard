/// <reference path="../../typings/knockout/knockout.d.ts" />
/// <reference path="../helpers/linq.ts" />
/// <reference path="../helpers/misc.ts" />
/// <reference path="ViewModel.ts"/>
/// <reference path="viewhelpers.ts" />
var __extends = this.__extends || function (d, b) {
    for (var p in b) if (b.hasOwnProperty(p)) d[p] = b[p];
    function __() { this.constructor = d; }
    __.prototype = b.prototype;
    d.prototype = new __();
};
var Billboard;
(function (Billboard) {
    (function (ViewModels) {
        /** ViewModel for a note on the Category List */
        var NoteTileViewModel = (function (_super) {
            __extends(NoteTileViewModel, _super);
            /** Create a new ViewModel for a Note on the Category List */
            function NoteTileViewModel(id, title, price) {
                var _this = this;
                _super.call(this);

                this.id = id;
                this.title = ko.observable(title);
                this.price = ko.observable(price);

                this.formattedPrice = ko.computed(function () {
                    return isNaN(_this.price()) || _this.price() === null || _this.price() === void 0 ? "" : "€ " + Math.round(_this.price()).toString();
                });
            }
            NoteTileViewModel.prototype.select = function () {
                // navigate to the actual note.
                document.location.href = "/Note/" + this.id;
            };
            return NoteTileViewModel;
        })(Billboard.ViewModels.ViewModel);
        ViewModels.NoteTileViewModel = NoteTileViewModel;

        var CategoryListContentViewModel = (function (_super) {
            __extends(CategoryListContentViewModel, _super);
            function CategoryListContentViewModel(raw) {
                _super.call(this);

                // create viewmodel for every note
                this.noteTiles = ko.observableArray(raw.notes.select(function (n) {
                    return new NoteTileViewModel(n.id, n.title, n.price);
                }));
            }
            return CategoryListContentViewModel;
        })(Billboard.ViewModels.ViewModel);
        ViewModels.CategoryListContentViewModel = CategoryListContentViewModel;
    })(Billboard.ViewModels || (Billboard.ViewModels = {}));
    var ViewModels = Billboard.ViewModels;
})(Billboard || (Billboard = {}));
//# sourceMappingURL=CategoryListContentViewModel.js.map
