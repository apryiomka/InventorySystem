namespace inventorySyctem.Services.Entities
{
    /// <summary>
    /// The result of performing a certain action
    /// </summary>
    public class ActionResult
    {
        /// <summary>
        /// Gets or sets the result type
        /// </summary>
        public ResultType Result { get; set; }

        /// <summary>
        /// Gets or sets the list of errors
        /// </summary>
        public string[] Errors { get; set; }
        public enum ResultType
        {
            NotSet,
            Success,
            Error
        }
    }
}
