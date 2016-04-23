using System;
using System.Collections.Generic;

namespace Cms.Core
{
    public interface IIoCWraper : IDisposable
    {
        object GetService(Type serviceType);
        IEnumerable<object> GetServices(Type serviceType);
        void Release(object service);
    }
}
