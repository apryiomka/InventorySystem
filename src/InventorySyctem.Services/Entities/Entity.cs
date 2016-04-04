namespace inventorySyctem.Services.Entities
{
    /// <summary>
    /// The base entity
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public abstract class Entity<T>
    {
        /// <summary>
        /// The entity unique identifier
        /// </summary>
        public T Id { get; set; }
    }
}
