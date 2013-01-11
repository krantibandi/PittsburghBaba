using System;
using FluentNHibernate.Automapping;
using PittsburghBabaTemple.Core.Interfaces;

namespace PittsburghBabaTemple.Core.Data
{
   
    public class AutoMapConfig : DefaultAutomappingConfiguration
    {
        public override bool ShouldMap(Type type)
        {
            if (type.FullName != null && (type == typeof(IEntity) || type.Name.Contains("ModelEntity") || type.FullName.Contains("DisplayClass"))) { return false; }
            return type.Namespace == "PittsburghBabaTemple.Core.Model";
        }
        public override bool IsId(FluentNHibernate.Member member)
        {
            return member.Name == "Id";
        }
    }
}
