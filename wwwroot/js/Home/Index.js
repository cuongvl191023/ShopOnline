const prevBtn = document.getElementById('prev-btn');
const nextBtn = document.getElementById('next-btn');
const lstProduct = document.getElementsByClassName('card');
const lstproductWidth = (lstProduct[0].offsetWidth + 20) * lstProduct.length;
let productWrapperPosition = 0;
let productWrapper = document.getElementById('lstproduct');
let productWrapperWidth = productWrapper.offsetWidth;
nextBtn.addEventListener('click', () => {
  if (productWrapperPosition > -(lstproductWidth - productWrapperWidth)) {
    productWrapperPosition -= 100;
    productWrapper.style.transform = `translateX(${productWrapperPosition}px)`;
  }
});
prevBtn.addEventListener('click', () => {
  if (productWrapperPosition < 0) {
    productWrapperPosition += 100;
    productWrapper.style.transform = `translateX(${productWrapperPosition}px)`;
  }
});
window.addEventListener('resize', function () {
  productWrapperPosition = 0;
  productWrapper = document.getElementById('lstproduct');
  productWrapperWidth = productWrapper.offsetWidth;
  productWrapper.style.transform = `translateX(0)`;
});