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

        public virtual string GetName()
        {
            return _name;
        }

        public virtual void SetName(string name)
        {
            _name = name;
        }

        public virtual bool IsValid()
        {
            return true;
        }

        public virtual bool IsBusy()
        {
            return _isBusy;
        }

        public virtual void SetBusy()
        {
            _isBusy = true;
        }

        public virtual void SetUnBusy()
        {
            _isBusy = false;
        }

        public virtual void AddComment(string comment)
        {
            _comment.Append(DateTime.Now.ToString("G") + ": " + comment);
        }

        public virtual string GetComment()
        {
            return _comment.ToString();
        }
    }
}
