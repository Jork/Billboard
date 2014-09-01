var Billboard;
(function (Billboard) {
    (function (_Repository) {
        var Repository = (function () {
            function Repository(controllerName) {
                this.controllerName = controllerName;
            }
            Repository.prototype.getById = function (id, args) {
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
                var verb = "PUT";

                if ($isUndefined(model.id) || model.id === Guid.Zero) {
                    verb = "POST";
                }

                var jsonString = ko.toJSON(model);

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
                var url = document.location.protocol + "//" + document.location.host + "/api/" + this.controllerName + "/" + model.id;

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
