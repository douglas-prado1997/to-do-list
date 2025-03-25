import { CommonModule } from '@angular/common';
import { Component } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { NgxPaginationModule } from 'ngx-pagination';
import { ITask } from '../../shared/queries/tasks/models/task';
import { TaskService } from '../../shared/queries/tasks/task-service';
import { MatDialog, MatDialogModule } from '@angular/material/dialog';
import { AddTaskComponent } from '../../shared/dialog/add-task/add-task.component';
import { ActivatedRoute, Router } from '@angular/router';
import { UserDialogComponent } from '../../shared/dialog/users/users.component';
import { IUser } from '../../shared/queries/users/models/user';
import { UserService } from '../../shared/queries/users/user-service';

@Component({
  selector: 'app-todo',
  standalone: true,
  imports: [FormsModule, CommonModule, NgxPaginationModule, MatDialogModule],
  templateUrl: './tasks.component.html',
  styleUrls: ['./tasks.component.scss'],
})
export class TasksComponent {
  tasks: ITask[] = [];
  newTask: ITask | null = null;
  newUser: IUser | null = null;

  page: number = 1;
  itemsPerPage: number = 5;
  userId!: number;
  name: string | null = null;

  constructor(
    public dialog: MatDialog,
    private taskService: TaskService,
    private userService: UserService,
    private route: ActivatedRoute,
    private router: Router 
  ) {}

  ngOnInit() {
    this.name = localStorage.getItem('username');
    this.route.queryParams.subscribe((params) => {
      this.userId = params['userId'];
      if (!this.userId) {
      alert("necessario fazer login");
        this.router.navigate(['/login']);
      }
      this.refresh();
    });
  }

  refresh() {
    this.taskService.getTasksByUserId(this.userId).subscribe((tasks) => {
      this.tasks = tasks;
    });
  }

  addTask() {
    const dialogRef = this.dialog.open(AddTaskComponent, {
      width: '400px',
      height: '300px',
      position: {
        left: '40%',
      },
      panelClass: 'centered-dialog',
    });

    dialogRef.afterClosed().subscribe((result) => {
      if (result) {
        if (!this.newTask) {
          this.newTask = {} as ITask;
        }
        this.newTask.name = result.name;
        this.newTask.description = result.description;
        this.newTask.completed = false;
        this.newTask.createDate = new Date().toISOString();
        this.newTask.removed = false;
        this.newTask.idUser = this.userId;
        this.taskService.addTask(this.newTask).subscribe(() => {
          this.newTask = null;
          this.refresh();
          alert('Tarefa adicionada com sucesso!');
        });
      }
    });
  }

  deleteTask(task: any) {
    this.taskService.deleteTask(task.id).subscribe(() => {
      this.refresh();
    });
  }

  completeTask(task: any) {
    task.completed = !task.completed;
    this.taskService.updateTask(task.id, task).subscribe(() => {
      this.refresh();
    });
  }

  addUser() {
    const dialogRef = this.dialog.open(UserDialogComponent, {
      width: '600px',
      height: '400px',
      position: {
        left: '30%',
      },
      panelClass: 'centered-dialog',
    });

    dialogRef.afterClosed().subscribe((result) => {
      if (result) {
        if (!this.newUser) {
          this.newUser = {} as IUser;
        }
        this.newUser.name = result.name;
        this.newUser.email = result.email;
        this.newUser.password = result.password;
        this.newUser.creationDate = new Date().toISOString();
        this.newUser.removed = false;
        this.userService.addUser(this.newUser).subscribe(() => {
          this.newUser = null;
          this.refresh();
          alert('usuário adicionado com sucesso!');
        });
      }
    });
  }

  logoff() {
    localStorage.clear();
    this.router.navigate(['/login']);
  }
}
