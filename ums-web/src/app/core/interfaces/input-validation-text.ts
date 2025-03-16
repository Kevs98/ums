/**
 * Represents the validation messages for input validations.
 */
export type ValidationMessages = {
    required?: string;
    email?: string;
    minlength?: string;
    maxlength?: string;
    pattern?: string;
    dateRangeInvalid?: string;
    // add more validation keys here
};

/**
 * Represents a type for input validations with text values.
 * It is a dictionary object where the keys are strings and the values are ValidationMessages.
 *
 * @example
 * const inputValidations: InputValidationsText = {
 *   username: {
 *     required: 'Username is required',
 *     minLength: 'Username must be at least 5 characters long',
 *   },
 *   email: {
 *     required: 'Email is required',
 *     email: 'Invalid email format',
 *   },
 * };
 */
export type InputValidationsText = { [key: string]: ValidationMessages };
