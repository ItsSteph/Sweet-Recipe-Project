using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;
using System;
using static System.Console;

namespace ConsoleApp5
{
    class Program

    {
        static void Main(string[] args)
        {
            figgle.Run();

            Console.WriteLine(">>> Type in 1 - check if the recipe found by PathToRecipe is sweet.");
            Console.WriteLine(">>> Type in 2 - update keyword list derived from the recipe collection.");

            int x = Convert.ToInt32(Console.ReadLine());
            if (x == 1) // CHECK THE RECIPE WITH EXISTING KEYWORDS

            {
                //// All text files are here @"C:\Users\...\Downloads\ReceptProjekt-Sweet\ConsoleApp5\ConsoleApp5\bin\Debug\netcoreapp3.1\"

                //// Recipe to test are @"C:\Users\...\Downloads\ReceptProjekt-Sweet\ConsoleApp5\ConsoleApp5\bin\Debug\netcoreapp3.1\Recipes\"
                //// Insert YOUR own pathway to Recipe instead for @"c:\dev\sandbox\testtext27.txt";
                
                string PathwayToRecipe = "TestaRecept.txt";

                // creates a DICTIONARY with words from the recipe
                Dictionary<string, Word> dictRecipeWords = RecipeReader.ReadTextFile(PathwayToRecipe);
               
                // Read file with meaningless words, that we won't add to the dictionary:
                string[] MeaninglessWords = File.ReadAllLines("stopwords.txt");


                // Opens files from testcount - two list with keywords that occur in Sweets recipes and Regular Food recipes
                // INSERT YOUR OWN pathway to files instead for these two
                string moSs = File.ReadAllText("MostOccSweetStop.txt");
                string moFs = File.ReadAllText("MostOccFoodStop.txt");

                string[] cpSweet = Regex.Split(moSs, @"\s+", RegexOptions.IgnorePatternWhitespace);
                string[] cpFood = Regex.Split(moFs, @"\s+", RegexOptions.IgnorePatternWhitespace);


                int SweetScore = 0;
                // we compare words from Recipe with words that are most frequent for Sweet recipes
                foreach (string w in cpSweet) // cpSweet is dict with MostOccSweetwords
                {
                    if (dictRecipeWords.ContainsKey(w))
                    {
                        Word CurrentWord1 = dictRecipeWords[w];
                        ////-----> Visa resultat i cmd Console.WriteLine("Key = {0}, Amount = {1}", CurrentWord1.Name, CurrentWord1.Amount);
                        SweetScore += CurrentWord1.Amount;
                    }

                }
                

                int FoodScore = 0;
                foreach (string w in cpFood)
                {
                    if (dictRecipeWords.ContainsKey(w))
                    {
                        Word CurrentWord2 = dictRecipeWords[w];
                        //-----> Visa resultat i cmd Console.WriteLine("Key = {0}, Amount = {1}", CurrentWord2.Name, CurrentWord2.Amount);
                        FoodScore += CurrentWord2.Amount;
                    }

                }

                Console.WriteLine("Sweet-Postive score is {0}.", SweetScore);
                Console.WriteLine("==== === ===");
                Console.WriteLine("Sweet-Negative score is {0},", FoodScore);
                Console.WriteLine("==== === ===");
                Console.WriteLine("Sweet-score is {0}.", SweetScore - FoodScore);

                if (SweetScore >= FoodScore)
                {
                    Console.WriteLine("Computer says it's a dessert!");
                }
                else
                {
                    Console.WriteLine("Computer says it's NOT a dessert!");
                }

            }

            if (x == 2)
            {// All textfiles can be found via pathway C:\...\source\repos\testcount\testcount\bin\Debug\netcoreapp3.1

                // Deletes the files so that each time the program launches new ones are created,
                // this way we avoid duplicates.
                File.Delete("MostOccSweet.txt");
                File.Delete("MostOccFood.txt");
                File.Delete("MostOccSweetStop.txt");
                File.Delete("MostOccFoodStop.txt");

                // Reads file with Sweet recipe collection and and creates dict with words and their frequency 
                Dictionary<string, double> freqsweet = DictFromCorpus("Sweet.txt");

                Console.WriteLine(" ===   DICT FROM {0}  === ", "Sweet.txt");
                // Prints out the dictionary and saves it into txt file
                PrintFrequencyTable(freqsweet, "MostOccSweet.txt");

                // SAME here - Reads file with Regular Food recipe collection and and creates dict 
                Dictionary<string, double> freqfood = DictFromCorpus("Food.txt");
                Console.WriteLine(" ===   DICT FROM {0}  === ", "Food.txt");
                PrintFrequencyTable(freqfood, "MostOccFood.txt");
                

                // Filters meaningless words (stopwords) from file with "Sweet" words
                string[] a = File.ReadAllLines("MostOccSweet.txt");
                string[] b = File.ReadAllLines("stopwords.txt");

                var sweet = a.Where(i => !b.Any(e => i.Contains(e)));

                Console.WriteLine("===   FOUND IN SWEET RECIPES   ===");
                Console.WriteLine(String.Join(Environment.NewLine, sweet));

                // Filters meaningless words (stopwords) from file with "Regular Food" words
                string[] c = File.ReadAllLines("MostOccFood.txt");
                string[] d = File.ReadAllLines("stopwords.txt");

                var food = c.Where(i => !d.Any(e => i.Contains(e)));

                Console.WriteLine("===   FOUND IN REGULAR RECIPES   ===");
                Console.WriteLine(String.Join(Environment.NewLine, food));

                using (System.IO.StreamWriter file =
                        new System.IO.StreamWriter("MostOccSweetStop.txt", true))                 //Use this file for screening for sweet words.
                {

                    file.WriteLine(String.Join(Environment.NewLine, sweet));

                }
                using (System.IO.StreamWriter file =
                                        new System.IO.StreamWriter("MostOccFoodStop.txt", true))    //Use this file for screening for sweet words.
                {

                    file.WriteLine(String.Join(Environment.NewLine, food));

                }

            }
            if (x != 1 && x != 2)
            {
                Console.WriteLine(">>> Wrong input. Run again.");
            }

            static Dictionary<string, double> DictFromCorpus(string TestArray)
            {
                // This method reads the file found by the PathToCorpus
                // returns dictionary dictFrequency of words and their frequency
                
                string CorpusText = File.ReadAllText(TestArray).ToLower();
                string[] CorpusWords = Regex.Split(CorpusText, @"\s+", RegexOptions.IgnorePatternWhitespace);

                var WordFromCorpus = Regex.Match(CorpusText, "\\w{4,20}");
                // Läser in ord med 4-20 st bokstäver mha Regular Exp. ==> Skapar en Dictionary med string, double med avseende på ordfrekvens
                Dictionary<string, double> dictFrequency = new Dictionary<string, double>();
                while (WordFromCorpus.Success)
                {
                    string word = WordFromCorpus.Value;
                    if (dictFrequency.ContainsKey(word))
                    {
                        dictFrequency[word]++;
                    }
                    else
                    {
                        dictFrequency.Add(word, 1);
                    }

                    WordFromCorpus = WordFromCorpus.NextMatch();
                }
                return dictFrequency;

            }

            static void PrintFrequencyTable(Dictionary<string,double> DictionaryToPrint, string SaveHere)
            {

                Console.WriteLine("Rank.  Ord Frekvens");
                Console.WriteLine(" ===   ===   ===");
                int rank = 1;
                foreach (var elem in DictionaryToPrint.OrderByDescending(a => a.Value).Take(200))
                {
                    Console.WriteLine(" {0,2}    {1,-4}             {2,5}", rank++, elem.Key, elem.Value);
                    {
                        //Skapar ny txt fil (samma filnamn som vi raderade i början)  med streamwriter och överför output:en 
                        using (System.IO.StreamWriter file =
                           new System.IO.StreamWriter(SaveHere, true))


                            file.WriteLine("{0} ", elem.Key, elem.Value);

                    }
                }
            }

        }

    }
}






