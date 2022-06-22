using System;
using System.Text;

namespace ClearArchitecture.SL
{
    public class ExtError
    {
        private readonly StringBuilder _errorText = new StringBuilder();
        private string _sender;

        public ExtError()
        {
        }

        public ExtError(string sender, string error) : this()
        {
            AddError(sender, error);
        }

        public ExtError(string sender, Exception e) : this()
        {
            AddError(sender, e);
        }

        public string GetErrorText()
        {
            return _errorText.ToString();
        }

        public ExtError AddError(string sender, string error)
        {
            if (string.IsNullOrEmpty(error)) return this;

            _sender = sender;
            _errorText.Append(DateTime.Now.ToString("G") + ": " + error);
            return this;
        }

        public ExtError AddError(string sender, Exception e)  
        {
            if (e == null) return this;

            _sender = sender;
            _errorText.Append(DateTime.Now.ToString("G") + ": " + e.Message);
            return this;
        }

        public bool HasError()
        {
            return (_errorText.Length > 0);
        }

        public string GetSender() 
        {
            return _sender;
        }

        public ExtError SetSender(string sender)  
        {
            _sender = sender;
            return this;
        }

    }
}
