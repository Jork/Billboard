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
