import { Component, OnInit, inject, signal } from '@angular/core';
import { CommonModule } from '@angular/common';
import { MatTableModule } from '@angular/material/table';
import { UsersService } from '../../services/users.service';
import type User from '../../models/user.entity';

@Component({
  selector: 'app-home',
  standalone: true,
  imports: [
    CommonModule, 
    MatTableModule  // ВАЖНО: MatTableModule должен быть здесь
  ],
  templateUrl: './home.html',
  styleUrl: './home.scss'
})
export class HomeComponent implements OnInit {
  users = signal<User[]>([]);
  usersService = inject(UsersService);
  displayedColumns: string[] = ['id', 'name'];

  ngOnInit() {
    this.usersService.getAll().subscribe({
      next: (v) => this.users.set(v),
      error: (e) => console.error(e),
      complete: () => console.info('complete') 
    });
  }
}
