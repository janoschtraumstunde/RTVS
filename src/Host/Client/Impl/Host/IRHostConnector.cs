// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License. See LICENSE in the project root for license information.

using System.Threading;
using System.Threading.Tasks;

namespace Microsoft.R.Host.Client.Host {
    public interface IRHostConnector {
        Task<RHost> Connect(string name, IRCallbacks callbacks, string rCommandLineArguments = null, int timeout = 3000, CancellationToken cancellationToken = default(CancellationToken));
    }
}