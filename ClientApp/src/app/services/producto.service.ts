import { Injectable, inject } from '@angular/core';
import { HttpClient, HttpParams } from '@angular/common/http';
import { Observable } from 'rxjs';
import { Producto, CrearProductoDto, ActualizarProductoDto } from '../models/producto.model';

@Injectable({
  providedIn: 'root'
})
export class ProductoService {
  private readonly http = inject(HttpClient);
  private readonly apiUrl = 'http://localhost:5017/api/productos';

  obtenerTodos(categoriaId?: number, buscar?: string): Observable<Producto[]> {
    let params = new HttpParams();
    if (categoriaId !== undefined && categoriaId !== null && categoriaId > 0) {
      params = params.set('categoriaId', categoriaId.toString());
    }
    if (buscar && buscar.trim() !== '') {
      params = params.set('buscar', buscar.trim());
    }
    return this.http.get<Producto[]>(this.apiUrl, { params });
  }

  obtenerPorId(id: number): Observable<Producto> {
    return this.http.get<Producto>(`${this.apiUrl}/${id}`);
  }

  crear(dto: CrearProductoDto): Observable<Producto> {
    return this.http.post<Producto>(this.apiUrl, dto);
  }

  actualizar(id: number, dto: ActualizarProductoDto): Observable<void> {
    return this.http.put<void>(`${this.apiUrl}/${id}`, dto);
  }

  eliminar(id: number): Observable<void> {
    return this.http.delete<void>(`${this.apiUrl}/${id}`);
  }
}
