using System;
using System.Text;

namespace ClearArchitecture.SL
{
    public abstract class AbsSubscriber : ISubscriber
    {
        private bool _isBusy = false;
        private string _name;
        private readonly StringBuilder _comment = new StringBuilder();

        protected AbsSubscriber()
        {
        }

        protected AbsSubscriber(string name) : base()
        {
            _name = name;
        }

        public string GetName()
        {
            return _name;
        }

        public void SetName(string name)
        {
            _name = name;
        }

        public bool IsValid()
        {
            return true;
        }

        public bool IsBusy()
        {
            return _isBusy;
        }

        public void SetBusy()
        {
            _isBusy = true;
        }

        public void SetUnBusy()
        {
            _isBusy = false;
        }

        public void AddComment(string comment)
        {
            _comment.Append(DateTime.Now.ToString("G") + ": " + comment);
        }

        public string GetComment()
        {
            return _comment.ToString();
        }
    }
}
