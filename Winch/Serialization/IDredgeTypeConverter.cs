using System;
using System.Collections.Generic;

namespace Winch.Serialization;

public interface IDredgeTypeConverter
{
    public void PopulateFields(object obj, Dictionary<string, object> data);
}