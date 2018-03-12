var saveeditproduct = document.getElementsByName("SaveEditProduct")[0];
saveeditproduct.disabled = true;

var submitproduct = document.getElementsByName("SubmitProduct")[0];
submitproduct.disabled = false;

function EditProduct(productDetails) {

    var product = productDetails.split(",");

    document.getElementsByName("ProductID")[0].value = product[0];
    document.getElementById("name").value = product[1];
    document.getElementById("price").value = product[2];
    document.getElementById("stock").value = product[3];

    document.getElementById("url").style.visibility = 'hidden';
    document.getElementById("urllabel").style.visibility = 'hidden';
    
    saveeditproduct.disabled = false;
    submitproduct.disabled = true;

    document.getElementById("productFormHeading").innerHTML = "Edit Product";
    document.getElementById("stocklabel").innerHTML = "Updated Stock";
}

function SaveDelay() {
    setTimeout(SaveChanges, 3000);
}

function SaveChanges() {

    document.getElementsByName("ProductID")[0].value = "";
    document.getElementById("name").value = "";
    document.getElementById("price").value = "";
    document.getElementById("stock").value = "";

    document.getElementById("url").style.visibility = 'visible';
    document.getElementById("urllabel").style.visibility = 'hidden';

    saveeditproduct.disabled = true;
    submitproduct.disabled = false;

    document.getElementById("productFormHeading").innerHTML = "Add New Product";
    document.getElementById("stocklabel").innerHTML = "Initial Stock";
}

function DeleteItem() {
    alert("Delete function called");
    document.getElementById("deletevalue").value = "delete";
}