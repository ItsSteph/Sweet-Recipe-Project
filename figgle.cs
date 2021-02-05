using System;
using static System.Console;
using System.Text;
using System.Collections.Generic;
using Figgle;

namespace ConsoleApp5
{
    class figgle
    {
        public static void Run()
        {
            WindowWidth = LargestWindowWidth;
            WindowHeight = LargestWindowHeight;

          
            string asciiSpooky = FiggleFonts.Whimsy.Render("Sweet or not");
            WriteLine(asciiSpooky);

            //string asciiHello = FiggleFonts.Standard.Render("Recipe");
            //WriteLine(asciiHello);

            //string asciiHello1 = FiggleFonts.Doh.Render("Guesser");
            //WriteLine(asciiHello1);
        }
    }
}