// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.


"use strict";
// Elements
const cartIcon = document.querySelector(".cart-icon");
const cartDropdownList = document.querySelector(".cart-dropdown-list");

const accountIcon = document.querySelector(".account-icon");
const accountDropdownMenu = document.querySelector(".account-dropdown-menu");

const navLinks = document.querySelectorAll(".navlink-container");

///////////////////////////////////////
// Event handlers

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


// Header navlinks
navLinks.forEach((link) => {
    link.addEventListener("click", function (e) {
        link.classList.add("active-navlink");
    });
});
