using System;

namespace inventorySyctem.Services.Validation
{
    /// <summary>
    /// Entity validation excepton
    /// </summary>
    public class EntityValidationException : ApplicationException
    {
        public EntityValidationException(string parameter, string error)
        {
            Parameter = parameter;
            Error = error;
        }

        /// <summary>
        /// Parameter name
        /// </summary>
        public string Parameter { get; }

        /// <summary>
        /// Validation error
        /// </summary>
        public string Error { get; }
    }
}
