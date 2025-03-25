import { CommonModule } from '@angular/common';
import { Component } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { IUser } from '../shared/queries/users/models/user';
import { Router } from '@angular/router';
import { UserService } from '../shared/queries/users/user-service';


@Component({
  selector: 'app-login',
  standalone: true,
  imports: [FormsModule, CommonModule],
  templateUrl: './login.component.html',
  styleUrl: './login.component.scss'
})
export class LoginComponent {

email: any;
password: any;
passwordVisible: boolean = false;
user: IUser | null = null;
idUser: string | null = null;
isLoading = false;

constructor(
  private userService: UserService,
  private router: Router 
) {}

login() {
  this.isLoading = true; 
  this.userService.login(this.email, this.password).subscribe({
    next: (tokenResponse) => {
      this.isLoading = false; 
      const name = localStorage.getItem('username');
      this.idUser = localStorage.getItem('userId');
      alert(`Bem-vindo, ${name}!`);
      this.router.navigate(['/tasks'], { queryParams: { userId: this.idUser } });
    },
    error: (err) => {
      this.isLoading = false; 
      alert('Email ou senha inválidos.');
    }
  });
}

  togglePasswordVisibility() {
    this.passwordVisible = !this.passwordVisible;
  }
}
