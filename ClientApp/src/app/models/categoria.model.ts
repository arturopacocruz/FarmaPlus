export interface Categoria {
  id: number;
  nombre: string;
  descripcion: string;
  cantidadProductos?: number;
}

export interface CrearCategoriaDto {
  nombre: string;
  descripcion: string;
}

export interface ActualizarCategoriaDto {
  nombre: string;
  descripcion: string;
}
