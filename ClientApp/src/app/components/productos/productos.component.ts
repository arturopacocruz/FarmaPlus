import { Component, OnInit, inject, ChangeDetectorRef } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { ProductoService } from '../../services/producto.service';
import { CategoriaService } from '../../services/categoria.service';
import { Producto, CrearProductoDto, ActualizarProductoDto } from '../../models/producto.model';
import { Categoria } from '../../models/categoria.model';
import { StockStatusDirective } from '../../directives/stock-status.directive';
import { RecetaBadgeDirective } from '../../directives/receta-badge.directive';
import { FormatoPrecioPipe } from '../../pipes/formato-precio.pipe';
import { EstadoVencimientoPipe } from '../../pipes/estado-vencimiento.pipe';

@Component({
  selector: 'app-productos',
  standalone: true,
  imports: [
    CommonModule,
    FormsModule,
    StockStatusDirective,
    RecetaBadgeDirective,
    FormatoPrecioPipe,
    EstadoVencimientoPipe
  ],
  templateUrl: './productos.component.html',
  styleUrl: './productos.component.css'
})
export class ProductosComponent implements OnInit {
  private readonly productoService = inject(ProductoService);
  private readonly categoriaService = inject(CategoriaService);
  private readonly cdr = inject(ChangeDetectorRef);

  productos: Producto[] = [];
  categorias: Categoria[] = [];
  cargando: boolean = true;
  errorMensaje: string = '';

  categoriaSeleccionadaId: number = 0;
  terminoBusqueda: string = '';

  modalAbierto: boolean = false;
  modoEdicion: boolean = false;
  productoSeleccionadoId: number = 0;

  productoForm: CrearProductoDto = {
    nombre: '',
    descripcion: '',
    precio: 0,
    stock: 0,
    codigoBarras: '',
    requiereReceta: false,
    fechaVencimiento: '',
    categoriaId: 1
  };

  ngOnInit(): void {
    this.cargarCategorias();
    this.cargarProductos();
  }

  cargarCategorias(): void {
    this.categoriaService.obtenerTodas().subscribe({
      next: (datos) => {
        this.categorias = datos;
        this.cdr.detectChanges();
      },
      error: () => {
        this.cdr.detectChanges();
      }
    });
  }

  cargarProductos(): void {
    this.cargando = true;
    this.errorMensaje = '';
    this.cdr.detectChanges();
    const catId = this.categoriaSeleccionadaId > 0 ? this.categoriaSeleccionadaId : undefined;
    this.productoService.obtenerTodos(catId, this.terminoBusqueda).subscribe({
      next: (datos) => {
        this.productos = datos;
        this.cargando = false;
        this.cdr.detectChanges();
      },
      error: () => {
        this.errorMensaje = 'No se pudo conectar con el servidor backend. Verifique que FarmaPlus esté ejecutándose en el puerto 5017.';
        this.cargando = false;
        this.cdr.detectChanges();
      }
    });
  }

  filtrarPorCategoria(id: number): void {
    this.categoriaSeleccionadaId = id;
    this.cargarProductos();
  }

  buscar(): void {
    this.cargarProductos();
  }

  limpiarFiltros(): void {
    this.categoriaSeleccionadaId = 0;
    this.terminoBusqueda = '';
    this.cargarProductos();
  }

  abrirModalCrear(): void {
    this.modoEdicion = false;
    const fechaDefecto = new Date();
    fechaDefecto.setFullYear(fechaDefecto.getFullYear() + 1);
    this.productoForm = {
      nombre: '',
      descripcion: '',
      precio: 10,
      stock: 50,
      codigoBarras: '',
      requiereReceta: false,
      fechaVencimiento: fechaDefecto.toISOString().split('T')[0],
      categoriaId: this.categorias.length > 0 ? this.categorias[0].id : 1
    };
    this.modalAbierto = true;
    this.cdr.detectChanges();
  }

  abrirModalEditar(p: Producto): void {
    this.modoEdicion = true;
    this.productoSeleccionadoId = p.id;
    this.productoForm = {
      nombre: p.nombre,
      descripcion: p.descripcion,
      precio: p.precio,
      stock: p.stock,
      codigoBarras: p.codigoBarras,
      requiereReceta: p.requiereReceta,
      fechaVencimiento: p.fechaVencimiento ? p.fechaVencimiento.split('T')[0] : '',
      categoriaId: p.categoriaId
    };
    this.modalAbierto = true;
    this.cdr.detectChanges();
  }

  cerrarModal(): void {
    this.modalAbierto = false;
    this.cdr.detectChanges();
  }

  guardarProducto(): void {
    if (!this.productoForm.nombre.trim()) return;

    if (this.modoEdicion) {
      const dto: ActualizarProductoDto = { ...this.productoForm };
      this.productoService.actualizar(this.productoSeleccionadoId, dto).subscribe({
        next: () => {
          this.cerrarModal();
          this.cargarProductos();
        },
        error: () => {
          this.errorMensaje = 'Error al actualizar el producto en el backend.';
          this.cdr.detectChanges();
        }
      });
    } else {
      this.productoService.crear(this.productoForm).subscribe({
        next: () => {
          this.cerrarModal();
          this.cargarProductos();
        },
        error: () => {
          this.errorMensaje = 'Error al guardar el nuevo producto.';
          this.cdr.detectChanges();
        }
      });
    }
  }

  eliminarProducto(id: number): void {
    if (confirm('¿Está seguro de eliminar este producto?')) {
      this.productoService.eliminar(id).subscribe({
        next: () => {
          this.cargarProductos();
        },
        error: () => {
          this.errorMensaje = 'Error al eliminar el producto.';
          this.cdr.detectChanges();
        }
      });
    }
  }
}
