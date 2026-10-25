import { Pipe, PipeTransform } from '@angular/core';

@Pipe({
  name: 'formatoPrecio',
  standalone: true
})
export class FormatoPrecioPipe implements PipeTransform {
  transform(valor: number, moneda: string = 'Bs.'): string {
    if (valor === null || valor === undefined || isNaN(valor)) {
      return `${moneda} 0.00`;
    }
    const partes = valor.toFixed(2).split('.');
    partes[0] = partes[0].replace(/\B(?=(\d{3})+(?!\d))/g, ',');
    return `${moneda} ${partes.join('.')}`;
  }
}
