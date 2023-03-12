"use strict";
// Elements
// Header icons
const cartIcon = document.querySelector(".cart-icon");
const cartDropdownList = document.querySelector(".cart-dropdown-list");

const accountIcon = document.querySelector(".account-icon");
const accountDropdownMenu = document.querySelector(".account-dropdown-menu");

const navbarShopBtn = document.querySelector(".navlink-shop-container");
const navbarShopBtnIcon = document.querySelector(".navlink-shop-container svg");
const shopDropdownMenu = document.querySelector(".navbar-dropdown-menu");

// Navbar linksa
const navLinks = document.querySelectorAll(".navlink-container");

// Home page courousel elements
const carouselDots = document.querySelectorAll(".carousel-dot");
const carouselSlides = document.querySelectorAll(".carousel");

///////////////////////////////////////
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

// navLinks.forEach((link) => {
//   link.addEventListener("click", function (e) {
//     link.classList.add("active-navlink");
//   });
// });

// Carousel change screen handler
// Kee[s track of current slide ]
let slideIndex = 0;
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

showSlides();

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
