using DataAccess.Interfaces;
using DataAccess.Wrappers;
using StructureMap;

namespace DataAccess
{
    public class DARegistry : Registry
    {
        public DARegistry()
        {
            For<IAdWrapper>().Use<AdWrapperDA>();
            For<ICategoryWrapper>().Use<CategoryWrapperDA>();
        }
    }
}

