import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { CartService } from '../../services/cart.service';
import { OrderService } from '../../services/order.service';

@Component({
  selector: 'app-checkout',
  standalone: true,
  imports: [CommonModule, FormsModule],
  templateUrl: './checkout.component.html',
  styleUrls: ['./checkout.component.scss']
})
export class CheckoutComponent implements OnInit {

  cart: any[] = [];
  total = 0;

  // CUSTOMER INFO
  name = '';
  email = '';
  phone = '';
  address = '';

  constructor(
    private cartService: CartService,
    private orderService: OrderService
  ) {}

  ngOnInit(): void {
    this.cart = this.cartService.getCart();
    this.total = this.cart.reduce((sum, item) => sum + (item.price * item.qty), 0);
  }

  placeOrder() {
    const order = {
      customerName: this.name,
      email: this.email,
      phone: this.phone,
      address: this.address,
      totalAmount: this.total,
      items: this.cart.map(x => ({
        productId: x.id,
        quantity: x.qty,
        price: x.price
      }))
    };

    this.orderService.createOrder(order).subscribe(res => {
      alert("Order placed successfully!");
      this.cartService.clear();
    });
  }
}
