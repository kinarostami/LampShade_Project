const CookieName = "cart-items"

// for add shopping cart
function addToCart(id, name, price, picture)
{
    let products = $.cookie(CookieName);
    if (products === undefined) {
        products = [];
    }
    else {
        products = JSON.parse(products);
    }

    const count = $("#productCount").val();
    const currentProduct = products.find(x => x.id === id);
    if (currentProduct !== undefined) {
        products.find(x => x.id === id).count = parseInt(currentProduct.count) + parseInt(count);
    }
    else {
        const product = {
            id,
            name,
            unitPrice: price,
            picture,
            count
        }

        products.push(product);
    }

    $.cookie(CookieName, JSON.stringify(products), { expires: 2, Path: "/" });
    updateCart();
}

// for count cart shopping
function updateCart() {
    let products = $.cookie(CookieName);
    products = JSON.parse(products);
    $("#cart_items_count").text(products.length);

    //for view list cart shopping
    const cartItemsWrapper = $("#cart_items_wrapper");
    cartItemsWrapper.html('');
    products.forEach(x => {
        const product = `<div class="single-cart-item">
                        <a href="javascript:void(0)" class="remove-icon" onclick="removeFromCart('${x.id}')">
                            <i class="ion-android-close"></i>
                        </a>
                        <div class="image">
                            <a href="single-product.html">
                                <img src="/ProductPictures/${x.picture}"
                                     class="img-fluid" alt="">
                            </a>
                        </div>
                        <div class="content">
                            <p class="product-title">
                                <a href="single-product.html">محصول : ${x.name}</a>
                            </p>
                            <p class="count">تعداد : ${x.count}</p>
                            <p class="count">قیمت واحد : ${x.unitPrice}</p>
                            </div>
                        </div>`;

        cartItemsWrapper.append(product);
    });
    
}
function removeFromCart(id){
    let products = $.cookie(CookieName);
    products = JSON.parse(products);
    let itemToRemove = products.findIndex(x => x.id === id);
    products.splice(itemToRemove, 1);
    $.cookie(CookieName, JSON.stringify(products), { expires: 2, Path: "/" });
    updateCart();
}

function changeCartItemCount(id, totalId, count) {
    debugger;
    var products = $.cookie(CookieName);
    products = JSON.parse(products);
    const productIndex = products.findIndex(x => x.id == id);
    products[productIndex].count = count;
    const product = products[productIndex];
    const newPrice = parseInt(product.unitPrice) * parseInt(count);
    $(`#${totalId}`).text(newPrice);
    //products[productIndex].totalPrice = newPrice;
    $.cookie(CookieName, JSON.stringify(products), { expires: 2, path: "/" });
    updateCart();

    // send sms
    //const settings = {
    //    "url": "My http",
    //    "method": "POST",
    //    "timeout": 0,
    //    "headers": {
    //        "Content-Type": "application/Json"
    //    },
    //    "data": JSON.stringify({ "productId": id, "count": count })
    //};

    //$.ajcax(settings).done(function (data) {
    //    if (data.IsInStock == false) {
    //        const warningsDiv = $('#productStockWarnings');
    //        if ($(`#${id}`).length == 0) {
    //            warningsDiv.append(`
    //                <div class="alert alert-warning" id="${id}">
    //                     <i class="fa fa-warning"></i>
    //                     تعداد درخواستی کالای
    //                     <strong>${data.productName}</strong>
    //                     در انبار موجود نیست
    //                </div>
    //            `);
    //        }
    //    } else {
    //        if ($(`#${id}`).length > 0) {
    //            $(`#${id}`).remove();
    //        }
    //    }
    //});
}