using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Windows;
using CalligraphyEditor.CalligraphyData;
using System.Data.Linq;

namespace CalligraphyEditor
{
    /// <summary>
    /// App.xaml 的交互逻辑
    /// </summary>
    public partial class App : Application
    {
        private static CalligraphyEntities _entities;

        public static  CalligraphyEntities Entities
        {
            get { return _entities; }
            set { _entities = value; }
        }        

        //protected override void OnStartup(StartupEventArgs e)
        //{
        //    base.OnStartup(e);            
        //}


        //static App()
        //{
        //    _entities = new CalligraphyEntities(new Uri("http://localhost/Calligraphy/WcfDataService.svc/"));            
            
        //}

        
    }
}
