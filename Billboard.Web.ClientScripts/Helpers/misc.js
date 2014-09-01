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

        

        function isNullOrUndefined(obj) {
            if (arguments.length === 0)
                return false;

            if (arguments.length > 1) {
                for (var i = 0; i < arguments.length; i++) {
                    var value = arguments[i];

                    if (ko.isObservable(value))
                        value = value();

                    if (arguments[i] === null || arguments[i] === void 0)
                        return true;
                }
            } else {
                var value = obj;

                if (ko.isObservable(value))
                    value = value();

                if (value === null || value === void 0)
                    return true;
            }

            return false;
        }
        Helpers.isNullOrUndefined = isNullOrUndefined;

        function rebind(obj) {
            var prototype = obj.constructor.prototype;

            for (var key in prototype) {
                var name = key;

                if (!obj.hasOwnProperty(name) && $.isFunction(prototype[name])) {
                    var method = prototype[name];

                    if (name !== "constructor" && !ko.isObservable(method)) {
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

var $isUndefined = Billboard.Helpers.isNullOrUndefined;

var $hasValue = function (obj) {
    return !Billboard.Helpers.IsNullOrUndefinedOrEmpty(obj);
};
