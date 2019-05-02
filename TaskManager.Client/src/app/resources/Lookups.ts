import { Role } from '../models/enums/Role';

export const Lookups = {
    Roles: [
        {
            Id: Role.User,
            Name: 'User'
        },
        {
            Id: Role.SiteAdministrator,
            Name: 'Site Administrator'
        },
        {
            Id: Role.ProjectsCreator,
            Name: 'Projects Creator'
        }
    ]
};
