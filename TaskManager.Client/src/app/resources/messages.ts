export const Messages = {
    Tasks: {
        Removed: 'Task was successfully removed.',
        Completed: 'Task was successfully completed',
        Added: 'Task was successfully Added',
        Assigned: 'Task was successfully assigned',
        Validation: {
            TimeToCompleteRequired: 'Time to complete can\'t be less then 1 hour.'
        }
    },
    Permisssions: {
        Updated: 'Permissions were successfully updated'
    },
    Projects: {
        Added: 'Project was successfully Added'
    },
    Features: {
        Added: 'Feature was successfully Added',
        Validation: {
            ProjectIsRequired: 'You should specify the project'
        }
    },
    Users: {
        Added: 'User was successfully Added'
    },
    Common: {
        FormValidationMessage: 'All the highlighted fields should be completed.',
        Validation: {
            Required: (fieldName: string) => fieldName + ' is required.',
            LengthBetween:
                (fieldName: string, minLength: number, maxLength: number) =>
                    fieldName + ' must be between ' + minLength + ' and ' + maxLength + ' characters long.',
            Email: 'Please provide a valid email address.'
        }
    },
    Errors: {
        Unauthorized: 'You do not have permissions to complete this operation.',
        Unknown: 'You request failed due to unknown error.'
    }
};
