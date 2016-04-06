namespace inventorySyctem.Services.Validation
{
    /// <summary>
    /// Base class to validate the entities
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public abstract class ValidationRule<T>
    {
        /// <summary>
        /// Basic validation rule
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public abstract bool IsValid(T entity);

        /// <summary>
        /// Gets the label of the broken rule
        /// </summary>
        public abstract EntityValidationException ValidationException { get; }
    }
}
