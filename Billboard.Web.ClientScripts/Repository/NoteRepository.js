var Billboard;
(function (Billboard) {
    (function (Repository) {
        var NoteRepository = (function () {
            function NoteRepository() {
                this.repository = new Repository.Repository("Note");
            }
            NoteRepository.prototype.getById = function (id) {
                var deferred = $.Deferred();

                this.repository.getById(id).done(function (raw) {
                    var model = new Billboard.Models.NoteModel(raw);
                    deferred.resolve(model);
                }).fail(deferred.reject);

                return deferred.promise();
            };

            NoteRepository.prototype.persist = function (model) {
                return this.repository.persist(model);
            };

            NoteRepository.prototype.remove = function (model) {
                return this.repository.remove(model);
            };

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
