using System;
using System.Collections.Generic;
using System.Linq;
using Sungero.Core;
using Sungero.CoreEntities;
using Tanais.ExampleMdl.ExampleTask;

namespace Tanais.ExampleMdl
{
  partial class ExampleTaskSharedHandlers
  {

    public virtual void AddendaGroupAdded(Sungero.Workflow.Interfaces.AttachmentAddedEventArgs e)
    {
      if (e.Attachment != null)
        Functions.ExampleTask.Remote.GrantAccessRightsToDocument(_obj, Sungero.Docflow.OfficialDocuments.As(e.Attachment));
    }

    public virtual void DocumentsGroupAdded(Sungero.Workflow.Interfaces.AttachmentAddedEventArgs e)
    {
      if (e.Attachment != null)
        Functions.ExampleTask.Remote.GrantAccessRightsToDocument(_obj, Sungero.Docflow.OfficialDocuments.As(e.Attachment));
    }

  }
}