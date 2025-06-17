using AutoMapper;

namespace FLP.AzureFunction.UnitTest.Stubs;

internal class IMapperStub
{
    public static IMapper GetMapper(params Profile[] profiles)
    {
        var configuration = new MapperConfiguration(cfg =>
        {
            profiles.ToList().ForEach(profile => cfg.AddProfile(profile));
        });
        return configuration.CreateMapper();
    }
}
