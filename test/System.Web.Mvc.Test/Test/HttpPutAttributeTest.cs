﻿using Xunit;

namespace System.Web.Mvc.Test
{
    public class HttpPutAttributeTest
    {
        [Fact]
        public void IsValidForRequestReturnsFalseIfHttpVerbIsNotPost()
        {
            HttpVerbAttributeHelper.TestHttpVerbAttributeWithInvalidVerb<HttpPutAttribute>("GET");
        }

        [Fact]
        public void IsValidForRequestReturnsTrueIfHttpVerbIsPost()
        {
            HttpVerbAttributeHelper.TestHttpVerbAttributeWithValidVerb<HttpPutAttribute>("PUT");
        }

        [Fact]
        public void IsValidForRequestThrowsIfControllerContextIsNull()
        {
            HttpVerbAttributeHelper.TestHttpVerbAttributeNullControllerContext<HttpPutAttribute>();
        }
    }
}
