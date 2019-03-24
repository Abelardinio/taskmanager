export const Messages = {
    Tasks: {
        Removed: 'Task was successfully removed.',
        Completed: 'Task was successfully completed',
        Added: 'Task was successfully Added',
        Validation: {
            TimeToCompleteRequired: 'Time to complete can\'t be less then 1 hour.'
        }
    },
    Projects: {
        Added: 'Project was successfully Added'
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
    }
};
