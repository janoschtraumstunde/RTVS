﻿using System.Runtime.InteropServices;
using Microsoft.VisualStudio.Imaging;
using Microsoft.VisualStudio.Shell;

namespace Microsoft.VisualStudio.R.Package.DataInspect {
    [Guid("3F6855E6-E2DB-46F2-9820-EDC794FE8AFE")]
    public class VariableGridWindowPane : ToolWindowPane {
        private VariableGridHost _gridHost;

        public VariableGridWindowPane() {
            Caption = Resources.VariableGrid_Caption;
            Content = _gridHost = new VariableGridHost();

            BitmapImageMoniker = KnownMonikers.VariableProperty;
        }

        internal void SetEvaluation(EvaluationWrapper evaluation) {
            if (!string.IsNullOrWhiteSpace(evaluation.Expression)) {
                Caption = string.Format("{0}: {1}", Resources.VariableGrid_Caption, evaluation.Expression);
            }

            _gridHost.SetEvaluation(evaluation);
        }
    }
}
