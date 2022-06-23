using System.Collections.Generic;

namespace ClearArchitecture.SL
{
    public abstract class AbsPresenter : AbsProviderSubscriber, IPresenterSubscriber
    {
        private readonly LifecycleObserver _lifecycle;
        private readonly List<IAction> _actions = new List<IAction>();

        protected AbsPresenter(string name) : base(name)
        {
            _lifecycle = new LifecycleObserver(this);
        }

        public void OnCreate()
        {
            //
        }

        public void OnStart()
        {
            //
        }

        public void OnReady()
        {
            //
        }

        public void OnDestroy()
        {
            //
        }

        public abstract void Read(IMessage message);

        public override List<string> GetProviderSubscription()
        {
            List<string> list = new List<string>();
            list.Add(PresenterUnion.NAME); 
            list.Add(MessengerUnion.NAME);
            return list;
        }

        public bool OnAction(IAction action)
        {
            return true;
        }

        public int GetState()
        {
            return _lifecycle.GetState();
        }

        public bool IsRegister()
        {
            return true;
        }
        new public bool IsValid()
        {
            return _lifecycle.GetState() != Lifecycle.ON_DESTROY;
        }

        public void AddAction(IAction action)
        {
            switch (GetState()) 
            {
                case Lifecycle.ON_DESTROY:
                    return;

                case Lifecycle.ON_START:
                case Lifecycle.ON_CREATE:  
                    if (!action.IsRun())
                    {
                            _actions.Add(action);
                    }
                    return;

                default: 
                    if (!action.IsRun())
                    {
                        _actions.Add(action);
                    }
                    DoActions();
                    return;
            }
        }

        protected void DoActions()
        {
            var deleted = new List<IAction>();
            for (int i=0;i < _actions.Count;i++)
            {
                if (GetState() != Lifecycle.ON_READY)
                {
                    break;
                }
                if (!_actions[i].IsRun())
                {
                    _actions[i].SetRun();
                    OnAction(_actions[i]);
                    deleted.Add(_actions[i]);
                }
            }
            foreach (IAction action in deleted)
            {
                _actions.Remove(action);
            }
        }

        public void SetState(int state)
        {
            _lifecycle.SetState(state);
        }
    }
}
