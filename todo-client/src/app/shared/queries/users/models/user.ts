export interface IUser {
    id: number;
    name: string;
    email: string;
    password: string;
    creationDate: string; 
    isSysAdmin: boolean;
    removed: boolean;
  }
  