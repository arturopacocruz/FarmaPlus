import { Pipe, PipeTransform } from '@angular/core';

@Pipe({
  name: 'estadoVencimiento',
  standalone: true
})
export class EstadoVencimientoPipe implements PipeTransform {
  transform(fechaIso: string | Date): string {
    if (!fechaIso) return 'Sin fecha';

    const fecha = new Date(fechaIso);
    const hoy = new Date();
    const diffTiempo = fecha.getTime() - hoy.getTime();
    const diffDias = Math.ceil(diffTiempo / (1000 * 60 * 60 * 24));

    const opciones: Intl.DateTimeFormatOptions = { day: '2-digit', month: '2-digit', year: 'numeric' };
    const fechaFormateada = fecha.toLocaleDateString('es-ES', opciones);

    if (diffDias < 0) {
      return `${fechaFormateada} (Vencido)`;
    } else if (diffDias <= 60) {
      return `${fechaFormateada} (Próximo a vencer)`;
    } else {
      return `${fechaFormateada} (Vigente)`;
    }
  }
}
