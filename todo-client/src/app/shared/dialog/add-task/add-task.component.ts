import { Component } from '@angular/core';
import { MatDialogModule, MatDialogRef } from '@angular/material/dialog';
import { FormsModule } from '@angular/forms';  
import { MatInputModule } from '@angular/material/input';

@Component({
  selector: 'app-add-task',
  standalone: true,
  imports: [FormsModule, MatDialogModule, MatInputModule], 
  templateUrl: './add-task.component.html',
  styleUrls: ['./add-task.component.scss']
})
export class AddTaskComponent {
  taskName: string = '';
  taskDescription: string = '';

  constructor(private dialogRef: MatDialogRef<AddTaskComponent>) {}

  closeDialog(): void {
    this.dialogRef.close();
  }
}
