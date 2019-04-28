export class PermissionsModel {
    public constructor(public Permissions: ProjectPermissionModel[]) { }
}

export class ProjectPermissionModel {
    public constructor(
        public ProjectId: number,
        public Permissions: number[]) { }
}
