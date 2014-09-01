Array.prototype.select = function select(convert) {
    var result = [];

    for (var index = 0; index < this.length; index++)
        result[index] = convert(this[index]);

    return result;
};
var Guid;
(function (Guid) {
    Guid.Zero = "00000000-0000-0000-0000-000000000000";
})(Guid || (Guid = {}));

var Billboard;
(function (Billboard) {
    (function (Helpers) {
        function IsNullOrUndefinedOrEmpty(obj) {
            if (isNullOrUndefined(obj) === true || obj === "")
                return true;

            return false;
        }
        Helpers.IsNullOrUndefinedOrEmpty = IsNullOrUndefinedOrEmpty;

        

        /** Check if the given object reference is null or undefined */
        function isNullOrUndefined(obj) {
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
            } else {
                var value = obj;

                // check if it is an observable, yes, then first get value from the observable
                if (ko.isObservable(value))
                    value = value();

                if (value === null || value === void 0)
                    return true;
            }

            return false;
        }
        Helpers.isNullOrUndefined = isNullOrUndefined;

        /** Rebind the this of all methods on the given object, to itself 	*/
        function rebind(obj) {
            var prototype = obj.constructor.prototype;

            for (var key in prototype) {
                var name = key;

                // if the field is a custom field and if it is a function
                if (!obj.hasOwnProperty(name) && $.isFunction(prototype[name])) {
                    var method = prototype[name];

                    // check if it a function and not an observable property
                    if (name !== "constructor" && !ko.isObservable(method)) {
                        // 'replace' the method with a method where the this pointer has been rebound to the object itself
                        if ($isUndefined(method, method.bind))
                            obj[name] = $.proxy(method, obj);
                        else
                            obj[name] = method.bind(obj);
                    }
                }
            }
        }
        Helpers.rebind = rebind;
    })(Billboard.Helpers || (Billboard.Helpers = {}));
    var Helpers = Billboard.Helpers;
})(Billboard || (Billboard = {}));

/**	Check if the given object reference is null or undefined
*/
var $isUndefined = Billboard.Helpers.isNullOrUndefined;

/**	Check if the given object reference is not null and not undefined and not an empty string, i.e. if it has an actual value.
*/
var $hasValue = function (obj) {
    return !Billboard.Helpers.IsNullOrUndefinedOrEmpty(obj);
};
/// <reference path="../helpers/misc.ts" />
/// <reference path="../helpers/linq.ts" />
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
/// <reference path="../models/iidentifier.ts" />
/// <reference path="../helpers/misc.ts" />
/// <reference path="irepository.ts" />
/// <reference path="../models/iidentifier.ts" />
var Billboard;
(function (Billboard) {
    (function (_Repository) {
        /** Generic implementation of a repository */
        var Repository = (function () {
            /** Create a new instance of a repository
            * @param	controllerName	The name of the api-controller the repository will be using
            */
            function Repository(controllerName) {
                this.controllerName = controllerName;
            }
            Repository.prototype.getById = function (id, args) {
                // determine address
                var url = document.location.protocol + "//" + document.location.host + "/api/" + this.controllerName;
                url += "/" + id;

                if (args) {
                    var isFirst = true;
                    for (var key in args) {
                        if (isFirst) {
                            url += "?" + key + "=" + encodeURIComponent(args[key]);
                            isFirst = false;
                        } else {
                            url += "&" + key + "=" + encodeURIComponent(args[key]);
                        }
                    }
                }

                return $.ajax({
                    url: url,
                    type: "GET",
                    contentType: "application/json; charset=utf-8"
                });
            };

            Repository.prototype.getRaw = function (uri) {
                var url = document.location.protocol + "//" + document.location.host + "/api/" + this.controllerName;
                if (uri)
                    url += "/" + uri;

                return $.ajax({
                    url: url,
                    type: "GET",
                    contentType: "application/json; charset=utf-8"
                });
            };

            Repository.prototype.persist = function (model) {
                // by default update
                var verb = "PUT";

                // if no id yet, then insert instead
                if ($isUndefined(model.id) || model.id === Guid.Zero) {
                    verb = "POST";
                }

                // create json of model
                var jsonString = ko.toJSON(model);

                // determine address
                var url = document.location.protocol + "//" + document.location.host + "/api/" + this.controllerName;

                if (verb === "PUT") {
                    url += "/" + model.id;
                }

                var deferred = $.Deferred();

                $.ajax({
                    url: url,
                    type: verb,
                    contentType: "application/json; charset=utf-8",
                    data: jsonString
                }).done(function (id) {
                    if (verb === "POST")
                        model.id = id;
                    deferred.resolve(id);
                }).fail(deferred.reject);

                return deferred.promise();
            };

            Repository.prototype.remove = function (model) {
                // determine address
                var url = document.location.protocol + "//" + document.location.host + "/api/" + this.controllerName + "/" + model.id;

                // call delete
                return $.ajax({
                    url: url,
                    type: "DELETE",
                    contentType: "application/json; charset=utf-8"
                });
            };
            return Repository;
        })();
        _Repository.Repository = Repository;
    })(Billboard.Repository || (Billboard.Repository = {}));
    var Repository = Billboard.Repository;
})(Billboard || (Billboard = {}));
/// <reference path="repository.ts" />
var Billboard;
(function (Billboard) {
    (function (Repository) {
        var CategoryRepository = (function () {
            function CategoryRepository() {
                this.repository = new Repository.Repository("Category");
            }
            /**
            * Create a new Category with the given name
            * @param	categoryName	The name of the new category
            */
            CategoryRepository.prototype.create = function (categoryName) {
                var deferred = $.Deferred();

                var model = new Billboard.Models.CategoryModel({
                    id: null,
                    name: categoryName,
                    notes: [],
                    noteCount: 0
                });

                this.repository.persist(model).done(function (id) {
                    return deferred.resolve(model);
                }).fail(deferred.reject);

                return deferred.promise();
            };

            /**
            * Persist the changes to the Category
            */
            CategoryRepository.prototype.persist = function (model) {
                return this.repository.persist(model);
            };

            CategoryRepository.prototype.remove = function (model) {
                return this.repository.remove(model);
            };

            /**
            * Get the Category by the Id
            * @return		A promise that will return the category
            */
            CategoryRepository.prototype.getById = function (id) {
                var deferred = $.Deferred();

                // call actual generic repository to retrieve the data
                this.repository.getById(id).done(function (raw) {
                    var model = new Billboard.Models.CategoryModel(raw);
                    deferred.resolve(model);
                }).fail(deferred.reject);

                return deferred.promise();
            };

            CategoryRepository.prototype.getByIdRaw = function (id, withNotes) {
                return this.repository.getById(id, { withNotes: withNotes });
            };

            /**
            * Get all the categories in a RAW format including a note count
            */
            CategoryRepository.prototype.getAllRaw = function () {
                return this.repository.getRaw();
            };
            return CategoryRepository;
        })();
        Repository.CategoryRepository = CategoryRepository;

        Repository.categories = new CategoryRepository();
    })(Billboard.Repository || (Billboard.Repository = {}));
    var Repository = Billboard.Repository;
})(Billboard || (Billboard = {}));
/// <reference path="repository.ts" />
var Billboard;
(function (Billboard) {
    (function (Repository) {
        var NoteRepository = (function () {
            function NoteRepository() {
                this.repository = new Repository.Repository("Note");
            }
            /**
            * Get the Note by the Id
            * @return		A promise that will return the note
            */
            NoteRepository.prototype.getById = function (id) {
                var deferred = $.Deferred();

                // call actual generic repository to retrieve the data
                this.repository.getById(id).done(function (raw) {
                    var model = new Billboard.Models.NoteModel(raw);
                    deferred.resolve(model);
                }).fail(deferred.reject);

                return deferred.promise();
            };

            NoteRepository.prototype.persist = function (model) {
                return this.repository.persist(model);
            };

            /**
            * remove the given note from the repository
            */
            NoteRepository.prototype.remove = function (model) {
                return this.repository.remove(model);
            };

            /**
            * Gets all the notes in the given category, in a raw format
            */
            NoteRepository.prototype.getNotesInCategoryRaw = function (categoryModel) {
                var deferred = $.Deferred();

                Repository.categories.getByIdRaw(categoryModel.id, true).done(function (rawCat) {
                    return deferred.resolve(rawCat.notes);
                }).fail(function (error) {
                    return deferred.reject(error);
                });

                return deferred.promise();
            };
            return NoteRepository;
        })();
        Repository.NoteRepository = NoteRepository;

        Repository.notes = new NoteRepository();
    })(Billboard.Repository || (Billboard.Repository = {}));
    var Repository = Billboard.Repository;
})(Billboard || (Billboard = {}));
/// <reference path="Scripts/typings/jquery/jquery.d.ts"/>
/// <reference path="Scripts/typings/signalr/signalr.d.ts"/>
/// <reference path="Scripts/typings/jquery.transit/jquery.transit.d.ts" />
/// <reference path="Scripts/typings/jqueryui/jqueryui.d.ts" />
/// <reference path="Scripts/typings/knockout/knockout.d.ts" />
var Billboard;
(function (Billboard) {
    function startup() {
        // get element with startup json
        var startupJsonElement = document.getElementById("startupJson");
        var startupJson = startupJsonElement.innerText;
        var startupArgs = JSON.parse(startupJson);

        // create viewmodel
        var viewModel = new Billboard.ViewModels[startupArgs.viewModelName](startupArgs.model);

        // and apply viewmodel using knockout
        ko.applyBindings(viewModel);
    }
    Billboard.startup = startup;
})(Billboard || (Billboard = {}));

$(document).ready(Billboard.startup);
/// <reference path="../helpers/misc.ts" />
var Billboard;
(function (Billboard) {
    (function (ViewModels) {
        var ViewModel = (function () {
            function ViewModel() {
                this.isBarVisible = ko.observable(false);

                // rebind the this pointer so viewmodel can be used with knockout.
                Billboard.Helpers.rebind(this);
            }
            ViewModel.prototype.canGoBack = function () {
                return window.history.length > 1;
            };

            ViewModel.prototype.goBack = function () {
                window.history.back();
            };
            return ViewModel;
        })();
        ViewModels.ViewModel = ViewModel;
    })(Billboard.ViewModels || (Billboard.ViewModels = {}));
    var ViewModels = Billboard.ViewModels;
})(Billboard || (Billboard = {}));
/// <reference path="../helpers/linq.ts" />
/// <reference path="viewmodel.ts" />
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

                // update the categories every 5 seconds. (todo: switch to SignalR)
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

            /**
            * Connect to the server and request an update of all the category information
            */
            CategoryListViewModel.prototype.updateCategories = function () {
                Billboard.Repository.categories.getAllRaw().done(this.receivedCategoryUpdates);
            };

            /**
            * Called when an tile update call finished.
            * We should now check if there are any differences and update them.
            */
            CategoryListViewModel.prototype.receivedCategoryUpdates = function (rawCategories) {
                var _this = this;
                // find updated or new tiles
                rawCategories.forEach(function (raw) {
                    var match = _this.categories().filter(function (c) {
                        return c.id == raw.id;
                    });
                    if (match.length) {
                        // found so  update
                        match[0].name(raw.name);
                        match[0].noteCount(raw.noteCount);
                    } else {
                        // not found, so create new tile
                        _this.categories.push(new Billboard.Models.CategoryModel(raw));
                    }
                });

                // find deleted tiles and remove them from categories
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
/// <reference path="../helpers/linq.ts" />
/// <reference path="viewmodel.ts" />
var Billboard;
(function (Billboard) {
    (function (ViewModels) {
        /** ViewModel for a note on the Category List */
        var NoteTileViewModel = (function (_super) {
            __extends(NoteTileViewModel, _super);
            /** Create a new ViewModel for a Note on the Category List */
            function NoteTileViewModel(model) {
                var _this = this;
                _super.call(this);

                this.model = model;

                this.formattedPrice = ko.computed(function () {
                    return isNaN(_this.model.price()) || _this.model.price() === null || _this.model.price() === void 0 ? "" : "€ " + Math.round(_this.model.price()).toString();
                });
            }
            NoteTileViewModel.prototype.select = function () {
                // navigate to the actual note.
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

            /**
            * Connect to the server and request an update of all the notes
            */
            CategoryViewModel.prototype.updateNotes = function () {
                Billboard.Repository.notes.getNotesInCategoryRaw(this.category).done(this.receivedCategoryNotesUpdate).fail(function (error) {
                    debugger;
                });
            };

            /**
            * Called when an tile update call finished.
            * We should now check if there are any differences and update them.
            */
            CategoryViewModel.prototype.receivedCategoryNotesUpdate = function (rawNotes) {
                var _this = this;
                // find updated or new tiles
                rawNotes.forEach(function (raw) {
                    var match = _this.notes().filter(function (vm) {
                        return vm.model.id == raw.id;
                    });
                    if (match.length) {
                        // found so  update
                        match[0].model.title(raw.title);
                        match[0].model.price(raw.price);
                    } else {
                        // not found, so create new tile
                        _this.notes.push(new NoteTileViewModel(new Billboard.Models.NoteModel(raw)));
                    }
                });

                // find deleted tiles and remove them from notes
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
/// <reference path="../helpers/linq.ts" />
/// <reference path="viewmodel.ts" />
/// <reference path="../repository/noterepository.ts" />
var Billboard;
(function (Billboard) {
    (function (ViewModels) {
        /**
        * The ViewModel for the Note view
        */
        var NoteViewModel = (function (_super) {
            __extends(NoteViewModel, _super);
            /**
            * Create a new Note ViewModel
            */
            function NoteViewModel(raw) {
                var _this = this;
                _super.call(this);

                // convert the raw model
                this.model = new Billboard.Models.NoteModel(raw);

                // format the price
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

                // keep track of errors
                this.errorText = ko.observable(null);
                this.hasError = ko.computed(function () {
                    return _this.errorText() != null;
                });
            }
            /**
            * Call to Save the changes in the note
            */
            NoteViewModel.prototype.save = function () {
                this.errorText(null);

                Billboard.Repository.notes.persist(this.model).done(this.onSaved).fail(this.onSaveFailed);
            };

            /**
            * Call to cancel the changes in the note
            */
            NoteViewModel.prototype.cancel = function () {
                // go back on a cancel
                window.history.back();
            };

            /**
            * Called when successfully saved
            */
            NoteViewModel.prototype.onSaved = function () {
                this.goBack();
            };

            /**
            * Called when the save failed.
            */
            NoteViewModel.prototype.onSaveFailed = function (error) {
                this.errorText("Opslaan notitie mislukt. " + JSON.stringify(error));
            };

            NoteViewModel.prototype.canGoBack = function () {
                return true;
            };

            NoteViewModel.prototype.goBack = function () {
                // query repository for category this note belongs to.
                // if found, go to that category, otherwise go to previous page.
                Billboard.Repository.categories.getById(this.model.categoryId).done(function (category) {
                    return document.location.href = "/Category/" + category.name();
                }).fail(window.history.back);
            };

            /**
            * Called when the user clicks the delete button to request deletion
            */
            NoteViewModel.prototype.askDelete = function () {
                this.isBarVisible(true);
            };

            /***
            * Called when users confirms the deletion of the note
            */
            NoteViewModel.prototype.confirmDelete = function () {
                var _this = this;
                this.isBarVisible(false);
                Billboard.Repository.notes.remove(this.model).done(this.goBack).fail(function () {
                    return _this.isBarVisible(true);
                });
            };

            /***
            * Called when the user aborts the deletion of the note
            */
            NoteViewModel.prototype.abortDelete = function () {
                this.isBarVisible(false);
            };
            return NoteViewModel;
        })(ViewModels.ViewModel);
        ViewModels.NoteViewModel = NoteViewModel;
    })(Billboard.ViewModels || (Billboard.ViewModels = {}));
    var ViewModels = Billboard.ViewModels;
})(Billboard || (Billboard = {}));
var Billboard;
(function (Billboard) {
    (function (View) {
        function textCssColor(text) {
            var hash = textHashCode(text);
            return 'tileColor' + ((hash >> 8) & 0xf).toString();
        }
        View.textCssColor = textCssColor;

        function textHashCode(text) {
            var hash = 0;
            var charCode;

            if (text.length == 0)
                return hash;

            for (var index = 0; index < text.length; index++) {
                charCode = text.charCodeAt(index);
                hash = ((hash << 5) - hash) + charCode;
                hash &= 0xffffffff;
            }

            return hash;
        }
    })(Billboard.View || (Billboard.View = {}));
    var View = Billboard.View;
})(Billboard || (Billboard = {}));
/// <reference path="../Scripts/typings/jquery.transit/jquery.transit.d.ts" />
var Billboard;
(function (Billboard) {
    (function (View) {
        function widthCollapseRemove(element, index, item) {
            var e = $(element);
            e.transition({ width: 0 }, 300, 'ease', function () {
                return e.remove();
            });
        }
        View.widthCollapseRemove = widthCollapseRemove;
    })(Billboard.View || (Billboard.View = {}));
    var View = Billboard.View;
})(Billboard || (Billboard = {}));
//# sourceMappingURL=Billboard.js.map
