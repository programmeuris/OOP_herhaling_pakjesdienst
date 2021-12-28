using System;
using System.Collections.Generic;
// using System.Linq;
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
using Herhaling_Pakjesdienst.Models;
using Herhaling_Pakjesdienst.DAL;

namespace Herhaling_Pakjesdienst.WPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        //=private=variables=======================================================================
        // PackageType is an enum, more info in PackageType.cs over in the Models project
        // the ? makes this a nullable type, meaning it can be whatever I said the enum can be (in PackageType.cs) OR null!
        // https://docs.microsoft.com/en-us/dotnet/csharp/language-reference/builtin-types/nullable-value-types
        private PackageType? _packageType;
        private Package _selectedPackage;
        private FileOperations _fileOperations;
        private List<Package> _packages;

        // constructor
        // this gets called when the program starts so I use it instead of window loaded event
        public MainWindow()
        {
            InitializeComponent();
            InitLogic();
        }

        //=private=methods=========================================================================
        // these don't get called by the MainWindow controls
        private void InitLogic()
        {
            // this method gets called in the constructor and handles stuff that
            // needs to be done when the program starts like disabling the update button
            // and loading in the data from the text file
            // could just as easily place all this code in the constructor, but this looks neater

            _packageType = null;
            _selectedPackage = null;
            _fileOperations = new FileOperations();
            _packages = _fileOperations.GetPackagesFromCsvFile();
            cboPackages.ItemsSource = _packages;
            btnUpdate.IsEnabled = false;
        }

        private void UpdateTypeLabel()
        {
            if (_packageType.HasValue) // test if _packageType is not null (because it's defined as a nullable type higher up)
            {
                // this is a neat shorter way to write an if statement
                // structure: [thing that will get changed] = [some condition] ? [value when condition is met] : [value when condition is not met];
                // more info: https://docs.microsoft.com/en-us/dotnet/csharp/language-reference/operators/conditional-operator
                lblSoortZending.Content = _packageType.Value == PackageType.OvernightPackage ?
                    "Toeslag per Kg" : "Vaste Kost";
            }
        }

        private bool ValidateFields(TextBox[] toValidate)
        {
            // this validation method is probably too complicated. I approached it as an exercise to reuse the names of the textboxes
            // as much as possible and to learn to work with range operators
            // you could probably get away with just checking every field and creating an error message for each

            // if packagetype is not selected, return invalid
            if (!_packageType.HasValue)
            {
                MessageBox.Show("Pakkettype moet geselecteerd worden.");
                return false;
            }

            // bool starts on true, from the moment a single field is not valid, gets set to false;
            bool areAllFieldsValid = true;

            //toValidate[0] = txtVerzenderNaam;
            //toValidate[1] = txtVerzenderAdres;
            //toValidate[2] = txtBestemmingNaam;
            //toValidate[3] = txtBestemmingAdres;
            //toValidate[4] = txtGewichtInKg;
            //toValidate[5] = txtPrijsPerKg;
            //toValidate[6] = txtVasteKostOfToeslag;

            //=validate=that=the=text=fields=are=not=empty=========================================
            // build a validation message based on which fields are empty
            var fullErrorMessage = new StringBuilder();
            var invalidFields = new StringBuilder();

            for (int i = 0; i < 4; i++)
            {
                if (string.IsNullOrWhiteSpace(toValidate[i].Text))
                {
                    areAllFieldsValid = false;
                    // the [3..] is a range operator, like a substring from the 3rd character on, to cut 'txt' from the textbox name
                    // more info: https://docs.microsoft.com/en-us/dotnet/csharp/whats-new/tutorials/ranges-indexes
                    invalidFields.Append($"'{toValidate[i].Name[3..]}', ");
                }
            }

            // the range [0..^2] cuts the last 2 characters from a string, so the last comma and space gets removed
            // similar to text = text.substring(0, text.length - 1); or something along those lines
            // if any text fields are invalid, include them in the error message, otherwise, the error message is empty
            if (!string.IsNullOrWhiteSpace(invalidFields.ToString()))
            {
                fullErrorMessage.AppendLine($"Veld(en) {invalidFields.ToString()[0..^2]} moeten ingevuld worden.\n");
            }

            //=validate=the=numeric=fields=========================================================
            // first we check if they have numerical values
            // then, we check if the numerical values are larger than 0 later on
            // reset the invalidFields stringbuilder so we can use it again
            invalidFields.Clear();

            // save the indexes of the numerical fields that are filled in so we can loop over them later to test if they're larger than 0
            // since we only need to do this check for fields that are filled in
            List<int> filledInNumericalFields = new List<int>();

            for (int i = 4; i < toValidate.Length; i++)
            {
                if (!double.TryParse(toValidate[i].Text, out double test))
                {
                    areAllFieldsValid = false;
                    invalidFields.Append($"'{toValidate[i].Name[3..]}', ");
                }
                else
                {
                    filledInNumericalFields.Add(i);
                }
            }

            if (!string.IsNullOrWhiteSpace(invalidFields.ToString()))
            {
                fullErrorMessage.AppendLine($"Veld(en) {invalidFields.ToString()[0..^2]} moeten een numerieke waarde bevatten.\n");
            }

            // if any numerical fields are non numerical or empty, add them to the error message
            invalidFields.Clear();
            

            // check if the filled in fields are all larger than 0;
            for (int i = 0; i < filledInNumericalFields.Count; i++)
            {
                double value = double.Parse(toValidate[filledInNumericalFields[i]].Text);
                if (value < 0)
                {
                    areAllFieldsValid = false;
                    invalidFields.Append($"'{toValidate[i].Name[3..]}', ");
                }
            }

            if (!string.IsNullOrWhiteSpace(invalidFields.ToString()))
            {
                fullErrorMessage.AppendLine($"Veld(en) {invalidFields.ToString()[0..^2]} mogen niet negatief zijn.");
            }

            if (!areAllFieldsValid)
            {
                MessageBox.Show(fullErrorMessage.ToString());
            }

            //TODO: log errors to log file. have to reqrite entire logger class and move it to models I guess

            return areAllFieldsValid;
        }

        private TextBox[] GetFields()
        {
            var fields = new TextBox[7];
            fields[0] = txtVerzenderNaam;
            fields[1] = txtVerzenderAdres;
            fields[2] = txtBestemmingNaam;
            fields[3] = txtBestemmingAdres;
            fields[4] = txtGewichtInKg;
            fields[5] = txtPrijsPerKg;
            fields[6] = txtVasteKostOfToeslag;

            return fields;
        }

        private void ClearFields(TextBox[] fields)
        {
            foreach (var field in fields)
            {
                field.Text = String.Empty;
            }
        }

        //=event=handlers==========================================================================
        // these get called when the user interacts with certain controls the MainWindow
        private void SoortVerzending_Click(object sender, RoutedEventArgs e)
        {
            // this event gets fired when the radiobuttons that decide the type of the package get checked
            // both radiobuttons trigger the same event.
            // Because they're in the same groupbox, only one can be selected at any given time

            // the button that fired the event is passed along as the sender object
            // to use it, we need to cast it back to its original class
            RadioButton btn = sender as RadioButton;

            // decide what to do based on the name of the button that fired the event
            switch (btn.Name)
            {
                // sets _packageType to whatever package type was selected
                // this way other methods can at all times check which type of package is selected
                // without having to go and check which radiobutton is selected every time, who has time for that, right?
                // the names are set in the MainWindow.xaml file

                case "rdbTwoday":
                    _packageType = PackageType.TwodayPackage;
                    break;
                case "rdbOvernight":
                    _packageType = PackageType.OvernightPackage;
                    break;
            }

            UpdateTypeLabel();
        }

        private void btnBereken_Click(object sender, RoutedEventArgs e)
        {
            var fields = GetFields();

            if (ValidateFields(fields))
            {
                Package package = null;
                switch (_packageType)
                {
                    case PackageType.OvernightPackage:
                        package = new OvernightPackage(fields[0].Text,
                                                       fields[1].Text,
                                                       fields[2].Text,
                                                       fields[3].Text,
                                                       double.Parse(fields[4].Text),
                                                       double.Parse(fields[5].Text),
                                                       double.Parse(fields[6].Text));
                        break;
                    case PackageType.TwodayPackage:
                        package = new TwodayPackage(txtVerzenderNaam.Text,
                                                    txtVerzenderAdres.Text,
                                                    txtBestemmingNaam.Text,
                                                    txtBestemmingAdres.Text,
                                                    double.Parse(txtGewichtInKg.Text),
                                                    double.Parse(txtPrijsPerKg.Text),
                                                    double.Parse(txtVasteKostOfToeslag.Text));
                        break;
                }

                if (package != null)
                {
                    _packages.Add(package);

                    // reset the itemssource because it got updated;
                    cboPackages.ItemsSource = null;
                    cboPackages.ItemsSource = _packages;

                    // clear all text after the package is added
                    ClearFields(fields);
                    lblTeBetalen.Content = $"Te betalen: {package.VerzendingsKosten()}";

                    
                }
            }
        }

        private void cboPackages_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            //var package = cboPackages
        }
    }
}
