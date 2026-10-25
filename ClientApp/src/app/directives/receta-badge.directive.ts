import { Directive, ElementRef, Input, OnChanges, Renderer2 } from '@angular/core';

@Directive({
  selector: '[appRecetaBadge]',
  standalone: true
})
export class RecetaBadgeDirective implements OnChanges {
  @Input('appRecetaBadge') requiereReceta: boolean = false;

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

    if (this.requiereReceta) {
      this.renderer.setStyle(elemento, 'background-color', '#ede9fe');
      this.renderer.setStyle(elemento, 'color', '#5b21b6');
      this.renderer.setStyle(elemento, 'border', '1px solid #c4b5fd');
      elemento.innerHTML = '<i class="fa-solid fa-file-prescription"></i> Requiere Receta';
    } else {
      this.renderer.setStyle(elemento, 'background-color', '#f1f5f9');
      this.renderer.setStyle(elemento, 'color', '#475569');
      this.renderer.setStyle(elemento, 'border', '1px solid #cbd5e1');
      elemento.innerHTML = '<i class="fa-solid fa-cart-shopping"></i> Venta Libre';
    }
  }
}
