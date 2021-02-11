using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using Microsoft.Win32;
using Portable.Licensing;
using Portable.Licensing.Validation;
using License = Portable.Licensing.License;

namespace F4Rkey
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        /// <summary>
        /// The license object.
        /// </summary>
        private License ulicense;

        /// <summary>
        /// The private key.
        /// </summary>
        private string privateKey;

        /// <summary>
        /// The public key.
        /// </summary>
        private string publicKey;

        public MainWindow()
        {
            InitializeComponent();
            LoadSettings();
            publickey.Text = "MIIBKjCB4wYHKoZIzj0CATCB1wIBATAsBgcqhkjOPQEBAiEA/////wAAAAEAAAAAAAAAAAAAAAD///////////////8wWwQg/////wAAAAEAAAAAAAAAAAAAAAD///////////////wEIFrGNdiqOpPns+u9VXaYhrxlHQawzFOw9jvOPD4n0mBLAxUAxJ02CIbnBJNqZnjhE50mt4GffpAEIQNrF9Hy4SxCR/i85uVjpEDydwN9gS3rM6D0oTlF2JjClgIhAP////8AAAAA//////////+85vqtpxeehPO5ysL8YyVRAgEBA0IABML91xTAnVFTLJZSieFij8/U7luzS9G0UjnEipEBSSLCqrtwPCRkuh0SMwQEpdyGJCrgx8S1N2jW3rjvLxa4ElM=";
            privatekey.Text = "MIICITAjBgoqhkiG9w0BDAEDMBUEELBoKMC9e6+YNX28GB+VHfECAQoEggH48gCyYUx0oG+nS6smeTzMzGo7gzynJJfHnJOUjH2rPcJwQviG7K4g+KU/8m3BuNkrsnyZ6UObFLyszTdj0Ly0/LJLBvXdhqD7HY4JPZs6ZHd7RVY4Lxve7n+P8pjxO0PYmW5iN4IhzBEn0Hc04ktMn7ECJHqMhJctUEvVqNUTScCMsbHDWf/xFfShsRVAmzKoutvQFsV0L1B9u0GUwcoke8qOuec4dhZh+LJrpIMXtHV7xtiTHve2i0DeGvW85maY0N3ohiEsx6rt6fy2fBhUv7n7mfLkJoJrf/jwv15Z/R54icTNybm4aGGhepgzCcZXX5ESE4+TnV9pJjELgE0SJ56QfEifGAqaZHPGmADBGSM0f50NMY5Qq5+pvIM9/d1V5Ao+4bb36sjdd0kdyZxO8bvetbC/UpI+rPsfEty4RQtSm3E6n7ttmBnBZIkErM5I88fSDuEitNSBOpAjG6Ips29E71kHytqduemj9FAdzwFjuLlvdLb4oFu5Ki6ir84PFLsfkSR7dLHyvqlKxx3w56vLO6AEyjkkkG37fWOm07AtxQ4fVlQUwxCjr+tHc3H6w44nMEzhmlhG3eaOpJ/jok7T3zqX1HkbNOY1cJmPFQX/Exu2u1G+CHvYnhvP4ya4AHMNIjncVlphklx3Gw4pYFu5xkVRx/ko";
            pass.Text = "F4R";
        }

        private void Generatekey_Click(object sender, RoutedEventArgs e)
        {
            if(pass.Text.Length > 1)
            {
                generateKey();
            }
            else
            {
                MessageBox.Show("The password cannot be empty !!!", "ERROR");
            }
            
        }

        private void generateKey()
        {
            
            
            MessageBoxResult result = MessageBox.Show("Generate a new key? F4R software must rebuild with a new key", "Warning", MessageBoxButton.OKCancel);

            switch (result)
            {
                case MessageBoxResult.OK:
                    var privKeySet = new HashSet<string>();
                    var pubKeySet = new HashSet<string>();

                    var keyGenerator = Portable.Licensing.Security.Cryptography.KeyGenerator.Create();
                    var keyPair = keyGenerator.GenerateKeyPair();
                    privateKey = keyPair.ToEncryptedPrivateKeyString(pass.Text);
                    publicKey = keyPair.ToPublicKeyString();

                    publickey.Text = publicKey;
                    privatekey.Text = privateKey;

                    break;
                case MessageBoxResult.Cancel:
                    MessageBox.Show("Nevermind then...");
                    break;
            }

            
            
        }

        private void Generatelicense_Click(object sender, RoutedEventArgs e)
        {
            if(customer.Text.Length <=0 || email.Text.Length <= 0 || productid.Text.Length <= 0)
            {
                MessageBox.Show("Incomplete data", "ERROR");
            }
            else
            {
                generateLicense();
            }
            
        }

        private void generateLicense()
        {
            /*
                1. define a new Dictionary for the addition attributes and add the attributes.
                2. define a new Dictionary for the product features and add the features.
                3. create new guid as unique identifier.
                4. define the license with the required parameters.
                5. select Path and filename to write the license.
            */
            var attributesDictionarty = new Dictionary<string, string>
            {
                {
                    "HID",productid.Text
                }
            };
            

            var licenseId = Guid.NewGuid();
            

            ulicense =
                    License.New().WithUniqueIdentifier(licenseId)
                        .As(GetLicenseType())
                        .WithMaximumUtilization(1)
                        .WithAdditionalAttributes(attributesDictionarty)
                        .LicensedTo(customer.Text, email.Text)
                        //.CreateAndSignWithPrivateKey(privateKey, pass.Text);
                        .CreateAndSignWithPrivateKey(privatekey.Text, pass.Text);

            var sfdlicense = new SaveFileDialog
            {
                Filter = "License (*.lic)|*.lic",
                FilterIndex = 1,
                RestoreDirectory = true,
                FileName = "lic",
                InitialDirectory = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location)
            };
            var result = sfdlicense.ShowDialog();
            if (result.HasValue && result.Value)
            {
                var sw = new StreamWriter(sfdlicense.FileName, false, Encoding.UTF8);
                sw.Write(ulicense.ToString());
                sw.Close();
                sw.Dispose();
            }
        }

        private LicenseType GetLicenseType()
        {
            //return this.cblicense.Text == "Trial" ? LicenseType.Trial : LicenseType.Standard;
            return LicenseType.Standard;
        }
        
        private void Testlicense_Click(object sender, RoutedEventArgs e)
        {
            /*
               1. clear validation listbox.
               2. define a new stream and select and read the license.
               3. read the values from the license.
               4. validate the license (expiration date and signature).
            */
            ListBoxValidation.Items.Clear();

            var ofdlicense = new OpenFileDialog();
            ////Dim LicStreamReader As StreamReader
            Stream myStream = null;

            ////OfDLicense.InitialDirectory = "c:\"
            ofdlicense.Filter = "License (*.lic)|*.lic";
            ofdlicense.FilterIndex = 1;
            ofdlicense.InitialDirectory = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);

            var result = ofdlicense.ShowDialog();
            if (!result.HasValue || !result.Value)
            {
                return;
            }

            try
            {
                myStream = ofdlicense.OpenFile();
                if (myStream.Length <= 0)
                {
                    return;
                }

                ulicense = License.Load(myStream);
                string username = "User Name : " + ulicense.Customer.Name;
                string email = "Email: " + ulicense.Customer.Email;
                string quantity = "Quantity: " + ulicense.Quantity.ToString(CultureInfo.InvariantCulture);
                string HID = "HardwareID: " + ulicense.AdditionalAttributes.Get("HID");
                string licenseType = ulicense.Type.ToString();
                var str = ValidateLicense(ulicense);
                var lbi = new ListBoxItem { Content = str };
                ListBoxValidation.Items.Add(username);
                ListBoxValidation.Items.Add(email);
                ListBoxValidation.Items.Add(quantity);
                ListBoxValidation.Items.Add(HID);
                ListBoxValidation.Items.Add(licenseType);
                ListBoxValidation.Items.Add(lbi);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Cannot read file from disk. Original error: " + ex.Message);
            }
            finally
            {
                //// Check this again, since we need to make sure we didn't throw an exception on open.
                if (myStream != null)
                {
                    myStream.Close();
                }
            }
        }

        private string ValidateLicense(License license)
        {
            //// validate license and define return value.
            const string ReturnValue = "License is Valid";

            var validationFailures =
                license.Validate()
                    .ExpirationDate()
                    .When(LicenseException)
                    .And()
                    .Signature(publickey.Text)
                    .AssertValidLicense();

            var failures = validationFailures as IValidationFailure[] ?? validationFailures.ToArray();

            return !failures.Any() ? ReturnValue : failures.Aggregate(string.Empty, (current, validationFailure) => current + validationFailure.HowToResolve + ": " + "\r\n" + validationFailure.Message + "\r\n");
        }

        private static bool LicenseException(License license)
        {
            //// check licensetype.
            return license.Type == LicenseType.Trial;
        }

        private void Window_Closed(object sender, EventArgs e)
        {
            SaveSettings();
        }

        private void SaveSettings()
        {
            Properties.Settings.Default.pub = publickey.Text;
            Properties.Settings.Default.priv = privatekey.Text;
            Properties.Settings.Default.pass = pass.Text;
            Properties.Settings.Default.Save();
        }

        private void LoadSettings()
        {
            //publicKey = Properties.Settings.Default.pub;
            publickey.Text = Properties.Settings.Default.pub;
            //privateKey = Properties.Settings.Default.priv;
            privatekey.Text = Properties.Settings.Default.priv;
            pass.Text = Properties.Settings.Default.pass;
        }
        
    }
}
