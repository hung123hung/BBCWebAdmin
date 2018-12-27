using System;
using Xunit;
using BBCWebAPI.Controllers.UI;
using BBCWebAPI.Data;

namespace Test
{
    public class UnitTest1
    {
        HomeController _homeController;
        DataContext _dataContext;
        public UnitTest1(DataContext dataContext)
        {
            _dataContext = dataContext;
            _homeController = new HomeController(_dataContext);
        }
        [Fact]
        public void RanDomString()
        {
            string x = _homeController.Randomcctring(10);
        }
    }
}
