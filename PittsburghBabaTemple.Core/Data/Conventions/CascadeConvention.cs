using FluentNHibernate.Conventions;

namespace PittsburghBabaTemple.Core.Data.Conventions
{
    public class CascadeConvention : IHasManyConvention, IHasManyToManyConvention, IReferenceConvention
    {
        public void Apply(FluentNHibernate.Conventions.Instances.IManyToOneInstance instance)
        {
            instance.Cascade.None();
        }

        public void Apply(FluentNHibernate.Conventions.Instances.IOneToManyCollectionInstance instance)
        {
            instance.Inverse();
            instance.Cascade.AllDeleteOrphan();
        }

        public void Apply(FluentNHibernate.Conventions.Instances.IManyToManyCollectionInstance instance)
        {
            instance.Cascade.All();
        }
    }
}