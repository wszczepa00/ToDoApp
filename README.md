# ToDoApp

Simple todo application using wpf technology with ORM, database supported in Entity Framework technology and communication with an external api for checking weather in given cities.

To Compile: 

You have to make class file APIKeysLocal.cs next to file ApiKeys.cs . The body of this class should look like this: 

```
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Calendar
{
    public static partial class ApiKeys
    {
        static ApiKeys()
        {
            ApiKey = "";
        }
    }
}      
```
To ApiKey variable you have to assign your own Api Key.
