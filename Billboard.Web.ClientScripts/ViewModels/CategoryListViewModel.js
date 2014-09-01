var __extends = this.__extends || function (d, b) {
    for (var p in b) if (b.hasOwnProperty(p)) d[p] = b[p];
    function __() { this.constructor = d; }
    __.prototype = b.prototype;
    d.prototype = new __();
};
var Billboard;
(function (Billboard) {
    (function (ViewModels) {
        var CategoryListViewModel = (function (_super) {
            __extends(CategoryListViewModel, _super);
            function CategoryListViewModel(rawModels) {
                var _this = this;
                _super.call(this);
                this.categories = ko.observableArray([]);

                $.each(rawModels, function (ix, raw) {
                    var category = new Billboard.Models.CategoryModel(raw);
                    window.setTimeout(function () {
                        return _this.categories.push(category);
                    }, 100 * ix);
                });

                this.categoryName = ko.observable("");

                window.setInterval(this.updateCategories, 5000);
            }
            CategoryListViewModel.prototype.select = function (item) {
                document.location.href = "/Category/" + item.name();
            };

            CategoryListViewModel.prototype.addNew = function () {
                this.isBarVisible(true);
            };

            CategoryListViewModel.prototype.addNewOk = function () {
                var _this = this;
                this.isBarVisible(false);

                Billboard.Repository.categories.create(this.categoryName()).done(function (category) {
                    return _this.categories.push(category);
                }).fail(function (error) {
                    return _this.isBarVisible(true);
                });
            };

            CategoryListViewModel.prototype.addNewCancel = function () {
                this.isBarVisible(false);
            };

            CategoryListViewModel.prototype.canGoBack = function () {
                return false;
            };

            CategoryListViewModel.prototype.updateCategories = function () {
                Billboard.Repository.categories.getAllRaw().done(this.receivedCategoryUpdates);
            };

            CategoryListViewModel.prototype.receivedCategoryUpdates = function (rawCategories) {
                var _this = this;
                rawCategories.forEach(function (raw) {
                    var match = _this.categories().filter(function (c) {
                        return c.id == raw.id;
                    });
                    if (match.length) {
                        match[0].name(raw.name);
                        match[0].noteCount(raw.noteCount);
                    } else {
                        _this.categories.push(new Billboard.Models.CategoryModel(raw));
                    }
                });

                this.categories().filter(function (c) {
                    return rawCategories.every(function (raw) {
                        return raw.id != c.id;
                    });
                }).forEach(function (c) {
                    return _this.categories.remove(c);
                });
            };
            return CategoryListViewModel;
        })(ViewModels.ViewModel);
        ViewModels.CategoryListViewModel = CategoryListViewModel;
    })(Billboard.ViewModels || (Billboard.ViewModels = {}));
    var ViewModels = Billboard.ViewModels;
})(Billboard || (Billboard = {}));
