﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Management.Automation;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Horker.Numerics.DataMaps;

namespace Horker.Numerics.PowerShell
{
    [Cmdlet("New", "DataMap")]
    [OutputType(typeof(DataMap))]
    public class NewDataMap : PSCmdlet
    {
        [Parameter(Position = 0, Mandatory = false)]
        public IDictionary FromDictionary;

        protected override void BeginProcessing()
        {
            DataMap map = null;

            if (FromDictionary != null)
                map = DataMap.FromDictionary(FromDictionary);
            else
                map = new DataMap();

            WriteObject(map);
        }
    }
}
