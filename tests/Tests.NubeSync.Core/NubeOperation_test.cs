﻿using System;
using NubeSync.Core;
using Xunit;

namespace Tests.NubeSync.Core.NubeOperation_test
{
    public class Always
    {
        [Fact]
        public void Id_is_generated()
        {
            var operation = new NubeOperation();

            Assert.NotEmpty(operation.Id);
        }
    }
}