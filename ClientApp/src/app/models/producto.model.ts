export interface Producto {
  id: number;
  nombre: string;
  descripcion: string;
  precio: number;
  stock: number;
  codigoBarras: string;
  requiereReceta: boolean;
  fechaVencimiento: string;
  categoriaId: number;
  categoriaNombre: string;
}

export interface CrearProductoDto {
  nombre: string;
  descripcion: string;
  precio: number;
  stock: number;
  codigoBarras: string;
  requiereReceta: boolean;
  fechaVencimiento: string;
  categoriaId: number;
}

export interface ActualizarProductoDto {
  nombre: string;
  descripcion: string;
  precio: number;
  stock: number;
  codigoBarras: string;
  requiereReceta: boolean;
  fechaVencimiento: string;
  categoriaId: number;
}
