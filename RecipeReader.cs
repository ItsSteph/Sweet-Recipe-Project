using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace ConsoleApp5
{
    public class RecipeReader
    {


        public static Dictionary<string, Word> ReadTextFile(string pathway)
        {
            // Reads file and returns Dictionary with words without meaningless words
            string Recipe = File.ReadAllText(pathway);
            string[] RecipeArray = Regex.Split(Recipe, @"\s+", RegexOptions.IgnorePatternWhitespace);
            string[] MeaninglessWords = File.ReadAllLines("stopwords.txt");

            Dictionary<string, Word> dictRecipeWords = new Dictionary<string, Word>();
            foreach (string Word in RecipeArray)
            {
                if (Word.Length > 3 && !MeaninglessWords.Contains(Word)) // 
                {
                    if (!dictRecipeWords.ContainsKey(Word))
                    {
                        Word CurrentWord1 = new Word(Word, 1);
                        dictRecipeWords.Add(CurrentWord1.Name, CurrentWord1);
                    }
                    else
                    {
                        Word CurrentWord1 = dictRecipeWords[Word];
                        CurrentWord1.Amount++;
                    }
                }
            }

            return dictRecipeWords;
        }
    }
}
