using System.Collections.Generic;

namespace TestAssignment.Utilities.Common.Data
{
    public class Error
    {
        private readonly IList<ErrorDetail> _additionalInfo;

        public Error(IList<ErrorDetail> info = null)
        {
            _additionalInfo = info ?? new List<ErrorDetail>();
        }

        public IList<ErrorDetail> AdditionalInfo => _additionalInfo;
    }

    public class ErrorDetail
    {
        public ErrorDetail(string key, string value)
        {
            Key = key;
            Value = value;
        }

        public ErrorDetail()
        {
            
        }
        public string Key { get; set; }
        public string Value { get; set; }

        #region Overrides of Object

        public override string ToString()
        {
            return $"{Key}: {Value}";
        }

        #endregion
    }
}