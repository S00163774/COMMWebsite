﻿'use strict';

(function ImmediateJob() {
    // Select all buttons on the products 
    var plus = document.getElementsByName("pQuantity");
    var minus = document.getElementsByName("mQuantity");
    var basket = document.getElementsByName("addToBasket");

    var plusValue = 0;
    var minusValue = 0;
    var basketValue = 0;
    // add id to all 'plus quantity' buttons
    for (var i = 0; i < plus.length; i++) {
        plus[i].setAttribute("id", "P" + plusValue);
        plusValue++;
    }
    // add id to all 'minus quantity' buttons
    for (var j = 0; j < minus.length; j++) {
        minus[j].setAttribute("id", "M" + minusValue);
        minusValue++;
    }
    // add id to all 'add to basket' buttons
    for (var k = 0; k < minus.length; k++) {
        basket[k].setAttribute("id", "B" + basketValue);
        basketValue++;
    }

    document.getElementById('updateBasket').addEventListener("click", UpdateCart, false);
    document.getElementById('checkoutBtn').addEventListener("click", CheckoutPage, false);
    document.getElementById('payBtn').addEventListener("click", ConfirmPage, false);
})();

function minusQuantity(clicked_id) {
    // select product id and quantity
    var pos = clicked_id.charAt(clicked_id.length - 1);
    var number = parseInt($('span.badge')[pos].innerText);
    // decrease the quantity and show result
    if (number <= 1) {
        number = 1;
    }
    else {
        number -= 1;
    }

    $('span.badge')[pos].innerText = number;
}

function plusQuantity(clicked_id) {
    // select product id and quantity
    var pos = clicked_id.charAt(clicked_id.length - 1);
    var number = parseInt($('span.badge')[pos].innerText);
    // increase the quantity and show result
    if (number >= 10) {
        number = 10;
    }
    else {
        number += 1;
    }

    $('span.badge')[pos].innerText = number;
}

function redirector() {
    window.location.href = "cart.cshtml";
}

function AddToBasket(clicked_id, productID, productPrice, productName, Image, Description) {
    // select quantity and product id
    var pos = clicked_id.charAt(clicked_id.length - 1);
    var quantity = $('span.badge')[pos].innerText;
    // place the product and quantity into session storage
    if (quantity != null) {

        var basketItem = [productID, quantity, productName, productPrice, Image, Description];
        var prod = new Product(basketItem);

        //var basketItem = { 'ProductID': productID, 'Quantity': quantity };
        sessionStorage.setItem('basketItem' + productID, JSON.stringify(prod));

        toastr.options = { "showMethod": "fadeIn", "hideMethod": "fadeOut", "timeOut": "3000", "closeButton": true };
        toastr["success"]('Added to basket!');
    }
}

function DisplayBasket() {
    // collect all the keys in storage
    GetKeys();
    // Change page to basket page
    redirector();
}

function GetKeys() { // Gets all keys in session storage and stores them in local storage
    var keys = [];
    localStorage.removeItem("allKeys");

    for (var i = 0; i < sessionStorage.length; i++) {
        keys[i] = sessionStorage.key(i);
    }
    localStorage.setItem("allKeys", keys);
}

function CartDisplay() {
    var totalPrice = 0;
    var collectiveProducts = localStorage.getItem("allKeys");
    //alert(collectiveProducts);
    var keys = collectiveProducts.split(',');

    var counter = 1;
    // Display each product in the basket here
    for (var i = 0; i < keys.length; i++) {
        var prod = sessionStorage.getItem(keys[i]);
        prod = JSON.parse(prod);
        $("div.panel-body").append('<div class="row"><div class="col-xs-2"><img class="img-responsive" src="http://placehold.it/100x70"></div><div class="col-xs-4"><h4 class="product-name"><strong>' + prod.Name + '</strong></h4><h4><small>' + prod.Description + '</small></h4></div><div class="col-xs-6"><div class="col-xs-6 text-right"><h6><strong><div class="pricedProds">' + prod.Price + '</div><span class="text-muted"> x</span></strong></h6></div><div class="col-xs-4"><input type="text" name="basketProds" class="form-control input-sm" value="' + prod.Quantity + '"></div><div class="col-xs-2"><button type="button" class="btn btn-link btn-xs" onclick="RemoveItem(' + prod.ID + ')"><span class="glyphicon glyphicon-trash"> </span></button></div></div></div>');
        var price = prod.Price * prod.Quantity;
        totalPrice += price;
        // Increment counter for each productID in the Basket
        counter++;
    }

    document.getElementById("myPrice").innerHTML = totalPrice;
}

function UpdateCart() {
    var quantities = [];
    var price = 0;

    for (var i = 0; i < document.getElementsByName("basketProds").length; i++) {
        quantities[i] = document.getElementsByName("basketProds")[i].value;
    }

    for (var j = 0; j < $(".pricedProds").length; j++) {
        price += (quantities[j] * parseFloat($(".pricedProds")[j].innerHTML).toFixed(2));
    }

    document.getElementById("myPrice").innerHTML = price;
}

function Product(temp) { // Model for each product in the basket
    this.ID = temp[0];
    this.Quantity = temp[1];
    this.Name = temp[2];
    this.Price = temp[3];
    this.Image = temp[4];
    this.Description = temp[5];
}

function RemoveItem(productID) { // Removes an item from the basket
    sessionStorage.removeItem("basketItem" + productID);
    GetKeys();
    location.reload();
}

function PriceGetter() {
    var price = document.getElementById("myPrice").innerHTML;
    sessionStorage.setItem("price", JSON.stringify(price));
}

function CheckoutPage() { // Sends to the checkout page
    PriceGetter();

    window.location.href = "checkout.cshtml";
}

function PayPage() {
    window.location.href = 'pay.cshtml';
}

function ConfirmPage() {
    window.location.href = 'ConfirmationPage.cshtml';
}