let header_hide = document.getElementById("header-hide");
let menu = document.getElementById("menu");
let account = document.getElementById("account-button");
window.addEventListener("resize", function () {
  if (window.innerWidth < 1480) {
    header_hide.style.display = "block";
    menu.style.display = "none";
    account.style.display = "none";
  } else {
    header_hide.style.display = "none";
    menu.style.display = "block";
    account.style.display = "block";
  }
});
window.addEventListener("load", function () {
  if (window.innerWidth < 1480) {
    header_hide.style.display = "block";
    menu.style.display = "none";
    account.style.display = "none";
  } else {
    header_hide.style.display = "none";
    menu.style.display = "block";
    account.style.display = "block";
  }
});