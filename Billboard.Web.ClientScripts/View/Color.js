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
