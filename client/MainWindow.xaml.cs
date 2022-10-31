using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
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
using System.Windows.Forms;
using client.TestService;

namespace client
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        TestService.TestServiceClient testServiceClient = new TestService.TestServiceClient();
        public int cursorY = 0;
        public int cursorX = 0;
        public string mouseClick = "";
        public Thread secondThread;
        public bool isThread = false;
        List<EventsClient> eventsClients = new List<EventsClient>();

        private void cesondThread()
        {
            while (true)
            {
                var recording = testServiceClient.Recording(Convert.ToInt32(System.Windows.Forms.Cursor.Position.X),
                Convert.ToInt32(System.Windows.Forms.Cursor.Position.Y),
                Convert.ToString(mouseClick.ToString()), DateTime.Now);
                List<EventsClient> events = new List<EventsClient>();
                foreach (var record in recording)
                {
                    events.Add(new EventsClient() { date = record.date, mouseClick = record.mouseClick, mouseX = record.mouseX, mouseY = record.mouseY });
                }

                eventsClients = events;
                int i = 3;
            }
        }

        public MainWindow()
        {
            InitializeComponent();
        }

        private void cbSort_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            switch (cbSort.SelectedIndex)
            {
                case 0:
                    spisok.ItemsSource = eventsClients.OrderBy(x => x.mouseX);
                    break;
                case 1:
                    spisok.ItemsSource = eventsClients.OrderBy(y => y.mouseY);
                    break;
                case 2:
                    spisok.ItemsSource = eventsClients.OrderBy(y => y.date.Date);
                    break;
                case 3:
                    spisok.ItemsSource = eventsClients.OrderBy(y => y.date.Hour);
                    break;
            }
        }

        private void start_Click(object sender, RoutedEventArgs e)
        {
            secondThread = new Thread(cesondThread);
            secondThread.Start();
            isThread = true;
        }

        private void end_Click(object sender, RoutedEventArgs e)
        {
            secondThread.Abort();
            isThread = false;
        }

        private void Window_Closed(object sender, EventArgs e)
        {
            if (isThread)
            {
                secondThread.Abort();
            }
        }

        private void update_Click(object sender, RoutedEventArgs e)
        {
            spisok.ItemsSource = eventsClients;
        }
    }
}
