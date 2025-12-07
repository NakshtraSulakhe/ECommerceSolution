import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { CommonModule } from '@angular/common';
import { ProductService } from '../../services/product.service';
import { CartService } from '../../services/cart.service';

@Component({
  selector: 'app-product-detail',
  standalone: true,
  imports: [CommonModule],
  templateUrl: './product-detail.component.html',
  styleUrls: ['./product-detail.component.scss']
})
export class ProductDetailComponent implements OnInit {

  id!: number;
  product: any = null;
  selectedImage = '';

  constructor(
    private route: ActivatedRoute,
    private productService: ProductService,
    private cart: CartService
  ) {}

  ngOnInit(): void {
    this.id = Number(this.route.snapshot.paramMap.get("id"));

    this.productService.getById(this.id).subscribe((res: any) => {
      this.product = res;

      // Main image
      this.selectedImage = res.imageUrl;
    });
  }

  changeImage(url: string) {
    this.selectedImage = url;
  }

  addToCart() {
    this.cart.addToCart(this.product);
    alert("Product added to cart!");
  }
}
