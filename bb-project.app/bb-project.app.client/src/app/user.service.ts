import { Injectable } from '@angular/core';

@Injectable()
export class UserService {
  private readonly storageKey = 'currentUserId';
  private readonly dummyUsers = ['user1', 'user2', 'user3'];

  getCurrentUser(): string {
    return localStorage.getItem(this.storageKey) ?? this.dummyUsers[0];
  }

  setCurrentUser(userId: string): void {
    localStorage.setItem(this.storageKey, userId);
  }

  getUsers(): string[] {
    return [...this.dummyUsers];
  }
}
