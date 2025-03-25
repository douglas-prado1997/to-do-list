import { Component } from '@angular/core';
import { MatDialogRef } from '@angular/material/dialog';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { NgxPaginationModule } from 'ngx-pagination';
import { MatDialogModule } from '@angular/material/dialog';
import { MatInputModule } from '@angular/material/input';

@Component({
  selector: 'app-user-dialog',
  standalone: true,
  imports: [FormsModule, CommonModule, NgxPaginationModule, MatDialogModule, MatInputModule],
  templateUrl: './users.component.html',
  styleUrl: './users.component.scss'
})
export class UserDialogComponent {
  name: string = '';
  email: string = '';
  password: string = '';
  passwordConfirm: string = '';

  constructor(private dialogRef: MatDialogRef<UserDialogComponent>) {}

  ngOnInit() {
    debugger
  }


  closeDialog(): void {
    this.dialogRef.close();
  }
}
