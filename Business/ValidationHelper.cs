using Microsoft.AspNetCore.Mvc.ModelBinding;
using System;
using System.Collections.Generic;
using System.Text;

namespace webDotNet.Business
{
    public class ModelValidationError : Dictionary<string, string>
    {
        public ModelValidationError()
        { }

        public new void Add(string key, string value)
        {
            if (this.ContainsKey(key))
            {
                this[key] += "\r\n" + value;
            }
            else
            {
                base.Add(key, value);
            }
        }

        public new string ToString()
        {
            string errorMsg = "";
            foreach (var item in this)
            {
                errorMsg = errorMsg == "" ? "Campos Violados no ViewModel" : errorMsg;
                errorMsg += errorMsg + $"\r\n{item.Key}:{item.Value}";
            }
            return errorMsg;
        }


        public void UpdateModelState(ModelStateDictionary modelState)
        {
            foreach (var item in this)
            {
                modelState.Remove(item.Key);
                modelState.AddModelError(item.Key, item.Value);
            }
        }
    }
}
