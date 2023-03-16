"use strict";
// Elements

// Overlay effect div
const overlayEffect = document.querySelector(".overlay");

// Sign in form
const signinForm = document.querySelector(".signin-form");

//Singin signup buttons
const singinBtn = document.querySelector(".signin-btn");

// Header icons
// Cart elements
const cartIcon = document.querySelector(".cart-icon");
const cartDropdownList = document.querySelector(".cart-dropdown-list");
const cartIconTotal = document.querySelector(".cart-icon-total");

const accountIcon = document.querySelector(".account-icon");
const accountDropdownMenu = document.querySelector(".account-dropdown-menu");

// Navbar shop btn and dropdown menu vars
const navbarShopBtn = document.querySelector(".navlink-shop-container");
const navbarShopBtnIcon = document.querySelector(".navlink-shop-container svg");
const shopDropdownMenu = document.querySelector(".navbar-dropdown-menu");

// Navbar linksa
const navLinks = document.querySelectorAll(".navlink-container");

// Home page courousel elements
const carousel = document.querySelector(".carousel-container");
const carouselDots = document.querySelectorAll(".carousel-dot");
const carouselSlides = document.querySelectorAll(".carousel");

// Home page books sections (Beste sellers - New arrivals)
const homeSectionBookImg = document.querySelectorAll(
    ".home-section-books-item img"
);

const bookImgOverlay = document.querySelectorAll(".add-to-cart-overlay");
const bookImgOverlayBtn = document.querySelectorAll(".add-to-cart-overlay-btn");

const homeSectionProductImg = document.querySelectorAll(
    ".home-section-books-item img"
);

const homeSectionProductTitle = document.querySelectorAll(
    ".home-section-books-item-title"
);
const homeSectionProductAuthor = document.querySelectorAll(
    ".home-section-books-item-author"
);
const homeSectionProductPrice = document.querySelectorAll(
    ".home-section-books-item-price"
);

// Forms input
const formInputs = document.querySelectorAll(".form-input input");
const formInputLabels = document.querySelectorAll(".form-input-label");
const formInputErrorMsgs = document.querySelectorAll(".form-error-msg");

// Shop filters
const shopFiltersLabelsIcons = document.querySelectorAll(
    ".shop-filters-filter-label-container svg"
);
const shopFiltersBoxContainers = document.querySelectorAll(
    ".shop-filters-box-filter"
);

const shopPriceFilter = document.querySelector(
    ".shop-filters-price-container input"
);

const shopPriceFilterSpan = document.querySelector(
    ".shop-filters-price-selection"
);

// Product card elements
const productCardImg = document.querySelectorAll(
    ".shop-content-product-card img"
);

const productCardTitle = document.querySelectorAll(
    ".shop-content-product-card-title"
);
const productCardAuthor = document.querySelectorAll(
    ".shop-content-product-card-author"
);
const productCardPrice = document.querySelectorAll(
    ".shop-content-product-card-price"
);
// Car icon
const productCardCartIcons = document.querySelectorAll(
    ".shop-content-product-card-bag-icon"
);

///////////////////////////////////////

// Keeps track of current slide
let slideIndex = 0;
if (carousel != null) {
    showSlides();
}

// Diplay cart total on header in load
displayCartElements();

// Event handlers

// Close elements on click outeside of them
window.addEventListener("click", function (e) {
    // Close cart dropdown list if the user clicks outside
    if (!(cartIcon.contains(e.target) || cartDropdownList.contains(e.target))) {
        cartDropdownList.style.setProperty("display", "none");
    }
    if (
        !(accountIcon.contains(e.target) || accountDropdownMenu.contains(e.target))
    ) {
        accountDropdownMenu.style.setProperty("display", "none");
    }
    if (
        !(navbarShopBtn.contains(e.target) || shopDropdownMenu.contains(e.target))
    ) {
        navbarShopBtnIcon.style.setProperty("transform", "rotate(0)");
        shopDropdownMenu.style.setProperty("display", "none");
    }
    if (overlayEffect.contains(e.target) && !signinForm.contains(e.target)) {
        overlayEffect.style.setProperty("display", "none");
        signinForm.style.setProperty("display", "none");
    }
});

// Header cart icon dropdown list fuctionality
cartIcon.addEventListener("click", function (e) {
    const display = cartDropdownList.style.getPropertyValue("display");

    if (display == "none" || display == "") {
        cartDropdownList.style.setProperty("display", "block");
    } else {
        cartDropdownList.style.setProperty("display", "none");
    }
});

// Header cart icon dropdown list fuctionality
accountIcon.addEventListener("click", function (e) {
    const display = accountDropdownMenu.style.getPropertyValue("display");
    if (display == "none" || display == "") {
        accountDropdownMenu.style.setProperty("display", "flex");
    } else {
        accountDropdownMenu.style.setProperty("display", "none");
    }
});

//Singin button handler
singinBtn.addEventListener("click", function (e) {
    accountDropdownMenu.style.setProperty("display", "none");
    overlayEffect.style.setProperty("display", "block");
    signinForm.style.setProperty("display", "flex");
});

navbarShopBtn.addEventListener("click", function (e) {
    const display = shopDropdownMenu.style.getPropertyValue("display");
    if (display == "none" || display == "") {
        shopDropdownMenu.style.setProperty("display", "grid");
        navbarShopBtnIcon.style.setProperty("transform", "rotate(-90deg)");
    } else {
        shopDropdownMenu.style.setProperty("display", "none");
        navbarShopBtnIcon.style.setProperty("transform", "rotate(0)");
    }
});

// Carousel change screen handler
carouselDots.forEach((dot, index) => {
    dot.addEventListener("click", function (e) {
        for (let i = 0; i < carouselSlides.length; i++) {
            carouselSlides[i].style.display = "none";
        }

        for (let i = 0; i < carouselDots.length; i++) {
            carouselDots[i].classList.remove("carousel-dot-fill");
        }

        slideIndex = index;

        carouselSlides[index].style.display = "flex";
        carouselDots[index].classList.add("carousel-dot-fill");
    });
});

// Automatic slideshow
function showSlides() {
    let i;
    const slidesSize = carouselSlides.length;
    const dotsSize = carouselDots.length;

    for (i = 0; i < slidesSize; i++) {
        carouselSlides[i].style.display = "none";
    }

    slideIndex++;

    if (slideIndex > slidesSize) {
        slideIndex = 1;
    }

    for (i = 0; i < dotsSize; i++) {
        carouselDots[i].classList.remove("carousel-dot-fill");
    }

    carouselSlides[slideIndex - 1].style.display = "flex";
    carouselDots[slideIndex - 1].classList.add("carousel-dot-fill");

    setTimeout(showSlides, 4000); // Change image every 2 seconds
}

// Form inputs handler
formInputs.forEach((input, index) => {
    input.addEventListener("click", function (e) {
        formInputLabels[index].classList.add("form-input-label-up");
        formInputLabels[index].classList.remove("form-input-label");
    });
    input.addEventListener("change", (event) => {
        if (input.name == "email") {
            if (!ValidateEmail(input.value)) {
                formInputErrorMsgs[index].textContent = "Invalid email address";
            }
        }
    });
});

// Home boooks sections displauy add to cart overlay handler
if (homeSectionBookImg) {
    homeSectionBookImg.forEach((bookImg, index) => {
        bookImg.addEventListener("mouseover", function (e) {
            const display = bookImgOverlay[index].style.getPropertyValue("display");
            if (display == "none" || display == "") {
                bookImgOverlay[index].style.setProperty("display", "flex");
            }
        });
        bookImgOverlayBtn[index].addEventListener("click", function (e) {
            // const addToCartTxt = document.querySelectorAll(
            //   ".add-to-cart-overlay-btn span"
            // );
            // addToCartTxt[index].style.setProperty("animation-name", "textOversize");
            const product = {
                img: homeSectionProductImg[index].getAttribute("src"),
                title: homeSectionProductTitle[index].textContent
                    .replace(/\s+/g, " ")
                    .trim(),
                author: homeSectionProductAuthor[index].textContent
                    .replace(/\s+/g, " ")
                    .trim(),
                price: parseFloat(
                    homeSectionProductPrice[index].textContent.replace(/[$,€]+/g, "")
                ),
            };

            addProductToCart(product);
        });
        bookImgOverlay[index].addEventListener("mouseleave", function (e) {
            bookImgOverlay[index].style.setProperty("display", "none");
        });
    });
}

// Shop checkkbox filters display handler
if (shopFiltersLabelsIcons) {
    shopFiltersLabelsIcons.forEach((input, index) => {
        input.addEventListener("click", function (e) {
            const display =
                shopFiltersBoxContainers[index].style.getPropertyValue("display");
            if (display == "none" || display == "") {
                shopFiltersBoxContainers[index].style.setProperty("display", "flex");
                shopFiltersLabelsIcons[index].style.setProperty(
                    "transform",
                    "rotate(90deg)"
                );
            } else {
                shopFiltersBoxContainers[index].style.setProperty("display", "none");
                shopFiltersLabelsIcons[index].style.setProperty(
                    "transform",
                    "rotate(0)"
                );
            }
        });
    });
}

// Price slider filter value text
if (shopPriceFilter) {
    shopPriceFilter.addEventListener("change", (event) => {
        shopPriceFilterSpan.textContent = shopPriceFilter.value + "€";
    });
    shopPriceFilter.addEventListener("mousemove", (event) => {
        shopPriceFilterSpan.textContent = shopPriceFilter.value + "€";
    });
    shopPriceFilter.addEventListener("touchmove", (event) => {
        shopPriceFilterSpan.textContent = shopPriceFilter.value + "€";
    });
}

// Add to cart handler
if (productCardCartIcons) {
    productCardCartIcons.forEach((cartIcon, index) => {
        cartIcon.addEventListener("click", function (e) {
            const product = {
                img: productCardImg[index].getAttribute("src"),
                title: productCardTitle[index].textContent.replace(/\s+/g, " ").trim(),
                author: productCardAuthor[index].textContent
                    .replace(/\s+/g, " ")
                    .trim(),
                price: parseFloat(
                    productCardPrice[index].textContent.replace(/[$,€]+/g, "")
                ),
            };

            addProductToCart(product);
        });
    });
}

// Validation functions
function ValidateEmail(email) {
    const emailRegex = /^\w+([\.-]?\w+)*@\w+([\.-]?\w+)*(\.\w{2,3})+$/;
    if (email.match(emailRegex)) {
        return true;
    }
    return false;
}

// Check a password between 6 to 20 characters which contain at least one numeric digit, one uppercase and one lowercase letter
function CheckPassword(password) {
    var pswRegex = /^(?=.*\d)(?=.*[a-z])(?=.*[A-Z]).{6,20}$/;
    if (password.match(pswRegex)) {
        return true;
    } else {
        return false;
    }
}

// Adds product to cart
function addProductToCart(product) {
    let newUserCart = [];

    if (localStorage.getItem("userCart")) {
        const currUserCart = JSON.parse(localStorage.userCart);
        newUserCart = checkAndUpadeCartIfExists(currUserCart, product);
    } else {
        product.quantity = 1;
        product.subTotal = product.price;
        newUserCart.push(product);
    }
    const newTotal = updateCartTotal(newUserCart);

    // Create user cart objects to local storage
    localStorage.setItem("userCart", JSON.stringify(newUserCart));
    localStorage.setItem("userCartTotal", JSON.stringify(newTotal));

    updateCartDropDownList(newUserCart, newTotal);
    displayCartElements();
}

// Checks if the product already exists in cart and updates the quantity, otherwise add it
function checkAndUpadeCartIfExists(cart, product) {
    const found = cart.some((el) => {
        if (el.title == product.title) {
            el.quantity++;
            el.subTotal = el.quantity * el.price;
            return true;
        }
        return false;
    });
    if (!found)
        cart.push({ ...product, ...{ quantity: 1, subTotal: product.price } });
    return cart;
}

function updateCartTotal(cart) {
    let total = 0;
    cart.forEach((product) => (total += product.subTotal));
    return total;
}

// Displays the whole cart's icon elements
function displayCartElements() {
    if (
        localStorage.getItem("userCartTotal") &&
        localStorage.getItem("userCart")
    ) {
        const cartTotal = JSON.parse(localStorage.userCartTotal);
        const cart = JSON.parse(localStorage.userCart);
        // Display  cart's total products span element
        createCartIconTotalProductsElement(cartTotal);
        cartIconTotal.textContent = "€" + parseFloat(cartTotal).toFixed(2);
        // Update cart's icon dropdown list with products
        updateCartDropDownList(cart, cartTotal);
    } else {
        cartIconTotal.textContent = "€0.00";

        const emptyCartTxt = document.createElement("li");
        const cartDropDownList = document.querySelector(".cart-dropdown-list ul");
        // Create msm to inform user tha his cart is empty
        emptyCartTxt.classList.add("cart-dropdown-list-empty");
        emptyCartTxt.textContent = "Your cart is empty";
        cartDropDownList.appendChild(emptyCartTxt);
    }
}

function updateCartDropDownList(cart, total) {
    const cartDropDownList = document.querySelector(".cart-dropdown-list ul");

    const cartTotal = document.querySelector(".cart-dropdown-list-total");

    let cartIconTotalProducts = document.querySelector(".cart-icon-items");
    if (cartIconTotalProducts) {
        cartIconTotalProducts.textContent = cart.length;
    } else {
        // console.log("poko");
        // cartIconTotalProducts = document.createElement("span");
        // cartIconTotalProducts.classList.add("cart-icon-items");
        // cartIconTotalProducts.textContent = cart.length;
        // cartDropdownList.parentNode.insertBefore(
        //   cartIconTotalProducts,
        //   cartDropdownList
        // );
    }

    while (cartDropDownList.firstChild) {
        cartDropDownList.removeChild(cartDropDownList.firstChild);
    }

    const cartListItems = cart.map((i) => createCartIconListProduct(i));
    cartListItems.forEach((i) => cartDropDownList.appendChild(i));
    cartTotal.textContent = "€" + parseFloat(total).toFixed(2);
}

function createCartIconListProduct(product) {
    const productContainer = document.createElement("li");
    productContainer.classList.add("cart-dropdown-list-item");

    // Product image
    const productImg = document.createElement("img");
    productImg.src = product.img;
    productImg.alt = product.title;

    // Product title
    const productTitle = document.createElement("span");
    productTitle.classList.add("cart-dropdown-list-item-title");
    productTitle.textContent = product.title;

    // Product subtotal
    const productSubtotal = document.createElement("span");
    productSubtotal.classList.add("cart-dropdown-list-item-price");
    productSubtotal.textContent = product.quantity + " X " + "€" + product.price;

    // Create list item
    productContainer.appendChild(productImg);
    productContainer.appendChild(productTitle);
    productContainer.appendChild(productSubtotal);
    return productContainer;
}

// Creates to the DOM the span element with the sum of products in the header's cart icon
function createCartIconTotalProductsElement(cart) {
    let cartIconTotalProducts = document.querySelector(".cart-icon-items");
    // check if already exists
    if (cartIconTotalProducts) {
        cartIconTotalProducts.textContent = cart.length;
        return;
    }
    // Create it
    cartIconTotalProducts = document.createElement("span");
    cartIconTotalProducts.classList.add("cart-icon-items");
    cartIconTotalProducts.textContent = cart.length;
    cartDropdownList.parentNode.insertBefore(
        cartIconTotalProducts,
        cartDropdownList
    );
}

function addCartTotalProducts() { }

{
    /* <li class="cart-dropdown-list-item">
      <img
        src="img/books/310KDa7YvxL._SY344_BO1,204,203,200_.jpg"
        alt="book title"
      />
      <span class="cart-dropdown-list-item-title">
        The Happiness Recipe: A Powerful Guide to Living What Matters
      </span>
      <span class="cart-dropdown-list-item-price">1 X 15,00 €</span>
    </li>; */
}
