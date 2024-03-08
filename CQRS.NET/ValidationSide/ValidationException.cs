using System;

namespace CQRS.NET {
    public class ValidationException : Exception {

        public ValidationException(string? message = null) : base(message) {

        }

    }
}