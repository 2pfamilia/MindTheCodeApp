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

// Product page elements
const productPageNumberInputPlusBtn = document.querySelector(
  ".number-input-plus.product-page"
);

const productPageNumberInputMinusBtn = document.querySelector(
  ".number-input-minus.product-page"
);

const productPageNumberInput = document.querySelector("input.product-page");

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

// Custom select element

const customSelects = document.querySelectorAll(".custom-select-container");

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
singinBtn.addEventListener("click", () => {
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

  input.addEventListener("click", function () {
    label.classList.add("form-input-label-up");
    label.classList.remove("form-input-label");
  });

  input.addEventListener("keydown", function () {
    label.classList.add("form-input-label-up");
    label.classList.remove("form-input-label");
  });

  input.addEventListener("focusout", () => {
    if (input.value == "") {
      label.classList.add("form-input-label");
      label.classList.remove("form-input-label-up");
      errorMsg.textContent = "";
    }
  });

  input.addEventListener("change", () => {
    if (input.name == "email") {
      if (!ValidateEmail(input.value)) {
        errorMsg.textContent = "Invalid email address";
      }
    } else if (input.name == "phone") {
      if (!ValidatePhone(input.value)) {
        errorMsg.textContent = "Invalid phone number";
      }
    } else if (input.name == "zip-code") {
      if (!ValidateZipCode(input.value)) {
        errorMsg.textContent = "Invalid Zip code";
      }
    }
  });
});

// Custom select element handler
if (customSelects) {
  customSelects.forEach((select) => {
    select.addEventListener("click", (e) => {
      const btn = select.querySelector("svg");
      const list = select.querySelector("ul");
      const input = select.querySelector("input");
      const inputTxt = select.querySelector(".custom-select-text");
      const label = select.querySelector("label");

      const display = list.style.getPropertyValue("display");
      if (display == "none" || display == "") {
        list.style.setProperty("display", "flex");
        btn.style.setProperty("transform", "rotate(90deg)");
      } else {
        if (e.target.tagName == "LI") {
          input.value = e.target.textContent.replace(/\s+/g, " ").trim();
          inputTxt.textContent = input.value;
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
}

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
      // const addToCartTxt = document.querySelectorAll(
      //   ".add-to-cart-overlay-btn span"
      // );
      // addToCartTxt[index].style.setProperty("animation-name", "textOversize");
      const product = {
        img: bookImg.getAttribute("src"),
        title: title.textContent.replace(/\s+/g, " ").trim(),
        author: author.textContent.replace(/\s+/g, " ").trim(),
        price: parseFloat(price.textContent.replace(/[$,€]+/g, "")),
      };

      addProductToCart(product);
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
  });
}

// Add to cart handler
if (productCardCartIcons) {
  productCardCartIcons.forEach((cartIcon, index) => {
    cartIcon.addEventListener("click", () => {
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

// Adds product to cart
function addProductToCart(product, quantity = 1) {
  let newUserCart = [];

  if (localStorage.getItem("userCart")) {
    const currUserCart = JSON.parse(localStorage.userCart);
    newUserCart = checkAndUpadeCartIfExists(currUserCart, product, quantity);
  } else {
    product.quantity = quantity;
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
function checkAndUpadeCartIfExists(cart, product, quantity = 1) {
  const found = cart.some((el) => {
    if (el.title == product.title) {
      el.quantity += quantity;
      el.subTotal = el.quantity * el.price;
      return true;
    }
    return false;
  });
  if (!found)
    cart.push({
      ...product,
      ...{ quantity: quantity, subTotal: product.price },
    });
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

// input plus and minus buttons handler
if (productPageNumberInputPlusBtn) {
  productPageNumberInputMinusBtn.addEventListener("click", () =>
    numberInputMinusBtn(productPageNumberInput, productPageNumberInputMinusBtn)
  );

  productPageNumberInputPlusBtn.addEventListener("click", () =>
    numberInputPlusBtn(productPageNumberInput, productPageNumberInputMinusBtn)
  );

  // Number input text handler
  productPageNumberInput.addEventListener("keyup", () =>
    numberInputOnKeyUpHandler(
      productPageNumberInput,
      productPageNumberInputMinusBtn
    )
  );

  productPageNumberInput.addEventListener("change", () =>
    numberInputOnChangeUpHandler(
      productPageNumberInput,
      productPageNumberInputMinusBtn
    )
  );
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
  productPageAddToCartBtn.addEventListener("click", () => {
    const product = {
      img: productPageProductImg.getAttribute("src"),
      title: productPageProductTitle.textContent.replace(/\s+/g, " ").trim(),
      author: productPageProductAuthor.textContent.replace(/\s+/g, " ").trim(),
      price: parseFloat(
        productPageProductPrice.textContent.replace(/[$,€]+/g, "")
      ),
    };
    const quantity = parseInt(productPageNumberInput.value);
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
  const currValue = parseInt(input.value);

  if (input.value.length == 0) {
    input.value = 1;
    minusBtn.style.setProperty("visibility", "hidden");
  }
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

// Emal validator
function ValidateEmail(email) {
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
  console.log("skata");
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
function CheckPassword(password) {
  var pswRegex = /^(?=.*\d)(?=.*[a-z])(?=.*[A-Z]).{6,20}$/;
  if (password.match(pswRegex)) {
    return true;
  } else {
    return false;
  }
}
