namespace ClearArchitecture.SL
{
    public abstract class AbsAction : IAction
    {
        private bool _isRun = false;

        protected AbsAction()
        {
        }

        protected AbsAction(string v)
        {
        }

        public bool IsRun()
        {
            return _isRun;
        }

        public void SetRun()
        {
            _isRun = true;
        }
    }
}
