import { Component, OnInit, inject, ChangeDetectorRef } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { CategoriaService } from '../../services/categoria.service';
import { Categoria, CrearCategoriaDto, ActualizarCategoriaDto } from '../../models/categoria.model';

@Component({
  selector: 'app-categorias',
  standalone: true,
  imports: [CommonModule, FormsModule],
  templateUrl: './categorias.component.html',
  styleUrl: './categorias.component.css'
})
export class CategoriasComponent implements OnInit {
  private readonly categoriaService = inject(CategoriaService);
  private readonly cdr = inject(ChangeDetectorRef);

  categorias: Categoria[] = [];
  cargando: boolean = true;
  errorMensaje: string = '';

  modalAbierto: boolean = false;
  modoEdicion: boolean = false;
  categoriaSeleccionadaId: number = 0;

  categoriaForm: CrearCategoriaDto = {
    nombre: '',
    descripcion: ''
  };

  ngOnInit(): void {
    this.cargarCategorias();
  }

  cargarCategorias(): void {
    this.cargando = true;
    this.errorMensaje = '';
    this.cdr.detectChanges();
    this.categoriaService.obtenerTodas().subscribe({
      next: (datos) => {
        this.categorias = datos;
        this.cargando = false;
        this.cdr.detectChanges();
      },
      error: () => {
        this.errorMensaje = 'No se pudo conectar con el backend de FarmaPlus. Verifique que esté ejecutándose.';
        this.cargando = false;
        this.cdr.detectChanges();
      }
    });
  }

  abrirModalCrear(): void {
    this.modoEdicion = false;
    this.categoriaForm = { nombre: '', descripcion: '' };
    this.modalAbierto = true;
    this.cdr.detectChanges();
  }

  abrirModalEditar(cat: Categoria): void {
    this.modoEdicion = true;
    this.categoriaSeleccionadaId = cat.id;
    this.categoriaForm = {
      nombre: cat.nombre,
      descripcion: cat.descripcion
    };
    this.modalAbierto = true;
    this.cdr.detectChanges();
  }

  cerrarModal(): void {
    this.modalAbierto = false;
    this.cdr.detectChanges();
  }

  guardarCategoria(): void {
    if (!this.categoriaForm.nombre.trim()) return;

    if (this.modoEdicion) {
      const dto: ActualizarCategoriaDto = {
        nombre: this.categoriaForm.nombre,
        descripcion: this.categoriaForm.descripcion
      };
      this.categoriaService.actualizar(this.categoriaSeleccionadaId, dto).subscribe({
        next: () => {
          this.cerrarModal();
          this.cargarCategorias();
        },
        error: () => {
          this.errorMensaje = 'Error al actualizar la categoría.';
          this.cdr.detectChanges();
        }
      });
    } else {
      this.categoriaService.crear(this.categoriaForm).subscribe({
        next: () => {
          this.cerrarModal();
          this.cargarCategorias();
        },
        error: () => {
          this.errorMensaje = 'Error al registrar la categoría.';
          this.cdr.detectChanges();
        }
      });
    }
  }

  eliminarCategoria(id: number): void {
    if (confirm('¿Está seguro de eliminar esta categoría?')) {
      this.categoriaService.eliminar(id).subscribe({
        next: () => {
          this.cargarCategorias();
        },
        error: () => {
          this.errorMensaje = 'Error al eliminar la categoría.';
          this.cdr.detectChanges();
        }
      });
    }
  }
}
