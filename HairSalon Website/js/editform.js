$(document).ready(function () {

    function PopUpEditProduct(id) {
        var data;
        data = JSON.parse(document.getElementsByName(id)[0].value);
        alert(data.ProductID);
    }

    $('.open-edit-form').magnificPopup({
        type: 'inline',
        midClick: true // Allow opening popup on middle mouse click. Always set it to true if you don't provide alternative source in href.
    });

});