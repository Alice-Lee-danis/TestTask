using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace Test
{
    [ServiceContract]
    public interface ITestService
    {
        [OperationContract]
        List<EventsClient> Recording(int cursorX, int cursorY, string eventClick, DateTime dateTime);

        
    }
}
