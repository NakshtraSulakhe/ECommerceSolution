import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { CartService } from '../../services/cart.service';

@Component({
  selector: 'app-cart',
  standalone: true,
  imports: [CommonModule],
  templateUrl: './cart.component.html',
  styleUrls: ['./cart.component.scss']
})
export class CartComponent implements OnInit {

  cart: any[] = [];
  total = 0;

  constructor(private cartService: CartService) {}

  ngOnInit(): void {
    this.loadCart();
  }

  loadCart() {
    this.cart = this.cartService.getCart();
    this.total = this.cart.reduce((sum, item) => sum + (item.price * item.qty), 0);
  }

  increase(id: number) {
    this.cartService.increaseQty(id);
    this.loadCart();
  }

  decrease(id: number) {
    this.cartService.decreaseQty(id);
    this.loadCart();
  }

  remove(id: number) {
    this.cartService.removeItem(id);
    this.loadCart();
  }
}
