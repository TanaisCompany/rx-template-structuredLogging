using System;
using System.Collections.Generic;
using System.Linq;
using Sungero.Core;
using Sungero.CoreEntities;

namespace Tanais.ExampleMdl.Structures.Module
{

  /// <summary>
  /// Должность.
  /// </summary>
  [Public]
  partial class JobTitle
  {
    // GUID сущности.
    public string GUID { get; set; }
    
    // Наименование.
    public string Name { get; set; }
    
    // ИД подразделения.
    public long? DepartmentId { get; set; }
    
    // Состояние.
    public string Status { get; set; }
  }

}