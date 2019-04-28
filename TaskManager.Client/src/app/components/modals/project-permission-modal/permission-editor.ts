import { Lookup } from 'src/app/models/Lookup';
import { PermissionEditingModel } from './permission-editing-model';
import { Utils } from 'src/app/common/utils';
import { PermissionsModel, ProjectPermissionModel } from 'src/app/models/PermissionsModel';

export class PermissionEditor {
    public constructor(private _permissionsLookup: Lookup[]) {
    }

    private _input: any = {};
    private _result: any = {};

    public add(projectId: number, permissions: number[]): PermissionEditingModel[] {
        const resultArray = this._permissionsLookup.map(x => new PermissionEditingModel(x.Id, x.Name, permissions.indexOf(x.Id) > -1));

        this._input[projectId] = permissions;
        this._result[projectId] = resultArray;
        return resultArray;
    }

    public get(projectId: number): PermissionEditingModel[] {
        return this._result[projectId];
    }

    public getChanges(): PermissionsModel {
        const permissions = [];

        Object.keys(this._input).forEach((key) => {
            const inputArray = this._input[key];
            const outputArray = (<PermissionEditingModel[]>this._result[key]).filter(x => x.IsChecked === true).map(x => x.Id);

            if (!Utils.equals(inputArray, outputArray)) {
                permissions.push(new ProjectPermissionModel(Number(key), outputArray));
            }
        });

        return new PermissionsModel(permissions);
    }
}
