namespace ClearArchitecture.SL
{
    public abstract class AbsModel<M,V> : IModel<V>, ILifecycleListener where M : IModel<V>
    {
        private readonly LifecycleObserver _lifecycle;
        private readonly IModelView<M,V> _modelView;

        protected AbsModel(IModelView<M,V> modelView)
        {
            _modelView = modelView;
            _lifecycle = new LifecycleObserver(this);
        }

        public void AddLifecycleObserver(ILifecycle stateable)
        {
            _modelView.AddLifecycleObserver(stateable);
        }

        public V GetView()
        {
            return (V)_modelView;
        }

        public bool IsValid()
        {
            return _modelView.IsValid();
        }

        public abstract void OnCreate();

        public abstract void OnDestroy();

        public abstract void OnReady();

        public abstract void OnStart();

        public int GetState()
        {
            return _lifecycle.GetState();
        }

        public void SetState(int state)
        {
            _lifecycle.SetState(state);
        }
    }
}
