"use strict";

var connection = new signalR.HubConnectionBuilder().withUrl("/Index").build();

connection.start()
    .then(() => { 
        alert("Connection Opened");

        InvokeProducts();
    })
    .catch( (err) => console.erro(err.toString()));

function InvokeProducts() {
    connection.invoke("SendProducts")
        .catch((err) => {
            console.error(err.toString())
        })
}

connection.on("RecieveProducts", (products) => {
    console.log(products);
    AddProductsToTable(products);
})

function AddProductsToTable(products) {
    const tbody = document.getElementById("table-body");

    tbody.innerHTML = "";

    for (let i = 0; i < products.length; i++) {
        tbody.innerHTML +=
            `<tr>
                <th scope="row">${products[i].productID}</td>
                <td>${products[i].productName}</td>
                <td>${products[i].quantityPerUnit}</td>
                <td>${products[i].unitPrice}</td>
             </tr>`;
    }

    //for (var pr in products) {
    //    tbody.innerHTML +=
    //        `<tr>
    //            <td>${pr.productID}</td>
    //            <td>${pr.productName}</td>
    //            <td>${pr.quantityPerUnit}</td>
    //            <td>${pr.unitPrice}</td>
    //         </tr>`;
    //}
}