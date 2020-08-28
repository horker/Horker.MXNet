using MxNet;
using MxNet.Gluon;
using MxNet.IO;
using MxNet.Metrics;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Management.Automation;
using System.Security.Cryptography.X509Certificates;
using System.Text;

namespace Horker.MxNet.PowerShell
{
    public class TrainingStatus
    {
        public int Epoch { get; private set; }
        public float TrainingLoss { get; private set; }
        public float ValidationLoss { get; private set; }
        public string MetricName { get; private set; }
        public float MetricValue { get; private set; }
        public TimeSpan TimeElapsed { get; private set; }

        public TrainingStatus(int epoch, float trainingLoss, float validationLoss, string metricName, float metricValue, TimeSpan timeElapsed)
        {
            Epoch = epoch;
            TrainingLoss = trainingLoss;
            ValidationLoss = validationLoss;
            MetricName = metricName;
            MetricValue = metricValue;
            TimeElapsed = timeElapsed;
        }
    }

    [Cmdlet("Start", "MxTraining")]
    public class StartMxTraining : PSCmdlet
    {
        [Parameter(Position = 0, Mandatory = true)]
        public Trainer Trainer { get; set; }

        [Parameter(Position = 1, Mandatory = true)]
        public Block Model { get; set; }

        [Parameter(Position = 2, Mandatory = true)]
        public DataIter TrainingData { get; set; }

        [Parameter(Position = 3, Mandatory = true)]
        public DataIter ValidationData { get; set; }

        [Parameter(Position = 4, Mandatory = true)]
        public Block LossFunction { get; set; }

        [Parameter(Position = 5, Mandatory = false)]
        public EvalMetric MetricFunction { get; set; }

        [Parameter(Position = 6, Mandatory = true)]
        public int MaxEpoch { get; set; }

        [Parameter(Position = 7, Mandatory = false)]
        public Context Context { get; set; } = null;

        [Parameter(Position = 8, Mandatory = false)]
        public int DisplayStep = 100;

        [Parameter(Position = 9, Mandatory = false)]
        public int DisplayDigits = 5;

        protected override void BeginProcessing()
        {
            if (Context == null)
                Context = Context.CurrentContext;

            var stopWatch = new Stopwatch();
            stopWatch.Start();

            for (var epoch = 1; epoch <= MaxEpoch; ++epoch)
            {
                TrainingData.Reset();
                var totalLoss = 0.0f;
                var dataSize = 0;

                while (!TrainingData.End())
                {
                    var batch = TrainingData.Next();
                    var data = batch.Data[0].AsInContext(Context);
                    var label = batch.Label[0].AsInContext(Context);

                    using (Autograd.Record())
                    {
                        var output = Model.Call(data);
                        var loss = (NDArray)LossFunction.Call(output, label);
                        loss.Backward();
                        totalLoss += loss.Sum();
                        dataSize += data.Shape[0];

                        Trainer.Step(batch.Data[0].Shape[0]);
                    }
                }

                if (epoch % DisplayStep == 0 || epoch == MaxEpoch)
                {
                    totalLoss /= dataSize;

                    ValidationData.Reset();
                    var totalValidLoss = 0.0f;
                    var validDataSize = 0;

                    if (MetricFunction != null)
                        MetricFunction.Reset();

                    while (!ValidationData.End())
                    {
                        var batch = ValidationData.Next();
                        var data = batch.Data[0].AsInContext(Context);
                        var label = batch.Label[0].AsInContext(Context);

                        var output = Model.Call(data);
                        var validLoss = (NDArray)LossFunction.Call(output, label);
                        totalValidLoss += validLoss.Sum();
                        validDataSize += data.Shape[0];

                        if (MetricFunction != null)
                            MetricFunction.Update(label, output);
                    }
                    totalValidLoss /= validDataSize;

                    string metricName = null;
                    float metric = float.NaN;

                    if (MetricFunction != null)
                        (metricName, metric) = MetricFunction.Get();

                    var status = new TrainingStatus(epoch,
                        (float)Math.Round(totalLoss, DisplayDigits),
                        (float)Math.Round(totalValidLoss, DisplayDigits),
                        metricName,
                        (float)Math.Round(metric, DisplayDigits),
                        stopWatch.Elapsed);

                    WriteObject(status);
                }
            }
        }
    }
}
