using System;
using System.Collections.Generic;
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
using System.Globalization;
using BingMapsRESTToolkit;

namespace TrafficRecorder
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private string BingMapsKey = System.Configuration.ConfigurationManager.AppSettings.Get("BingMapsKey");

        RoutList rl;
        public MainWindow()
        {
            InitializeComponent();
        }

        private void miGetResponse_Click(object sender, RoutedEventArgs e)
        {
            var r = new RouteRequest()
            {
                RouteOptions = new RouteOptions()
                {
                    Avoid = new List<AvoidType>()
                    {
                        AvoidType.MinimizeTolls
                    },
                    TravelMode = TravelModeType.Driving,
                    DistanceUnits = DistanceUnitType.Miles,
                    Heading = 45,
                    RouteAttributes = new List<RouteAttributeType>()
                    {
                        RouteAttributeType.RoutePath
                    },
                    Optimize = RouteOptimizationType.TimeWithTraffic
                },
                Waypoints = new List<SimpleWaypoint>()
                {
                    new SimpleWaypoint(){
                        Address = "Microsoft+Corporate+West+Campus+Building+STUDIO+B%2C+15101+NE+40th+St%2C+Redmond%2C+WA+98052"
                    },
                    new SimpleWaypoint(){
                        Address = "47.659477,-122.123499",
                        IsViaPoint = true
                    },
                    new SimpleWaypoint(){
                        Address = "6544+194th+Pl+NE%2C+Redmond%2C+WA+98052"
                    }
                },

                BingMapsKey = BingMapsKey
            };

            ProcessRequestAsync(r);
        }

        private void miAddRoute_Click(object sender, RoutedEventArgs e)
        {
            Route Home = new Route()
            {
                Name = "Home"
            };

            Home.From.Address = "Microsoft Corporate West Campus Building STUDIO B 15101 NE 40th St, Redmond WA 98052";
            Home.Via.Address = "47.659477,-122.123499";
            Home.Via.IsViaPoint = true;
            Home.To.Address = "6544 194th PL NE, Redmond WA 98052";

            rl.Add(Home);
         

        }

        private async void ProcessRequestAsync(BaseRestRequest request)
        {
            try
            {
                Console.WriteLine(request.GetRequestUrl());

                var start = DateTime.Now;

                //Process the request by using the ServiceManager.
                var response = await ServiceManager.GetResponseAsync(request);

                var end = DateTime.Now;

                var processingTime = end - start;

                Console.WriteLine( string.Format(CultureInfo.InvariantCulture, "{0:0} ms", processingTime.TotalMilliseconds));

                
               // List<ObjectNode> nodes = new List<ObjectNode>();
                //nodes.Add(new ObjectNode("result", response));
               // ResultTreeView.ItemsSource = nodes;

                //ResponseTab.IsSelected = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void ListViewItem_Selected(object sender, RoutedEventArgs e)
        {

        }
    }
}
