using Bogus;

namespace FLP.AzureFunction.UnitTest.Fixtures;

internal abstract class BasicFixture<T>
    where T : class
{
    protected Faker<T> Faker = new Faker<T>("en_US").StrictMode(true);

    public T Generate()
    {
        return Faker.Generate();
    }

    public IEnumerable<T> Generate(int count)
    {
        return Faker.Generate(count);
    }

}
