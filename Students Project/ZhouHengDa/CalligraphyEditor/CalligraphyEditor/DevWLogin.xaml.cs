using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using CalligraphyEditor.CalligraphyData;
using System.Xml.Linq;
using System.Reflection;
using CalligraphyEditor.Properties;

namespace CalligraphyEditor
{
    /// <summary>
    /// DevWLogin.xaml 的交互逻辑
    /// </summary>
    public partial class DevWLogin : Window
    {
        public DevWLogin()
        {
            InitializeComponent();
        }

        private void Window_Initialized(object sender, EventArgs e)
        {
            try
            {
                CalligraphyEditor.App.Entities = new CalligraphyEntities(new Uri((string)Settings.Default["CalligraphyDataServiceUri"]));
                CalligraphyEditor.App.Entities.WritingEntity += new EventHandler<System.Data.Services.Client.ReadingWritingEntityEventArgs>(Entities_WritingEntity);
                
                MainWindow mw = new MainWindow();
                mw.Show();
                this.Close();
            }
            catch (Exception ep)
            {
                MessageBox.Show(ep.Message, "登录失败", MessageBoxButton.OK, MessageBoxImage.Error, MessageBoxResult.Cancel);
                this.Close();
            }
        }

        void Entities_WritingEntity(object sender, System.Data.Services.Client.ReadingWritingEntityEventArgs e)
        {
            // e.Data gives you the XElement for the Serialization of the Entity 
            //Using XLinq  , you can  add/Remove properties to the element Payload  
            XName xnEntityProperties = XName.Get("properties", e.Data.GetNamespaceOfPrefix("m").NamespaceName);
            XElement xePayload = null;
            foreach (PropertyInfo property in e.Entity.GetType().GetProperties())
            {
                object[] doNotSerializeAttributes = property.GetCustomAttributes(typeof(DoNotSerializeAttribute), false);
                if (doNotSerializeAttributes.Length > 0)
                {
                    if (xePayload == null)
                    {
                        xePayload = e.Data.Descendants().Where<XElement>(xe => xe.Name == xnEntityProperties).First<XElement>();
                    }
                    //The XName of the property we are going to remove from the payload
                    XName xnProperty = XName.Get(property.Name, e.Data.GetNamespaceOfPrefix("d").NamespaceName);
                    //Get the Property of the entity  you don't want sent to the server
                    XElement xeRemoveThisProperty = xePayload.Descendants().Where<XElement>(xe => xe.Name == xnProperty).First<XElement>();
                    //Remove this property from the Payload sent to the server 
                    xeRemoveThisProperty.Remove();
                }
                else
                {
                    System.Diagnostics.Debug.Write("Entities_WritingEntity");
                }
            }
        }
    }
}
