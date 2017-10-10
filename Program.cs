﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace AnagramSolver {
    internal class Program {
        
        public static void Main(string[] args) {
            var anagram = GetAnagram();
            // After we have parsed the anagram, we must find the words matching the solver rules.
            var solutions = Solve(anagram, "db.csv");
            if (solutions != null) {
                Console.WriteLine("[!] No solution was found.");
            }
            
            Console.WriteLine("[OK] Solution is '" + solutions.First() + "'");
           
        }

        private static string Solve(string anagram, string filePath) {
            var letters = anagram.ToLower().ToCharArray();
            string result = null;
            var resultWeight = 0;
            try {
                using (var reader = new StreamReader(filePath)) {
                    string line;
                    
                    while ((line = reader.ReadLine()) != null) {
                        //Console.WriteLine("[-] Line Parsed: " + line);
                        var row = line.Split(',');
                        var value = row[0].ToLower();
                        var len = Convert.ToInt32(row[1]);
                        // atc = cat
                        // atcs = cat's
                        if(len > anagram.Length) continue;
                        var weight = value.IndexOfAny(letters);
                        if (weight <= 0 || weight <= resultWeight) continue;
                        result = value;
                        resultWeight = weight;
                        Console.WriteLine("[+] Result changed with " + result + " weight " + resultWeight);

                    }
                }
            }
            catch (Exception e) {
                Console.WriteLine(e);
                throw;
            }
            
           
            
            return result;
        }
        
        private static string GetAnagram(int maxCharacters = 3, int minCharacters = 1){
            // Here we define the pattern for the anagram we want to solve. 
            // The pattern consists in only alphabetics characters a-z (Uppers or Lowercase)
            // And from the characters length range defined in the parameters.
            var pattern = "^[a-zA-Z]{"+ minCharacters+","+ maxCharacters +"}$";
            //We prepare the variable where to store the anagram
            string anagram;
            // This will store if the anagram is valid or not
            bool isValid;
            // We repeat the user input until he give us a valid anagram matching the pattern defined above
            do {
                Console.Write("Word: ");
                anagram = Console.ReadLine();
                // We check and assing the pattern match of the given anagram, if it's true we continue, yey we have a geniuns
                if (isValid = Regex.IsMatch(anagram, pattern)) continue;
                // Otherwise, for the monkeys, we will output some messages to provide the necessary information about our pattern requierements.
                Console.WriteLine("[!] Only A-Za-z characters are accepted.");
                Console.WriteLine("[!] The maximum length of characters is " + maxCharacters + " and the minimum is " + minCharacters);
            } while(!isValid);
            Console.WriteLine("[OK] Anagram is matching the pattern!");
            // In the end we return the valid anagram
            return anagram;
        }
    }
}