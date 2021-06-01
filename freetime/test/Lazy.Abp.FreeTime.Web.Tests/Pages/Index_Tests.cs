using System.Threading.Tasks;
using Shouldly;
using Xunit;

namespace Lazy.Abp.FreeTime.Pages
{
    public class Index_Tests : FreeTimeWebTestBase
    {
        [Fact]
        public async Task Welcome_Page()
        {
            var response = await GetResponseAsStringAsync("/");
            response.ShouldNotBeNull();
        }
    }
}
