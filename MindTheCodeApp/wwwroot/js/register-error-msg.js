const registerForm = document.querySelector(".signup-page-form");

registerForm && (() => {
    const errorMsg = registerForm.querySelector(".form-main-error-msg");
    errorMsg.style.setProperty("display", "block");
    errorMsg.textContent = "Email address is already registered";    
})();