import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root'
})
export class CartService {

  private CART_KEY = "cart_items";

  constructor() {}

  getCart() {
    return JSON.parse(localStorage.getItem(this.CART_KEY) || "[]");
  }

  saveCart(cart: any[]) {
    localStorage.setItem(this.CART_KEY, JSON.stringify(cart));
  }

  addToCart(product: any) {
    const cart = this.getCart();

    const existing = cart.find((x: any) => x.id === product.id);

    if (existing) {
      existing.qty += 1;
    } else {
      cart.push({
        id: product.id,
        name: product.name,
        price: product.price,
        imageUrl: product.imageUrl,
        qty: 1
      });
    }

    this.saveCart(cart);
  }

  increaseQty(id: number) {
    const cart = this.getCart();
    const item = cart.find((x: any) => x.id === id);

    if (item) item.qty += 1;

    this.saveCart(cart);
  }

  decreaseQty(id: number) {
    const cart = this.getCart();
    const item = cart.find((x: any) => x.id === id);

    if (item && item.qty > 1) item.qty -= 1;
    else this.removeItem(id);

    this.saveCart(cart);
  }

  removeItem(id: number) {
    let cart = this.getCart();
    cart = cart.filter((x: any) => x.id !== id);
    this.saveCart(cart);
  }

  clear() {
    localStorage.removeItem(this.CART_KEY);
  }
}
