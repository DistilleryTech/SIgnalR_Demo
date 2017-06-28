namespace SignalRKit.Web.Utilities
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;

    /// <summary>
    /// FluentValidation methods
    /// </summary>
    public static class FluentValidationHelper
    {
        private static IList<Type> assemblyTypes;

        /// <summary>
        /// The executing assembly types
        /// </summary>
        private static IList<Type> AssemblyTypes
        {
            get
            {
                if (assemblyTypes == null)
                    assemblyTypes = Assembly.GetExecutingAssembly().GetTypes();

                return assemblyTypes;
            }
        }

        /// <summary>
        /// Gets the validator
        /// </summary>
        /// <param name="validatingObject">The validating object</param>
        /// <returns>Object validator</returns>
        public static FluentValidation.IValidator GetValidator(Object validatingObject)
        {
            var baseValidatorType = typeof(FluentValidation.AbstractValidator<>);
            var objType = validatingObject.GetType();
            var validatingObjectType = baseValidatorType.MakeGenericType(objType);

            var validatorType = FindValidatorType(validatingObjectType);
            if (validatorType != null)
                return (FluentValidation.IValidator)Activator.CreateInstance(validatorType);

            return null;
        }

        /// <summary>
        /// Finds the type of the validator
        /// </summary>
        /// <param name="validatingObjectType">Type of the validating object</param>
        /// <returns></returns>
        public static Type FindValidatorType(Type validatingObjectType)
        {
            return AssemblyTypes.FirstOrDefault(t => t.IsSubclassOf(validatingObjectType));
        }
    }
}