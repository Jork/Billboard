var Billboard;
(function (Billboard) {
    (function (ViewModels) {
        (function (Helpers) {
            function textCssColor(text) {
                var hash = textHashCode(text);
                return 'tileColor' + ((hash >> 8) & 0xf).toString();
            }
            Helpers.textCssColor = textCssColor;

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
            Helpers.textHashCode = textHashCode;
        })(ViewModels.Helpers || (ViewModels.Helpers = {}));
        var Helpers = ViewModels.Helpers;
    })(Billboard.ViewModels || (Billboard.ViewModels = {}));
    var ViewModels = Billboard.ViewModels;
})(Billboard || (Billboard = {}));
//# sourceMappingURL=ViewHelpers.js.map
