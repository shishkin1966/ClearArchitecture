using System;
using System.Collections.Generic;

namespace ClearArchitecture.SL
{
    public class ModelUnion : AbsSmallUnion, IModelUnion
    {
        public const string NAME = "ModelUnion";

        public ModelUnion(string name) : base(name)
        {
        }

        public override int CompareTo(IProvider other)
        {
            if (other is ModelUnion)
            { return 0; }
            else
            { return 1; }
        }

        public List<string> GetTitles()
        {
            List<string> list = new List<string>();
            foreach(IProviderSubscriber subscriber in this.GetSubscribers())
            {
                var model = (IModelSubscriber)subscriber;
                if (model.IsValid() && !string.IsNullOrEmpty(model.GetTitle()))
                {
                    list.Add(model.GetTitle());
                }
            }
            return list;
        }

        public IModelSubscriber GetModel(string name)
        {
            return base.GetSubscriber(name) as IModelSubscriber;
        }

        public IModelSubscriber GetModelByTile(string title)
        {
            if (string.IsNullOrEmpty(title)) return default;

            foreach (IProviderSubscriber subscriber in this.GetSubscribers())
            {
                if (subscriber is IModelSubscriber m) 
                {
                    if (m.GetTitle() == title)
                    {
                        return m;
                    }
                }
            }
            return default;
        }

        public override void OnUnRegisterSubscriber(IProviderSubscriber subscriber)
        {
            base.OnUnRegisterSubscriber(subscriber);

            GC.Collect();
        }
    }
}
