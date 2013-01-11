using System;
using FluentNHibernate;
using FluentNHibernate.Conventions;
namespace PittsburghBabaTemple.Core.Data.Conventions
{
    public class CustomForeignKeyConvention : ForeignKeyConvention
    {
        protected override string GetKeyName(Member property, Type type)
        {
            if (property == null)
            {
                return type.Name + "Id";
            }
            else if (property.Name.Contains("Parent"))
            {
                return "ParentId";
            }

            return property.Name + "Id";
        }
    }
}

