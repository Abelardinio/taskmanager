import { Role } from './enums/Role';

export class UserInfo {
    constructor(
        public username: string,
        public firstName: string,
        public lastName: string,
        public email: string,
        public role: Role
    ) { }
}
