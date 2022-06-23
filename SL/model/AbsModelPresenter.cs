namespace ClearArchitecture.SL
{
    public abstract class AbsModelPresenter<V> : AbsPresenter, IModelPresenter<V>
    {
        private readonly IModel<V> _model;

        protected AbsModelPresenter(string name, IModel<V> model) : base(name)
        {
            _model = model;
        }

        public IModel<V> GetModel()
        {
            return _model;
        }

        public V GetView()
        {
            return _model.GetView();
        }
    }
}
