using AutoFixture;
using AutoFixture.AutoNSubstitute;
using AutoFixture.Xunit2;

namespace SSIM.Parser.Tests;

public class AutoDataNSubstituteAttribute : AutoDataAttribute
{
    public AutoDataNSubstituteAttribute()
        : base(() => new Fixture().Customize(
            new CompositeCustomization(new AutoNSubstituteCustomization(), 
                new SupportMutableValueTypesCustomization())))
    {
        
    }
}

public class InlineAutoNSubstituteAttribute : InlineAutoDataAttribute
{
    public InlineAutoNSubstituteAttribute(object[] values)
        : base( new CompositeCustomization(new AutoNSubstituteCustomization(), 
            new SupportMutableValueTypesCustomization()), values)
    {

    }
}