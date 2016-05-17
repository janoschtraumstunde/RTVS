﻿// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License. See LICENSE in the project root for license information.

using System;
using Microsoft.Common.Core;
using Microsoft.R.Components.Controller;

namespace Microsoft.VisualStudio.R.Package.Commands {
    internal class CommandAsyncToOleMenuCommandShim : PackageCommand {
        private readonly ICommandAsync _command;

        public CommandAsyncToOleMenuCommandShim(Guid group, int id, ICommandAsync command)
            : base(group, id) {
            if (command == null) {
                throw new ArgumentNullException(nameof(command));
            }
            _command = command;
        }

        protected override void SetStatus() {
            var status = _command.Status;
            Supported = status.HasFlag(CommandStatus.Supported);
            Enabled = status.HasFlag(CommandStatus.Enabled);
            Visible = !status.HasFlag(CommandStatus.Invisible);
        }

        protected override void Handle(object inArg, out object outArg) {
            outArg = null;
            _command.InvokeAsync().DoNotWait();
        }
    }
}
