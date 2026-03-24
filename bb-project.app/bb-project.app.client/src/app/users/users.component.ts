import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { UserService } from '../user.service';

@Component({
  selector: 'app-users',
  templateUrl: './users.component.html',
  standalone: false
})
export class UsersComponent implements OnInit {
  users: string[] = [];
  currentUser = '';
  customUser = '';

  constructor(private userService: UserService, private router: Router) {}

  ngOnInit(): void {
    this.users = this.userService.getUsers();
    this.currentUser = this.userService.getCurrentUser();
  }

  selectUser(userId: string): void {
    if (!userId) return;
    this.userService.setCurrentUser(userId);
    this.currentUser = userId;
  }

  addCustomUser(): void {
    const trimmed = this.customUser.trim();
    if (!trimmed) return;
    if (!this.users.includes(trimmed)) {
      this.users.push(trimmed);
    }
    this.selectUser(trimmed);
    this.customUser = '';
  }

  goToPlans(): void {
    this.router.navigate(['/plans']);
  }
}
