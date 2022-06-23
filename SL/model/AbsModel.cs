using System.Collections.Generic;

namespace ClearArchitecture.SL
{
    public abstract class AbsModel<V> : AbsProviderSubscriber, IModel<V>
    {
        private readonly IModelView<V> _modelView;
        private readonly LifecycleObserver _observer;
        private readonly List<IAction> _actions = new List<IAction>();

        protected AbsModel(string name, IModelView<V> modelView) : base(name)
        {
            _modelView = modelView;
            _observer = new LifecycleObserver(this);
        }

        public V GetView()
        {
            return (V)_modelView;
        }

        new public bool IsValid()
        {
            return _modelView.IsValid();
        }

        public virtual void OnCreate()
        {
            //
        }

        public virtual void OnDestroy()
        {
            //
        }

        public virtual void OnReady()
        {
            //
        }

        public virtual void OnStart()
        {
            //
        }

        public int GetState()
        {
            return _modelView.GetState();
        }

        public void SetState(int state)
        {
            _observer.SetState(state);
        }

        public override List<string> GetProviderSubscription()
        {
            List<string> list = new List<string>();
            list.Add(ModelUnion.NAME);
            list.Add(MessengerUnion.NAME);
            return list;
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
            for (int i = 0; i < _actions.Count; i++)
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

        public bool OnAction(IAction action)
        {
            return true;
        }

        public abstract void Read(IMessage message);
    }
}
