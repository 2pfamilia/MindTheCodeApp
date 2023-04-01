"use strict";
// Elements

// Overlay effect div
const overlayEffect = document.querySelector(".overlay");

// Language bar on header
const langBarListItems = document.querySelectorAll(".language-bar-list-item");

// Sign in form
const signinForm = document.querySelector(".signin-form");

//Singin signup buttons
const singinBtn = document.querySelector(".signin-btn");

// Header icons

// Mobile navbar menu icon
const mobileNavbarIcon = document.querySelector(".mobile-navbar-icon");
const mobileMenuContainer = document.querySelector(".mobile-navbar-menu");
const mobileNavbarCloseIcon = document.querySelector(".mobile-navbar-close-icon");
const mobileNavbarShopBtn = document.querySelector(".mobile-navbar-shop-btn");


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

// Home page courousel elements
const carousel = document.querySelector(".carousel-container");
const carouselDots = document.querySelectorAll(".carousel-dot");
const carouselSlides = document.querySelectorAll(".carousel");

// Home page books sections (Beste sellers - New arrivals)
const homeSectionBookImg = document.querySelectorAll(
    ".home-section-books-item img"
);

// Forms input
const formInputs = document.querySelectorAll(".form-input");

// Form btn
const formBtns = document.querySelectorAll("button.form-btn");

// Custom select element
const customSelects = document.querySelectorAll(".custom-select-container");

// Custom number input
const numberInputContainers = document.querySelectorAll(
    ".number-input-container"
);

// Shop filters
const shopFiltersBtn = document.querySelector(
    ".shop-filters-sidebar button"
);

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
const productCardImg = document.querySelectorAll(".product-card img");

const productCardTitle = document.querySelectorAll(".product-card-title");
const productCardAuthor = document.querySelectorAll(".product-card-author");
const productCardPrice = document.querySelectorAll(".product-card-price");
// Car icon
const productCardCartIcons = document.querySelectorAll(
    ".product-card-bag-icon"
);

const productPageAddToCartBtn = document.querySelector(
    ".product-page-add-to-cart"
);

// Prodact page product's data
const productPageProductImg = document.querySelector(
    ".product-page-product img"
);
const productPageProductTitle = document.querySelector(
    ".product-page-product-title"
);
const productPageProductAuthor = document.querySelector(
    ".product-page-product-author a"
);
const productPageProductPrice = document.querySelector(
    ".product-page-product-price"
);

// Checkout page elements

// Acount page navbar
const myAccountNavLinks = document.querySelectorAll(".my-account-nav-link");
let accountCurrentPageIndex = 0;

// My cart page
const myCartPageContainer = document.querySelector(".my-cart-page-container");



// Site URL

const siteUrl = "https://localhost:44362/";

let userCart = [];
let Products = [];
let userCartTotal = 0;

// If user cart data is empty get it from local storage
if (localStorage.getItem("userCartTotal") && localStorage.getItem("userCart")) {
    userCartTotal = JSON.parse(localStorage.userCartTotal);
    userCart = [...JSON.parse(localStorage.userCart)];
}

function updateCartLocalStorage() {
    localStorage.setItem("userCart", JSON.stringify(userCart));
    localStorage.setItem("userCartTotal", JSON.stringify(userCartTotal));
}

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


    if (!mobileMenuContainer.contains(e.target) && !mobileNavbarIcon.contains(e.target)) {
        mobileMenuContainer.style.setProperty("display", "none");
    }

    customSelects.forEach((select) => {
        if (!select.contains(e.target)) {
            const btn = select.querySelector("svg");
            const list = select.querySelector("ul");
            list.style.setProperty("display", "none");
            btn.style.setProperty("transform", "rotate(0)");
        }
    });
});


mobileNavbarIcon && mobileNavbarIcon.addEventListener("click", () => {
    mobileMenuContainer.style.setProperty("display", "flex");
})

mobileNavbarCloseIcon && mobileNavbarCloseIcon.addEventListener("click", () => {
    mobileMenuContainer.style.setProperty("display", "none");
})

mobileNavbarShopBtn && mobileNavbarShopBtn.addEventListener("click", () => {
    const shopMenuContainer = document.querySelector(".mobile-navbar-menu-subcategory-container");
    const display = shopMenuContainer.style.getPropertyValue("display");
    if (display == "none" || display == "") {
        shopMenuContainer.style.setProperty("display", "flex");
    } else {
        shopMenuContainer.style.setProperty("display", "none");
    }
})


langBarListItems.forEach(item => {
    const form = item.closest('form');
    const input = form.querySelector('input');
    if (input.value == item.getAttribute("data-id")) {
        item.classList.add("selected");
    }

    item.addEventListener('click', () => {
        if (!item.classList.contains("selected")) {
            langBarListItems.forEach(i => {
                i.classList.remove("selected");
            })
            item.classList.add("selected");
            const selectedLang = item.getAttribute("data-id");
            input.value = selectedLang;
            form.submit();
        }
    })
})


// Header cart icon dropdown list fuctionality
cartIcon.addEventListener("click", () => {
    const display = cartDropdownList.style.getPropertyValue("display");

    if (display == "none" || display == "") {
        cartDropdownList.style.setProperty("display", "block");
    } else {
        cartDropdownList.style.setProperty("display", "none");
    }
});

// Header cart icon dropdown list fuctionality
accountIcon.addEventListener("click", () => {
    const display = accountDropdownMenu.style.getPropertyValue("display");
    if (display == "none" || display == "") {
        accountDropdownMenu.style.setProperty("display", "flex");
    } else {
        accountDropdownMenu.style.setProperty("display", "none");
    }
});

//Singin button handler
singinBtn && singinBtn.addEventListener("click", () => {
    accountDropdownMenu.style.setProperty("display", "none");
    overlayEffect.style.setProperty("display", "block");
    signinForm.style.setProperty("display", "flex");
});

navbarShopBtn.addEventListener("click", () => {
    const display = shopDropdownMenu.style.getPropertyValue("display");
    if (display == "none" || display == "") {
        shopDropdownMenu.style.setProperty("display", "grid");
        navbarShopBtnIcon.style.setProperty("transform", "rotate(-90deg)");
    } else {
        shopDropdownMenu.style.setProperty("display", "none");
        navbarShopBtnIcon.style.setProperty("transform", "rotate(0)");
    }
});

if (myCartPageContainer) {
    if (userCart.length > 0 && userCartTotal > 0) {
        const myCartListItems = userCart.map((i) => CreateMyCartPageListItem(i));

        const emptyCartMsgContainer = myCartPageContainer.querySelector(
            ".my-cart-page-empty-cart"
        );

        const myCartPageTotalSection = myCartPageContainer.querySelector(
            ".my-cart-page-checkout-container"
        );

        const myCartPageListHeader = myCartPageContainer.querySelector(
            ".my-cart-page-item-header"
        );
        emptyCartMsgContainer.style.setProperty("display", "none");
        myCartPageTotalSection.style.setProperty("display", "flex");
        myCartPageListHeader.style.setProperty("display", "flex");

        myCartListItems.forEach((i) =>
            myCartPageContainer.insertBefore(i, myCartPageTotalSection)
        );

        const numberInputs = myCartPageContainer.querySelectorAll(
            ".number-input-container input"
        );

        const productSubtotals = myCartPageContainer.querySelectorAll(
            ".my-cart-page-item-subtotal"
        );

        // Span element that displays carf total
        const cartTotalElement = myCartPageContainer.querySelector(
            ".my-cart-page-total"
        );
        // Update cart total Span element
        cartTotalElement.textContent = "€" + parseFloat(userCartTotal).toFixed(2);

        numberInputs.forEach((input, index) => {
            const product = createDeepObjectCopy(userCart[index]);
            input.addEventListener("change", () => {
                const inputValue = input.value;
                addProductToCart(product, parseInt(input.value));
                productSubtotals[index].textContent =
                    "€" + parseFloat(inputValue * product.price).toFixed(2);

                cartTotalElement.textContent =
                    "€" + parseFloat(userCartTotal).toFixed(2);
            });
        });
    }
}

// Carousel change screen handler
carouselDots.forEach((dot, index) => {
    dot.addEventListener("click", () => {
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
formInputs.forEach((inputContainer) => {
    const input = inputContainer.querySelector("input");
    const label = inputContainer.querySelector("label");
    const errorMsg = inputContainer.querySelector("span");
    if (input.value.length > 0 && label.classList.contains("form-input-label")) {
        label.classList.add("form-input-label-up");
        label.classList.remove("form-input-label");
    }

    input.addEventListener("click", function () {
        label.classList.add("form-input-label-up");
        label.classList.remove("form-input-label");
    });

    input.addEventListener("keydown", function () {
        label.classList.add("form-input-label-up");
        label.classList.remove("form-input-label");
        errorMsg.textContent = "";
    });

    input.addEventListener("focusout", () => {
        if (input.value == "") {
            label.classList.add("form-input-label");
            label.classList.remove("form-input-label-up");
            errorMsg.textContent = "";
        }
    });

    input.addEventListener("change", () => {
        if (input.name == "Email") {
            if (!validateEmail(input.value)) {
                errorMsg.style.setProperty("display", "inline");
                errorMsg.textContent = "Invalid email address.";
            }
        } else if (input.name == "Phone") {
            if (!ValidatePhone(input.value)) {
                errorMsg.textContent = "Invalid phone number.";
                errorMsg.style.setProperty("display", "inline");
            }
        } else if (input.name == "PostalCode") {
            if (!ValidateZipCode(input.value)) {
                errorMsg.textContent = "Invalid Zip code.";
                errorMsg.style.setProperty("display", "inline");
            }
        } else if (input.type == "password") {
            const parent = document.querySelector(".password-confirmation");
            if (parent) {
                const passwordConfirmInput = parent.querySelectorAll('input[type="password"]')[1];
                const passwordConfirmErrorMsg =
                    passwordConfirmInput.parentElement.querySelector(".form-error-msg");

                if (passwordConfirmInput.value.length > 0) {
                    if (!passwordCofirmation(input.value, passwordConfirmInput.value)) {
                        passwordConfirmErrorMsg.textContent =
                            "Password confirmation does not match.";
                    } else {
                        passwordConfirmErrorMsg.textContent = "";
                    }
                }
            }

            if (!input.classList.contains("singin") && !checkPassword(input.value)) {
                errorMsg.textContent =
                    "At least 1 numeric digit, 1 uppercase and 1 lowercaser letter (6 to 20 characters).";
                errorMsg.style.setProperty("display", "inline");
            }
        } else if (input.name.includes("PasswordConfirmation")) {
            const parent = document.querySelector(".password-confirmation");
            const passwordInput = parent.querySelector('input[type="password"]:first');
            if (!passwordCofirmation(input.value, passwordInput.value)) {
                errorMsg.textContent = "Password confirmation does not match.";
                errorMsg.style.setProperty("display", "inline");
            }
        }
    });
});

// Forms submit button handler
formBtns.forEach((btn) => {
    btn.addEventListener("click", (e) => {
        e.preventDefault();
        const form = btn.closest("form");
        const inputs = form.querySelectorAll(".form-input");
        const customSelects = form.querySelectorAll(".custom-select-container");
        let hasErrors = false

        inputs.forEach((inputContainer) => {
            const input = inputContainer.querySelector("input");
            const errorMsg = inputContainer.querySelector("span");
            if (errorMsg.textContent.length != 0) {
                hasErrors = true;
            }
            if (input.hasAttribute("required") && input.value.length == 0) {
                errorMsg.textContent = "This field is required.";
                errorMsg.style.setProperty("display", "inline");
                hasErrors = true;
            }
        });

        //
        customSelects.forEach((selectContainer) => {
            if (selectContainer.classList.contains("form-select")) {
                const selectInput = selectContainer.querySelector("input");
                if (selectInput.value.length == 0) {
                    const parent = selectContainer.parentNode;
                    const errorMsg = parent.querySelector(".form-error-msg");
                    errorMsg.textContent = "This field is required.";
                    errorMsg.style.setProperty("display", "inline");
                    hasErrors = true;
                }
            }
        });

        if (!hasErrors) {
            form.submit();
        }

    });
});

// Custom select element handler
customSelects && customSelects.forEach((select) => {
    const btn = select.querySelector("svg");
    const list = select.querySelector("ul");
    const input = select.querySelector("input");
    const inputTxt = select.querySelector(".custom-select-text");
    const label = select.querySelector("label");

    if (
        input.value.length > 0 &&
        label.classList.contains("form-input-label")
    ) {
        label.classList.add("form-input-label-up");
        label.classList.remove("form-input-label");

        const listItem = document.querySelectorAll(".custom-select-container-dropdown-list li");
        listItem.forEach(li => {
            if (li.getAttribute("data-id") == input.value) {
                inputTxt.textContent = input.value;
            }
        })
    }

    select.addEventListener("click", (e) => {
        const display = list.style.getPropertyValue("display");
        if (display == "none" || display == "") {
            list.style.setProperty("display", "flex");
            btn.style.setProperty("transform", "rotate(90deg)");
        } else {
            if (e.target.tagName == "LI") {
                input.value = e.target.getAttribute("data-id").replace(/\s+/g, " ").trim();
                inputTxt.textContent = e.target.textContent.replace(/\s+/g, " ").trim();
            }
            list.style.setProperty("display", "none");
            btn.style.setProperty("transform", "rotate(0)");
        }

        if (input.value.length > 0 && inputTxt.textContent.length > 0) {
            label.classList.add("form-input-label-up");
            label.classList.remove("form-input-label");
        }
    });
});

// Home boooks sections displauy add to cart overlay handler
if (homeSectionBookImg) {
    homeSectionBookImg.forEach((bookImg) => {
        const parent = bookImg.parentElement;
        const overlay = parent.querySelector(".add-to-cart-overlay");
        const overlayBtn = parent.querySelector(".add-to-cart-overlay-btn");

        bookImg.addEventListener("mouseover", () => {
            const display = overlay.style.getPropertyValue("display");
            if (display == "none" || display == "") {
                overlay.style.setProperty("display", "flex");
            }
        });

        overlayBtn.addEventListener("click", () => {
            const title = parent.querySelector(".home-section-books-item-title");
            const author = parent.querySelector(".home-section-books-item-author");
            const price = parent.querySelector(".home-section-books-item-price");
            const product = {
                id: parent.getAttribute("data-id"),
                img: bookImg.getAttribute("src"),
                title: title.textContent.replace(/\s+/g, " ").trim(),
                author: author.textContent.replace(/\s+/g, " ").trim(),
                price: parseFloat(price.textContent.replace(/[$€]+/g, "")),
            };
            addProductToCart(product, 1, true);
        });
        overlay.addEventListener("mouseleave", function () {
            overlay.style.setProperty("display", "none");
        });
    });
}

// Shop checkkbox filters display handler
if (shopFiltersLabelsIcons) {
    shopFiltersLabelsIcons.forEach((input, index) => {
        input.addEventListener("click", () => {
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
    shopPriceFilter.addEventListener("change", () => {
        shopPriceFilterSpan.textContent = shopPriceFilter.value + "€";
    });
    shopPriceFilter.addEventListener("mousemove", () => {
        shopPriceFilterSpan.textContent = shopPriceFilter.value + "€";
    });
    shopPriceFilter.addEventListener("touchmove", () => {
        shopPriceFilterSpan.textContent = shopPriceFilter.value + "€";
    }, {passive: true});
}

// Shop page apply filters button
shopFiltersBtn && shopFiltersBtn.addEventListener("click", (e) => {
    e.preventDefault();

    let filters = {
        SearchTerm: "",
        CategoryIDs: [],
        AuthorIDs: [],
        maxPrice: 0
    }

    let currentFilters = {
        SearchTerm: "",
        CategoryIDs: [],
        AuthorIDs: [],
        maxPrice: 0
    }

    // Fieldsets
    const filterContainers = document.querySelectorAll('.shop-filters-filter-container')



    filterContainers.forEach(filter => {

        const type = filter.getAttribute("data-name");
        const inputs = filter.querySelectorAll(".shop-filters-box-filter-item input")

        
        inputs.forEach(input => {
            currentFilters[type].push(parseInt(input.value))

            if (input.checked) {
                filters[type].push(parseInt(input.value))
            }
        });
    });
   
    // Price slider
    const priceSlider = document.querySelector('.shop-filters-price-container input');
    currentFilters.maxPrice = parseFloat(priceSlider.getAttribute('max'));
    filters.maxPrice = parseFloat(priceSlider.value);

      
    let needsUpdate = false

    for (let key in filters) {
        if (key == "CategoryIDs" || key == "AuthorIDs") {
            if (filters[key].length == 0) {
                filters[key] = [...currentFilters[key]]
            } else {
                needsUpdate = true;
            }
        } else if (key =="SearchTerm") {
            if (filters[key] != currentFilters[key]) {
                filters[key] = currentFilters[key].slice();
                needsUpdate = true;
            }
        } else if (key == "maxPrice") {
            if (filters[key] > 0) {
                needsUpdate = true;
            } else {
                filters[key] = currentFilters[key];

            }
            
        }
        filters.maxPrice = String(filters.maxPrice);
    }
    needsUpdate && postFilters(filters);
})

async function postFilters(filters = {}) {
    const products = await fetch(`${siteUrl}shop/filters`, {
        method: "POST",
        body: JSON.stringify(filters),
        headers: {
            "Content-Type": "application/json",
        },
    }).then((response) => response.json())
        .catch((error) => {
            console.error("Error:", error);
        });
    updateShopPage(products);    
           
}


function createShopProductItems(product) {
    // Product Card
    const productContainer = document.createElement("div");
    productContainer.classList.add("product-card");
    productContainer.setAttribute('data-id', product.BookId);


    // Product image
    const productImg = document.createElement("img");
    productImg.src = product.Photo.FilePath;
    productImg.alt = `${product.Title} cover`;

    // Product Title
    const productTitle = document.createElement("a");
    productTitle.classList.add("product-card-title");
    productTitle.setAttribute("href", `shop/product/${product.BookId}`);
    productTitle.textContent = product.Title;

    // Product Author
    const productAuthor = document.createElement("a");
    productAuthor.classList.add("product-card-author");
    productAuthor.setAttribute("href", `authors/details/${product.Author.AuthorId
        }`);
    productAuthor.textContent = product.Author.Name;
      
    //Price
    const productPrice = document.createElement("span");
    productPrice.classList.add("product-card-price");
    productPrice.textContent = product.Price;

    // Add to card icon
    const iconSVG = document.createElementNS("http://www.w3.org/2000/svg", "svg");
    iconSVG.classList.add("product-card-bag-icon");
    iconSVG.setAttribute("height", "24");
    iconSVG.setAttribute("width", "24");
    const iconPath = document.createElementNS(
        "http://www.w3.org/2000/svg",
        "path"
    );
    iconPath.setAttribute(
        "d",
        "M6 22q-.825 0-1.412-.587Q4 20.825 4 20V8q0-.825.588-1.412Q5.175 6 6 6h2q0-1.65 1.175-2.825Q10.35 2 12 2q1.65 0 2.825 1.175Q16 4.35 16 6h2q.825 0 1.413.588Q20 7.175 20 8v12q0 .825-.587 1.413Q18.825 22 18 22Zm0-2h12V8h-2v2q0 .425-.287.712Q15.425 11 15 11t-.712-.288Q14 10.425 14 10V8h-4v2q0 .425-.287.712Q9.425 11 9 11t-.712-.288Q8 10.425 8 10V8H6v12Zm4-14h4q0-.825-.587-1.412Q12.825 4 12 4q-.825 0-1.412.588Q10 5.175 10 6ZM6 20V8v12Z"
    );
    

   
    productContainer.appendChild(productImg);
    productContainer.appendChild(productTitle);
    productContainer.appendChild(productAuthor);
    productContainer.appendChild(productPrice);
    productContainer.appendChild(iconSVG);   

   
        iconSVG.addEventListener("click", () => {
            const container = cartIcon.closest(".product-card");
            const product = {
                id: product.BookId,
                img: product.Photo.FilePath,
                title: product.Title.replace(/\s+/g, " ").trim(),
                author: product.Author.Name
                    .replace(/\s+/g, " ")
                    .trim(),
                price: parseFloat(
                    product.Price.textContent.replace(/[$€]+/g, "")
                ),
            };

            addProductToCart(product, 1, true);
        });

    return productContainer;
}


function updateShopPage(products) {
    const productCardsContainer = document.querySelector(".shop-content-list");
    while (productCardsContainer.lastElementChild) {
        productCardsContainer.removeChild(productCardsContainer.lastElementChild);
    }
    products.forEach(item => productCardsContainer.appendChild(createShopProductItems(item)));

}


// Add to cart handler
if (productCardCartIcons) {
    productCardCartIcons.forEach((cartIcon, index) => {
        cartIcon.addEventListener("click", () => {
            const container = cartIcon.closest(".product-card");
            const product = {
                id: container.getAttribute("data-id"),
                img: productCardImg[index].getAttribute("src"),
                title: productCardTitle[index].textContent.replace(/\s+/g, " ").trim(),
                author: productCardAuthor[index].textContent
                    .replace(/\s+/g, " ")
                    .trim(),
                price: parseFloat(
                    productCardPrice[index].textContent.replace(/[$€]+/g, "")
                ),
            };

            addProductToCart(product, 1, true);
        });
    });
}

// Adds product to cart
function addProductToCart(product, quantity = 1, onlyAdd = false) {
    if (userCart.length > 0 && userCartTotal > 0) {
        checkAndUpadeCartIfExists(product, quantity, onlyAdd);
    } else {
        product.quantity = quantity;
        product.subTotal = product.price * product.quantity;
        userCart.push(product);
    }
    // Create user cart objects to local storage
    updateCartTotal();
    updateCartLocalStorage();
    updateCartDropDownList();
    displayCartElements();
}

// Remove Product from cart
function removeProductFormCart(product) {
    if (userCart.length > 0 && userCartTotal > 0) {
        userCart = userCart.filter((item) => item.id != product.id);
        updateCartTotal();
        updateCartLocalStorage();
        updateCartDropDownList();
        displayCartElements();
    }
}

// Checks if the product already exists in cart and updates the quantity, otherwise add it
function checkAndUpadeCartIfExists(product, quantity = 1, onlyAdd) {
    const found = userCart.some((el) => {
        if (el.id == product.id) {
            if (!onlyAdd) {
                el.quantity = quantity;
                el.subTotal = el.quantity * el.price;
            }
            return true;
        }
        return false;
    });
    if (!found) {
        userCart.push({
            ...product,
            ...{quantity: quantity, subTotal: product.price * quantity},
        });
    }
}

function updateCartTotal() {
    let newCartTotal = 0;
    userCart.forEach((product) => (newCartTotal += product.subTotal));
    userCartTotal = newCartTotal;
}

// Displays the whole cart's icon elements
function displayCartElements() {
    // View and checkout btns
    const btnsContainer = document.querySelector(
        ".cart-dropdown-list-btn-container"
    );
    btnsContainer.style.setProperty("display", "flex");
    if (userCart.length > 0 && userCartTotal > 0) {
        // Display  cart's total products span element
        createCartIconTotalProductsElement();
        cartIconTotal.textContent = "€" + parseFloat(userCartTotal).toFixed(2);
        // Update cart's icon dropdown list with products

        updateCartDropDownList();
    } else {
        cartIconTotal.textContent = "€0.00";

        const emptyCartTxt = document.createElement("li");
        const cartDropDownList = document.querySelector(".cart-dropdown-list ul");

        // Create msm to inform user tha his cart is empty
        emptyCartTxt.classList.add("cart-dropdown-list-empty");
        emptyCartTxt.textContent = "Your cart is empty";
        btnsContainer.style.setProperty("display", "none");

        cartDropDownList.appendChild(emptyCartTxt);
    }
}

function updateCartDropDownList() {
    const cartDropDownList = document.querySelector(".cart-dropdown-list ul");

    const cartTotal = document.querySelector(".cart-dropdown-list-total");

    let cartIconTotalProducts = document.querySelector(".cart-icon-items");
    if (cartIconTotalProducts) {
        if (userCart.length > 0) {
            cartIconTotalProducts.textContent = userCart.length;
        } else {
            cartIconTotalProducts.remove();
        }
    }

    while (cartDropDownList.firstChild) {
        cartDropDownList.removeChild(cartDropDownList.firstChild);
    }

    const cartListItems = userCart.map((i) => createCartIconListProduct(i));
    cartListItems.forEach((i) => cartDropDownList.appendChild(i));
    cartTotal.textContent = "€" + parseFloat(userCartTotal).toFixed(2);
}

function createCartIconListProduct(product) {
    const productContainer = document.createElement("li");
    productContainer.classList.add("cart-dropdown-list-item");
    productContainer.setAttribute('data-id', product.id);


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
    productSubtotal.textContent =
        product.quantity + " X " + "€" + parseFloat(product.price).toFixed(2);

    // Create list item
    productContainer.appendChild(productImg);
    productContainer.appendChild(productTitle);
    productContainer.appendChild(productSubtotal);
    return productContainer;
}

// Creates to the DOM the span element with the sum of products in the header's cart icon
function createCartIconTotalProductsElement() {
    let cartIconTotalProducts = document.querySelector(".cart-icon-items");
    // check if already exists
    if (cartIconTotalProducts) {
        cartIconTotalProducts.textContent = userCart.length;
        return;
    }
    // Create it
    cartIconTotalProducts = document.createElement("span");
    cartIconTotalProducts.classList.add("cart-icon-items");
    cartIconTotalProducts.textContent = userCart.length;
    cartDropdownList.parentNode.insertBefore(
        cartIconTotalProducts,
        cartDropdownList
    );
}

// input plus and minus buttons handler
numberInputContainers.forEach((numberInput) => {
    const plusBtn = numberInput.querySelector(".number-input-plus");
    const minusBtn = numberInput.querySelector(".number-input-minus");
    const input = numberInput.querySelector("input");

    CustomInputAddListeners(minusBtn, plusBtn, input);
});

function CustomInputAddListeners(minusBtn, plusBtn, input) {
    // Create the event.
    const changeEvent = new CustomEvent("change");

    minusBtn.addEventListener("click", () => {
        numberInputMinusBtn(input, minusBtn);
        input.dispatchEvent(changeEvent);
    });

    plusBtn.addEventListener("click", () => {
        {
            numberInputPlusBtn(input, minusBtn);
            input.dispatchEvent(changeEvent);
        }
    });

    // Number input text handler
    input.addEventListener("keyup", () => {
        numberInputOnKeyUpHandler(input, minusBtn);
        // input.dispatchEvent(changeEvent);
    });

    input.addEventListener("focusout", () => {
        numberInputOnFocusOutHandler(input, minusBtn);
        // input.dispatchEvent(changeEvent);
    });

    input.addEventListener("change", () => {
        numberInputOnChangeUpHandler(input, minusBtn);
    });
}

// Number input handlers
function numberInputPlusBtn(input, minusBtn) {
    const currValue = parseInt(input.value);
    minusBtn.style.setProperty("visibility", "visible");
    input.value = currValue + 1;
}

function numberInputMinusBtn(input, minusBtn) {
    const currValue = parseInt(input.value);
    if (currValue - 1 > 1) {
        input.value = currValue - 1;
    } else if (currValue - 1 == 1) {
        input.value = currValue - 1;
        minusBtn.style.setProperty("visibility", "hidden");
    }
}

if (productPageAddToCartBtn) {
    // Get product's info
    const product = {
        img: productPageProductImg.getAttribute("src"),
        title: productPageProductTitle.textContent.replace(/\s+/g, " ").trim(),
        author: productPageProductAuthor.textContent.replace(/\s+/g, " ").trim(),
        price: parseFloat(
            productPageProductPrice.textContent.replace(/[$,€]+/g, "")
        ),
    };

    const custonNumberInput = document.querySelector(
        ".number-input-container input"
    );

    // If products exists in cart set to the input the cart's quantity
    if (userCart.length > 0 && userCartTotal > 0) {
        userCart.forEach((i) => {
            if (i.title == product.title && i.quantity > 1) {
                const minusBtn = document.querySelector(".number-input-minus");
                minusBtn.style.setProperty("visibility", "visible");
                custonNumberInput.value = i.quantity;
            }
        });
    }

    productPageAddToCartBtn.addEventListener("click", () => {
        const quantity = parseInt(custonNumberInput.value);
        addProductToCart(product, quantity);
    });
}

// Handles non-numeric inputs
function numberInputOnKeyUpHandler(input, minusBtn) {
    if (!input.value.match(/^(\s*|\d+)$/)) {
        input.value = 1;
        minusBtn.style.setProperty("visibility", "hidden");
    }
}

// Number input on change handler
function numberInputOnChangeUpHandler(input, minusBtn) {
    if (input.value.length == 0 || isNaN(input.value)) {
        input.value = 1;
        minusBtn.style.setProperty("visibility", "hidden");
        return;
    }

    const currValue = parseInt(input.value);

    switch (currValue) {
        case 0:
            input.value = 1;
            minusBtn.style.setProperty("visibility", "hidden");
            break;
        case 1:
            minusBtn.style.setProperty("visibility", "hidden");
            break;
        default:
            // Prevents modify insertions like 05 to 5
            input.value = currValue;
            minusBtn.style.setProperty("visibility", "visible");
    }
}

// Number input on focus out  handler
function numberInputOnFocusOutHandler(input, minusBtn) {
    if (input.value.length == 0 || isNaN(input.value)) {
        input.value = 1;
        minusBtn.style.setProperty("visibility", "hidden");
    }
}

myAccountNavLinks.forEach((navLink, index) => {
    navLink.addEventListener("click", (e) => {
        const navLinkIcon = navLink.querySelector("svg");
        // Set up pages array
        const personalInfoPage = document.querySelector(".personal-info-form");
        const changePasswordPage = document.querySelector(".change-password-form");
        const ordersPage = document.querySelector(".account-page-orders");
        const accountPagesArr = [personalInfoPage, ordersPage, changePasswordPage];

        if (accountCurrentPageIndex != index) {
            accountPagesArr.forEach((page, i) => {
                if (index != i) {
                    page.style.setProperty("display", "none");
                    const hiddenNavLinkIcon = myAccountNavLinks[i].querySelector("svg");
                    hiddenNavLinkIcon.style.setProperty("visibility", "hidden");
                } else {
                    accountCurrentPageIndex = index;
                    navLinkIcon.style.setProperty("visibility", "visible");
                    page.style.setProperty("display", "flex");
                }
            });
        }
    });
});

// Creates cart list items in My-Cart page
function CreateMyCartPageListItem(product) {
    const cartItemContainer = document.createElement("section");
    cartItemContainer.classList.add("my-cart-page-item");

    // Image and Title container
    const imgTitleContainer = document.createElement("div");
    imgTitleContainer.classList.add(
        "my-cart-page-item-img-title-container",
        "width-40"
    );

    // Product image
    const productImg = document.createElement("img");
    productImg.src = product.img;
    productImg.alt = product.title;

    // Product title
    const productTitle = document.createElement("a");
    productTitle.src = "/";
    productTitle.classList.add("my-cart-page-item-title");
    productTitle.textContent = product.title;

    imgTitleContainer.appendChild(productImg);
    imgTitleContainer.appendChild(productTitle);

    // Product price
    const productPrice = document.createElement("span");
    productPrice.classList.add("my-cart-page-item-price", "width-10");
    productPrice.textContent = "€" + parseFloat(product.price).toFixed(2);

    // Product subtotal
    const productSubtotal = document.createElement("span");
    productSubtotal.classList.add("my-cart-page-item-subtotal", "width-10");
    productSubtotal.textContent =
        "€" + parseFloat(product.quantity * product.price).toFixed(2);

    // Create list item
    cartItemContainer.appendChild(CreateMyCartPageRevoveIcon(product));
    cartItemContainer.appendChild(imgTitleContainer);
    cartItemContainer.appendChild(productPrice);
    cartItemContainer.appendChild(createMyCartPageNumberInput(product.quantity));
    cartItemContainer.appendChild(productSubtotal);

    return cartItemContainer;
}

function CreateMyCartPageRevoveIcon(product) {
    const removeIconContainer = document.createElement("div");
    removeIconContainer.classList.add("my-cart-page-item-remove-icon", "width-5");

    const iconSVG = document.createElementNS("http://www.w3.org/2000/svg", "svg");
    iconSVG.setAttribute("height", "24");
    iconSVG.setAttribute("width", "24");
    const iconPath = document.createElementNS(
        "http://www.w3.org/2000/svg",
        "path"
    );
    iconPath.setAttribute(
        "d",
        "M6.4 19 5 17.6l5.6-5.6L5 6.4 6.4 5l5.6 5.6L17.6 5 19 6.4 13.4 12l5.6 5.6-1.4 1.4-5.6-5.6Z"
    );
    iconSVG.appendChild(iconPath);
    removeIconContainer.appendChild(iconSVG);

    iconSVG.addEventListener("click", () => {
        removeProductFormCart(product);
        const cartItemContainer = removeIconContainer.closest(".my-cart-page-item");
        cartItemContainer.remove();
        if (userCart.length == 0) {
            showEmptyCartMsg();
        }
    });

    return removeIconContainer;
}

function showEmptyCartMsg() {
    const emptyCartMsgContainer = myCartPageContainer.querySelector(
        ".my-cart-page-empty-cart"
    );

    const myCartPageTotalSection = myCartPageContainer.querySelector(
        ".my-cart-page-checkout-container"
    );

    const myCartPageListHeader = myCartPageContainer.querySelector(
        ".my-cart-page-item-header"
    );
    emptyCartMsgContainer.style.setProperty("display", "flex");
    myCartPageTotalSection.style.setProperty("display", "none");
    myCartPageListHeader.style.setProperty("display", "none");
}

function createMyCartPageNumberInput(value) {
    const inputNumberContainer = document.createElement("div");
    inputNumberContainer.classList.add("number-input-container");

    const input = document.createElement("input");
    input.setAttribute("type", "text");
    input.classList.add("none");
    input.value = value;

    const minusBtn = document.createElement("button");
    minusBtn.classList.add("number-input-minus");

    const plusBtn = document.createElement("button");
    plusBtn.classList.add("number-input-plus");

    const minusSVG = document.createElementNS(
        "http://www.w3.org/2000/svg",
        "svg"
    );
    minusSVG.setAttribute("height", "24");
    minusSVG.setAttribute("width", "24");
    const minusPath = document.createElementNS(
        "http://www.w3.org/2000/svg",
        "path"
    );
    minusPath.setAttribute("d", "M5 13v-2h14v2Z");
    minusSVG.appendChild(minusPath);

    const plusSVG = document.createElementNS("http://www.w3.org/2000/svg", "svg");
    plusSVG.setAttribute("height", "24");
    plusSVG.setAttribute("width", "24");
    const plusPath = document.createElementNS(
        "http://www.w3.org/2000/svg",
        "path"
    );
    plusPath.setAttribute("d", "M11 19v-6H5v-2h6V5h2v6h6v2h-6v6Z");
    plusSVG.appendChild(plusPath);

    if (value > 1) {
        minusBtn.style.setProperty("visibility", "visible");
    }
    minusBtn.appendChild(minusSVG);
    plusBtn.appendChild(plusSVG);

    inputNumberContainer.appendChild(minusBtn);
    inputNumberContainer.appendChild(input);
    inputNumberContainer.appendChild(plusBtn);
    CustomInputAddListeners(minusBtn, plusBtn, input);

    return inputNumberContainer;
}

// Creates a deep copy of an object
function createDeepObjectCopy(item) {
    const clone = JSON.parse(JSON.stringify(item));
    return clone;
}

// Emal validator
function validateEmail(email) {
    const emailRegex = /^\w+([\.-]?\w+)*@\w+([\.-]?\w+)*(\.\w{2,3})+$/;
    if (email.match(emailRegex)) {
        return true;
    }
    return false;
}

// Phone number validator
function ValidatePhone(phone) {
    const phoneRegex = /^\s*-?[0-9]{1,10}\s*$/;
    if (phone.match(phoneRegex)) {
        return true;
    }
    return false;
}

// Zip code validator
function ValidateZipCode(zipCode) {
    const zipCodeRegex = /^\s*-?[0-9]{1,5}\s*$/;
    if (zipCode.match(zipCodeRegex)) {
        return true;
    }
    return false;
}

// Check a password between 6 to 20 characters which contain at least one numeric digit, one uppercase and one lowercase letter
function checkPassword(password) {
    var pswRegex = /^(?=.*\d)(?=.*[a-z])(?=.*[A-Z]).{6,20}$/;
    if (password.match(pswRegex)) {
        return true;
    } else {
        return false;
    }
}

function passwordCofirmation(password, passwordCofirmation) {
    if (password == passwordCofirmation) {
        return true;
    } else {
        return false;
    }
}

function arraysEqual(a, b) {
    if (a === b) return true;
    if (a == null || b == null) return false;
    if (a.length !== b.length) return false;

    // If you don't care about the order of the elements inside
    // the array, you should sort both arrays here.
    // Please note that calling sort on an array will modify that array.
    // you might want to clone your array first.

    for (var i = 0; i < a.length; ++i) {
        if (a[i] !== b[i]) return false;
    }
    return true;
}


function floatToString(num) {
    return num.toFixed(Math.max(1, num.toString().substr(num.toString().indexOf(".") + 1).length));
}

async function checkoutPostData(url = "", data = {}) {
    const response = await fetch(url, {
        method: "POST",
        credentials: "same-origin",
        headers: {
            "Content-Type": "application/json",
        },
        redirect: "follow",
        body: JSON.stringify(data),
    });
    return response;
}

const checkout = document.querySelectorAll(".checkout-btn");

checkout.forEach(element => {
        element.addEventListener("click", async (event) => {
            event.preventDefault();

            const cart_data = userCart.map(c => ({
                bookId: c.id,
                quantity: c.quantity
            }));

            const url = `https://${window.location.host}/checkout`;

            const response = await checkoutPostData(url, cart_data);

            if (response.status == 200) {
                userCart = [];
                userCartTotal = 0;
                updateCartLocalStorage();
                window.location.replace(`https://${window.location.host}/`);
            }
        });
    }
);