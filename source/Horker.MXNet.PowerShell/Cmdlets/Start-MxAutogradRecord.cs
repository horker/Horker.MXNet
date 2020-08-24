using MxNet;
using NumpyLib;
using System;
using System.Collections.Generic;
using System.Management.Automation;
using System.Text;

namespace Horker.MxNet.PowerShell
{
    [Cmdlet("Start", "MxAutogradRecord")]
    [OutputType(typeof(Autograd._RecordingStateScope))]
    public class StartMxAutogradScope : PSCmdlet
    {
        [Parameter(Position = 0, Mandatory = false)]
        public SwitchParameter PassThru;

        public static Autograd._RecordingStateScope Scope { get; set; }

        protected override void BeginProcessing()
        {
            if (Scope != null)
            {
                try
                {
                    Scope.Dispose();
                }
                finally
                {
                    Scope = null;
                }
            }

            Scope = Autograd.Record();
            if (PassThru)
                WriteObject(Scope);
        }
    }

    [Cmdlet("Stop", "MxAutogradRecord")]
    [OutputType(typeof(void))]
    public class StopMxAutogradScope : PSCmdlet
    {
        [Parameter(Position = 0, Mandatory = false)]
        public Autograd._RecordingStateScope Scope { get; set; } = null;

        protected override void BeginProcessing()
        {
            var s = Scope ?? StartMxAutogradScope.Scope;

            if (s == null)
            {
                WriteError(new ErrorRecord(new ApplicationException("Autograd recording is not active"), null, ErrorCategory.InvalidOperation, null));
                return;
            }

            s.Dispose();
            StartMxAutogradScope.Scope = null;
        }
    }

    [Cmdlet("Get", "MxAutogradRecord")]
    [OutputType(typeof(Autograd._RecordingStateScope))]
    public class GetMxAutogradRecord : PSCmdlet
    {
        protected override void BeginProcessing()
        {
            WriteObject(StartMxAutogradScope.Scope);
        }
    }
}
