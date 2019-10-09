using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Newtonsoft.Json;

namespace Evoflare.API.Core
{
    public class ErrorResponseModel : ResponseModel
    {
        public ErrorResponseModel() : base("error") { }

        public ErrorResponseModel(string message) : this()
        {
            Message = message;
        }

        public ErrorResponseModel(ModelStateDictionary modelState) : this()
        {
            Message = "The model state is invalid.";
            ValidationErrors = new Dictionary<string, IEnumerable<string>>();

            var keys = modelState.Keys.ToList();
            var values = modelState.Values.ToList();

            for (var i = 0; i < values.Count; i++)
            {
                var value = values[i];

                if (keys.Count <= i)
                {
                    // Keys not available for some reason.
                    break;
                }

                var key = keys[i];

                if (value.ValidationState != ModelValidationState.Invalid || value.Errors.Count == 0)
                {
                    continue;
                }

                var errors = value.Errors.Select(e => e.ErrorMessage);
                ValidationErrors.Add(key, errors);
            }
        }

        public ErrorResponseModel(Dictionary<string, IEnumerable<string>> errors) : this("Errors have occurred.", errors) { }

        public ErrorResponseModel(string errorKey, string errorValue) : this(errorKey, new string[] { errorValue }) { }

        public ErrorResponseModel(string errorKey, IEnumerable<string> errorValues) : this(new Dictionary<string, IEnumerable<string>> { { errorKey, errorValues } }) { }

        public ErrorResponseModel(string message, Dictionary<string, IEnumerable<string>> errors) : this()
        {
            Message = message;
            ValidationErrors = errors;
        }

        public string Message { get; set; }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public Dictionary<string, IEnumerable<string>> ValidationErrors { get; set; }
        // For use in development environments.
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string ExceptionMessage { get; set; }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string ExceptionStackTrace { get; set; }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string InnerExceptionMessage { get; set; }
    }
}