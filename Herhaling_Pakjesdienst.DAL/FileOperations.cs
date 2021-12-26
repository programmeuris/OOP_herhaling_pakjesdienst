using System;
using System.Collections.Generic;
using System.IO;
using Herhaling_Pakjesdienst.Models;

namespace Herhaling_Pakjesdienst.DAL
{
    public class FileOperations
    {
        // this class needs to:
        //      read data from a txt file
        //      not seppuku when it encounters any errors
        //      log these errors in an error file
        //      only send out valid records
        //      (linking the data to the combobox is done in the WPF project)

        private const string _pakjesFileName = "pakjesdienst.txt";
        
        private const char separator = ';';

        public List<Package> GetPackagesFromCsvFile(string fName = _pakjesFileName)
        {
            // If file exists, proces data into useable types
            //                 log corrupt data if any
            // if not,         return null, log error message

            string[] lines;

            try
            {
                // File is a class that does most things we learned with the streamreader and all
                // usually much shorter
                // more info: https://docs.microsoft.com/en-us/dotnet/api/system.io.file?view=netcore-3.1
                lines = File.ReadAllLines(fName);
            }
            catch (Exception ex)
            {
                Logger.LogReadError(ex.Message);
                // once an error occurs, we don't need to execute the rest of this method
                return null;
            }

            List<Package> packages = new List<Package>();

            // using a for instead of foreach because I'm using the counter to log which line throws an exception, if any
            for (int i = 0; i < lines.Length; i++)
            {
                // line structure:
                // PackageType;Naamverzender;AdresVerzender;NaamOntvanger;AdresOntvanger;Gewicht;Prijs;ToeslagOfVasteKost
                // split each line of text in separate parts
                var split = lines[i].Split(separator);

                // initializing at -1 so if by chance a value does not get replaced from the textline,
                // the package will throw an exception
                double weight = -1;
                double priceKg = -1;
                double toeslag = -1;

                double.TryParse(split[5], out weight);
                double.TryParse(split[6], out priceKg);
                double.TryParse(split[7], out toeslag);

                try
                {
                    Package package;
                    switch (split[0])
                    {
                        // these can also throw ValueBelowZeroException when they receive negative values
                        case "TwoDay":
                            package = new TwodayPackage(split[1],
                                                        split[2],
                                                        split[3],
                                                        split[4],
                                                        weight,
                                                        priceKg,
                                                        toeslag);
                            break;
                        case "Overnight":
                            package = new OvernightPackage(split[1],
                                                           split[2],
                                                           split[3],
                                                           split[4],
                                                           weight,
                                                           priceKg,
                                                           toeslag);
                            break;
                        default:
                            // this exception builds a message that includes the invalid package type as well as the line it occurred on
                            // line = i + 1 because i starts from 0 and humans usually don't
                            throw new PackageTypeInvalidException(split[0]);
                    }

                    // if we get here, it means that there was no exception, so the package gets added to the list
                    packages.Add(package);

                }
                catch (Exception ex)
                {
                    // my logger class logs the exception type so I find it unnecessary to use different catches
                    // for different types of exceptions
                    Logger.LogDataError(ex.Message, i + 1);
                }
            }

            return packages;
        }
    }
}
