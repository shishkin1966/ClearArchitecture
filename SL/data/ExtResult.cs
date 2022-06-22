using System;

namespace ClearArchitecture.SL
{
    public class ExtResult : IValidated
    {
        public const int NOT_SEND = -1;
        public const int LAST = -2;

        private object _data = null;
        private ExtError _error = new ExtError();
        private int _order = NOT_SEND;
        private string _name;
        private int _id = 0;

        public ExtResult(object data)
        {
            _data = data;
        }

        public object GetData() 
        {
            return _data;
        }

        public ExtResult SetData(object data) 
        {
            _data = data;
            return this;
        }

        public ExtError GetError() {
            return _error;
        }

        public ExtResult SetError(ExtError error)
        {
            _error = error;
            return this;
        }

        public ExtResult SetError(string sender, string error)  
        {
            if (string.IsNullOrEmpty(error)) return this;

            _error.AddError(sender, error);
            return this;
        }

        public ExtResult SetError(string sender, Exception e)  
        {
            if (e == null) return this;

            _error.AddError(sender, e);
            return this;
        }

        public string GetErrorText() 
        {
            return _error.GetErrorText();
        }
        public string GetSender() 
        {
            return _error.GetSender();
        }

        public bool IsValid()
        {
            return _data != null && !_error.HasError();
        }

        public bool IsEmpty() 
        {
            return _data == null;
        }

        public int GetOrder() 
        {
            return _order;
        }

        public ExtResult SetOrder(int order) 
        {
            _order = order;
            return this;
        }

        public bool HasError()
        {
            return !_error.HasError();
        }

        public string GetName() 
        {
            return _name;
        }

        public ExtResult SetName(string name)  
        {
            _name = name;
            return this;
        }
        public int GetId() 
        {
            return _id;
        }

        public ExtResult SetId(int id)  
        {
            _id = id;
            return this;
        }


}
}
