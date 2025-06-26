using System;
using System.Collections.Generic;
using System.Linq;
using Sungero.Core;
using Sungero.CoreEntities;
using Tanais.ExampleMdl.ExampleAssignment;

namespace Tanais.ExampleMdl.Client
{
  partial class ExampleAssignmentActions
  {
    public virtual void Complete(Sungero.Workflow.Client.ExecuteResultActionArgs e)
    {
      
    }

    public virtual bool CanComplete(Sungero.Workflow.Client.CanExecuteResultActionArgs e)
    {
      return true;
    }

  }

}