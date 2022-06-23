namespace ClearArchitecture.SL
{
    public abstract class AbsModel<V> : IModel<V>
    {
        private readonly IModelView<V> _modelView;
        private readonly LifecycleObserver _observer;

        protected AbsModel(IModelView<V> modelView)
        {
            _modelView = modelView;
            _observer = new LifecycleObserver(this);
        }

        public V GetView()
        {
            return (V)_modelView;
        }

        public bool IsValid()
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
    }
}
