﻿using System.Linq;
using Xunit;

namespace System.Web.Mvc.Test
{
    public class FilterProvidersTest
    {
        [Fact]
        public void DefaultFilterProviders()
        {
            // Assert
            Assert.NotNull(FilterProviders.Providers.Single(fp => fp is GlobalFilterCollection));
            Assert.NotNull(FilterProviders.Providers.Single(fp => fp is FilterAttributeFilterProvider));
            Assert.NotNull(FilterProviders.Providers.Single(fp => fp is ControllerInstanceFilterProvider));
        }
    }
}
