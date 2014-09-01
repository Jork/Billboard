var Billboard;
(function (Billboard) {
    (function (Repository) {
        var CategoryRepository = (function () {
            function CategoryRepository() {
                this.repository = new Repository.Repository("Category");
            }
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

            CategoryRepository.prototype.persist = function (model) {
                return this.repository.persist(model);
            };

            CategoryRepository.prototype.remove = function (model) {
                return this.repository.remove(model);
            };

            CategoryRepository.prototype.getById = function (id) {
                var deferred = $.Deferred();

                this.repository.getById(id).done(function (raw) {
                    var model = new Billboard.Models.CategoryModel(raw);
                    deferred.resolve(model);
                }).fail(deferred.reject);

                return deferred.promise();
            };

            CategoryRepository.prototype.getByIdRaw = function (id, withNotes) {
                return this.repository.getById(id, { withNotes: withNotes });
            };

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
