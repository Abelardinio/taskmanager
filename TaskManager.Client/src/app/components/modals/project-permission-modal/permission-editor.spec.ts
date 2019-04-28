import { PermissionEditor } from './permission-editor';
import { Lookup } from 'src/app/models/Lookup';

describe('Permission editor tests', () => {

    let editor: PermissionEditor;
    const lookup: Lookup[] = [
        new Lookup(0, ''),
        new Lookup(1, ''),
        new Lookup(2, '')
    ];

    beforeEach(() => {
        editor = new PermissionEditor(lookup);
    });

    it('should return editing model', () => {
        const input = [1];
        const projectId = 1;

        const editingModel = editor.add(projectId, input);

        expect(editingModel[0].IsChecked).toBe(false);
        expect(editingModel[1].IsChecked).toBe(true);
        expect(editingModel[2].IsChecked).toBe(false);
    });

    it('should verify if there are no changes in editing model', () => {
        const input = [1];
        const projectId = 1;

        editor.add(projectId, input);

        expect(editor.getChanges().Permissions.length).toBe(0);
    });

    it('should detect changes in model', () => {
        const input = [1];
        const projectId = 1;

        const editingModel = editor.add(projectId, input);

        editingModel[0].IsChecked = true;

        const changes = editor.getChanges();

        expect(changes.Permissions.length).toBe(1);
        expect(changes.Permissions[0].ProjectId).toBe(1);
        expect(changes.Permissions[0].Permissions.length).toBe(2);
        expect(changes.Permissions[0].Permissions[0]).toBe(0);
        expect(changes.Permissions[0].Permissions[1]).toBe(1);
    });
});
