﻿// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License. See LICENSE in the project root for license information.

using System;
using Microsoft.Common.Core;
using Microsoft.R.Components.InteractiveWorkflow;
using Microsoft.VisualStudio.R.Package.Commands;
using Microsoft.VisualStudio.R.Package.Utilities;
using Microsoft.VisualStudio.R.Packages.R;
using Microsoft.VisualStudio.Shell.Interop;

namespace Microsoft.VisualStudio.R.Package.Repl.Workspace {
    internal sealed class ShowRInteractiveWindowsCommand : PackageCommand {
        private readonly IRInteractiveWorkflowProvider _interactiveWorkflowProvider;
        private readonly IInteractiveWindowComponentContainerFactory _componentContainerFactory;

        public ShowRInteractiveWindowsCommand(IRInteractiveWorkflowProvider interactiveWorkflowProvider, IInteractiveWindowComponentContainerFactory componentContainerFactory) :
            base(RGuidList.RCmdSetGuid, RPackageCommandId.icmdShowReplWindow) {
            _interactiveWorkflowProvider = interactiveWorkflowProvider;
            _componentContainerFactory = componentContainerFactory;
        }

        protected override void Handle() {
            var interactiveWorkflow = _interactiveWorkflowProvider.GetOrCreate();
            var window = interactiveWorkflow.ActiveWindow;
            if (window != null) {
                window.Container.Show(true);
                 return;
            }

            interactiveWorkflow
                .GetOrCreateVisualComponent(_componentContainerFactory)
                .ContinueOnRanToCompletion(w => w.Container.Show(true));

            var frame = ToolWindowUtilities.FindToolWindow(RGuidList.ReplInteractiveWindowProviderGuid);

            object value;
            frame.GetProperty((int)__VSFPROPID.VSFPROPID_CreateToolWinFlags, out value);
            frame.SetProperty((int)__VSFPROPID.VSFPROPID_CreateToolWinFlags, (int)value | (int)__VSCREATETOOLWIN.CTW_fForceCreate);
        }
    }
}
