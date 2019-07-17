


    //شمارنده کاراکتر ها
var badgeElement = document.querySelectorAll('.mdl-badge-counter');
    for (var i = 0; i < badgeElement.length; i++) {

        var dataId = badgeElement.item(i).getAttribute('for');

        document.getElementById(dataId.toString()).addEventListener('keyup', function () {

            document.querySelector('div.mdl-badge-counter[for="' + this.id + '"]').setAttribute("data-badge", document.getElementById(this.id).value.length);
        }, true);
        document.getElementById(dataId.toString()).addEventListener('change', function () {

            document.querySelector('div.mdl-badge-counter[for="' + this.id + '"]').setAttribute("data-badge", document.getElementById(this.id).value.length);
        }, true);
        document.querySelector('div.mdl-badge-counter[for="' + dataId + '"]').setAttribute("data-badge", document.getElementById(dataId).value.length);
    }
    window.addEventListener("pageshow", function () {
        var items = document.querySelectorAll(".mdl-textfield")
        for (var i = 0; i < items.length; i++) {
            var validItem = items.item(i).querySelector(".mdl-textfield__input")
            if (validItem.classList.contains("input-validation-error")) {
                items.item(i).classList.add('is-invalid');
            }

        }
    });
    

