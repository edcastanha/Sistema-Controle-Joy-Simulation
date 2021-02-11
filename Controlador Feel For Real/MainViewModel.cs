using Controlador_Feel_For_Real.MVVM;
using EMCHid;
using System;
using System.Threading.Tasks;
using System.Reflection;
using System.Linq;
using System.IO;
using Portable.Licensing;
using Portable.Licensing.Validation;

namespace Controlador_Feel_For_Real
{
    class MainViewModel : ViewModel
    {
        //licening
        private License ulicense;
        private string publicKey;
        public string hardwareId { get; set; }
        private string licenseHID;
        public string licenseVisibility { get; set; }
        public string licensetxt { get; set; }

        ///Themes and Led Control
        ///
        public string themesColor { get; set; }
        private bool setFireBrick { get; set; }
        private bool setForestGreen { get; set; }
        private bool setDarkBlue { get; set; }
        private bool setPurple { get; set; }
        private bool setGoldenRod { get; set; }
        private bool setDarkCyan { get; set; }
        private bool setDeepPink { get; set; }

        ///
        /// firmware software
        /// 
        //DispatcherTimer fwinittimer;
        public string swver { get; set; }
        public string fwver { get; set; }
        private bool firmwareCheck { get; set; }
        private bool firmwarepass { get; set; }

        /// <summary>
        /// HIDutility
        /// </summary>
        private const byte ReportID = 238;
        public HidUtility HidUtil;
        public bool WaitingForDevice { get; private set; }
        /// <summary>
        /// Uicontrol
        /// </summary>
        public string mainWindow { get; set; }
        public string deviceon { get; set; }
        public string deviceoff { get; set; }

        /// <summary>
        /// Input Page
        /// </summary>
        private UInt16 angle { get; set; }
        public int RotateAngle { get; set; }
        public string RotateAngletxt { get; set; }
        private UInt16 Cpr { get; set; }
        private bool findCPR { get; set; }
        public string findCPRVis { get; set; }
        public string findCPRValue { get; set; }
        private bool readCPRValue;
        private bool closereadCPRValue;

        private byte desktopEffectForce { get; set; }
        private bool SetDesktopEffectForce { get; set; }
        private string desktopEffectType { get; set; }
        private bool SetdesktopEffectOff { get; set; }
        private bool SetdesktopEffectSpring { get; set; }
        private bool SetdesktopEffectDamper { get; set; }
        private bool SetdesktopEffectFriction { get; set; }

        //EMC DEVICE ADC input
        public int StirRight { get; set; }
        public int StirLeft { get; set; }
        public int Throttle { get; set; }
        public int Brake { get; set; }
        public int Clutch { get; set; }
        public int Handbrake { get; set; }

        //EMC DEVICE Feature Report Command
        private bool SetAngle;
        private bool SetCenter { get; set; }
        private bool SetFalseCenter { get; set; }
        private bool SetCpr { get; set; }
        private bool SetClearEeprom { get; set; }
        private bool SetGasmin { get; set; }
        private bool SetGasmax { get; set; }
        private bool SetBrakemin { get; set; }
        private bool SetBrakemax { get; set; }
        private bool SetClutchmin { get; set; }
        private bool SetClutchmax { get; set; }
        private bool SetHandbrakemin { get; set; }
        private bool SetHandbrakemax { get; set; }
        private bool disableHandbrake { get; set; }
        private bool enableHandbrake { get; set; }
        private bool disableClutch { get; set; }
        private bool enableClutch { get; set; }

        /// <summary>
        /// Output page only
        /// </summary>

        private string pwmfreqstring { get; set; }
        private byte pwmfreq { get; set; }
        private bool Setpwmfreq { get; set; }
        private byte _softlock { get; set; }
        private byte _constant { get; set; }
        private byte _spring { get; set; }
        private byte _sine { get; set; }
        private byte _damper { get; set; }
        private byte _friction { get; set; }
        private byte _inertia { get; set; }
        private byte gainSquare { get; set; }
        private byte gainConstant { get; set; }
        private byte gainSpring { get; set; }
        private byte gainSine { get; set; }
        private byte minforce { get; set; }
        private byte maxforce { get; set; }

        // output page feature report
        private bool SetSoftlock { get; set; }
        public string Constanttxt { get; set; }
        private bool SetConstant { get; set; }
        public string Springtxt { get; set; }
        private bool SetSpring { get; set; }
        public string Sinetxt { get; set; }
        private bool SetSine { get; set; }
        private bool SetDamper { get; set; }
        private bool SetFriction { get; set; }
        private bool SetInertia { get; set; }
        private bool SetSquare { get; set; }

        private bool SetgainConstant { get; set; }
        private bool SetgainSpring { get; set; }
        private bool SetgainSine { get; set; }

        private bool Setminforce { get; set; }
        private bool Setmaxforce { get; set; }

        /// <summary>
        /// Shifter Page Only
        /// </summary>

        public string GearText { get; set; }
        public string gearColor { get; set; }
        private bool xyRawRequest { get; set; }
        private bool closexyRawRequest { get; set; }
        public string rawX { get; set; }
        public string rawY { get; set; }
        public int knobX { get; set; }
        public int knobY { get; set; }
        public string calibratebutton { get; set; }
        public string xhigh { get; set; }
        public string linexhigh { get; set; }
        public string xmid { get; set; }
        public string linexmid { get; set; }
        public string xlow { get; set; }
        public string linexlow { get; set; }

        //shifter page feature report
        private string shiftermode { get; set; }
        private bool enableshifter { get; set; }
        private bool disableshifter { get; set; }
        private bool enableSeqshifter { get; set; }
        private bool enableHshifter { get; set; }
        private bool saveShifter { get; set; }
        private bool setYmax { get; set; }
        private bool setYmin { get; set; }
        private bool setXmax { get; set; }
        private bool setXmin { get; set; }
        private bool resetShifter { get; set; }

        private UInt16 xLow { get; set; }
        private bool setxLow { get; set; }

        private UInt16 xMid { get; set; }
        private bool setxMid { get; set; }

        private UInt16 xHigh { get; set; }
        private bool setxHigh { get; set; }

        private UInt16 yLow { get; set; }
        private bool setyLow { get; set; }

        private UInt16 yHigh { get; set; }
        private bool setyHigh { get; set; }

        public MainViewModel()
        {
            //license
            publicKey = "MIIBKjCB4wYHKoZIzj0CATCB1wIBATAsBgcqhkjOPQEBAiEA/////wAAAAEAAAAAAAAAAAAAAAD///////////////8wWwQg/////wAAAAEAAAAAAAAAAAAAAAD///////////////wEIFrGNdiqOpPns+u9VXaYhrxlHQawzFOw9jvOPD4n0mBLAxUAxJ02CIbnBJNqZnjhE50mt4GffpAEIQNrF9Hy4SxCR/i85uVjpEDydwN9gS3rM6D0oTlF2JjClgIhAP////8AAAAA//////////+85vqtpxeehPO5ysL8YyVRAgEBA0IABML91xTAnVFTLJZSieFij8/U7luzS9G0UjnEipEBSSLCqrtwPCRkuh0SMwQEpdyGJCrgx8S1N2jW3rjvLxa4ElM=";
            hardwareId = mobo.GenerateUID();
            licenseVisibility = "Visible";
            checkLicense();

            //Default Themes
            themesColor = "Firebrick";

            //HID
            HidUtil = new HidUtility();
            //HidUtil.SelectDevice(new Device(0x2341, 0x8036));//arduino leonardo
            HidUtil.SelectDevice(new Device(0x8364, 0x1001));//ITS

            ///firmware
            //swver = "VER : 0.934";
            swver = "VER : 2.0";
            fwver = "EMC Firmware : Checking...";


            //UI
            mainWindow = "false";
            deviceon = "Collapsed";
            deviceoff = "Collapsed";

            // Input Page
            angle = 0;
            StirLeft = 0;
            StirRight = 0;
            Throttle = 0;
            Brake = 0;
            Clutch = 0;
            Handbrake = 0;

            SetAngle = false;
            SetFalseCenter = false;
            SetGasmin = false;
            SetGasmax = false;
            SetBrakemin = false;
            SetBrakemax = false;
            SetClutchmin = false;
            SetClutchmax = false;
            SetHandbrakemin = false;
            SetHandbrakemax = false;

            // output page
            Constanttxt = "Default";
            Springtxt = "Default";
            Sinetxt = "Default";
            SetSoftlock = false;
            SetConstant = false;
            SetSpring = false;
            SetSine = false;
            SetDamper = false;
            SetFriction = false;
            SetInertia = false;
            SetSquare = false;
            SetgainConstant = false;
            SetgainSpring = false;
            SetgainSine = false;
            Setpwmfreq = false;
            Setminforce = false;
            Setmaxforce = false;

            //shifterpage

            GearText = "N";
            gearColor = "#FF00B400";
            calibratebutton = "false";
            xhigh = "true";
            linexhigh = "Visible";
            xmid = "true";
            linexmid = "Visible";
            xlow = "true";
            linexlow = "Visible";

            enableshifter = false;
            disableshifter = false;
            enableSeqshifter = false;
            enableHshifter = false;
            xyRawRequest = false;
            resetShifter = false;
            rawX = "X :";
            rawY = "Y :";
            knobX = 0;
            knobY = 0;

            HidUtil.RaiseConnectionStatusChangedEvent += ConnectionStatusChangedHandler;
            HidUtil.RaiseSendPacketEvent += SendPacketHandler;
            HidUtil.RaisePacketSentEvent += PacketSentHandler;
            HidUtil.RaiseReceivePacketEvent += ReceivePacketHandler;
            HidUtil.RaisePacketReceivedEvent += PacketReceivedHandler;

            //init UI
            if (HidUtil.ConnectionStatus == HidUtility.UsbConnectionStatus.Connected)
            {

                mainWindow = "true";
                deviceon = "Visible";
                deviceoff = "Collapsed";

                /*
                firmwareCheck = true;
                //await Task.Delay(1000);
                Thread.Sleep(1000);

                if (fwver.Contains("Wrong Version!!"))
                {
                    //Console.WriteLine("firmware is not correct");
                    //firmwarepass = true;
                    //await Task.Delay(1000);
                    HidUtil.CloseDevice();
                    mainWindow = "false";
                    deviceon = "Collapsed";
                    deviceoff = "Visible";
                    //dfuVisibility = "Visible";
                    //fwinittimer.Stop();
                }
                else //(fwver == "EMC Firmware : 0.94")
                {
                    //Console.WriteLine("firmware is correct");
                    mainWindow = "true";
                    deviceon = "Visible";
                    deviceoff = "Collapsed";
                    //dfuVisibility = "Collapsed";
                    firmwarepass = true;
                    //fwinittimer.Stop();
                }*/

            }
            else
            {
                //Console.WriteLine("firmware is unknown");
                //fwinittimer.Stop();
                mainWindow = "false";
                deviceon = "Collapsed";
                deviceoff = "Visible";
                //dfuVisibility = "Visible";
                if (!fwver.Contains("Alerta de  Versão!"))
                {
                    fwver = "Verificação de Firmware : ITS3";
                }

            }
            NotifyPropertyChanged("mainWindow");
            NotifyPropertyChanged("deviceon");
            NotifyPropertyChanged("deviceoff");
            NotifyPropertyChanged("fwver");

            themesCommand = new Command(setThemesParam, true);
            SetCenterCommand = new Command(() => { SetCenter = true; });
            pedalCommand = new Command(DoPedalParameterisedCommand, true);
            CleareepromCommand = new Command(() => { SetClearEeprom = true; });
            cprfinderCommand = new Command(cprfinderCommandParam, true);
            shifterCalibratorCommand = new Command(setShifterCalibratorParam, true);

        }

        public void ConnectionStatusChangedHandler(object sender, HidUtility.ConnectionStatusEventArgs e)
        {
            //Console.WriteLine("ConnectionStatusChanged {0}", e.ConnectionStatus);
            switch (e.ConnectionStatus)
            {
                case HidUtility.UsbConnectionStatus.Connected:
                    //fwinittimer.Start();
                    //firmwareinit();
                    mainWindow = "true";
                    deviceon = "Visible";
                    deviceoff = "Collapsed";
                    break;
                case HidUtility.UsbConnectionStatus.Disconnected:
                    mainWindow = "false";
                    deviceon = "Collapsed";
                    deviceoff = "Visible";
                    //dfuVisibility = "Visible";
                    if (!fwver.Contains("Wrong Version!!"))
                    {
                        fwver = "EMC Firmware : Unknown";
                    }
                    Reset();
                    //Console.WriteLine("Disconected angle : {0}",angle);
                    break;
                case HidUtility.UsbConnectionStatus.NotWorking:
                    mainWindow = "false";
                    deviceon = "Collapsed";
                    deviceoff = "Visible";
                    fwver = "EMC Device Not Working";
                    break;
            }
            ///UI
            NotifyPropertyChanged("deviceon");
            NotifyPropertyChanged("deviceoff");
            NotifyPropertyChanged("mainWindow");
            //NotifyPropertyChanged("dfuVisibility");
            NotifyPropertyChanged("fwver");

        }

        public void SendPacketHandler(object sender, UsbBuffer OutBuffer)
        {

            OutBuffer.clear();

            #region FIRMWARE
            if (firmwareCheck == true)
            {
                OutBuffer.buffer[0] = 144;
                OutBuffer.buffer[1] = 7;
                firmwareCheck = false;
            }

            if (firmwarepass == true)
            {
                OutBuffer.buffer[0] = 144;
                OutBuffer.buffer[1] = 8;
                firmwarepass = false;
            }
            #endregion

            #region INPUTPAGE
            //Set Feature Reports Command
            if (SetAngle == true)
            {
                OutBuffer.buffer[0] = ReportID;
                OutBuffer.buffer[1] = 2;
                byte[] anglebytearray = BitConverter.GetBytes(angle);
                OutBuffer.buffer[2] = anglebytearray[1];
                OutBuffer.buffer[3] = anglebytearray[0];
                SetAngle = false;
            }

            if (SetCenter == true)
            {
                OutBuffer.buffer[0] = ReportID;
                OutBuffer.buffer[1] = 7;
                SetCenter = false;
                FalseCenter();
            }
            if (SetFalseCenter == true)
            {
                OutBuffer.buffer[0] = ReportID;
                OutBuffer.buffer[1] = 8;
                SetFalseCenter = false;
            }

            if (SetCpr == true)
            {
                OutBuffer.buffer[0] = ReportID;
                OutBuffer.buffer[1] = 1;
                byte[] cprbytearray = BitConverter.GetBytes(Cpr);
                OutBuffer.buffer[2] = cprbytearray[1];
                OutBuffer.buffer[3] = cprbytearray[0];
                SetCpr = false;
            }

            if (findCPR == true)
            {
                OutBuffer.buffer[0] = 144;
                OutBuffer.buffer[1] = 5;
                findCPR = false;
            }

            if (closereadCPRValue == true)
            {
                OutBuffer.buffer[0] = 144;
                OutBuffer.buffer[1] = 6;
                closereadCPRValue = false;
            }

            if (SetGasmin == true)
            {
                OutBuffer.buffer[0] = ReportID;
                OutBuffer.buffer[1] = 9;
                SetGasmin = false;
            }

            if (SetGasmax == true)
            {
                OutBuffer.buffer[0] = ReportID;
                OutBuffer.buffer[1] = 10;
                SetGasmax = false;
            }

            if (SetBrakemin == true)
            {
                OutBuffer.buffer[0] = ReportID;
                OutBuffer.buffer[1] = 11;
                SetBrakemin = false;
            }

            if (SetBrakemax == true)
            {
                OutBuffer.buffer[0] = ReportID;
                OutBuffer.buffer[1] = 12;
                SetBrakemax = false;
            }

            if (SetClutchmin == true)
            {
                OutBuffer.buffer[0] = ReportID;
                OutBuffer.buffer[1] = 13;
                SetClutchmin = false;
            }

            if (SetClutchmax == true)
            {
                OutBuffer.buffer[0] = ReportID;
                OutBuffer.buffer[1] = 14;
                SetClutchmax = false;
            }

            if (SetHandbrakemin == true)
            {
                OutBuffer.buffer[0] = ReportID;
                OutBuffer.buffer[1] = 15;
                SetHandbrakemin = false;
            }

            if (SetHandbrakemax == true)
            {
                OutBuffer.buffer[0] = ReportID;
                OutBuffer.buffer[1] = 16;
                SetHandbrakemax = false;
            }

            if (SetClearEeprom == true)
            {
                OutBuffer.buffer[0] = 144;
                OutBuffer.buffer[1] = 4;
                SetClearEeprom = false;
            }

            if (enableClutch == true)
            {
                OutBuffer.buffer[0] = 144;
                OutBuffer.buffer[1] = 13;
                enableClutch = false;
            }

            if (disableClutch == true)
            {
                OutBuffer.buffer[0] = 144;
                OutBuffer.buffer[1] = 14;
                disableClutch = false;
            }

            if (enableHandbrake == true)
            {
                OutBuffer.buffer[0] = 144;
                OutBuffer.buffer[1] = 15;
                enableHandbrake = false;
            }

            if (disableHandbrake == true)
            {
                OutBuffer.buffer[0] = 144;
                OutBuffer.buffer[1] = 16;
                disableHandbrake = false;
            }

            #endregion

            #region OUTPUTPAGE
            ////////Output page

            if (Setminforce == true)
            {
                OutBuffer.buffer[0] = ReportID;
                OutBuffer.buffer[1] = 38;
                OutBuffer.buffer[6] = minforce;
                Setminforce = false;
            }

            if (Setmaxforce == true)
            {
                OutBuffer.buffer[0] = ReportID;
                OutBuffer.buffer[1] = 39;
                OutBuffer.buffer[6] = maxforce;
                Setmaxforce = false;
            }

            if (Setpwmfreq == true)
            {
                OutBuffer.buffer[0] = 238;
                OutBuffer.buffer[1] = 2;
                OutBuffer.buffer[2] = pwmfreq;
                Setpwmfreq = false;
            }

            if (SetSoftlock == true)
            {
                OutBuffer.buffer[0] = ReportID;
                OutBuffer.buffer[1] = 6;
                OutBuffer.buffer[4] = _softlock;
                SetSoftlock = false;
            }

            if (SetConstant == true)
            {
                OutBuffer.buffer[0] = ReportID;
                OutBuffer.buffer[1] = 3;
                OutBuffer.buffer[4] = _constant;
                SetConstant = false;
            }

            if (SetSpring == true)
            {
                OutBuffer.buffer[0] = ReportID;
                OutBuffer.buffer[1] = 4;
                OutBuffer.buffer[4] = _spring;
                SetSpring = false;
            }

            if (SetSine == true)
            {
                OutBuffer.buffer[0] = ReportID;
                OutBuffer.buffer[1] = 5;
                OutBuffer.buffer[4] = _sine;
                SetSine = false;
            }

            if (SetDamper == true)
            {
                OutBuffer.buffer[0] = ReportID;
                OutBuffer.buffer[1] = 32;
                OutBuffer.buffer[4] = _damper;
                SetDamper = false;
            }

            if (SetFriction == true)
            {
                OutBuffer.buffer[0] = ReportID;
                OutBuffer.buffer[1] = 33;
                OutBuffer.buffer[4] = _friction;
                SetFriction = false;
            }

            if (SetInertia == true)
            {
                OutBuffer.buffer[0] = ReportID;
                OutBuffer.buffer[1] = 34;
                OutBuffer.buffer[4] = _inertia;
                SetInertia = false;
            }

            if (SetgainConstant == true)
            {
                OutBuffer.buffer[0] = ReportID;
                OutBuffer.buffer[1] = 35;
                OutBuffer.buffer[5] = gainConstant;
                SetgainConstant = false;
            }

            if (SetgainSpring == true)
            {
                OutBuffer.buffer[0] = ReportID;
                OutBuffer.buffer[1] = 36;
                OutBuffer.buffer[5] = gainSpring;
                SetgainSpring = false;
            }

            if (SetgainSine == true)
            {
                OutBuffer.buffer[0] = ReportID;
                OutBuffer.buffer[1] = 37;
                OutBuffer.buffer[5] = gainSine;
                SetgainSine = false;
            }

            if (SetDesktopEffectForce == true)
            {
                OutBuffer.buffer[0] = ReportID;
                OutBuffer.buffer[1] = 40;
                OutBuffer.buffer[7] = desktopEffectForce;
                SetDesktopEffectForce = false;
            }

            if (SetdesktopEffectOff == true)
            {
                OutBuffer.buffer[0] = 144;
                OutBuffer.buffer[1] = 9;
                SetdesktopEffectOff = false;
            }

            if (SetdesktopEffectSpring == true)
            {
                OutBuffer.buffer[0] = 144;
                OutBuffer.buffer[1] = 10;
                SetdesktopEffectSpring = false;
            }

            if (SetdesktopEffectDamper == true)
            {
                OutBuffer.buffer[0] = 144;
                OutBuffer.buffer[1] = 11;
                SetdesktopEffectDamper = false;
            }

            if (SetdesktopEffectFriction == true)
            {
                OutBuffer.buffer[0] = 144;
                OutBuffer.buffer[1] = 12;
                SetdesktopEffectFriction = false;
            }

            if (SetSquare == true)
            {
                OutBuffer.buffer[0] = ReportID;
                OutBuffer.buffer[1] = 41;
                OutBuffer.buffer[5] = gainSquare;
                SetSquare = false;
            }
            #endregion

            #region SHIFTERPAGE
            //Shifter Page
            if (enableHshifter == true)
            {
                OutBuffer.buffer[0] = ReportID;
                OutBuffer.buffer[1] = 29;
                enableHshifter = false;
            }

            if (enableSeqshifter == true)
            {
                OutBuffer.buffer[0] = ReportID;
                OutBuffer.buffer[1] = 30;
                enableSeqshifter = false;
            }

            if (disableshifter == true)
            {
                OutBuffer.buffer[0] = ReportID;
                OutBuffer.buffer[1] = 31;
                disableshifter = false;
            }

            if (xyRawRequest == true)
            {
                OutBuffer.buffer[0] = 144;
                OutBuffer.buffer[1] = 2;
                xyRawRequest = false;
            }



            if (closexyRawRequest == true)
            {
                OutBuffer.buffer[0] = 144;
                OutBuffer.buffer[1] = 3;
                closexyRawRequest = false;
            }

            if (saveShifter == true)
            {
                OutBuffer.buffer[0] = ReportID;
                OutBuffer.buffer[1] = 27;
                saveShifter = false;
            }

            if (resetShifter == true)
            {
                OutBuffer.buffer[0] = ReportID;
                OutBuffer.buffer[1] = 28;
                resetShifter = false;
            }

            if (setYmax == true)
            {
                OutBuffer.buffer[0] = ReportID;
                OutBuffer.buffer[1] = 23;
                setYmax = false;
            }

            if (setYmin == true)
            {
                OutBuffer.buffer[0] = ReportID;
                OutBuffer.buffer[1] = 24;
                setYmin = false;
            }

            if (setXmax == true)
            {
                OutBuffer.buffer[0] = ReportID;
                OutBuffer.buffer[1] = 25;
                setXmax = false;
            }

            if (setXmin == true)
            {
                OutBuffer.buffer[0] = ReportID;
                OutBuffer.buffer[1] = 26;
                setXmin = false;
            }

            if (setxHigh == true)
            {
                OutBuffer.buffer[0] = ReportID;
                OutBuffer.buffer[1] = 17;
                byte[] val = BitConverter.GetBytes(xHigh);
                OutBuffer.buffer[5] = val[1];
                OutBuffer.buffer[6] = val[0];
                setxHigh = false;
            }

            if (setxMid == true)
            {
                OutBuffer.buffer[0] = ReportID;
                OutBuffer.buffer[1] = 19;
                byte[] val = BitConverter.GetBytes(xMid);
                OutBuffer.buffer[5] = val[1];
                OutBuffer.buffer[6] = val[0];
                setxMid = false;
            }

            if (setxLow == true)
            {
                OutBuffer.buffer[0] = ReportID;
                OutBuffer.buffer[1] = 18;
                byte[] val = BitConverter.GetBytes(xLow);
                OutBuffer.buffer[5] = val[1];
                OutBuffer.buffer[6] = val[0];
                setxLow = false;
            }

            if (setyHigh == true)
            {
                OutBuffer.buffer[0] = ReportID;
                OutBuffer.buffer[1] = 21;
                byte[] val = BitConverter.GetBytes(yHigh);
                OutBuffer.buffer[5] = val[1];
                OutBuffer.buffer[6] = val[0];
                setyHigh = false;
            }

            if (setyLow == true)
            {
                OutBuffer.buffer[0] = ReportID;
                OutBuffer.buffer[1] = 22;
                byte[] val = BitConverter.GetBytes(yLow);
                OutBuffer.buffer[5] = val[1];
                OutBuffer.buffer[6] = val[0];
                setyLow = false;
            }
            ///out buffer transfer
            #endregion

            #region LEDCONTROL
            if (setFireBrick == true)
            {
                OutBuffer.buffer[0] = 144;
                OutBuffer.buffer[1] = 17;
                setFireBrick = false;
            }

            if (setForestGreen == true)
            {
                OutBuffer.buffer[0] = 144;
                OutBuffer.buffer[1] = 18;
                setForestGreen = false;
            }

            if (setDarkBlue == true)
            {
                OutBuffer.buffer[0] = 144;
                OutBuffer.buffer[1] = 19;
                setDarkBlue = false;
            }

            if (setPurple == true)
            {
                OutBuffer.buffer[0] = 144;
                OutBuffer.buffer[1] = 20;
                setPurple = false;
            }

            if (setGoldenRod == true)
            {
                OutBuffer.buffer[0] = 144;
                OutBuffer.buffer[1] = 21;
                setGoldenRod = false;
            }

            if (setDarkCyan == true)
            {
                OutBuffer.buffer[0] = 144;
                OutBuffer.buffer[1] = 22;
                setDarkCyan = false;
            }

            if (setDeepPink == true)
            {
                OutBuffer.buffer[0] = 144;
                OutBuffer.buffer[1] = 23;
                setDeepPink = false;
            }

            #endregion

            OutBuffer.RequestTransfer = true;
        }

        public void PacketSentHandler(object sender, UsbBuffer OutBuffer)
        {
            WaitingForDevice = OutBuffer.TransferSuccessful;
        }

        public void ReceivePacketHandler(object sender, UsbBuffer InBuffer)
        {
            WaitingForDevice = true;
            InBuffer.RequestTransfer = WaitingForDevice;
        }

        public void PacketReceivedHandler(object sender, UsbBuffer InBuffer)
        {

            if (InBuffer.buffer[0] == 3)
            {
                //if Input page true
                int steering = (int)(InBuffer.buffer[6] << 8) + InBuffer.buffer[5];

                if (readCPRValue)
                {
                    if (steering < 32768)
                    {
                        findCPRValue = steering.ToString();
                    }
                    else if (steering > 32767)
                    {
                        findCPRValue = (Math.Abs(steering - 65535)).ToString();
                    }
                    NotifyPropertyChanged("findCPRValue");
                    RotateAngle = 0;
                    StirLeft = 0;
                    StirRight = 0;
                }
                else
                {
                    int x = 0;
                    if (steering < 32768)
                    {
                        x = steering;
                    }
                    else if (steering > 32767)
                    {
                        x = steering - 65535;
                    }

                    if (x < 0)
                    {
                        StirLeft = Math.Abs(x);
                        StirRight = 0;
                    }
                    else
                    {
                        StirRight = x;
                        StirLeft = 0;
                    }
                    int inMin = -32767;
                    int inMax = 32767;
                    int outMin = -(angle / 2);
                    int outMax = angle / 2;
                    RotateAngle = (x - inMin) * (outMax - outMin) / (inMax - inMin) + outMin;
                    RotateAngletxt = RotateAngle.ToString() + "°";

                    NotifyPropertyChanged("RotateAngle");
                    NotifyPropertyChanged("RotateAngletxt");
                    NotifyPropertyChanged("StirLeft");
                    NotifyPropertyChanged("StirRight");
                }

                int gasraw = (InBuffer.buffer[8] << 8) + InBuffer.buffer[7];
                if (gasraw < 32768)
                {
                    Throttle = gasraw;
                }
                else if (gasraw > 32767)
                {
                    Throttle = gasraw - 65535;
                }

                Brake = (InBuffer.buffer[14] << 8) + InBuffer.buffer[13];
                Clutch = (InBuffer.buffer[10] << 8) + InBuffer.buffer[9];
                Handbrake = (InBuffer.buffer[12] << 8) + InBuffer.buffer[11];

                NotifyPropertyChanged("Throttle");
                NotifyPropertyChanged("Brake");
                NotifyPropertyChanged("Clutch");
                NotifyPropertyChanged("Handbrake");

                // if Shifter Page true
                var gear = InBuffer.buffer[1];
                switch (gear)
                {
                    case 0:
                        GearText = "N";
                        gearColor = "ForestGreen";
                        break;
                    case 1:
                        GearText = "1";
                        gearColor = "#7FFFFFFF";
                        break;
                    case 2:
                        GearText = "2";
                        gearColor = "#7FFFFFFF";
                        break;
                    case 4:
                        GearText = "3";
                        gearColor = "#7FFFFFFF";
                        break;
                    case 8:
                        GearText = "4";
                        gearColor = "#7FFFFFFF";
                        break;
                    case 16:
                        GearText = "5";
                        gearColor = "#7FFFFFFF";
                        break;
                    case 32:
                        GearText = "6";
                        gearColor = "#7FFFFFFF";
                        break;
                    case 64:
                        GearText = "7";
                        gearColor = "#7FFFFFFF";
                        break;
                    case 128:
                        GearText = "R";
                        gearColor = "#CCB40000";
                        break;
                }
                NotifyPropertyChanged("GearText");
                NotifyPropertyChanged("gearColor");

            }

            if (InBuffer.buffer[0] == 134)
            {
                if (InBuffer.buffer[1] == 94)
                {
                    fwver = "EMC Firmware : 0." + Convert.ToString((byte)InBuffer.buffer[1]) + "(update available)";

                }
                else if (InBuffer.buffer[1] > 94)
                {
                    fwver = "EMC Firmware : 0." + Convert.ToString((byte)InBuffer.buffer[1]);
                }
                else
                {
                    fwver = "EMC Firmware : Wrong Version!! (Ver : 0." + Convert.ToString((byte)InBuffer.buffer[1]) + ")";
                }
                NotifyPropertyChanged("fwver");
                firmwarepass = true;
                Console.WriteLine("firmwareVER {0}", fwver);
            }

            if (InBuffer.buffer[0] == 128)
            {


                var x = (InBuffer.buffer[2] << 8) + InBuffer.buffer[1];
                rawX = "X : " + x.ToString();
                knobX = mapper(x, 0, 1024, -12, 153);
                var y = (InBuffer.buffer[4] << 8) + InBuffer.buffer[3];
                rawY = "Y : " + y.ToString();
                knobY = mapper(y, 0, 1024, 113, -12);
                NotifyPropertyChanged("rawX");
                NotifyPropertyChanged("rawY");
                NotifyPropertyChanged("knobX");
                NotifyPropertyChanged("knobY");
            }

        }

        private int mapper(int val, int inMin, int inMax, int outMin, int outMax)
        {
            return (val - inMin) * (outMax - outMin) / (inMax - inMin) + outMin;
        }

        #region License
        private static bool LicenseException(License license)
        {
            //// check licensetype.
            return license.Type == LicenseType.Trial;
        }

        private void checkLicense()
        {
            var myStream = Stream.Null;

            try
            {
                var sr = new StreamReader(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) + "\\F4R.lic");
                myStream = sr.BaseStream;
                if (myStream.Length <= 0)
                {
                    return;
                }

                ulicense = License.Load(myStream);
                licensetxt = ValidateLicense(ulicense);
                licenseHID = ulicense.AdditionalAttributes.Get("HID");
            }
            catch (Exception ex)
            {
                //MessageBox.Show("Cannot read file from disk. Original error: " + ex.Message);
                licensetxt = "Favor encaminhar o ID do aplicativo para o e-mail: its3@its3.com.br";
            }
            finally
            {
                //// Check this again, since we need to make sure we didn't throw an exception on open.
                if (myStream != null)
                {
                    myStream.Close();
                }
            }

            if (licensetxt == "Validação Completa")
            {
                if (licenseHID == hardwareId)
                {
                    licenseVisibility = "Collapsed";
                }
                else
                {
                    licenseVisibility = "Visible";
                }
            }
            else
            {
                licenseVisibility = "Visible";
            }
            NotifyPropertyChanged("licenseVisibility");
            NotifyPropertyChanged("licensetxt");
        }

        private string ValidateLicense(License license)
        {
            //// validate license and define return value.
            const string ReturnValue = "Validação Completa";

            var validationFailures =
                license.Validate()
                    .ExpirationDate()
                    .When(LicenseException)
                    .And()
                    .Signature(publicKey)
                    .AssertValidLicense();

            var failures = validationFailures as IValidationFailure[] ?? validationFailures.ToArray();

            return !failures.Any() ? ReturnValue : failures.Aggregate(string.Empty, (current, validationFailure) => current + validationFailure.HowToResolve + ": " + "\r\n" + validationFailure.Message + "\r\n");
        }

        #endregion

        #region THEMES
        private Command themesCommand;
        public Command DothemesCommand
        {
            get { return themesCommand; }
        }
        private void setThemesParam(object parameter)
        {
            var param = parameter.ToString();
            switch (param)
            {
                case "Red":
                    themesColor = "Firebrick";//R = 178 G 34 B 34
                    setFireBrick = true;
                    break;
                case "Green":
                    themesColor = "ForestGreen";//R = 34 G 139 B 34
                    setForestGreen = true;
                    break;
                case "Blue":
                    themesColor = "DarkBlue";//R = 0 G 0 B 139
                    setDarkBlue = true;
                    break;
                case "Purple":
                    themesColor = "Purple";//R = 128 G 0 B 128
                    setPurple = true;
                    break;
                case "Gold":
                    themesColor = "Goldenrod";//R = 218 G 165 B 32
                    setGoldenRod = true;
                    break;
                case "Cyan":
                    themesColor = "DarkCyan";//R = 0 G 139 B 139
                    setDarkCyan = true;
                    break;
                case "Pink":
                    themesColor = "DeepPink";//R = 0 G 139 B 139
                    setDeepPink = true;
                    break;
            }
            NotifyPropertyChanged("themesColor");
        }
        #endregion

        #region RESET
        private void Reset()
        {
            angle = 0;
            NotifyPropertyChanged("angle");
            _softlock = 0;
            NotifyPropertyChanged("_softlock");

            gainConstant = 0;
            NotifyPropertyChanged("gainConstant");
            gainSpring = 0;
            NotifyPropertyChanged("gainSpring");
            gainSine = 0;
            NotifyPropertyChanged("gainSine");

            _damper = 0;
            NotifyPropertyChanged("_damper");
            _friction = 0;
            NotifyPropertyChanged("_friction");
            _inertia = 0;
            NotifyPropertyChanged("_inertia");

            minforce = 0;
            NotifyPropertyChanged("minforce");
            maxforce = 0;
            NotifyPropertyChanged("maxforce");

            _constant = 0;
            NotifyPropertyChanged("_constant");
            _spring = 0;
            NotifyPropertyChanged("_spring");
            _sine = 0;
            NotifyPropertyChanged("_sine");

            desktopEffectType = "Off";
            NotifyPropertyChanged("desktopEffectType");
            desktopEffectForce = 0;
            NotifyPropertyChanged("desktopEffectForce");

            shiftermode = "Off";
            NotifyPropertyChanged("shiftermode");
        }
        #endregion

        #region INPUTPAGE
        public UInt16 Angle
        {
            get
            {
                return angle;
            }
            set
            {
                if (value != angle)
                {
                    angle = value;
                    SetAngle = true;

                }

            }
        }

        public async void FalseCenter()
        {
            await Task.Delay(50);
            if (SetFalseCenter == false)
            {
                SetFalseCenter = true;
            }
            //Console.WriteLine(SetFalseCenter);
        }

        private Command SetCenterCommand;
        public Command DoSetCenter
        {
            get { return SetCenterCommand; }
        }

        public string GetSetCpr
        {
            get => Cpr.ToString();
            set
            {
                Cpr = Convert.ToUInt16(value);
                SetCpr = true;
            }
        }

        public Command DoPedalCommand
        {
            get { return pedalCommand; }
        }
        private Command pedalCommand;
        private void DoPedalParameterisedCommand(object parameter)
        {
            //Messages.Add("Calling a Parameterised Command - the Parameter is '" + parameter.ToString() + "'.");
            Console.WriteLine(parameter);
            switch (parameter)
            {
                case "Gasmax":
                    SetGasmax = true;
                    break;
                case "Gasmin":
                    SetGasmin = true;
                    break;
                case "BrakeMax":
                    SetBrakemax = true;
                    break;
                case "BrakeMin":
                    SetBrakemin = true;
                    break;
                case "ClutchMax":
                    SetClutchmax = true;
                    break;
                case "ClutchMin":
                    SetClutchmin = true;
                    break;
                case "HandbrakeMax":
                    SetHandbrakemax = true;
                    break;
                case "HandbrakeMin":
                    SetHandbrakemin = true;
                    break;
                case "disableClutch":
                    disableClutch = true;
                    break;
                case "disableHandbrake":
                    disableHandbrake = true;
                    break;
            }
        }

        public bool DisableClutch
        {
            set
            {
                if (value == true)
                {
                    enableClutch = true;
                }
                else
                {
                    disableClutch = true;
                }
            }
        }

        public bool DisableHandbrake
        {
            set
            {
                if (value == true)
                {
                    enableHandbrake = true;
                }
                else
                {
                    disableHandbrake = true;
                }
            }
        }

        private Command CleareepromCommand;
        public Command Docleareeprom
        {
            get { return CleareepromCommand; }
        }

        #region CPRFINDER
        private Command cprfinderCommand;
        public Command DocprFinderCommand
        {
            get { return cprfinderCommand; }
        }

        private void cprfinderCommandParam(object parameter)
        {
            switch (parameter)
            {
                case "findcpr":
                    findCPR = true;
                    findCPRVis = "Visible";
                    readCPRValue = true;
                    break;
                case "close":
                    findCPRVis = "Collapsed";
                    readCPRValue = false;
                    closereadCPRValue = true;
                    break;
            }
            NotifyPropertyChanged("findCPRVis");
        }

        public byte DesktopEffectForce
        {
            get
            {
                return desktopEffectForce;
            }
            set
            {
                desktopEffectForce = value;
                SetDesktopEffectForce = true;
            }
        }

        public string DesktopEffectType
        {
            get
            {
                return desktopEffectType;
            }
            set
            {
                desktopEffectType = value;
                switch (desktopEffectType)
                {
                    case "Off":
                        SetdesktopEffectOff = true;
                        break;
                    case "Spring":
                        SetdesktopEffectSpring = true;
                        break;
                    case "Damper":
                        SetdesktopEffectDamper = true;
                        break;
                    case "Friction":
                        SetdesktopEffectFriction = true;
                        break;
                }
            }
        }

        #endregion
        #endregion

        #region OUTPUTPAGE
        /// <summary>
        /// Output page viewmodel
        /// </summary>
        /// 

        public byte Minforce
        {
            get
            {
                return minforce;
            }
            set
            {
                minforce = value;
                Setminforce = true;
            }
        }

        public byte Maxforce
        {
            get
            {
                return maxforce;
            }
            set
            {
                maxforce = value;
                Setmaxforce = true;
            }
        }

        public byte GetSoftlock
        {
            get { return _softlock; }
            set
            {
                _softlock = value;
                SetSoftlock = true;
            }
        }

        public byte GetConstant
        {
            get { return _constant; }
            set
            {
                if (value != _constant)
                {
                    _constant = value;
                    SetConstant = true;
                    if (value == 100)
                    {
                        Constanttxt = "Default";

                    }
                    else if (value > 100)
                    {
                        float multiply = ((float)value / 100);
                        Constanttxt = "x " + Convert.ToString(multiply);

                    }
                    NotifyPropertyChanged("Constanttxt");
                }


            }
        }

        public byte GetSpring
        {
            get { return _spring; }
            set
            {
                if (value != _spring)
                {
                    _spring = value;
                    SetSpring = true;
                    if (value == 100)
                    {
                        Springtxt = "Default";

                    }
                    else if (value > 100)
                    {
                        float multiply = ((float)value / 100);
                        Springtxt = "x " + Convert.ToString(multiply);

                    }
                    NotifyPropertyChanged("Springtxt");
                }
            }
        }

        public byte GetSine
        {
            get { return _sine; }
            set
            {
                if (value != _sine)
                {
                    _sine = value;
                    SetSine = true;
                    if (value == 100)
                    {
                        Sinetxt = "Default";

                    }
                    else if (value > 100)
                    {
                        float multiply = ((float)value / 100);
                        Sinetxt = "x " + Convert.ToString(multiply);

                    }
                    NotifyPropertyChanged("Sinetxt");
                }
            }
        }

        public byte GetDamper
        {
            get { return _damper; }
            set
            {
                _damper = value;
                SetDamper = true;

            }
        }

        public byte GetFriction
        {
            get { return _friction; }
            set
            {
                _friction = value;
                SetFriction = true;

            }
        }

        public byte GetInertia
        {
            get { return _inertia; }
            set
            {
                _inertia = value;
                SetInertia = true;

            }
        }

        public byte GainConstant
        {
            get
            {
                return gainConstant;
            }
            set
            {
                gainConstant = value;
                SetgainConstant = true;
            }
        }

        public byte GainSpring
        {
            get
            {
                return gainSpring;
            }
            set
            {
                gainSpring = value;
                SetgainSpring = true;
            }
        }

        public byte GainSine
        {
            get
            {
                return gainSine;
            }
            set
            {
                gainSine = value;
                SetgainSine = true;
            }
        }

        public byte GainSquare
        {
            get
            {
                return gainSquare;
            }
            set
            {
                gainSquare = value;
                SetSquare = true;
            }
        }


        public string PWMfreq
        {
            get
            {
                return pwmfreqstring;
            }
            set
            {
                pwmfreqstring = value;
                switch (pwmfreqstring)
                {
                    case "4KHz":
                        pwmfreq = 4;
                        break;
                    case "8KHz":
                        pwmfreq = 8;
                        break;
                    case "16KHz":
                        pwmfreq = 16;
                        break;
                    case "20Khz":
                        pwmfreq = 20;
                        break;
                }
                Setpwmfreq = true;
            }
        }
        #endregion

        #region SHIFTERPAGE

        public string Shiftermode
        {
            get
            {
                return shiftermode;
            }

            set
            {
                shiftermode = value;

                switch (shiftermode)
                {
                    case "Off":
                        calibratebutton = "false";
                        disableshifter = true;
                        break;
                    case "Hshifter":
                        calibratebutton = "true";
                        xhigh = "true";
                        linexhigh = "Visible";
                        xmid = "true";
                        linexmid = "Visible";
                        xlow = "true";
                        linexlow = "Visible";
                        enableHshifter = true;
                        break;
                    case "Sequential":
                        calibratebutton = "true";
                        xhigh = "false";
                        linexhigh = "Hidden";
                        xmid = "false";
                        linexmid = "Hidden";
                        xlow = "false";
                        linexlow = "Hidden";
                        enableSeqshifter = true;
                        break;
                }
                NotifyPropertyChanged("calibratebutton");
                NotifyPropertyChanged("xhigh");
                NotifyPropertyChanged("linexhigh");
                NotifyPropertyChanged("xmid");
                NotifyPropertyChanged("linexmid");
                NotifyPropertyChanged("xlow");
                NotifyPropertyChanged("linexlow");
            }
        }

        private Command shifterCalibratorCommand;
        public Command DoShifterCalibratorCommand
        {
            get { return shifterCalibratorCommand; }
        }

        private void setShifterCalibratorParam(object parameter)
        {
            var param = parameter.ToString();
            Console.WriteLine(param);
            switch (param)
            {
                case "Calibrate":
                    xyRawRequest = true;
                    calibratebutton = "false";

                    break;
                case "Reset":
                    resetShifter = true;
                    break;
                case "Save":
                    saveShifter = true;
                    break;
                case "Ok":
                    closexyRawRequest = true;

                    if (Shiftermode != "Off")
                    {
                        calibratebutton = "true";
                    }
                    else
                    {
                        calibratebutton = "false";
                    }
                    rawX = "X :";
                    rawY = "Y :";
                    break;
                case "Ymax":
                    setYmax = true;
                    break;
                case "Ymin":
                    setYmin = true;
                    break;
                case "Xmax":
                    setXmax = true;
                    break;
                case "Xmin":
                    setXmin = true;
                    break;
            }
            NotifyPropertyChanged("calibratebutton");
            NotifyPropertyChanged("rawX");
            NotifyPropertyChanged("rawY");
        }

        //private UInt16 xLow { get; set; }
        public UInt16 XLow
        {
            get
            {
                return xLow;
            }
            set
            {
                if (value != xLow)
                {
                    xLow = value;
                    setxLow = true;
                }

            }
        }
        //private UInt16 xMid { get; set; }
        public UInt16 XMid
        {
            get
            {
                return xMid;
            }
            set
            {
                if (value != xMid)
                {
                    xMid = value;
                    setxMid = true;
                }

            }
        }
        //private UInt16 xHigh { get; set; }
        public UInt16 XHigh
        {
            get
            {
                return xHigh;
            }
            set
            {
                if (value != xHigh)
                {
                    xHigh = value;
                    setxHigh = true;
                }

            }
        }
        //private UInt16 yLow { get; set; }
        public UInt16 YLow
        {
            get
            {
                return yLow;
            }
            set
            {
                if (value != yLow)
                {
                    yLow = value;
                    setyLow = true;
                }

            }
        }
        // private UInt16 yHigh { get; set; }
        public UInt16 YHigh
        {
            get
            {
                return yHigh;
            }
            set
            {
                if (value != yHigh)
                {
                    yHigh = value;
                    setyHigh = true;
                }

            }
        }
        #endregion

    }
}
