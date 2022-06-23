namespace ClearArchitecture.SL
{
    public abstract class AbsModel<V> : IModel<V>
    {
        private readonly IModelView<V> _modelView;

        protected AbsModel(IModelView<V> modelView)
        {
            _modelView = modelView;
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
            switch (state)
            {
                case Lifecycle.ON_CREATE:
                    OnCreate();
                    break;
                case Lifecycle.ON_START:
                    OnStart();
                    break;
                case Lifecycle.ON_READY:
                    OnReady();
                    break;
                case Lifecycle.ON_DESTROY:
                    OnDestroy();
                    break;
            }
        }
    }
}
