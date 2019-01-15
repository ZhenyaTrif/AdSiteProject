using BL;
using DataAccess;
using StructureMap;

namespace DependencyInjection
{
    public class MainRegistry : Registry
    {
        public MainRegistry()
        {
            Scan(
                scan => {
                    scan.WithDefaultConventions();
                    scan.AssemblyContainingType<BlRegistry>();
                    scan.AssemblyContainingType<DARegistry>();
                    scan.LookForRegistries();
                });
        }
    }
}
