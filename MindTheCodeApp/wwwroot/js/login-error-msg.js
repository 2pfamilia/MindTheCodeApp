signinForm && (() => {
    overlayEffect.style.setProperty("display", "block");
    signinForm.style.setProperty("display", "flex");

    const EmailInput = signinForm.querySelectorAll("input")[0];
    const label = signinForm.querySelectorAll("label")[0];
    const errorMsg = signinForm.querySelector(".form-main-error-msg");
    label.classList.add("form-input-label-up");
    label.classList.remove("form-input-label");

    EmailInput.value = document.getElementById("user-email").getAttribute("data-name");
    errorMsg.style.setProperty("display", "block");
    errorMsg.textContent = "Wrong Email or Password";    
})();