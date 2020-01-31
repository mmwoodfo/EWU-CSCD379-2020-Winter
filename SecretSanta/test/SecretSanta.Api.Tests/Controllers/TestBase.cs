using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;

namespace SecretSanta.Api.Tests.Controllers
{
    public abstract class TestBase<TEntity>
    {
       protected abstract TEntity CreateInstance();
    }
}
