using DevExpress.Mvvm.POCO;
using DevExpress.Xpf.Map;
using MapAnimateWpf.ViewModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Reflection;
using Newtonsoft.Json;
using System.Linq;
using DevExpress.Xpf.Map.Native;

namespace MapAnimateWpf
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public delegate void DPropertyChanged();
    public partial class MainWindow : Window,INotifyPropertyChanged
    {
        static void Add()
        {

        }
        public ObservableCollection<HjViewModel> HjListVM { get; set; } = new ObservableCollection<HjViewModel>();
      

        public event PropertyChangedEventHandler PropertyChanged=(sender,e)=> 
        {
            var name=e.PropertyName;
            var propertyChangeName = string.Concat(name, "PropertyChanged");
            var obj = sender.GetType();
            var methodInfo=obj.GetMethod(propertyChangeName);
            if (methodInfo!=null)
            {
                methodInfo.Invoke(sender, null);
            }
           
        };

        private HjViewModel selectedItem;
        public HjViewModel SelectedItem
        {
            get { return selectedItem; }
            set { selectedItem = value;
                PropertyChanged(this, new PropertyChangedEventArgs(nameof(SelectedItem)));
            }
        }
        
        public void SelectedItemPropertyChanged()
        {
            //var uuu=VisualTreeHelper.GetChild(this.vtt, 0) as Canvas;
            //MessageBox.Show($"选中:{selectedItem.Name}");

            var resultLayer=FindTargetControl<VectorLayerPanel>(this.vtt);
            var mapItemPresenters = resultLayer.Children;
            var vList = new List<Viewbox>();
            Viewbox selectViewBox = null;
            foreach (var item in mapItemPresenters)
            {
                
                var dObj = item as DependencyObject;
                
                if (dObj==null)
                {
                    continue;
                }
                var viewBox = FindTargetControl<Viewbox>(dObj);
                vList.Add(viewBox);
                var mapItemPrensenter= item as MapItemPresenter;
                var mapInfo=mapItemPrensenter.DataContext as MapItemInfo;
                var bindSource = mapInfo.Source;
                if(bindSource==selectedItem)
                {
                    selectViewBox = viewBox;
                    Console.WriteLine("giao!!!!");
                }
                
            }
            if (selectViewBox!=null)
            {
                TestAnimate.CreatRotateAnimal(TimeSpan.FromSeconds(0.4), selectViewBox).Begin();
            }
            
           // VisualTreeHelper.GetChild(bb.Template.Template,0);
        }

        public MainWindow()
        {
            InitializeComponent();
            InitData();
        }

        public void InitData()
        {
            var rd = new Random((int)DateTime.Now.Ticks);
            for (int i = 0; i < 10; i++)
            {
                var pname = string.Concat(Environment.MachineName, ":", i.ToString());
                var lat = rd.Next(-9000, 9000)/100d;
                var lon = rd.Next(-18000, 18000) / 100d;
                HjListVM.Add(new HjViewModel { Name = pname, Location = new GeoPoint(lat,lon) });
            }

        }

        private T FindTargetControl<T>(DependencyObject dependencyObject) where T:DependencyObject
        {
            if (dependencyObject==null)
            {
                throw new NullReferenceException("传入控件不能为空");
            }
            var index = 0;
            DependencyObject result = dependencyObject;
            do
            {
                result = VisualTreeHelper.GetChild(result, 0);
                index += 1;
            }
            while (result != null && (result as T) == null);
            return result as T;
        }

       
    }
}
