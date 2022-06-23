namespace ClearArchitecture.SL
{
    public abstract class AbsModelPresenter<V> : AbsPresenter, IModelPresenter<V>
    {
        protected AbsModelPresenter(string name) : base(name)
        {
        }

        public abstract IModel<V> GetModel();
        public abstract V GetView();
    }
}
