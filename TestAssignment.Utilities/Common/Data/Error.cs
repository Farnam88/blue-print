using System.Collections.Generic;

namespace TestAssignment.Utilities.Common.Data
{
    public class Error
    {
        private readonly IDictionary<string, string> _additionalInfo;

        public Error(IDictionary<string, string> info = null)
        {
            _additionalInfo = info ?? new Dictionary<string, string>();
        }

        public IDictionary<string, string> AdditionalInfo => _additionalInfo;
    }
}