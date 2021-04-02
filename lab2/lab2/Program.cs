using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;

namespace lab2 {
    class Program {
        private static string _addressOut;
        private static string _addressCsv;
        private static string _fileExtension;
        
        static void Main(string[] args) {

            if (args.Length != 3) 
                throw new ArgumentException("Pass correct number of arguments");

            _addressOut = args[0];
            _addressCsv = args[1];
            _fileExtension = args[2];

            if (!Directory.Exists(_addressOut))  
                throw new ArgumentException("Podana ścieżka jest niepoprawna");

            if (!File.Exists(_addressCsv)) { 
                AddNewErrorToLog("Nie znaleziono pliku CSV");
                throw new FileNotFoundException("Plik CSV nie istnieje");
            }

            if (!_fileExtension.Equals("json")) {
                AddNewErrorToLog("Podano rozszerzenie inne niż JSON");
                throw new ArgumentException("Json is the only available argument here");
            }
            
            FileInfo fileInfo = new FileInfo(_addressCsv);
            StreamReader streamReader = new StreamReader(fileInfo.OpenRead());
            string line;
            HashSet<Student> hashSet = new(new Student());
            while ((line = streamReader.ReadLine()) != null) {
                string[] lineAtomValues = line.Split(",");

                if (lineAtomValues.Length == 9) {
                    if (lineAtomValues.Any(atomValue => atomValue.Equals("")))
                        AddNewErrorToLog("Rekord zawiera pustą wartość - nie dodano Studenta do wpliku wyjściowego ."+ _fileExtension);
                    else {
                        var student = new Student {
                            FirstName = lineAtomValues[0],
                            LastName = lineAtomValues[1],
                            FieldOfStudy = lineAtomValues[2],
                            ModeOfStudy = lineAtomValues[3],
                            IndexNumber = lineAtomValues[4],
                            BirthDay = lineAtomValues[5],
                            Email = lineAtomValues[6],
                            MotherName = lineAtomValues[7],
                            FatherName = lineAtomValues[8],
                        };

                        hashSet.Add(student);
                    }
                }
                else 
                    AddNewErrorToLog("Zła liczba kolumn");
            }
            
            StreamWriter streamWriter = new(_addressOut+@"\out.txt");
            foreach (var student in hashSet) {
                streamWriter.WriteLine(JsonSerializer.Serialize(student));
                streamWriter.Flush();
            }
            
            streamReader.Dispose();
            streamWriter.Dispose();
        }

        private static void AddNewErrorToLog(string errorDescription) {
             using StreamWriter streamWriter = new (_addressOut+@"\log.txt",true); 
             streamWriter.WriteLine(errorDescription);
        }
    }
}