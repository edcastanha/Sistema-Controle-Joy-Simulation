using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace Controlador_Feel_For_Real
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public ObservableCollection<string> configcbItems = new ObservableCollection<string>();
        public ObservableCollection<string> shiftercbItems = new ObservableCollection<string>();
        public ObservableCollection<string> desktopeffectcbItems = new ObservableCollection<string>();

        public MainWindow()
        {
            InitializeComponent();
            configcbItems.Add("Default");
            configcbItems.Add("NewConfig");
            configcb.ItemsSource = configcbItems;
            configcb.SelectedIndex = 0;

            shiftercbItems.Add("Off");
            shiftercbItems.Add("Hshifter");
            shiftercbItems.Add("Sequential");
            shiftercb.ItemsSource = shiftercbItems;
            shiftercb.SelectedIndex = 0;

            desktopeffectcbItems.Add("Off");
            desktopeffectcbItems.Add("Spring");
            desktopeffectcbItems.Add("Damper");
            desktopeffectcbItems.Add("Friction");
            eftypecb.ItemsSource = desktopeffectcbItems;
            eftypecb.SelectedIndex = 0;

            configcb.IsEditable = false;
            saveconfigbutton.IsEnabled = false;
            deleteconfigbutton.IsEnabled = false;

            leftC1.Visibility = Visibility.Visible;
            leftC2.Visibility = Visibility.Collapsed;

            rightC1.Visibility = Visibility.Visible;
            rightC2.Visibility = Visibility.Collapsed;
        }

        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                Point position = Mouse.GetPosition(this);
                if (position.X <= 800 && position.Y <= 40)
                {
                    DragMove();
                }
            }
        }

        private void closebutton_Click(object sender, RoutedEventArgs e)
        {
            if (calibrator.IsEnabled)
            {
                MessageBox.Show("Close Shifter Calibrator First", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else
            {
                Close();
            }
        }

        private void minimizebutton_Click(object sender, RoutedEventArgs e)
        {
            WindowState = WindowState.Minimized;
        }

        private void Window_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.L && Keyboard.Modifiers == ModifierKeys.Control)
            {
                if(lednumbox.Visibility == Visibility.Collapsed)
                {
                    lednumbox.Visibility = Visibility.Visible;
                }
                else
                {
                    lednumbox.Visibility = Visibility.Collapsed;
                }
            }
        }

        private void Btnsetnumled_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Brakechkbox_Click(object sender, RoutedEventArgs e)
        {
            //use the message queue to send a message.
            var messageQueue = SnackbarThree.MessageQueue;
            var message = "";


            if (brakechkbox.IsChecked == true)
            {
                
                brakemin.IsEnabled = true;
                brakemax.IsEnabled = true;
                message = "Freio Ativado";
            }
            else
            {
                brakemin.IsEnabled = false;
                brakemax.IsEnabled = false;
                message = "Freio Desligado";
            }

            //the message queue can be called from any thread
            Task.Factory.StartNew(() => messageQueue.Enqueue(message));
        }
        private void Clutchchkbox_Click(object sender, RoutedEventArgs e)
        {
            //use the message queue to send a message.
            var messageQueue = SnackbarThree.MessageQueue;
            var message = "";


            if (clutchchkbox.IsChecked == true)
            {
                clutchmin.IsEnabled = true;
                clutchmax.IsEnabled = true;
                message = "Embreagem Ativada";
            }
            else
            {
                clutchmin.IsEnabled = false;
                clutchmax.IsEnabled = false;
                message = "Embreagem Desligada";
            }

            //the message queue can be called from any thread
            Task.Factory.StartNew(() => messageQueue.Enqueue(message));
        }

        private void Handbrakechkbox_Click(object sender, RoutedEventArgs e)
        {
            //use the message queue to send a message.
            var messageQueue = SnackbarThree.MessageQueue;
            var message = "";

            if (handbrakechkbox.IsChecked == true)
            {
                hanbrakemin.IsEnabled = true;
                handbrakemax.IsEnabled = true;
                message = "HandBrake Ativado";
            }
            else
            {
                hanbrakemin.IsEnabled = false;
                handbrakemax.IsEnabled = false;
                message = "HandBrake Desligado";
            }

            //the message queue can be called from any thread
            Task.Factory.StartNew(() => messageQueue.Enqueue(message));
        }

        private void Slider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            try
            {
                //Slider AngleSlider = (Slider)sender;
                //AngleSlider.GetBindingExpression(Slider.ValueProperty).UpdateSource();
                int val = Convert.ToInt32(e.NewValue);

                steeringangleval.Text = Convert.ToString(val) + "°";
            }
            catch
            {
                //nothin to do
            }
        }

        private void softlockslider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            try
            {
                float val = Convert.ToInt32(e.NewValue);
                var percent = (val / 100) * 100;
                soflockval.Text = Convert.ToString((int)percent) + " %";
            }
            catch
            {

            }
        }

        private void Minforceslider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            int val = Convert.ToInt32(e.NewValue);
            minforceval.Text = Convert.ToString(val) + " %";
        }

        private void Maxforceslider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            if (maxforceslider.Value > 15)
            {
                maxforceval.Text = Convert.ToString((int)maxforceslider.Value) + " %";
            }

        }

        private void Damperslider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            float val = Convert.ToInt32(e.NewValue);
            var percent = (val / 200) * 100;
            damperval.Text = Convert.ToString((int)percent) + " %";
        }

        private void Frictionslider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            float val = Convert.ToInt32(e.NewValue);
            var percent = (val / 200) * 100;
            frictionval.Text = Convert.ToString((int)percent) + " %";
        }

        private void Inertiaslider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            float val = Convert.ToInt32(e.NewValue);
            var percent = (val / 200) * 100;
            inertiaval.Text = Convert.ToString((int)percent) + " %";
        }

        private void configcb_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            switch (configcb.SelectedIndex)
            {
                case 0:
                    saveconfigbutton.IsEnabled = false;
                    deleteconfigbutton.IsEnabled = false;
                    configcb.IsEditable = false;
                    loadConfigButtons.IsEnabled = true;
                    break;
                case 1:
                    saveconfigbutton.IsEnabled = true;
                    deleteconfigbutton.IsEnabled = false;
                    configcb.IsEditable = true;
                    loadConfigButtons.IsEnabled = false;
                    break;
            }
            if (configcb.SelectedIndex > 1)
            {
                saveconfigbutton.IsEnabled = false;
                configcb.IsEditable = false;
                deleteconfigbutton.IsEnabled = true;
                loadConfigButtons.IsEnabled = true;
            }
            Console.WriteLine(configcbItems);
        }

        //scale Effect
        private void Constantgainslider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            int val = Convert.ToInt32(e.NewValue);
            constantgainval.Text = Convert.ToString(val) + " %";
        }

        private void Springgainslider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            int val = Convert.ToInt32(e.NewValue);
            springgainval.Text = Convert.ToString(val) + " %";
        }

        private void Sinegainslider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            int val = Convert.ToInt32(e.NewValue);
            sinegainval.Text = Convert.ToString(val) + " %";
        }


        //shifter
        private void Shiftercb_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            switch (shiftercb.SelectedIndex)
            {
                case 0:
                    shiftercb.Foreground = new SolidColorBrush(Color.FromArgb(204, 180, 0, 0));
                    break;
                case 1://Hshifter
                    shiftercb.Foreground = new SolidColorBrush(Colors.ForestGreen);
                    xminbtn.IsEnabled = true;
                    xmaxbtn.IsEnabled = true;
                    break;
                case 2://seqshifter
                    shiftercb.Foreground = new SolidColorBrush(Colors.ForestGreen);
                    xminbtn.IsEnabled = false;
                    xmaxbtn.IsEnabled = false;
                    break;

            }
        }

        private void Shiftercb_DropDownOpened(object sender, EventArgs e)
        {
            shiftercb.Foreground = new SolidColorBrush(Color.FromArgb(127, 255, 255, 255));
        }

        private void calibratebutton_Click(object sender, RoutedEventArgs e)
        {
            calibrator.IsEnabled = true;
            shiftercb.IsEnabled = false;
            knob.Visibility = Visibility.Visible;
        }

        private void ResetButton_Click(object sender, RoutedEventArgs e)
        {
            //Y Shifter
            yhigh.Value = 768;
            ylow.Value = 256;

            //X Shifter
            xhigh.Value = 768;
            xmid.Value = 512;
            xlow.Value = 256;

            //use the message queue to send a message.
            var messageQueue = SnackbarThree.MessageQueue;
            var message = "Shifter Reset";

            //the message queue can be called from any thread
            Task.Factory.StartNew(() => messageQueue.Enqueue(message));
        }

        private void Okbutton_Click(object sender, RoutedEventArgs e)
        {
            calibrator.IsEnabled = false;
            shiftercb.IsEnabled = true;
            knob.Visibility = Visibility.Collapsed;

        }

        private void Applybutton_Click(object sender, RoutedEventArgs e)
        {
            //use the message queue to send a message.
            var messageQueue = SnackbarThree.MessageQueue;
            var message = "Shifter Saved";

            //the message queue can be called from any thread
            Task.Factory.StartNew(() => messageQueue.Enqueue(message));
        }

        private void yhigh_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            int val = Convert.ToInt32(e.NewValue);
            yhighval.Text = val.ToString();
            lineYhigh.RenderTransform = new TranslateTransform(0, mapper(val, 0, 1024, 125, 0));
        }

        private void ylow_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            int val = Convert.ToInt32(e.NewValue);
            ylowval.Text = val.ToString();
            lineYlow.RenderTransform = new TranslateTransform(0, mapper(val, 0, 1024, 125, 0));
        }

        private void xmid_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            int val = Convert.ToInt32(e.NewValue);
            xmidval.Text = val.ToString();
            linexmid.RenderTransform = new TranslateTransform(mapper(val, 0, 1024, 0, 165), 0);
        }

        private void xhigh_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            int val = Convert.ToInt32(e.NewValue);
            xhighval.Text = val.ToString();
            linexhigh.RenderTransform = new TranslateTransform(mapper(val, 0, 1024, 0, 165), 0);
        }

        private void xlow_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            int val = Convert.ToInt32(e.NewValue);
            xlowval.Text = val.ToString();
            linexlow.RenderTransform = new TranslateTransform(mapper(val, 0, 1024, 0, 165), 0);
        }

        private int mapper(int val, int inMin, int inMax, int outMin, int outMax)
        {
            return (val - inMin) * (outMax - outMin) / (inMax - inMin) + outMin;
        }

        //Dekstop Effect
        private void Desktopslider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            int val = Convert.ToInt32(e.NewValue);
            desktopeffectval.Text = Convert.ToString(val) + " %";
        }

        private void Eftypecb_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            eftypecb.FontSize = 9;
            if (eftypecb.SelectedIndex == 0)
            {
                eftypecb.Foreground = new SolidColorBrush(Color.FromArgb(204, 180, 0, 0));
            }
            else
            {
                eftypecb.Foreground = new SolidColorBrush(Colors.ForestGreen);
            }
        }

        private void Eftypecb_dropdownOpened(object sender, EventArgs e)
        {
            eftypecb.Foreground = new SolidColorBrush(Color.FromArgb(127, 255, 255, 255));
            eftypecb.FontSize = 11;
        }

        private void onLoad(object sender, RoutedEventArgs e)
        {
            LoadSettings();

            if (Properties.Settings.Default.array.Count > 0)
            {
                string[] cbitemadd = new string[Properties.Settings.Default.array.Count];
                for (int i = 0; i < Properties.Settings.Default.array.Count; i++)
                {
                    switch (i)
                    {
                        case 0:
                            cbitemadd = Properties.Settings.Default.array[0].Split(' ');
                            configcbItems.Add(cbitemadd[0]);
                            break;
                        case 1:
                            cbitemadd = Properties.Settings.Default.array[1].Split(' ');
                            configcbItems.Add(cbitemadd[0]);
                            break;
                        case 2:
                            cbitemadd = Properties.Settings.Default.array[2].Split(' ');
                            configcbItems.Add(cbitemadd[0]);
                            break;
                        case 3:
                            cbitemadd = Properties.Settings.Default.array[3].Split(' ');
                            configcbItems.Add(cbitemadd[0]);
                            break;
                        case 4:
                            cbitemadd = Properties.Settings.Default.array[4].Split(' ');
                            configcbItems.Add(cbitemadd[0]);
                            break;
                        case 5:
                            cbitemadd = Properties.Settings.Default.array[5].Split(' ');
                            configcbItems.Add(cbitemadd[0]);
                            break;
                        case 6:
                            cbitemadd = Properties.Settings.Default.array[6].Split(' ');
                            configcbItems.Add(cbitemadd[0]);
                            break;
                        case 7:
                            cbitemadd = Properties.Settings.Default.array[7].Split(' ');
                            configcbItems.Add(cbitemadd[0]);
                            break;
                        case 8:
                            cbitemadd = Properties.Settings.Default.array[8].Split(' ');
                            configcbItems.Add(cbitemadd[0]);
                            break;
                        case 9:
                            cbitemadd = Properties.Settings.Default.array[9].Split(' ');
                            configcbItems.Add(cbitemadd[0]);
                            break;
                        case 10:
                            cbitemadd = Properties.Settings.Default.array[10].Split(' ');
                            configcbItems.Add(cbitemadd[0]);
                            break;
                        case 11:
                            cbitemadd = Properties.Settings.Default.array[11].Split(' ');
                            configcbItems.Add(cbitemadd[0]);
                            break;
                        case 12:
                            cbitemadd = Properties.Settings.Default.array[12].Split(' ');
                            configcbItems.Add(cbitemadd[0]);
                            break;
                        case 13:
                            cbitemadd = Properties.Settings.Default.array[10].Split(' ');
                            configcbItems.Add(cbitemadd[0]);
                            break;
                        case 14:
                            cbitemadd = Properties.Settings.Default.array[11].Split(' ');
                            configcbItems.Add(cbitemadd[0]);
                            break;
                        case 15:
                            cbitemadd = Properties.Settings.Default.array[12].Split(' ');
                            configcbItems.Add(cbitemadd[0]);
                            break;
                    }

                }
            }
        }

        private async void LoadSettings()
        {
            //////inputpage
            await Task.Delay(20);
            angleslider.Value = Properties.Settings.Default.sliderangle;
            //await Task.Delay(20);
            //cprval.Text = Properties.Settings.Default.cpr;
            await Task.Delay(20);
            clutchchkbox.IsChecked = Properties.Settings.Default.enableclutch;
            if ((bool)clutchchkbox.IsChecked == true)
            {
                clutchmax.IsEnabled = true;
                clutchmin.IsEnabled = true;
            }
            else
            {
                clutchmax.IsEnabled = false;
                clutchmin.IsEnabled = false;
            }
            await Task.Delay(20);
            handbrakechkbox.IsChecked = Properties.Settings.Default.enablehandbrake;
            if ((bool)handbrakechkbox.IsChecked == true)
            {
                hanbrakemin.IsEnabled = true;
                handbrakemax.IsEnabled = true;
            }
            else
            {
                hanbrakemin.IsEnabled = false;
                handbrakemax.IsEnabled = false;
            }

            ////outputpage
            await Task.Delay(20);
            softlockslider.Value = Properties.Settings.Default.softlock;
            await Task.Delay(20);
            constantslider.Value = Properties.Settings.Default.constant;
            await Task.Delay(20);
            springslider.Value = Properties.Settings.Default.spring;
            await Task.Delay(20);
            sineslider.Value = Properties.Settings.Default.sine;
            await Task.Delay(20);
            damperslider.Value = Properties.Settings.Default.damper;
            await Task.Delay(20);
            inertiaslider.Value = Properties.Settings.Default.inertia;
            await Task.Delay(20);
            frictionslider.Value = Properties.Settings.Default.friction;
            await Task.Delay(20);
            //triangleslider.Value = Properties.Settings.Default.triangle;
            //await Task.Delay(20);
            //squareslider.Value = Properties.Settings.Default.square;
            await Task.Delay(20);
            constantgainslider.Value = Properties.Settings.Default.gainconstan;
            await Task.Delay(20);
            springgainslider.Value = Properties.Settings.Default.gainspring;
            await Task.Delay(20);
            sinegainslider.Value = Properties.Settings.Default.gainsine;
            await Task.Delay(20);
            minforceslider.Value = Properties.Settings.Default.minforce;
            await Task.Delay(20);
            maxforceslider.Value = Properties.Settings.Default.maxforce;


            ///shifterpage

            await Task.Delay(20);
            shiftercb.SelectedIndex = Properties.Settings.Default.shifterindex;
            await Task.Delay(500);
            yhigh.Value = Properties.Settings.Default.yhigh;
            await Task.Delay(20);
            ylow.Value = Properties.Settings.Default.ylow;
            await Task.Delay(20);
            xhigh.Value = Properties.Settings.Default.xhigh;
            await Task.Delay(20);
            xmid.Value = Properties.Settings.Default.xmid;
            await Task.Delay(20);
            xlow.Value = Properties.Settings.Default.xlow;

            //desktop effect
            await Task.Delay(20);
            eftypecb.SelectedIndex = Properties.Settings.Default.desktopeftype;
            await Task.Delay(20);
            desktopslider.Value = Properties.Settings.Default.desktopforce;

        }

        private void onClose(object sender, EventArgs e)
        {

            //buttonTimer.Stop();
            SaveSettings();
        }

        private void SaveSettings()
        {
            //////inputpage
            Properties.Settings.Default.sliderangle = angleslider.Value;
            //Properties.Settings.Default.cpr = cprval.Text;
            Properties.Settings.Default.enableclutch = (bool)clutchchkbox.IsChecked;
            Properties.Settings.Default.enablehandbrake = (bool)handbrakechkbox.IsChecked;

            //////outputpage
            Properties.Settings.Default.softlock = softlockslider.Value;
            Properties.Settings.Default.constant = constantslider.Value;
            Properties.Settings.Default.spring = springslider.Value;
            Properties.Settings.Default.sine = sineslider.Value;
            Properties.Settings.Default.damper = damperslider.Value;
            Properties.Settings.Default.inertia = inertiaslider.Value;
            Properties.Settings.Default.friction = frictionslider.Value;
            //Properties.Settings.Default.triangle = triangleslider.Value;
            //Properties.Settings.Default.square = squareslider.Value;
            Properties.Settings.Default.gainconstan = constantgainslider.Value;
            Properties.Settings.Default.gainspring = springgainslider.Value;
            Properties.Settings.Default.gainsine = sinegainslider.Value;
            Properties.Settings.Default.minforce = minforceslider.Value;
            Properties.Settings.Default.maxforce = maxforceslider.Value;

            //////shifterpage
            Properties.Settings.Default.shifterindex = (byte)shiftercb.SelectedIndex;
            Properties.Settings.Default.yhigh = yhigh.Value;
            Properties.Settings.Default.ylow = ylow.Value;
            Properties.Settings.Default.xhigh = xhigh.Value;
            Properties.Settings.Default.xmid = xmid.Value;
            Properties.Settings.Default.xlow = xlow.Value;

            //desktop effect
            Properties.Settings.Default.desktopeftype = 0;
            Properties.Settings.Default.desktopforce = desktopslider.Value;

            Properties.Settings.Default.Save();
        }

        private void saveConfig(object sender, EventArgs args)
        {
            if (configcb.Text == "NewConfig")
            {
                configcb.Text = GetRandomString();
            }
            configcbItems.Add(configcb.Text.ToString());
            configcb.SelectedIndex = configcbItems.Count - 1;

            string[] conf = new string[24];
            conf[0] = configcbItems[configcbItems.Count - 1];
            conf[1] = angleslider.Value.ToString();

            conf[2] = softlockslider.Value.ToString();
            conf[3] = constantslider.Value.ToString();
            conf[4] = springslider.Value.ToString();
            conf[5] = sineslider.Value.ToString();
            conf[6] = damperslider.Value.ToString();
            conf[7] = inertiaslider.Value.ToString();
            conf[8] = frictionslider.Value.ToString();
            //conf[9] = triangleslider.Value.ToString();
            //conf[10] = squareslider.Value.ToString();

            conf[11] = shiftercb.SelectedIndex.ToString();
            conf[12] = yhigh.Value.ToString();
            conf[13] = ylow.Value.ToString();
            conf[14] = xhigh.Value.ToString();
            conf[15] = xmid.Value.ToString();
            conf[16] = xlow.Value.ToString();

            conf[17] = constantgainslider.Value.ToString();
            conf[18] = springgainslider.Value.ToString();
            conf[19] = sinegainslider.Value.ToString();

            conf[20] = minforceslider.Value.ToString();
            conf[21] = maxforceslider.Value.ToString();

            conf[22] = clutchchkbox.IsChecked.ToString();
            conf[23] = handbrakechkbox.IsChecked.ToString();
            string addconf = string.Join(" ", conf);

            if (Properties.Settings.Default.array.Count < 16)
            {
                Properties.Settings.Default.array.Add(addconf);
            }
            //use the message queue to send a message.
            var messageQueue = SnackbarThree.MessageQueue;
            var message = configcb.Text.ToString() + " Saved";

            //the message queue can be called from any thread
            Task.Factory.StartNew(() => messageQueue.Enqueue(message));
        }

        private async void loadConfig(object sender, EventArgs args)
        {
            if (configcb.SelectedIndex > 1)
            {
                string[] load = Properties.Settings.Default.array[configcb.SelectedIndex - 2].Split(' ');
                //inputpage
                angleslider.Value = Convert.ToDouble(load[1]);
                await Task.Delay(20);
                clutchchkbox.IsChecked = Convert.ToBoolean(load[22]);
                if ((bool)clutchchkbox.IsChecked == true)
                {
                    clutchmax.IsEnabled = true;
                    clutchmin.IsEnabled = true;
                }
                else
                {
                    clutchmax.IsEnabled = false;
                    clutchmin.IsEnabled = false;
                }

                await Task.Delay(20);
                handbrakechkbox.IsChecked = Convert.ToBoolean(load[23]);
                if ((bool)handbrakechkbox.IsChecked == true)
                {
                    hanbrakemin.IsEnabled = true;
                    handbrakemax.IsEnabled = true;
                }
                else
                {
                    hanbrakemin.IsEnabled = false;
                    handbrakemax.IsEnabled = false;
                }

                //outputpage
                await Task.Delay(20);
                softlockslider.Value = Convert.ToDouble(load[2]);
                await Task.Delay(20);
                constantslider.Value = Convert.ToDouble(load[3]);
                await Task.Delay(20);
                springslider.Value = Convert.ToDouble(load[4]);
                await Task.Delay(20);
                sineslider.Value = Convert.ToDouble(load[5]);
                await Task.Delay(20);
                damperslider.Value = Convert.ToDouble(load[6]);
                await Task.Delay(20);
                inertiaslider.Value = Convert.ToDouble(load[7]);
                await Task.Delay(20);
                frictionslider.Value = Convert.ToDouble(load[8]);
                //await Task.Delay(20);
                //triangleslider.Value = Convert.ToDouble(load[9]);
                //await Task.Delay(20);
                //squareslider.Value = Convert.ToDouble(load[10]);

                //shifterpage
                await Task.Delay(20);
                shiftercb.SelectedIndex = Convert.ToByte(load[11]);
                await Task.Delay(500);
                yhigh.Value = Convert.ToDouble(load[12]);
                await Task.Delay(20);
                ylow.Value = Convert.ToDouble(load[13]);
                await Task.Delay(20);
                xhigh.Value = Convert.ToDouble(load[14]);
                await Task.Delay(20);
                xmid.Value = Convert.ToDouble(load[15]);
                await Task.Delay(20);
                xlow.Value = Convert.ToDouble(load[16]);

                //outputpage gain
                await Task.Delay(20);
                constantgainslider.Value = Convert.ToDouble(load[17]);
                await Task.Delay(20);
                springgainslider.Value = Convert.ToDouble(load[18]);
                await Task.Delay(20);
                sinegainslider.Value = Convert.ToDouble(load[19]);

                await Task.Delay(20);
                minforceslider.Value = Convert.ToDouble(load[20]);
                await Task.Delay(20);
                maxforceslider.Value = Convert.ToDouble(load[21]);
            }

            else if (configcb.SelectedIndex == 0)//default
            {
                //inputpage
                angleslider.Value = 540;
                await Task.Delay(20);
                clutchchkbox.IsChecked = true;
                await Task.Delay(20);
                handbrakechkbox.IsChecked = true;

                //outputpage
                await Task.Delay(20);
                softlockslider.Value = 50;
                await Task.Delay(20);
                constantslider.Value = 100;
                await Task.Delay(20);
                springslider.Value = 100;
                await Task.Delay(20);
                sineslider.Value = 100;
                await Task.Delay(20);
                damperslider.Value = 20;
                await Task.Delay(20);
                inertiaslider.Value = 20;
                await Task.Delay(20);
                frictionslider.Value = 20;
                //await Task.Delay(20);
                //triangleslider.Value = 0;
                //await Task.Delay(20);
                //squareslider.Value = 0;
                await Task.Delay(20);
                constantgainslider.Value = 50;
                await Task.Delay(20);
                springgainslider.Value = 50;
                await Task.Delay(20);
                sinegainslider.Value = 50;

                await Task.Delay(20);
                minforceslider.Value = 5;
                await Task.Delay(20);
                maxforceslider.Value = 85;
                await Task.Delay(20);
                eftypecb.SelectedIndex = 0;
                await Task.Delay(20);
                desktopslider.Value = 0;

                //shifterpage
                await Task.Delay(20);
                shiftercb.SelectedIndex = 0;
                await Task.Delay(500);
                yhigh.Value = 768;
                await Task.Delay(20);
                ylow.Value = 256;
                await Task.Delay(20);
                xhigh.Value = 768;
                await Task.Delay(20);
                xmid.Value = 512;
                await Task.Delay(20);
                xlow.Value = 256;
            }
        }

        private void deleteConfig(object sender, EventArgs args)
        {
            //use the message queue to send a message.
            var messageQueue = SnackbarThree.MessageQueue;
            var message = configcb.Text.ToString() + "Exclusão Realizada";

            //the message queue can be called from any thread
            Task.Factory.StartNew(() => messageQueue.Enqueue(message));

            if (configcb.SelectedIndex < 1)
            {
                return;
            }
            Properties.Settings.Default.array.RemoveAt(configcb.SelectedIndex - 2);
            configcbItems.RemoveAt(configcb.SelectedIndex);
            configcb.SelectedIndex = 0;
        }

        public static string GetRandomString()
        {
            string path = System.IO.Path.GetRandomFileName();
            path = path.Replace(".", ""); // Remove period.
            return path;
        }

        private void LeftControlButton_Click(object sender, RoutedEventArgs e)
        {
            RotateTransform rotateTransform = leftControlButton.LayoutTransform as RotateTransform;

            if (rotateTransform == null)
            {
                rotateTransform = new RotateTransform(-180);
                leftControlButton.LayoutTransform = rotateTransform;
            }
            else
            {
                rotateTransform.Angle += 180;
            }

            if (leftC1.Visibility == Visibility.Visible)
            {
                leftC1.Visibility = Visibility.Collapsed;
                leftC2.Visibility = Visibility.Visible;
            }
            else
            {
                leftC1.Visibility = Visibility.Visible;
                leftC2.Visibility = Visibility.Collapsed;
            }
        }

        private void RightControlButton_Click(object sender, RoutedEventArgs e)
        {
            RotateTransform rotateTransform = rightControlButton.LayoutTransform as RotateTransform;

            if (rotateTransform == null)
            {
                rotateTransform = new RotateTransform(-180);
                rightControlButton.LayoutTransform = rotateTransform;
            }
            else
            {
                rotateTransform.Angle += 180;
            }

            if (rightC1.Visibility == Visibility.Visible)
            {
                rightC1.Visibility = Visibility.Collapsed;
                rightC2.Visibility = Visibility.Visible;
            }
            else
            {
                rightC1.Visibility = Visibility.Visible;
                rightC2.Visibility = Visibility.Collapsed;
            }
        }

        private void Web_RequestNavigate(object sender, System.Windows.Navigation.RequestNavigateEventArgs e)
        {
            Process.Start(new ProcessStartInfo(e.Uri.AbsoluteUri));
            e.Handled = true;
        }

        private void Fb_RequestNavigate(object sender, System.Windows.Navigation.RequestNavigateEventArgs e)
        {
            Process.Start(new ProcessStartInfo(e.Uri.AbsoluteUri));
            e.Handled = true;
        }

        private void Yt_RequestNavigate(object sender, System.Windows.Navigation.RequestNavigateEventArgs e)
        {
            Process.Start(new ProcessStartInfo(e.Uri.AbsoluteUri));
            e.Handled = true;
        }

        private void Contactus_RequestNavigate(object sender, System.Windows.Navigation.RequestNavigateEventArgs e)
        {
            Process.Start(new ProcessStartInfo(e.Uri.AbsoluteUri));
            e.Handled = true;
        }

        private void Webbtn_Click(object sender, RoutedEventArgs e)
        {
            System.Diagnostics.Process.Start("https://www.its3.com.br");
        }

        private void Fbbtn_Click(object sender, RoutedEventArgs e)
        {
            System.Diagnostics.Process.Start("https://www.facebook.com/ITS3.br/");
        }

        private void Igbtn_Click(object sender, RoutedEventArgs e)
        {
            System.Diagnostics.Process.Start("https://www.instagram.com/its3.br/");
        }

        private void Msgrbtn_Click(object sender, RoutedEventArgs e)
        {
            System.Diagnostics.Process.Start("https://m.me/1998829890172768?source=page_share_attachment");
        }

        
    }
}
