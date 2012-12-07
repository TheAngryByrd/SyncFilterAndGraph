using System;
using System.Linq;
using System.IO;
using System.IO.IsolatedStorage;
using System.Collections.Generic;
using FilterControl;
using LightSwitchApplication.CustomControls;
using Microsoft.LightSwitch;
using Microsoft.LightSwitch.Framework.Client;
using Microsoft.LightSwitch.Presentation;
using Microsoft.LightSwitch.Presentation.Extensions;
using Microsoft.LightSwitch.Threading;

namespace LightSwitchApplication
{
    public partial class PersonDetail
    {
        LabGraph _labGraph { get; set; }
        partial void Person_Loaded(bool succeeded)
        {

            // Write your code here.
            this.SetDisplayNameFromEntity(this.Person);
        }

        void labGraph_ControlAvailable(object sender, ControlAvailableEventArgs e)
        {
            _labGraph = e.Control as LabGraph;
        }

        partial void Person_Changed()
        {
            // Write your code here.
            this.SetDisplayNameFromEntity(this.Person);
        }

        partial void PersonDetail_Saved()
        {
            // Write your code here.
            this.SetDisplayNameFromEntity(this.Person);
        }

        partial void FilterQuery_Changed(System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            if (_labGraph != null)
            {
                Dispatchers.Main.BeginInvoke(() =>
                {
                   _labGraph.Redraw();
                });
             
            }
        }

        partial void PersonDetail_Activated()
        {
            // Write your code here.
            var labGraph = this.FindControl("LabGraph");
            labGraph.ControlAvailable += new EventHandler<ControlAvailableEventArgs>(labGraph_ControlAvailable);
            
        }
    }
}