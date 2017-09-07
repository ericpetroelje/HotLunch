using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotLunch.Domain.Library.Common
{
    public class CodedResult<TError>
    {
        public CodedResult(bool success)
        {
            IsSuccess = success;
            Errors = Enumerable.Empty<TError>();
        }

        public CodedResult(TError error)
        {
            if (error != null)
            {
                IsSuccess = false;
                Errors = new[] { error, };
            }
            else
            {
                IsSuccess = true;
                Errors = Enumerable.Empty<TError>();
            }
        }

        public CodedResult(IEnumerable<TError> errors)
        {
            IsSuccess = errors == null || !errors.Any();
            Errors = errors ?? Enumerable.Empty<TError>();
        }

        public bool IsSuccess { get; protected set; }
        public TError Error { get { return Errors.FirstOrDefault(); } }
        public IEnumerable<TError> Errors { get; protected set; }
    }
    public class CodedResult<TError, TResult> : CodedResult<TError>
    {
        public CodedResult(TError error) : base(error) { }

        public CodedResult(TResult result)
            : this(result, null)
        {
            Result = result;
        }

        public CodedResult(TResult result, TError error)
            : this(result, error != null ? new[] { error, } : null)
        {
            Result = result;
        }

        public CodedResult(IEnumerable<TError> errors = null) : base(errors) { }

        public CodedResult(TResult result, IEnumerable<TError> errors = null)
            : base(errors)
        {
            Result = result;
        }

        public TResult Result { get; protected set; }
    }

    public class CodedResultWarning<TError, TWarning> : CodedResult<TError>
    {
        public CodedResultWarning(TError error) : this(error != null ? new[] { error, } : null, null) { }

        public CodedResultWarning(TWarning warning) : this(null, warning != null ? new[] { warning, } : null) { }

        public CodedResultWarning(TError error, TWarning warning) : this(error != null ? new[] { error, } : null, warning != null ? new[] { warning, } : null) { }

        public CodedResultWarning(IEnumerable<TError> errors = null, IEnumerable<TWarning> warnings = null)
            : base(errors)
        {
            Warnings = warnings ?? Enumerable.Empty<TWarning>();
        }

        public TWarning Warning { get { return Warnings.FirstOrDefault(); } }
        public IEnumerable<TWarning> Warnings { get; protected set; }
    }

    public class CodedResult<TError, TWarning, TResult> : CodedResult<TError, TResult>
    {
        public CodedResult(IEnumerable<TError> errors = null, IEnumerable<TWarning> warnings = null)
            : base(errors)
        {
            Warnings = warnings ?? Enumerable.Empty<TWarning>();
        }

        public CodedResult(TResult result, IEnumerable<TError> errors = null, IEnumerable<TWarning> warnings = null)
            : base(result, errors)
        {
            Warnings = warnings ?? Enumerable.Empty<TWarning>();
        }

        public TWarning Warning { get { return Warnings.FirstOrDefault(); } }
        public IEnumerable<TWarning> Warnings { get; protected set; }
    }
}
