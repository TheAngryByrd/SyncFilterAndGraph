using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using System.Windows.Controls.DataVisualization.Charting;
using Microsoft.LightSwitch.Framework.Client;
using Microsoft.LightSwitch.Presentation.Implementation;

namespace LightSwitchApplication.CustomControls
{
    public partial class LabGraph : UserControl
    {
        private List<string> dependentPathList = new List<string>() {"EyeLashes","LuckyNumber","BloodCellCount" };
        private string independentPath = "DateIssued";

        public LabGraph()
        {
            InitializeComponent();
            this.Checklist.ItemsSource = dependentPathList;      
            this.Loaded += new RoutedEventHandler(LabGraph_Loaded);      
     
        }

        /// <summary>
        /// The DataContext
        /// </summary>
        private VisualCollection<Lab> Labs
        {
            get
            {
                return (VisualCollection<Lab>)this.DataContext;
            }
        }

        /// <summary>
        /// Whenever the user clicks a checkbox, redraw
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void CheckBoxZone_Checked(object sender, RoutedEventArgs e)
        {
            Redraw();
        }

        /// <summary>
        /// When the graph is loaded, redraw
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void LabGraph_Loaded(object sender, RoutedEventArgs e)
        {
            Redraw();
        }

        void Redraw()
        {
          
            
            this.LabChart.Series.Clear();
            IEnumerable<CheckBox> children = this.Checklist.FindVisualChildren<CheckBox>();;

            foreach (var item in children)
            {
                if (item.IsChecked.Value)
                {
                    //Create a new line series
                    var lineSeries = new LineSeries();
                    string contentPath = item.Content.ToString();
                    lineSeries.Title = contentPath;
                    lineSeries.Name = contentPath;
                    lineSeries.IndependentValuePath = independentPath;
                    lineSeries.DependentValuePath = contentPath;                 

                    //Only bind labs that aren't null, otherwise the graph shows zero for that day
                    var itemList = new List<Lab>();
                    itemList.AddRange(Labs.Where(lab => lab.GetPropertyValue(contentPath) != null));   
                    //lineSeries.ItemsSource = itemList;
                    lineSeries.ItemsSource = Labs;
                    
                    this.LabChart.Series.Add(lineSeries);
                }
         
            }
        }


    
    }
}
