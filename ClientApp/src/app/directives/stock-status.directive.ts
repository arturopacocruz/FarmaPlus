import { Directive, ElementRef, Input, OnChanges, Renderer2 } from '@angular/core';

@Directive({
  selector: '[appStockStatus]',
  standalone: true
})
export class StockStatusDirective implements OnChanges {
  @Input('appStockStatus') stock: number = 0;

  constructor(private el: ElementRef, private renderer: Renderer2) {}

  ngOnChanges(): void {
    this.aplicarEstilo();
  }

  private aplicarEstilo(): void {
    const elemento = this.el.nativeElement;
    
    this.renderer.setStyle(elemento, 'display', 'inline-flex');
    this.renderer.setStyle(elemento, 'align-items', 'center');
    this.renderer.setStyle(elemento, 'gap', '0.35rem');
    this.renderer.setStyle(elemento, 'padding', '0.25rem 0.65rem');
    this.renderer.setStyle(elemento, 'border-radius', '9999px');
    this.renderer.setStyle(elemento, 'font-size', '0.78rem');
    this.renderer.setStyle(elemento, 'font-weight', '700');
    this.renderer.setStyle(elemento, 'letter-spacing', '0.02em');

    if (this.stock <= 0) {
      this.renderer.setStyle(elemento, 'background-color', '#fee2e2');
      this.renderer.setStyle(elemento, 'color', '#991b1b');
      this.renderer.setStyle(elemento, 'border', '1px solid #f87171');
      elemento.innerHTML = '<i class="fa-solid fa-triangle-exclamation"></i> Agotado (0)';
    } else if (this.stock < 15) {
      this.renderer.setStyle(elemento, 'background-color', '#fef3c7');
      this.renderer.setStyle(elemento, 'color', '#92400e');
      this.renderer.setStyle(elemento, 'border', '1px solid #fbbf24');
      elemento.innerHTML = `<i class="fa-solid fa-clock-rotate-left"></i> Crítico: ${this.stock} unid.`;
    } else {
      this.renderer.setStyle(elemento, 'background-color', '#d1fae5');
      this.renderer.setStyle(elemento, 'color', '#065f46');
      this.renderer.setStyle(elemento, 'border', '1px solid #34d399');
      elemento.innerHTML = `<i class="fa-solid fa-circle-check"></i> Stock: ${this.stock} unid.`;
    }
  }
}
