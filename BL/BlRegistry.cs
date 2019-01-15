using BL.Interfaces;
using DataAccess.Interfaces;
using DataAccess.Wrappers;
using StructureMap;

namespace BL
{
    public class BlRegistry : Registry
    {
        public BlRegistry()
        {
            For<IBridge>().Use<Bridge>();

            Forward<IAdWrapper, AdWrapperDA>();
            Forward<ICategoryWrapper, CategoryWrapperDA>();
        }
    }
}
