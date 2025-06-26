using System;
using System.Collections.Generic;
using System.Linq;
using Sungero.Core;
using Sungero.CoreEntities;
using Tanais.ExampleMdl.ExampleAssignment;

namespace Tanais.ExampleMdl
{
  partial class ExampleAssignmentSharedHandlers
  {

    public virtual void AddendaGroupAdded(Sungero.Workflow.Interfaces.AttachmentAddedEventArgs e)
    {
      if (e.Attachment != null && ExampleTasks.Is(_obj.MainTask))
        Functions.ExampleTask.Remote.GrantAccessRightsToDocument(ExampleTasks.As(_obj.MainTask), Sungero.Docflow.OfficialDocuments.As(e.Attachment));
    }
  }

}