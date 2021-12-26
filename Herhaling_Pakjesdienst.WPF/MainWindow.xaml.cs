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
            _fileOperations = new FileOperations();
            _packages = _fileOperations.GetPackagesFromCsvFile();
            cboPackages.ItemsSource = _packages;
            btnUpdate.IsEnabled = false;
        }

        private void UpdateLabel()
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

            UpdateLabel();
        }
    }
}
